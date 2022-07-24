using Confluent.Kafka;
using Example.EventBus.Abstractions;
using Example.EventBus.Kafka.Extensions;
using Example.EventBus.Kafka.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Example.EventBus.Kafka;

public class KafkaEventBus : IEventBus, IExternalEventBus
{
    private readonly ProducerConfig _producerConfig;
    private readonly ConsumerConfig _consumerConfig;

    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<KafkaEventBus> _logger;
    private readonly EventBusOptions _options;

    public KafkaEventBus(IServiceProvider serviceProvider, ILogger<KafkaEventBus> logger, EventBusOptions options)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _options = options;
        _logger.LogInformation("Url: {KafkaEventBusUrl}", _options.Url);

        _producerConfig = new ProducerConfig
        {
            BootstrapServers = _options.Url
        };

        if (!string.IsNullOrEmpty(_options.UserName))
        {
            _producerConfig.SaslMechanism = SaslMechanism.ScramSha512;
            _producerConfig.SecurityProtocol = SecurityProtocol.SaslPlaintext;
            _producerConfig.SaslUsername = _options.UserName;
            _producerConfig.SaslPassword = _options.Password;
        }

        _consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _options.Url,
            AllowAutoCreateTopics = _options.AllowAutoCreateTopic,
            AutoOffsetReset = AutoOffsetReset.Latest,
            EnableAutoCommit = false,
            MaxPollIntervalMs = 120 * 60 * 1000,
            GroupId = $"{_options.GroupId}-consumer-group",
        };

        if (!string.IsNullOrEmpty(_options.UserName))
        {
            _consumerConfig.SaslMechanism = SaslMechanism.ScramSha512;
            _consumerConfig.SecurityProtocol = SecurityProtocol.SaslPlaintext;
            _consumerConfig.SaslUsername = _options.UserName;
            _consumerConfig.SaslPassword = _options.Password;
        }
    }

    public void Publish(IntegrationEvent @event)
    {
        var topicName = GetTopicName(@event.GetType());
        var message = JsonConvert.SerializeObject(@event);
        using (var producer = new ProducerBuilder<string, string>(_producerConfig).Build())
        {
            try
            {
                _logger.LogDebug(
                    "Send message [{IntegrationEventCorrelationId}] to {TopicName}. Message: {EventMessage}",
                    @event.CorrelationId, topicName, message.SanitizingLog());
                producer.Produce(topicName,
                    new Message<string, string> { Value = message, Key = @event.CorrelationId.ToString() },
                    DeliveryReportHandler);
                producer.Flush(TimeSpan.FromSeconds(10));
            }
            catch (ProduceException<string, IntegrationEvent> e)
            {
                _logger.LogError(e, "Delivery failed: {Reason}", e.Error.Reason);
            }
        }

        void DeliveryReportHandler(DeliveryReport<string, string> report)
        {
            if (report.Error.IsError)
            {
                _logger.LogError("Delivery Error: {Reason} on topic {IntegrationEventId}", @event.CorrelationId);
            }
            else
            {
                _logger.LogDebug("Delivered {IntegrationEventId} to {EventKey}", @event.CorrelationId,
                    report.TopicPartitionOffset);
            }
        }
    }

    private string GetTopicName(Type eventType)
    {
        return (_options.TopicPrefix != null
                   ? _options.TopicPrefix.ToLower() + "."
                   : string.Empty)
               + GetEventName(eventType).ToKebabCase() + "-event";
    }

    private string GetEventName(Type eventType)
    {
        return eventType.Name.Replace("IntegrationEvent", "");
    }

    public void Subscribe<T>(string topicName = null) where T : IntegrationEvent
    {
        Subscribe<T, IIntegrationEventHandler<T>>(topicName);
    }

    public void Subscribe<T, TH>(string topicName)
        where T : IntegrationEvent
        where TH : class, IIntegrationEventHandler<T>
    {
        topicName ??= GetTopicName(typeof(T));
        var eventName = GetEventName(typeof(T));
        _logger.LogInformation("Subscribing event {EventName} with {EventHandler}",
            eventName, typeof(TH).GetGenericTypeName());
        Task.Run(async () =>
        {
            using (var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build())
            {
                consumer.Subscribe(topicName);
                _logger.LogInformation("{kafkaTopicName} subscribed with consumer {kafkaConsumerGroupId}",
                    topicName, _consumerConfig.GroupId);

                try
                {
                    while (true)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume();
                            try
                            {
                                _logger.LogDebug("Handle incoming message to '{TopicName}. Message: {EventMessage}'",
                                    topicName, consumeResult.Message.Value.SanitizingLog());
                                await ProcessEventAsync<T, TH>(consumeResult.Message.Value);
                                consumer.Commit();
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex,
                                    "----- ERROR Processing message \"{EventMessage}\" on event {EventName}",
                                    consumeResult.Message.Value.SanitizingLog(), eventName);
                            }
                        }
                        catch (ConsumeException ex)
                        {
                            if (ex.Message != "Broker: Unknown topic or partition")
                            {
                                throw;
                            }

                            _logger.LogWarning(ex, "Error on consume {TopicName}", topicName);
                        }
                    }
                }
                catch (OperationCanceledException ex)
                {
                    _logger.LogInformation(ex, "Event {EventName} operation cancelled", eventName);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Event {EventName} unhandled exception", eventName);
                }
                finally
                {
                    consumer.Close();
                }
            }
        });
    }

    private async Task ProcessEventAsync<T, TH>(string message)
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var handler = scope.ServiceProvider.GetService<TH>();

            if (handler == null)
                throw new Exception($"Not found handler {typeof(TH).Name}");

            var integrationEvent = JsonConvert.DeserializeObject<T>(message);
            await handler.Handle(integrationEvent);
        }
    }
}
namespace Example.EventBus.Kafka.Options;

public class EventBusOptions
{
    public string GroupId { get; set; }
    public string Url { get; set; }
    public string TopicPrefix { get; set; }
    public bool AllowAutoCreateTopic { get; set; }

    public string UserName { get; set; }
    public string Password { get; set; }

    public const string InternalEventBusOptions = "EventBusOptions";
    public const string ExternalEventBusOptions = nameof(ExternalEventBusOptions);
}
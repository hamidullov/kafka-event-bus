namespace Example.EventBus.Abstractions;
public interface IEventBus
{
    void Publish(IntegrationEvent @event);

    void Subscribe<T>(string topicName = null)
        where T : IntegrationEvent;
}

public interface IExternalEventBus : IEventBus {};
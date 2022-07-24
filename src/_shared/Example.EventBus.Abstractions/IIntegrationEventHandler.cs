namespace Example.EventBus.Abstractions;

public interface IIntegrationEventHandler<in TIntegrationEvent>
{
    Task Handle(TIntegrationEvent @event);
}
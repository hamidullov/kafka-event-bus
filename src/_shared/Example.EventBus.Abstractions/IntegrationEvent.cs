namespace Example.EventBus.Abstractions;

public abstract class IntegrationEvent
{
    protected IntegrationEvent() : this(Guid.NewGuid().ToString(), DateTime.UtcNow)
    {
    }

    protected IntegrationEvent(string correlationId) : this(correlationId, DateTime.UtcNow)
    {
    }

    protected IntegrationEvent(string correlationId, DateTime createDate)
    {
        CorrelationId = correlationId;
        CreationDate = createDate;
    }


    public string CorrelationId { get; private set; }


    public DateTime CreationDate { get; private set; }
}
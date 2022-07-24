using MediatR;

namespace Example.DDD;

public class TAuditableEntity<T>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public T Id { get; set; }
    
    // /// <summary>
    // /// Кем создано
    // /// </summary>
    // public string CreatedBy { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime Created { get; set; }
    
    // /// <summary>
    // /// Кем изменено
    // /// </summary>
    // public string LastModifiedBy { get; set; }
    
    /// <summary>
    /// Дата изменения
    /// </summary>
    public DateTime? LastModified { get; set; }
    
    private List<INotification> _domainEvents;
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents = _domainEvents ?? new List<INotification>();
        _domainEvents.Add(eventItem);
    }
    
    public void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}

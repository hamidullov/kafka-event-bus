using Example.Common.Enums;
using Example.Common.Exceptions;
using Example.DDD;

namespace EmiasFlgGateway.Domain;

public class Study : TAuditableEntity<Guid>
{
    protected Study()
    {
    }

    public Study(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }
    
    public bool IsCompleted { get; private set; }

    public string? ErrorMessage { get; private set; }


    public void Complete(string? errorMessage = null)
    {
        ErrorMessage = errorMessage;
        IsCompleted = true;
    }
    
}
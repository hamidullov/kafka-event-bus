using Example.DDD;
using Example.EfCore.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Example.EfCore;

public abstract class BaseAuditableDbContext : DbContext
{
    private readonly IMediator? _mediator;

    protected BaseAuditableDbContext(DbContextOptions options) : base(options)
    {
    }

    protected BaseAuditableDbContext(DbContextOptions options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        if (_mediator != null)
        {
            await _mediator.DispatchDomainEventsAsync<string>(this);
            await _mediator.DispatchDomainEventsAsync<int>(this);
            await _mediator.DispatchDomainEventsAsync<long>(this);
            await _mediator.DispatchDomainEventsAsync<Guid>(this);
        }

        SetEntityAuditFields<string>();
        SetEntityAuditFields<int>();
        SetEntityAuditFields<long>();
        SetEntityAuditFields<Guid>();

        // try
        // {
        return await base.SaveChangesAsync(cancellationToken);
        // }
        // catch (DbUpdateException e)
        // {
        //     switch (e.InnerException)
        //     {
        //         case PostgresException postgresException when postgresException.Routine == "_bt_check_unique":
        //             throw new EntityAlreadyExistsException(postgresException.Detail);
        //         default:
        //             throw;
        //     }
        // }
    }

    private void SetEntityAuditFields<T>()
    {
        foreach (var entry in ChangeTracker.Entries<TAuditableEntity<T>>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    // entry.Entity.CreatedBy = _currentUserService.UserId;
                    if (entry.Entity.Created == default)
                    {
                        entry.Entity.Created = DateTime.UtcNow;
                    }

                    break;
                case EntityState.Modified:
                    // entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModified = DateTime.UtcNow;
                    break;
            }
        }
    }
}
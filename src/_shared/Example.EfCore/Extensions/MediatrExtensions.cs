using Example.DDD;
using MediatR;

namespace Example.EfCore.Extensions;

public static class MediatrExtensions
{
    public static async Task DispatchDomainEventsAsync<T>(this IMediator mediator, BaseAuditableDbContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<TAuditableEntity<T>>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
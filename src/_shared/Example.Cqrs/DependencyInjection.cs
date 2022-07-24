using System.Reflection;
using Example.EfCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Cqrs;

public static class DependencyInjection
{
    public static IServiceCollection AddAppCqrs<T>(this IServiceCollection services, Assembly commandQueryPlaces = null) where T : BaseAuditableDbContext
    {
        if (commandQueryPlaces == null)
        {
            commandQueryPlaces = Assembly.GetEntryAssembly();
        }
        services.AddMediatR(commandQueryPlaces);
        services.AddScoped<BaseAuditableDbContext, T>();
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(EfCommandTransactionBehaviour<,>));
        return services;
    }
}
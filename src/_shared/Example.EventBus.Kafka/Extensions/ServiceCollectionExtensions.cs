using System.Reflection;
using Example.EventBus.Abstractions;
using Example.EventBus.Kafka.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Example.EventBus.Kafka.Extensions;

public static class ServiceCollectionExtensions
{
    private static bool IsRegistred = false;
    public static IServiceCollection AddInternalKafkaEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        if (!IsRegistred)
        {
            IsRegistred = true;
            services.AddAssemblyEventHandlers(AppDomain.CurrentDomain.GetAssemblies());
        }
        services.AddSingleton<IEventBus>((serviceProvider) =>
        {
            var logger = serviceProvider.GetService<ILogger<KafkaEventBus>>();
            var options = new EventBusOptions();
            configuration.GetSection(EventBusOptions.InternalEventBusOptions).Bind(options);
            return new KafkaEventBus(serviceProvider, logger, options);
        });
      
        return services;
    }

    public static IServiceCollection AddExternalKafkaEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        if (!IsRegistred)
        {
            IsRegistred = true;
            services.AddAssemblyEventHandlers(AppDomain.CurrentDomain.GetAssemblies());
        }
        services.AddSingleton<IExternalEventBus>((serviceProvider) =>
        {
            var logger = serviceProvider.GetService<ILogger<KafkaEventBus>>();
            var options = new EventBusOptions();
            configuration.GetSection(EventBusOptions.ExternalEventBusOptions).Bind(options);
            return new KafkaEventBus(serviceProvider, logger, options);
        });

        return services;
    }

    public static IServiceCollection AddAssemblyEventHandlers(this IServiceCollection services, Assembly[] assemblies)
    {
        var eventHandlerTypes = from x in assemblies.SelectMany(r => r.GetTypes())
            let interfaces = x.GetInterfaces().Where(r => r.IsGenericType)
            where !x.IsAbstract && !x.IsInterface &&
                  interfaces.Any(i => typeof(IIntegrationEventHandler<>).IsAssignableFrom(i.GetGenericTypeDefinition()))
            select x;


        foreach (var handlerType in eventHandlerTypes)
        {
            var concreteType = typeof(IIntegrationEventHandler<>)
                .MakeGenericType(handlerType.GetInterfaces()
                    .First(i => typeof(IIntegrationEventHandler<>).IsAssignableFrom(i.GetGenericTypeDefinition()))
                    .GenericTypeArguments.First());
            services.AddScoped(concreteType, handlerType);
        }

        return services;
    }
}
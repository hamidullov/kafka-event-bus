using Microsoft.Extensions.DependencyInjection;

namespace Example.Mapper;

public static class DependencyInjection
{
    public static IServiceCollection AddAppAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile), typeof(MappingProfile));
        return services;
    }

  
}
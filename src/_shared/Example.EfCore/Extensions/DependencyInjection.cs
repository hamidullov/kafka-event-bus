using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Example.EfCore.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddAppDbContext<T>(this IServiceCollection services, string connectionString)
        where T : DbContext
    {
        services.AddDbContext<T>(options =>
        {
            options.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        });

        return services;
    }

    public static void MigrateAppDbContext<T>(this IServiceProvider serviceProvider) where T : DbContext
    {
        using var scope = serviceProvider.CreateScope();
        try
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<T>();
            dataContext.Database.Migrate();
        }
        catch (Exception ex)
        {
           Console.WriteLine("Error on initializing/migrating database. {0}", ex);
           throw;
        }
    }
}
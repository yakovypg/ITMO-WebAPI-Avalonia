using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace HarryPotter.Server.DataAccess.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        Action<PostgresConnectionConfiguration> configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        _ = services.AddPlatformPostgres(t => t.Configure(configuration))
            .AddPlatformMigrations(typeof(ServicesExtensions).Assembly)
            .AddScoped<ILikedEntitiesHandler, LikedEntitiesHandler>()
            .AddScoped<ILikedEntitiesRepository, LikedEntitiesRepository>();

        return services;
    }

    public static async Task UseDataAccessAsync(this IServiceScope scope)
    {
        ArgumentNullException.ThrowIfNull(scope, nameof(scope));
        await scope.UsePlatformMigrationsAsync(default);
    }
}

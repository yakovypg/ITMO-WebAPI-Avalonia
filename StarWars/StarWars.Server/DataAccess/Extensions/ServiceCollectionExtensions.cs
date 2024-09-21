using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Microsoft.Extensions.DependencyInjection;

namespace StarWars.Server.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccess(
            this IServiceCollection services,
            Action<PostgresConnectionConfiguration> postgresConfig)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(postgresConfig, nameof(postgresConfig));

            _ = services.AddPlatformPostgres(t => t.Configure(postgresConfig));
            _ = services.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);
            _ = services.AddScoped<IEntityRepository, FavoriteEntityRepository>();

            return services;
        }
    }
}

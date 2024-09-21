using IPManager.Server.DataAccess;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IpManager.Server.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> config)
    {
        ArgumentNullException.ThrowIfNull(collection, nameof(collection));
        ArgumentNullException.ThrowIfNull(config, nameof(config));

        _ = collection.AddPlatformPostgres(t => t.Configure(config));
        _ = collection.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);
        _ = collection.AddScoped<IIpRepository, FavoriteIpRepository>();
        _ = collection.AddScoped<IOrganizationRepository, OrganizationRepository>();

        return collection;
    }
}

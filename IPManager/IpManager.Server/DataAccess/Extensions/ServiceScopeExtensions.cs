using Itmo.Dev.Platform.Postgres.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace IpManager.Server.DataAccess.Extensions;

public static class ServiceScopeExtensions
{
    public static async Task UseDataAccessAsync(this IServiceScope serviceScope)
    {
        ArgumentNullException.ThrowIfNull(serviceScope, nameof(serviceScope));
        await serviceScope.UsePlatformMigrationsAsync(default);
    }
}

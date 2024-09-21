using IpManager.Server.DataAccess.Extensions;
using IPManager.Server.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace IpManager.Server.Services;

public static class DatabaseConnectionService
{
    public static IIpRepository FavoriteIpRepository { get; }
    public static IOrganizationRepository OrganizationRepository { get; }

    static DatabaseConnectionService()
    {
        IServiceCollection services = new ServiceCollection();

        _ = services.AddDataAccess(t =>
        {
            t.Port = 5432;
            t.Host = "127.0.0.1";
            t.Username = "postgres";
            t.Password = "postgres";
            t.Database = "ipinfo";
            t.SslMode = "Prefer";
        });

        ServiceProvider provider = services.BuildServiceProvider();

        using IServiceScope scope = provider.CreateScope();
        scope.UseDataAccessAsync().Wait();

        FavoriteIpRepository = scope.ServiceProvider.GetRequiredService<IIpRepository>();
        OrganizationRepository = scope.ServiceProvider.GetRequiredService<IOrganizationRepository>();
    }
}

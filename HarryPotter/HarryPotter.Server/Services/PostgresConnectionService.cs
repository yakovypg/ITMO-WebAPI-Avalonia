using HarryPotter.Server.DataAccess;
using HarryPotter.Server.DataAccess.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace HarryPotter.Server.Services;

public static class PostgresConnectionService
{
    static PostgresConnectionService()
    {
        var serviceCollection = new ServiceCollection().AddDataAccess(t =>
        {
            t.Username = "postgres";
            t.Password = "postgres";
            t.Database = "harrypotter";
            t.SslMode = "Prefer";

            t.Port = 5432;
            t.Host = "localhost";
        });

        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        using IServiceScope scope = serviceProvider.CreateScope();
        scope.UseDataAccessAsync().Wait();

        LikedEntitiesHandler = scope.ServiceProvider
            .GetRequiredService<ILikedEntitiesHandler>();

        LikedEntitiesRepository = scope.ServiceProvider
            .GetRequiredService<ILikedEntitiesRepository>();
    }

    public static ILikedEntitiesHandler LikedEntitiesHandler { get; }
    public static ILikedEntitiesRepository LikedEntitiesRepository { get; }
}

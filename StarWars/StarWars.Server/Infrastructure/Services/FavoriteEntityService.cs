using Microsoft.Extensions.DependencyInjection;
using StarWars.Server.DataAccess;
using StarWars.Server.DataAccess.Extensions;

namespace StarWars.Server.Infrastructure.Services
{
    public static class FavoriteEntityService
    {
        private static IEntityRepository? _favoriteEntityRepository;

        public static async Task<IEntityRepository> GetFavoriteEntityRepository()
        {
            _favoriteEntityRepository ??= await CreateFavoriteEntityRepository();
            return _favoriteEntityRepository;
        }

        private static async Task<IEntityRepository> CreateFavoriteEntityRepository()
        {
            IServiceCollection services = new ServiceCollection();

            _ = services.AddDataAccess(t =>
            {
                t.Host = "127.0.0.1";
                t.Port = 5432;
                t.Username = "postgres";
                t.Password = "postgres";
                t.Database = "starwars";
                t.SslMode = "Prefer";
            });

            ServiceProvider provider = services.BuildServiceProvider();

            using IServiceScope scope = provider.CreateScope();
            await scope.UseDataAccessAsync();

            return scope.ServiceProvider.GetRequiredService<IEntityRepository>();
        }
    }
}

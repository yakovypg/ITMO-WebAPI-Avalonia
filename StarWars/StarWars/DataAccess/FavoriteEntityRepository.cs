using StarWars.Infrastructure.Converters;
using StarWars.Infrastructure.Services;
using StarWars.Models.Entities;
using StarWars.SharedConfig;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StarWars.DataAccess
{
    public class FavoriteEntityRepository : IEntityRepository
    {
        private static readonly string ServerUrl = $"{ServerConfig.Protocol}://{ServerConfig.Host}:{ServerConfig.Port}";
        private static readonly string ServerFavoriteEntitiesUrl = $"{ServerUrl}{ServerConfig.FavoriteEntitiesRoute}";

        public async IAsyncEnumerable<Entity> FindAllEntitiesAsync()
        {
            HttpClient httpClient = new();
            HttpResponseMessage response = await httpClient.GetAsync(ServerFavoriteEntitiesUrl);

            string responseContent = await response.Content.ReadAsStringAsync();
            string[] entitiesUrls = responseContent.Split();

            foreach (string entityUrl in entitiesUrls)
            {
                EntityType entityType = EntityUrlService.RecognizeEntityType(entityUrl);

                Entity entity = entityType switch
                {
                    EntityType.Character => new CharacterConverter().Convert(
                        await EntityDataService.CharactersRepository.FindEntityByUrlAsync(entityUrl)),

                    EntityType.Film => new FilmConverter().Convert(
                        await EntityDataService.FilmsRepository.FindEntityByUrlAsync(entityUrl)),

                    EntityType.Planet => new PlanetConverter().Convert(
                        await EntityDataService.PlanetsRepository.FindEntityByUrlAsync(entityUrl)),

                    EntityType.Specie => new SpecieConverter().Convert(
                        await EntityDataService.SpeciesRepository.FindEntityByUrlAsync(entityUrl)),

                    EntityType.Starship => new StarshipConverter().Convert(
                        await EntityDataService.StarshipsRepository.FindEntityByUrlAsync(entityUrl)),

                    EntityType.Vehicle => new VehicleConverter().Convert(
                        await EntityDataService.VehiclesRepository.FindEntityByUrlAsync(entityUrl)),

                    _ => throw new ArgumentOutOfRangeException(nameof(entityType)),
                };

                entity.IsFavorite = true;
                yield return entity;
            }
        }

        public async Task AddAsync(Entity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            var content = new StringContent(entity.Url);

            HttpClient httpClient = new();
            _ = await httpClient.PutAsync(ServerFavoriteEntitiesUrl, content);
        }

        public async Task RemoveAsync(Entity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            string url = $"{ServerFavoriteEntitiesUrl}{entity.Url}";

            HttpClient httpClient = new();
            _ = await httpClient.DeleteAsync(url);
        }
    }
}

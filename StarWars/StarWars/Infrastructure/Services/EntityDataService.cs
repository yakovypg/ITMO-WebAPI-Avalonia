using StarWars.DataAccess;
using StarWars.Infrastructure.Converters;
using StarWars.Models.Entities;
using StarWars.Models.Serialization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Infrastructure.Services
{
    public static class EntityDataService
    {
        static EntityDataService()
        {
            FavoriteEntityRepository = new FavoriteEntityRepository();

            CharactersRepository = new SwapiEntityDataRepository<CharacterData>();
            FilmsRepository = new SwapiEntityDataRepository<FilmData>();
            PlanetsRepository = new SwapiEntityDataRepository<PlanetData>();
            SpeciesRepository = new SwapiEntityDataRepository<SpecieData>();
            StarshipsRepository = new SwapiEntityDataRepository<StarshipData>();
            VehiclesRepository = new SwapiEntityDataRepository<VehicleData>();
        }

        public static IEntityRepository FavoriteEntityRepository { get; }

        public static IEntityDataRepository<CharacterData> CharactersRepository { get; }
        public static IEntityDataRepository<FilmData> FilmsRepository { get; }
        public static IEntityDataRepository<PlanetData> PlanetsRepository { get; }
        public static IEntityDataRepository<SpecieData> SpeciesRepository { get; }
        public static IEntityDataRepository<StarshipData> StarshipsRepository { get; }
        public static IEntityDataRepository<VehicleData> VehiclesRepository { get; }

        public static async Task<IEnumerable<string>> LoadDataAsync<TEntityData, TEntity>(
            IEnumerable<string> source,
            IEntityDataRepository<TEntityData> repository,
            IEntityDataConverter<TEntityData, TEntity> converter)
            where TEntity : Entity
            where TEntityData : EntityBaseData
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            ArgumentNullException.ThrowIfNull(converter, nameof(converter));

            const string urlBase = "http";
            var output = new List<string>();

            foreach (string sourceItem in source)
            {
                if (string.IsNullOrEmpty(sourceItem) || !sourceItem.StartsWith(urlBase))
                {
                    output.Add(sourceItem);
                    continue;
                }

                TEntityData data = await repository.FindEntityByUrlAsync(sourceItem);
                TEntity character = converter.Convert(data);

                output.Add(character.ToString());
            }

            return output;
        }
    }
}

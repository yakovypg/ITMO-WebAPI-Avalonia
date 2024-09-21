using StarWars.Models.Entities;
using StarWars.Models.Serialization;
using System;

namespace StarWars.Infrastructure.Services
{
    public static class EntityUrlService
    {
        private const string _baseUrl = "https://swapi.dev/api/";
        private const string _filmsUrlPart = "films/";
        private const string _peopleUrlPart = "people/";
        private const string _planetsUrlPart = "planets/";
        private const string _speciesUrlPart = "species/";
        private const string _starshipsUrlPart = "starships/";
        private const string _vehiclesUrlPart = "vehicles/";

        public static string GetUrl<T>() where T : EntityBaseData
        {
            Type entityType = typeof(T);

            return entityType == typeof(FilmData)
                ? UrlService.Combine(_baseUrl, _filmsUrlPart)
                : entityType == typeof(CharacterData)
                ? UrlService.Combine(_baseUrl, _peopleUrlPart)
                : entityType == typeof(PlanetData)
                ? UrlService.Combine(_baseUrl, _planetsUrlPart)
                : entityType == typeof(SpecieData)
                ? UrlService.Combine(_baseUrl, _speciesUrlPart)
                : entityType == typeof(StarshipData)
                ? UrlService.Combine(_baseUrl, _starshipsUrlPart)
                : entityType == typeof(VehicleData)
                ? UrlService.Combine(_baseUrl, _vehiclesUrlPart)
                : throw new ArgumentOutOfRangeException(nameof(entityType));
        }

        public static EntityType RecognizeEntityType(string url)
        {
            ArgumentNullException.ThrowIfNull(url, nameof(url));

            return url.Contains(_filmsUrlPart)
                ? EntityType.Film
                : url.Contains(_peopleUrlPart)
                ? EntityType.Character
                : url.Contains(_planetsUrlPart)
                ? EntityType.Planet
                : url.Contains(_speciesUrlPart)
                ? EntityType.Specie
                : url.Contains(_starshipsUrlPart)
                ? EntityType.Starship
                : url.Contains(_vehiclesUrlPart)
                ? EntityType.Vehicle
                : throw new ArgumentOutOfRangeException(nameof(url));
        }
    }
}

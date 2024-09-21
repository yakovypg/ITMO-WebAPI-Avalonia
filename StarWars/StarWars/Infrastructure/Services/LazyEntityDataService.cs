using StarWars.Infrastructure.Converters;
using StarWars.Infrastructure.Extensions;
using StarWars.Models;
using StarWars.Models.Entities;
using StarWars.Models.Entities.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Infrastructure.Services
{
    public static class LazyEntityDataService
    {
        public static async Task LoadAllData(Entity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            entity.IsReady = false;

            if (entity is ICharactersContainer charactersContainer)
                await LoadCharacters(charactersContainer);

            if (entity is IResidentsContainer residentsContainer)
                await LoadResidents(residentsContainer);

            if (entity is IPilotsContainer pilotsContainer)
                await LoadPilots(pilotsContainer);

            if (entity is IFilmsContainer filmsContainer)
                await LoadFilms(filmsContainer);

            if (entity is IPlanetsContainer planetsContainer)
                await LoadPlanets(planetsContainer);

            if (entity is IHomeworldContainer homeworldContainer)
                await LoadHomeworld(homeworldContainer);

            if (entity is ISpeciesContainer speciesContainer)
                await LoadSpecies(speciesContainer);

            if (entity is IStarshipsContainer starshipsContainer)
                await LoadStarships(starshipsContainer);

            if (entity is IVehiclesContainer vehiclesContainer)
                await LoadVehicles(vehiclesContainer);

            entity.IsReady = true;
        }

        public static async Task LoadCharacters(ICharactersContainer container)
        {
            ArgumentNullException.ThrowIfNull(container, nameof(container));

            IEnumerable<string> loadedData = await EntityDataService.LoadDataAsync(
                container.Characters,
                EntityDataService.CharactersRepository,
                new CharacterConverter());

            container.Characters.Reset(loadedData);
            NotifyContainerChanged(container, nameof(container.Characters));
        }

        public static async Task LoadResidents(IResidentsContainer container)
        {
            ArgumentNullException.ThrowIfNull(container, nameof(container));

            IEnumerable<string> loadedData = await EntityDataService.LoadDataAsync(
                container.Residents,
                EntityDataService.CharactersRepository,
                new CharacterConverter());

            container.Residents.Reset(loadedData);
            NotifyContainerChanged(container, nameof(container.Residents));
        }

        public static async Task LoadPilots(IPilotsContainer container)
        {
            ArgumentNullException.ThrowIfNull(container, nameof(container));

            IEnumerable<string> loadedData = await EntityDataService.LoadDataAsync(
                container.Pilots,
                EntityDataService.CharactersRepository,
                new CharacterConverter());

            container.Pilots.Reset(loadedData);
            NotifyContainerChanged(container, nameof(container.Pilots));
        }

        public static async Task LoadFilms(IFilmsContainer container)
        {
            ArgumentNullException.ThrowIfNull(container, nameof(container));

            IEnumerable<string> loadedData = await EntityDataService.LoadDataAsync(
                container.Films,
                EntityDataService.FilmsRepository,
                new FilmConverter());

            container.Films.Reset(loadedData);
            NotifyContainerChanged(container, nameof(container.Films));
        }

        public static async Task LoadPlanets(IPlanetsContainer container)
        {
            ArgumentNullException.ThrowIfNull(container, nameof(container));

            IEnumerable<string> loadedData = await EntityDataService.LoadDataAsync(
                container.Planets,
                EntityDataService.PlanetsRepository,
                new PlanetConverter());

            container.Planets.Reset(loadedData);
            NotifyContainerChanged(container, nameof(container.Planets));
        }

        public static async Task LoadHomeworld(IHomeworldContainer container)
        {
            ArgumentNullException.ThrowIfNull(container, nameof(container));

            IEnumerable<string> loadedData = await EntityDataService.LoadDataAsync(
                new string[] { container.Homeworld },
                EntityDataService.PlanetsRepository,
                new PlanetConverter());

            container.Homeworld = loadedData.FirstOrDefault() ?? string.Empty;
            NotifyContainerChanged(container, nameof(container.Homeworld));
        }

        public static async Task LoadSpecies(ISpeciesContainer container)
        {
            ArgumentNullException.ThrowIfNull(container, nameof(container));

            IEnumerable<string> loadedData = await EntityDataService.LoadDataAsync(
                container.Species,
                EntityDataService.SpeciesRepository,
                new SpecieConverter());

            container.Species.Reset(loadedData);
            NotifyContainerChanged(container, nameof(container.Species));
        }

        public static async Task LoadStarships(IStarshipsContainer container)
        {
            ArgumentNullException.ThrowIfNull(container, nameof(container));

            IEnumerable<string> loadedData = await EntityDataService.LoadDataAsync(
                container.Starships,
                EntityDataService.StarshipsRepository,
                new StarshipConverter());

            container.Starships.Reset(loadedData);
            NotifyContainerChanged(container, nameof(container.Starships));
        }

        public static async Task LoadVehicles(IVehiclesContainer container)
        {
            ArgumentNullException.ThrowIfNull(container, nameof(container));

            IEnumerable<string> loadedData = await EntityDataService.LoadDataAsync(
                container.Vehicles,
                EntityDataService.VehiclesRepository,
                new VehicleConverter());

            container.Vehicles.Reset(loadedData);
            NotifyContainerChanged(container, nameof(container.Vehicles));
        }

        private static void NotifyContainerChanged(object container, string propertyName)
        {
            ArgumentNullException.ThrowIfNull(container, nameof(container));
            ArgumentNullException.ThrowIfNull(propertyName, nameof(propertyName));

            if (container is IReactiveModel reactiveModel)
            {
                reactiveModel.OnPropertyChanged(propertyName);

                if (container is IFieldValuesContainer)
                    reactiveModel.OnPropertyChanged(nameof(IFieldValuesContainer.FieldValues));
            }
        }
    }
}

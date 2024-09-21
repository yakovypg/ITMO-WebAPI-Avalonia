using StarWars.DataAccess;
using StarWars.Infrastructure.Converters;
using StarWars.Infrastructure.Services;
using StarWars.Models.Entities;
using StarWars.Models.Serialization;
using System;
using System.Threading.Tasks;

namespace StarWars.ViewModels
{
    public class PlanetsViewModel(FavoriteEntitiesViewModel favoriteEntitiesViewModel)
        : EntityViewModel<Planet, PlanetData>(favoriteEntitiesViewModel
            ?? throw new ArgumentNullException(nameof(favoriteEntitiesViewModel)))
    {
        public PlanetsViewModel()
            : this(new FavoriteEntitiesViewModel())
        {
        }

        protected override IEntityDataRepository<PlanetData> EntityRepository => EntityDataService.PlanetsRepository;
        protected override IEntityDataConverter<PlanetData, Planet> EntityConverter { get; } = new PlanetConverter();
    }
}

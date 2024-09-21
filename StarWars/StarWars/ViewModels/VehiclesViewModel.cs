using StarWars.DataAccess;
using StarWars.Infrastructure.Converters;
using StarWars.Infrastructure.Services;
using StarWars.Models.Entities;
using StarWars.Models.Serialization;
using System;

namespace StarWars.ViewModels
{
    public class VehiclesViewModel(FavoriteEntitiesViewModel favoriteEntitiesViewModel)
        : EntityViewModel<Vehicle, VehicleData>(favoriteEntitiesViewModel
            ?? throw new ArgumentNullException(nameof(favoriteEntitiesViewModel)))
    {
        public VehiclesViewModel()
            : this(new FavoriteEntitiesViewModel())
        {
        }

        protected override IEntityDataRepository<VehicleData> EntityRepository => EntityDataService.VehiclesRepository;
        protected override IEntityDataConverter<VehicleData, Vehicle> EntityConverter { get; } = new VehicleConverter();
    }
}

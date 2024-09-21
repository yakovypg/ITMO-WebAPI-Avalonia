using StarWars.DataAccess;
using StarWars.Infrastructure.Converters;
using StarWars.Infrastructure.Services;
using StarWars.Models.Entities;
using StarWars.Models.Serialization;
using System;
using System.Threading.Tasks;

namespace StarWars.ViewModels
{
    public class SpeciesViewModel(FavoriteEntitiesViewModel favoriteEntitiesViewModel)
        : EntityViewModel<Specie, SpecieData>(favoriteEntitiesViewModel
            ?? throw new ArgumentNullException(nameof(favoriteEntitiesViewModel)))
    {
        public SpeciesViewModel()
            : this(new FavoriteEntitiesViewModel())
        {
        }

        protected override IEntityDataRepository<SpecieData> EntityRepository => EntityDataService.SpeciesRepository;
        protected override IEntityDataConverter<SpecieData, Specie> EntityConverter { get; } = new SpecieConverter();
    }
}

using StarWars.DataAccess;
using StarWars.Infrastructure.Converters;
using StarWars.Infrastructure.Services;
using StarWars.Models.Entities;
using StarWars.Models.Serialization;
using System;
using System.Threading.Tasks;

namespace StarWars.ViewModels
{
    public class StarshipsViewModel(FavoriteEntitiesViewModel favoriteEntitiesViewModel)
        : EntityViewModel<Starship, StarshipData>(favoriteEntitiesViewModel
            ?? throw new ArgumentNullException(nameof(favoriteEntitiesViewModel)))
    {
        public StarshipsViewModel()
            : this(new FavoriteEntitiesViewModel())
        {
        }

        protected override IEntityDataRepository<StarshipData> EntityRepository => EntityDataService.StarshipsRepository;
        protected override IEntityDataConverter<StarshipData, Starship> EntityConverter { get; } = new StarshipConverter();
    }
}

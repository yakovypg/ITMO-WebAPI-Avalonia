using StarWars.DataAccess;
using StarWars.Infrastructure.Converters;
using StarWars.Infrastructure.Services;
using StarWars.Models.Entities;
using StarWars.Models.Serialization;
using System;

namespace StarWars.ViewModels
{
    public class FilmsViewModel(FavoriteEntitiesViewModel favoriteEntitiesViewModel)
        : EntityViewModel<Film, FilmData>(favoriteEntitiesViewModel
            ?? throw new ArgumentNullException(nameof(favoriteEntitiesViewModel)))
    {
        public FilmsViewModel()
            : this(new FavoriteEntitiesViewModel())
        {
        }

        protected override IEntityDataRepository<FilmData> EntityRepository => EntityDataService.FilmsRepository;
        protected override IEntityDataConverter<FilmData, Film> EntityConverter { get; } = new FilmConverter();
    }
}

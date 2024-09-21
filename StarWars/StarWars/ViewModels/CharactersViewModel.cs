using StarWars.DataAccess;
using StarWars.Infrastructure.Converters;
using StarWars.Infrastructure.Services;
using StarWars.Models.Entities;
using StarWars.Models.Serialization;
using System;

namespace StarWars.ViewModels
{
    public class CharactersViewModel(FavoriteEntitiesViewModel favoriteEntitiesViewModel)
        : EntityViewModel<Character, CharacterData>(favoriteEntitiesViewModel
            ?? throw new ArgumentNullException(nameof(favoriteEntitiesViewModel)))
    {
        public CharactersViewModel()
            : this(new FavoriteEntitiesViewModel())
        {
        }

        protected override IEntityDataRepository<CharacterData> EntityRepository => EntityDataService.CharactersRepository;
        protected override IEntityDataConverter<CharacterData, Character> EntityConverter { get; } = new CharacterConverter();
    }
}

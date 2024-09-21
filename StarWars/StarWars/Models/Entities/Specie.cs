using StarWars.Infrastructure.Services;
using StarWars.Models.Entities.Containers;
using System;
using System.Collections.Generic;

namespace StarWars.Models.Entities
{
    public record Specie(
        DateTime? CreationDate,
        DateTime? EditedDate,
        string Url,
        double? AverageHeight,
        double? AverageLifespan,
        string Classification,
        string Designation,
        List<string> EyeColors,
        List<string> HairColors,
        string Language,
        string Name,
        List<string> Characters,
        List<string> Films,
        List<string> SkinColors)
        : Entity(CreationDate, EditedDate, Url),
            ICharactersContainer, IFilmsContainer, IHomeworldContainer
    {
        private string _homeworld = string.Empty;
        public string Homeworld
        {
            get => _homeworld;
            set => RaiseAndSetIfChanged(ref _homeworld, value);
        }

        public Specie Self => this;
        public override string IconName => "fa-users";
        public override string Presenter => Name;

        public override IReadOnlyCollection<FieldValue> FieldValues => new List<FieldValue>()
        {
            new(NameService.SplitCamelCase(nameof(Name)), ValuePresentationService.GetPresenter(Name)),
            new(NameService.SplitCamelCase(nameof(Language)), ValuePresentationService.GetPresenter(Language)),
            new(NameService.SplitCamelCase(nameof(Homeworld)), ValuePresentationService.GetPresenter(Homeworld)),
            new(NameService.SplitCamelCase(nameof(Designation)), ValuePresentationService.GetPresenter(Designation)),
            new(NameService.SplitCamelCase(nameof(Classification)), ValuePresentationService.GetPresenter(Classification)),
            new(NameService.SplitCamelCase(nameof(AverageLifespan)), ValuePresentationService.GetPresenter(AverageLifespan)),
            new(NameService.SplitCamelCase(nameof(AverageHeight)), ValuePresentationService.GetPresenter(AverageHeight)),
            new(NameService.SplitCamelCase(nameof(CreationDate)), ValuePresentationService.GetPresenter(CreationDate)),
            new(NameService.SplitCamelCase(nameof(EditedDate)), ValuePresentationService.GetPresenter(EditedDate)),
            new(NameService.SplitCamelCase(nameof(Url)), ValuePresentationService.GetPresenter(Url)),
            new(NameService.SplitCamelCase(nameof(SkinColors)), ValuePresentationService.GetPresenter(SkinColors)),
            new(NameService.SplitCamelCase(nameof(EyeColors)), ValuePresentationService.GetPresenter(EyeColors)),
            new(NameService.SplitCamelCase(nameof(HairColors)), ValuePresentationService.GetPresenter(HairColors)),
            new(NameService.SplitCamelCase(nameof(Films)), ValuePresentationService.GetPresenter(Films)),
            new(NameService.SplitCamelCase(nameof(Characters)), ValuePresentationService.GetPresenter(Characters)),
        };

        public override string ToString()
        {
            return Presenter;
        }
    }
}

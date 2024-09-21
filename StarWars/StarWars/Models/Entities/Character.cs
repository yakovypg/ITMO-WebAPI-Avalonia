using StarWars.Infrastructure.Services;
using StarWars.Models.Entities.Containers;
using System;
using System.Collections.Generic;

namespace StarWars.Models.Entities
{
    public record Character(
        DateTime? CreationDate,
        DateTime? EditedDate,
        string Url,
        string BirthYear,
        string EyeColor,
        List<string> Films,
        string Gender,
        string HairColor,
        double? Height,
        double? Mass,
        string Name,
        string SkinColor,
        List<string> Species,
        List<string> Starships,
        List<string> Vehicles)
        : Entity(CreationDate, EditedDate, Url),
            IFilmsContainer, ISpeciesContainer, IStarshipsContainer, IVehiclesContainer, IHomeworldContainer
    {
        private string _homeworld = string.Empty;
        public string Homeworld
        {
            get => _homeworld;
            set => RaiseAndSetIfChanged(ref _homeworld, value);
        }

        public Character Self => this;
        public override string IconName => "fa-user";
        public override string Presenter => Name;

        public override IReadOnlyCollection<FieldValue> FieldValues => new List<FieldValue>()
        {
            new(NameService.SplitCamelCase(nameof(Name)), ValuePresentationService.GetPresenter(Name)),
            new(NameService.SplitCamelCase(nameof(Mass)), ValuePresentationService.GetPresenter(Mass)),
            new(NameService.SplitCamelCase(nameof(Height)), ValuePresentationService.GetPresenter(Height)),
            new(NameService.SplitCamelCase(nameof(Gender)), ValuePresentationService.GetPresenter(Gender)),
            new(NameService.SplitCamelCase(nameof(BirthYear)), ValuePresentationService.GetPresenter(BirthYear)),
            new(NameService.SplitCamelCase(nameof(EyeColor)), ValuePresentationService.GetPresenter(EyeColor)),
            new(NameService.SplitCamelCase(nameof(SkinColor)), ValuePresentationService.GetPresenter(SkinColor)),
            new(NameService.SplitCamelCase(nameof(HairColor)), ValuePresentationService.GetPresenter(HairColor)),
            new(NameService.SplitCamelCase(nameof(Homeworld)), ValuePresentationService.GetPresenter(Homeworld)),
            new(NameService.SplitCamelCase(nameof(CreationDate)), ValuePresentationService.GetPresenter(CreationDate)),
            new(NameService.SplitCamelCase(nameof(EditedDate)), ValuePresentationService.GetPresenter(EditedDate)),
            new(NameService.SplitCamelCase(nameof(Url)), ValuePresentationService.GetPresenter(Url)),
            new(NameService.SplitCamelCase(nameof(Films)), ValuePresentationService.GetPresenter(Films)),
            new(NameService.SplitCamelCase(nameof(Species)), ValuePresentationService.GetPresenter(Species)),
            new(NameService.SplitCamelCase(nameof(Starships)), ValuePresentationService.GetPresenter(Starships)),
            new(NameService.SplitCamelCase(nameof(Vehicles)), ValuePresentationService.GetPresenter(Vehicles)),
        };

        public override string ToString()
        {
            return Presenter;
        }
    }
}

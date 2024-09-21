using StarWars.Infrastructure.Services;
using StarWars.Models.Entities.Containers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace StarWars.Models.Entities
{
    public record Starship(
        DateTime? CreationDate,
        DateTime? EditedDate,
        string Url,
        string Mglt,
        long? CargoCapacity,
        string Consumables,
        long? CostInCredits,
        long? Crew,
        double? HyperdriveRating,
        double? Length,
        string Manufacturer,
        double? MaxAtmospheringSpeed,
        string Model,
        string Name,
        long? Passengers,
        List<string> Films,
        List<string> Pilots,
        string StarshipClass)
        : Entity(CreationDate, EditedDate, Url), IFilmsContainer, IPilotsContainer
    {
        public Starship Self => this;
        public override string IconName => "fa-star";
        public override string Presenter => Name;

        public override IReadOnlyCollection<FieldValue> FieldValues => new List<FieldValue>()
        {
            new(NameService.SplitCamelCase(nameof(Name)), ValuePresentationService.GetPresenter(Name)),
            new(NameService.SplitCamelCase(nameof(Model)), ValuePresentationService.GetPresenter(Model)),
            new(NameService.SplitCamelCase(nameof(StarshipClass)), ValuePresentationService.GetPresenter(StarshipClass)),
            new(NameService.SplitCamelCase(nameof(Passengers)), ValuePresentationService.GetPresenter(Passengers)),
            new(NameService.SplitCamelCase(nameof(MaxAtmospheringSpeed)), ValuePresentationService.GetPresenter(MaxAtmospheringSpeed)),
            new(NameService.SplitCamelCase(nameof(Manufacturer)), ValuePresentationService.GetPresenter(Manufacturer)),
            new(NameService.SplitCamelCase(nameof(Length)), ValuePresentationService.GetPresenter(Length)),
            new(NameService.SplitCamelCase(nameof(HyperdriveRating)), ValuePresentationService.GetPresenter(HyperdriveRating)),
            new(NameService.SplitCamelCase(nameof(Crew)), ValuePresentationService.GetPresenter(Crew)),
            new(NameService.SplitCamelCase(nameof(CostInCredits)), ValuePresentationService.GetPresenter(CostInCredits)),
            new(NameService.SplitCamelCase(nameof(Consumables)), ValuePresentationService.GetPresenter(Consumables)),
            new(NameService.SplitCamelCase(nameof(CargoCapacity)), ValuePresentationService.GetPresenter(CargoCapacity)),
            new(NameService.SplitCamelCase(nameof(Mglt)), ValuePresentationService.GetPresenter(Mglt)),
            new(NameService.SplitCamelCase(nameof(CreationDate)), ValuePresentationService.GetPresenter(CreationDate)),
            new(NameService.SplitCamelCase(nameof(EditedDate)), ValuePresentationService.GetPresenter(EditedDate)),
            new(NameService.SplitCamelCase(nameof(Url)), ValuePresentationService.GetPresenter(Url)),
            new(NameService.SplitCamelCase(nameof(Films)), ValuePresentationService.GetPresenter(Films)),
            new(NameService.SplitCamelCase(nameof(Pilots)), ValuePresentationService.GetPresenter(Pilots)),
        };

        public override string ToString()
        {
            return Presenter;
        }
    }
}

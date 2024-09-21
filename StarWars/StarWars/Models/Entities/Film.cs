using StarWars.Infrastructure.Services;
using StarWars.Models.Entities.Containers;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace StarWars.Models.Entities
{
    public record Film(
        DateTime? CreationDate,
        DateTime? EditedDate,
        string Url,
        List<string> Characters,
        string Director,
        int EpisodeId,
        string OpeningCrawl,
        List<string> Planets,
        List<string> Producers,
        DateTime? ReleaseDate,
        List<string> Species,
        List<string> Starships,
        string Title,
        List<string> Vehicles)
        : Entity(CreationDate, EditedDate, Url),
            ICharactersContainer, IPlanetsContainer, ISpeciesContainer, IStarshipsContainer, IVehiclesContainer
    {
        public Film Self => this;
        public override string IconName => "fa-video";
        public override string Presenter => Title;

        public override IReadOnlyCollection<FieldValue> FieldValues => new List<FieldValue>()
        {
            new(NameService.SplitCamelCase(nameof(Title)), ValuePresentationService.GetPresenter(Title)),
            new(NameService.SplitCamelCase(nameof(Director)), ValuePresentationService.GetPresenter(Director)),
            new(NameService.SplitCamelCase(nameof(EpisodeId)), ValuePresentationService.GetPresenter(EpisodeId)),
            new(NameService.SplitCamelCase(nameof(OpeningCrawl)), ValuePresentationService.GetPresenter(OpeningCrawl)),
            new(NameService.SplitCamelCase(nameof(ReleaseDate)), ValuePresentationService.GetPresenter(ReleaseDate)),
            new(NameService.SplitCamelCase(nameof(CreationDate)), ValuePresentationService.GetPresenter(CreationDate)),
            new(NameService.SplitCamelCase(nameof(EditedDate)), ValuePresentationService.GetPresenter(EditedDate)),
            new(NameService.SplitCamelCase(nameof(Url)), ValuePresentationService.GetPresenter(Url)),
            new(NameService.SplitCamelCase(nameof(Characters)), ValuePresentationService.GetPresenter(Characters)),
            new(NameService.SplitCamelCase(nameof(Planets)), ValuePresentationService.GetPresenter(Planets)),
            new(NameService.SplitCamelCase(nameof(Producers)), ValuePresentationService.GetPresenter(Producers)),
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

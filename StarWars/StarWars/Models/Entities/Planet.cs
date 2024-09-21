using StarWars.Infrastructure.Services;
using StarWars.Models.Entities.Containers;
using System;
using System.Collections.Generic;

namespace StarWars.Models.Entities
{
    public record Planet(
        DateTime? CreationDate,
        DateTime? EditedDate,
        string Url,
        string Climate,
        double? Diameter,
        List<string> Films,
        double? Gravity,
        string Name,
        double? OrbitalPeriod,
        long? Population,
        List<string> Residents,
        double? RotationPeriod,
        bool SurfaceWater,
        string Terrain)
        : Entity(CreationDate, EditedDate, Url), IFilmsContainer, IResidentsContainer
    {
        public Planet Self => this;
        public override string IconName => "fa-globe";
        public override string Presenter => Name;

        public override IReadOnlyCollection<FieldValue> FieldValues => new List<FieldValue>()
        {
            new(NameService.SplitCamelCase(nameof(Name)), ValuePresentationService.GetPresenter(Name)),
            new(NameService.SplitCamelCase(nameof(Population)), ValuePresentationService.GetPresenter(Population)),
            new(NameService.SplitCamelCase(nameof(Climate)), ValuePresentationService.GetPresenter(Climate)),
            new(NameService.SplitCamelCase(nameof(Diameter)), ValuePresentationService.GetPresenter(Diameter)),
            new(NameService.SplitCamelCase(nameof(Gravity)), ValuePresentationService.GetPresenter(Gravity)),
            new(NameService.SplitCamelCase(nameof(OrbitalPeriod)), ValuePresentationService.GetPresenter(OrbitalPeriod)),
            new(NameService.SplitCamelCase(nameof(RotationPeriod)), ValuePresentationService.GetPresenter(RotationPeriod)),
            new(NameService.SplitCamelCase(nameof(SurfaceWater)), ValuePresentationService.GetPresenter(SurfaceWater)),
            new(NameService.SplitCamelCase(nameof(Terrain)), ValuePresentationService.GetPresenter(Terrain)),
            new(NameService.SplitCamelCase(nameof(CreationDate)), ValuePresentationService.GetPresenter(CreationDate)),
            new(NameService.SplitCamelCase(nameof(EditedDate)), ValuePresentationService.GetPresenter(EditedDate)),
            new(NameService.SplitCamelCase(nameof(Url)), ValuePresentationService.GetPresenter(Url)),
            new(NameService.SplitCamelCase(nameof(Films)), ValuePresentationService.GetPresenter(Films)),
            new(NameService.SplitCamelCase(nameof(Residents)), ValuePresentationService.GetPresenter(Residents)),
        };

        public override string ToString()
        {
            return Presenter;
        }
    }
}

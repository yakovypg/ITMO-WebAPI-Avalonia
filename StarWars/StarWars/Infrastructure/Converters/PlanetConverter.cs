using StarWars.Models.Entities;
using StarWars.Models.Serialization;
using System;
using System.Diagnostics.CodeAnalysis;

namespace StarWars.Infrastructure.Converters
{
    public class PlanetConverter : IEntityDataConverter<PlanetData, Planet>
    {
        public Planet Convert(PlanetData entityData)
        {
            ArgumentNullException.ThrowIfNull(entityData, nameof(entityData));

            DateTime? createdDate = null;
            DateTime? editedDate = null;
            double? diameter = null;
            double? gravity = null;
            double? orbitalPeriod = null;
            double? rotationPeriod = null;
            long? population = null;

            if (DateTime.TryParse(entityData.Created, out DateTime parsedCreatedDate))
                createdDate = parsedCreatedDate;

            if (DateTime.TryParse(entityData.Edited, out DateTime parsedEditedDate))
                editedDate = parsedEditedDate;

            if (double.TryParse(entityData.Diameter, out double parsedDiameter))
                diameter = parsedDiameter;

            if (double.TryParse(entityData.Gravity, out double parsedGravity))
                gravity = parsedGravity;

            if (double.TryParse(entityData.OrbitalPeriod, out double parsedOrbitalPeriod))
                orbitalPeriod = parsedOrbitalPeriod;

            if (double.TryParse(entityData.RotationPeriod, out double parsedRotationPeriod))
                rotationPeriod = parsedRotationPeriod;

            if (long.TryParse(entityData.Population, out long parsedPopulation))
                population = parsedPopulation;

            return new Planet(
                createdDate,
                editedDate,
                entityData.Url ?? string.Empty,
                entityData.Climate ?? string.Empty,
                diameter,
                new(entityData.Films),
                gravity,
                entityData.Name ?? string.Empty,
                orbitalPeriod,
                population,
                new(entityData.Residents),
                rotationPeriod,
                entityData.SurfaceWater == "1",
                entityData.Terrain ?? string.Empty);
        }

        public bool TryConvert(PlanetData entityData, [NotNullWhen(true)] out Planet? entity)
        {
            try
            {
                entity = Convert(entityData);
                return true;
            }
            catch
            {
                entity = null;
                return false;
            }
        }
    }
}

using StarWars.Models.Entities;
using StarWars.Models.Serialization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace StarWars.Infrastructure.Converters
{
    public class SpecieConverter : IEntityDataConverter<SpecieData, Specie>
    {
        public Specie Convert(SpecieData entityData)
        {
            ArgumentNullException.ThrowIfNull(entityData, nameof(entityData));

            DateTime? createdDate = null;
            DateTime? editedDate = null;
            double? averageHeight = null;
            double? averageLifespan = null;

            if (DateTime.TryParse(entityData.Created, out DateTime parsedCreatedDate))
                createdDate = parsedCreatedDate;

            if (DateTime.TryParse(entityData.Edited, out DateTime parsedEditedDate))
                editedDate = parsedEditedDate;

            if (double.TryParse(entityData.AverageHeight, out double parsedAverageHeight))
                averageHeight = parsedAverageHeight;

            if (double.TryParse(entityData.AverageLifespan, out double parsedAverageLifespan))
                averageLifespan = parsedAverageLifespan;

            return new Specie(
                createdDate,
                editedDate,
                entityData.Url ?? string.Empty,
                averageHeight,
                averageLifespan,
                entityData.Classification ?? string.Empty,
                entityData.Designation ?? string.Empty,
                entityData.EyeColors?.Split(", ").ToList() ?? [],
                entityData.HairColors?.Split(", ").ToList() ?? [],
                entityData.Language ?? string.Empty,
                entityData.Name ?? string.Empty,
                new(entityData.People),
                new(entityData.Films),
                entityData.SkinColors?.Split(", ").ToList() ?? [])
            {
                Homeworld = entityData.Homeworld ?? string.Empty,
            };
        }

        public bool TryConvert(SpecieData entityData, [NotNullWhen(true)] out Specie? entity)
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

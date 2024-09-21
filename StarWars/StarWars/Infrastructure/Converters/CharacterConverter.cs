using StarWars.Models.Entities;
using StarWars.Models.Serialization;
using System;
using System.Diagnostics.CodeAnalysis;

namespace StarWars.Infrastructure.Converters
{
    public class CharacterConverter : IEntityDataConverter<CharacterData, Character>
    {
        public Character Convert(CharacterData entityData)
        {
            ArgumentNullException.ThrowIfNull(entityData, nameof(entityData));

            DateTime? createdDate = null;
            DateTime? editedDate = null;
            double? height = null;
            double? mass = null;

            if (DateTime.TryParse(entityData.Created, out DateTime parsedCreatedDate))
                createdDate = parsedCreatedDate;

            if (DateTime.TryParse(entityData.Edited, out DateTime parsedEditedDate))
                editedDate = parsedEditedDate;

            if (double.TryParse(entityData.Height, out double parsedHeight))
                height = parsedHeight;

            if (double.TryParse(entityData.Mass, out double parsedMass))
                mass = parsedMass;

            return new Character(
                createdDate,
                editedDate,
                entityData.Url ?? string.Empty,
                entityData.BirthYear ?? string.Empty,
                entityData.EyeColor ?? string.Empty,
                new(entityData.Films),
                entityData.Gender ?? string.Empty,
                entityData.HairColor ?? string.Empty,
                height,
                mass,
                entityData.Name ?? string.Empty,
                entityData.SkinColor ?? string.Empty,
                new(entityData.Species),
                new(entityData.Starships),
                new(entityData.Vehicles))
            {
                Homeworld = entityData.Homeworld ?? string.Empty,
            };
        }

        public bool TryConvert(CharacterData entityData, [NotNullWhen(true)] out Character? entity)
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

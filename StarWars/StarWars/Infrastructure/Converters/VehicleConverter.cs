using StarWars.Models.Entities;
using StarWars.Models.Serialization;
using System;
using System.Diagnostics.CodeAnalysis;

namespace StarWars.Infrastructure.Converters
{
    public class VehicleConverter : IEntityDataConverter<VehicleData, Vehicle>
    {
        public Vehicle Convert(VehicleData entityData)
        {
            ArgumentNullException.ThrowIfNull(entityData, nameof(entityData));

            DateTime? createdDate = null;
            DateTime? editedDate = null;
            double? length = null;
            double? maxAtmospheringSpeed = null;
            long? cargoCapacity = null;
            long? costInCredits = null;
            long? crew = null;
            long? passengers = null;

            if (DateTime.TryParse(entityData.Created, out DateTime parsedCreatedDate))
                createdDate = parsedCreatedDate;

            if (DateTime.TryParse(entityData.Edited, out DateTime parsedEditedDate))
                editedDate = parsedEditedDate;

            if (double.TryParse(entityData.Length, out double parsedLength))
                length = parsedLength;

            if (double.TryParse(entityData.MaxAtmospheringSpeed, out double parsedMaxAtmospheringSpeed))
                maxAtmospheringSpeed = parsedMaxAtmospheringSpeed;

            if (long.TryParse(entityData.CargoCapacity, out long parsedCargoCapacity))
                cargoCapacity = parsedCargoCapacity;

            if (long.TryParse(entityData.CostInCredits, out long parsedCostInCredits))
                costInCredits = parsedCostInCredits;

            if (long.TryParse(entityData.Crew, out long parsedCrew))
                crew = parsedCrew;

            if (long.TryParse(entityData.Passengers, out long parsedPassengers))
                passengers = parsedPassengers;

            return new Vehicle(
                createdDate,
                editedDate,
                entityData.Url ?? string.Empty,
                cargoCapacity,
                entityData.Consumables ?? string.Empty,
                costInCredits,
                crew,
                length,
                entityData.Manufacturer ?? string.Empty,
                maxAtmospheringSpeed,
                entityData.Model ?? string.Empty,
                entityData.Name ?? string.Empty,
                passengers,
                new(entityData.Pilots),
                new(entityData.Films),
                entityData.VehicleClass ?? string.Empty);
        }

        public bool TryConvert(VehicleData entityData, [NotNullWhen(true)] out Vehicle? entity)
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

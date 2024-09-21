using Newtonsoft.Json;
using System.Collections.Generic;

namespace StarWars.Models.Serialization
{
    public class StarshipData : EntityBaseData
    {
        [JsonProperty(PropertyName = "MGLT")]
        public string? Mglt { get; set; }

        [JsonProperty(PropertyName = "cargo_capacity")]
        public string? CargoCapacity { get; set; }

        [JsonProperty(PropertyName = "consumables")]
        public string? Consumables { get; set; }

        [JsonProperty(PropertyName = "cost_in_credits")]
        public string? CostInCredits { get; set; }

        [JsonProperty(PropertyName = "crew")]
        public string? Crew { get; set; }

        [JsonProperty(PropertyName = "hyperdrive_rating")]
        public string? HyperdriveRating { get; set; }

        [JsonProperty(PropertyName = "length")]
        public string? Length { get; set; }

        [JsonProperty(PropertyName = "manufacturer")]
        public string? Manufacturer { get; set; }

        [JsonProperty(PropertyName = "max_atmosphering_speed")]
        public string? MaxAtmospheringSpeed { get; set; }

        [JsonProperty(PropertyName = "model")]
        public string? Model { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "passengers")]
        public string? Passengers { get; set; }

        [JsonProperty(PropertyName = "films")]
        public List<string> Films { get; set; } = [];

        [JsonProperty(PropertyName = "pilots")]
        public List<string> Pilots { get; set; } = [];

        [JsonProperty(PropertyName = "starship_class")]
        public string? StarshipClass { get; set; }
    }
}

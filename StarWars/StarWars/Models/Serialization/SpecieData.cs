using Newtonsoft.Json;
using System.Collections.Generic;

namespace StarWars.Models.Serialization
{
    public class SpecieData : EntityBaseData
    {
        [JsonProperty(PropertyName = "average_height")]
        public string? AverageHeight { get; set; }

        [JsonProperty(PropertyName = "average_lifespan")]
        public string? AverageLifespan { get; set; }

        [JsonProperty(PropertyName = "classification")]
        public string? Classification { get; set; }

        [JsonProperty(PropertyName = "designation")]
        public string? Designation { get; set; }

        [JsonProperty(PropertyName = "eye_colors")]
        public string? EyeColors { get; set; }

        [JsonProperty(PropertyName = "hair_colors")]
        public string? HairColors { get; set; }

        [JsonProperty(PropertyName = "homeworld")]
        public string? Homeworld { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string? Language { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "people")]
        public List<string> People { get; set; } = [];

        [JsonProperty(PropertyName = "films")]
        public List<string> Films { get; set; } = [];

        [JsonProperty(PropertyName = "skin_colors")]
        public string? SkinColors { get; set; }
    }
}

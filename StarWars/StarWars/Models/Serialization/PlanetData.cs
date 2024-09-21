using Newtonsoft.Json;
using System.Collections.Generic;

namespace StarWars.Models.Serialization
{
    public class PlanetData : EntityBaseData
    {
        [JsonProperty(PropertyName = "climate")]
        public string? Climate { get; set; }

        [JsonProperty(PropertyName = "diameter")]
        public string? Diameter { get; set; }

        [JsonProperty(PropertyName = "films")]
        public List<string> Films { get; set; } = [];

        [JsonProperty(PropertyName = "gravity")]
        public string? Gravity { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "orbital_period")]
        public string? OrbitalPeriod { get; set; }

        [JsonProperty(PropertyName = "population")]
        public string? Population { get; set; }

        [JsonProperty(PropertyName = "residents")]
        public List<string> Residents { get; set; } = [];

        [JsonProperty(PropertyName = "rotation_period")]
        public string? RotationPeriod { get; set; }

        [JsonProperty(PropertyName = "surface_water")]
        public string? SurfaceWater { get; set; }

        [JsonProperty(PropertyName = "terrain")]
        public string? Terrain { get; set; }
    }
}

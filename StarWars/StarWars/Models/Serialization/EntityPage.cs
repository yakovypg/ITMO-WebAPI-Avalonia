using Newtonsoft.Json;
using System.Collections.Generic;

namespace StarWars.Models.Serialization
{
    public class EntityPage<T> where T : EntityBaseData
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "next")]
        public string? Next { get; set; }

        [JsonProperty(PropertyName = "previous")]
        public string? Previous { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<T> Results { get; set; } = [];
    }
}

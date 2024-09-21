using Newtonsoft.Json;
using System.Collections.Generic;

namespace StarWars.Models.Serialization
{
    public class FilmData : EntityBaseData
    {
        [JsonProperty(PropertyName = "characters")]
        public List<string> Characters { get; set; } = [];

        [JsonProperty(PropertyName = "director")]
        public string? Director { get; set; }

        [JsonProperty(PropertyName = "episode_id")]
        public int EpisodeId { get; set; }

        [JsonProperty(PropertyName = "opening_crawl")]
        public string? OpeningCrawl { get; set; }

        [JsonProperty(PropertyName = "planets")]
        public List<string> Planets { get; set; } = [];

        [JsonProperty(PropertyName = "producer")]
        public string? Producer { get; set; }

        [JsonProperty(PropertyName = "release_date")]
        public string? ReleaseDate { get; set; }

        [JsonProperty(PropertyName = "species")]
        public List<string> Species { get; set; } = [];

        [JsonProperty(PropertyName = "starships")]
        public List<string> Starships { get; set; } = [];

        [JsonProperty(PropertyName = "title")]
        public string? Title { get; set; }

        [JsonProperty(PropertyName = "vehicles")]
        public List<string> Vehicles { get; set; } = [];

    }
}

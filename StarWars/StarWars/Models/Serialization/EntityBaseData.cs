using Newtonsoft.Json;

namespace StarWars.Models.Serialization
{
    public abstract class EntityBaseData
    {
        [JsonProperty(PropertyName = "created")]
        public string? Created { get; set; }

        [JsonProperty(PropertyName = "edited")]
        public string? Edited { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string? Url { get; set; }

        public virtual string? ParentUrl
        {
            get
            {
                if (string.IsNullOrEmpty(Url) || !Url.Contains('/'))
                    return null;

                string url = Url[^1] == '/'
                    ? Url.Remove(Url.Length - 1)
                    : Url;

                int lastSlashIndex = url.LastIndexOf('/');

                return lastSlashIndex > 0
                    ? url.Remove(lastSlashIndex)
                    : null;
            }
        }
    }
}

using Newtonsoft.Json;
using Digger.Infra.Diigo.Helpers;

namespace Digger.Infra.Diigo.Models
{
    [JsonObject]
    public class Bookmark
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }

        [JsonProperty("desc")]
        public string? Desc { get; set; }

        [JsonProperty("tags")]
        public string? Tags { get; set; }

        [JsonProperty("shared")]
        [JsonConverter(typeof(CustomBooleanConverter))]
        public bool Shared { get; set; }

        [JsonProperty("readLater")]
        [JsonConverter(typeof(CustomBooleanConverter))]
        public bool ReadLater { get; set; }

        [JsonProperty("merge")]
        public bool Merge { get; set; }
    }
}

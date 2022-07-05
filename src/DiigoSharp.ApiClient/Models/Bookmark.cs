using System.Text.Json.Serialization;

namespace DiigoSharp.ApiClient.Models
{
    public class Bookmark
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("desc")]
        public string Desc { get; set; }

        [JsonPropertyName("tags")]
        public string Tags { get; set; }

        [JsonPropertyName("shared")]
        public string Shared { get; set; }

        [JsonPropertyName("readLater")]
        public string ReadLater { get; set; }

        [JsonPropertyName("merge")]
        public bool Merge { get; set; }
    }
}
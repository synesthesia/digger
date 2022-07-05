using System.Text.Json.Serialization;

namespace DiigoSharp.ApiClient.Models
{
    public class BookmarkItem
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("user")]
        public string User { get; set; }

        [JsonPropertyName("desc")]
        public string Desc { get; set; }

        [JsonPropertyName("tags")]
        public string Tags { get; set; }

        [JsonPropertyName("shared")]
        public string Shared { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonPropertyName("comments")]
        public List<object> Comments { get; set; }

        [JsonPropertyName("annotations")]
        public List<object> Annotations { get; set; }
    }
}
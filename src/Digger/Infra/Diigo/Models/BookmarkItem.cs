using Digger.Infra.Diigo.Helpers;
using Newtonsoft.Json;

namespace Digger.Infra.Diigo.Models
{
    [JsonObject]
    public class BookmarkItem
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }

        [JsonProperty("user")]
        public string? User { get; set; }

        [JsonProperty("desc")]
        public string? Desc { get; set; }

        [JsonProperty("tags")]
        public string? Tags { get; set; }

        [JsonProperty("shared")]
        [JsonConverter(typeof(CustomBooleanConverter))]
        public bool? Shared { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }

        [JsonProperty("annotations")]
        public List<Annotation> Annotations { get; set; }

        public BookmarkItem()
        {
            Comments = new List<Comment>();
            Annotations = new List<Annotation>();

        }
    }
}

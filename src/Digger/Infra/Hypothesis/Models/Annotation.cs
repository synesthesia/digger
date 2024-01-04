using Newtonsoft.Json;
namespace Digger.Infra.Hypothesis.Models
{
    // see https://h.readthedocs.io/en/latest/api-reference/v1/#tag/annotations/paths/~1annotations~1{id}/get
    public class Annotation
    {
               
        [JsonProperty("consumer")]
        public string? Consumer { get; set; }

        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("user")]
        public string? User { get; set; }

        [JsonProperty("uri")]
        public string? Uri { get; set; }

        [JsonProperty("text")]
        public string? Text { get; set; }

        [JsonProperty("tags")]
        public List<string>? Tags { get; set; }

        [JsonProperty("group")]
        public string? Group { get; set; }

        [JsonProperty("permissions")]
        public object? Permissions { get; set; }

        [JsonProperty("target")]
        public List<Target>? Target { get; set; }

        [JsonProperty("document")]
        public Document? Document { get; set; }

        [JsonProperty("links")]
        public object? Links { get; set; }

        [JsonProperty("hidden")]
        public bool? Hidden { get; set; }

        [JsonProperty("flagged")]
        public bool Flagged { get; set; }

        [JsonProperty("references")]
        public List<string>? References { get; set; }

        [JsonProperty("user_info")]
        public UserInfo? UserInfo { get; set; }


    }


}
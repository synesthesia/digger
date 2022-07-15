using Newtonsoft.Json;
using Digger.Infra.Diigo.Helpers;

namespace Digger.Infra.Diigo.Models
{
    [JsonObject]
    public class Annotation
    {
        /// <summary>
        /// Comment content
        /// </summary>
        [JsonProperty("content")]
        public string? Content {get; set;}


        [JsonProperty("comments")]
        public Comment[]? Comments { get; set; }

        /// <summary>
        /// Diigo User name of comment author
        /// </summary>
        [JsonProperty("user")]
        public string? User {get; set;}


        /// <summary>
        /// Date/Time when comment was created
        /// </summary>
        [JsonConverter(typeof(CustomDateTimeConverter))]
        [JsonProperty("created_at")]
        public DateTime Created {get; set;}

    }
}

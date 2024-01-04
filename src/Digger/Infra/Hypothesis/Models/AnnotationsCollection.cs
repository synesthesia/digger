using Newtonsoft.Json;

namespace Digger.Infra.Hypothesis.Models
{
    public class AnnotationsCollection
    {
        [JsonProperty("rows")]
        public List<Annotation>? Rows { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
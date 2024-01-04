using Newtonsoft.Json;
namespace Digger.Infra.Hypothesis.Models
{
    public class Selector
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("value")]
        public string? Value { get; set; }

        [JsonProperty("conformsTo")]
        public string? ConformsTo { get; set; }
    }


}

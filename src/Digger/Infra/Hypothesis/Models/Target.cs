using Newtonsoft.Json;
namespace Digger.Infra.Hypothesis.Models
{
    public class Target
    {
        [JsonProperty("source")]
        public string? Source { get; set; }

        [JsonProperty("selector")]
        public List<Selector>? Selector { get; set; }
    }


}

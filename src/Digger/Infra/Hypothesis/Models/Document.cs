using Newtonsoft.Json;
namespace Digger.Infra.Hypothesis.Models
{
    public class Document
    {
        [JsonProperty("title")]
        public List<string>? Title { get; set; }
    }


}

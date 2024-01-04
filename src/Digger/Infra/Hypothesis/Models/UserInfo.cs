using Newtonsoft.Json;
namespace Digger.Infra.Hypothesis.Models
{
    public class UserInfo
    {
        [JsonProperty("display_name")]
        public string? DisplayName { get; set; }
    }


}

using System.Net.Http;

namespace Digger.Infra.Hypothesis
{
    public class HypothesisClient : IHypothesisClient
    {
        private readonly HttpClient _httpClient;

        public HypothesisClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}

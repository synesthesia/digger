using System.Net.Http;
using Digger.Infra.Helpers;
using Digger.Infra.Hypothesis.Models;
using static Digger.Infra.Hypothesis.ApiEndPoints;
using Newtonsoft.Json;
using Digger.Infra.Hypothesis.Exceptions;


namespace Digger.Infra.Hypothesis
{
    public class HypothesisClient : IHypothesisClient
    {
        private readonly HttpClient _httpClient;

        public HypothesisClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AnnotationsCollection> SearchAnnotations(SearchParameters searchParameters)
        {
            string requestUri = QueryStringHelpers.AddQueryString(AnnotationApiUrls.Search, GetQueryParameters(searchParameters));
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                //var content = await response.Content.ReadFromJsonAsync<IEnumerable<BookmarkItem>>();
                var json = await response.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<AnnotationsCollection>(json);
                return content ?? new AnnotationsCollection();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new HypothesisClientException(response.StatusCode, message);
            }
        }

        private Dictionary<string, string> GetQueryParameters(object obj)
        {
            var queryStrings = new Dictionary<string, string>();
                       var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(obj, null);
                if (propertyValue == null)
                {
                    continue;
                }
                var stringValue = propertyValue.ToString()?.ToLower();
                if (stringValue != null)
                {
                    queryStrings.Add(property.Name.ToLower(), stringValue);
                }
            }

            return queryStrings;
        }
    }
}

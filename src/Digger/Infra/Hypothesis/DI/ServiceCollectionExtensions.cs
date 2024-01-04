using Ardalis.GuardClauses;
using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Digger.Infra.Hypothesis.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHypothesisClient(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var options = sp.GetRequiredService<IOptions<HypothesisOptions>>().Value;
            var apiToken = options.ApiToken;
            Guard.Against.Null(services, nameof(services));
            Guard.Against.NullOrEmpty(apiToken, nameof(apiToken));

            services.AddHttpClient<IHypothesisClient, HypothesisClient>((c,sp) =>
            {
                c.BaseAddress = new Uri(Constants.BASE_URL);
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
                return new HypothesisClient(c);
                
            });

            return services;
        }

    }
}
using Ardalis.GuardClauses;
using Digger.Infra.Diigo;
using Digger.Infra.Diigo.Configuration;
using System.Net;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDiigoClient(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var options = sp.GetRequiredService<IOptions<DiigoOptions>>().Value;
            var apiKey = options.ApiKey;
            var userName = options.UserName;
            var password = options.Password;

            Guard.Against.Null(services, nameof(services));
            Guard.Against.NullOrEmpty(apiKey, nameof(apiKey));
            Guard.Against.NullOrEmpty(userName, nameof(userName));
            Guard.Against.NullOrEmpty(password, nameof(password));

            services.AddHttpClient<IDiigoClient, DiigoClient>((c,sp) =>
            {
                c.BaseAddress = new Uri(Constants.BASE_URL);
                return new DiigoClient(c, apiKey);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler()
                {
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(userName, password),
                };
            });

            return services;
        }
    }
}

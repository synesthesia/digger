using Ardalis.GuardClauses;
using DiigoSharp.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDiigoClient(this IServiceCollection services, string apiKey, string userName, string password)
        {
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

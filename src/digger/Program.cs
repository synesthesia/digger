
using Digger.Infra.Diigo.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Digger
{
    internal class Program
    {
        public static async Task<int> Main(string[] args)
        {
            try
            {

                var builder =
                    Host.CreateDefaultBuilder(args)
                        .ConfigureLogging(logging =>
                        {
                            logging.ClearProviders();
                            logging.AddConsole();
                        });

                builder.ConfigureServices((ctx, services) =>
                {
                    var configurationRoot = ctx.Configuration;

                    services.Configure<DiigoOptions>(
                        configurationRoot.GetSection("Diigo"));

                    services.AddDiigoClient();

                    services.AddSingleton<IQueryBookmarks, BookmarksQuery>();
                });

                var app = builder.Build();

                var query = app.Services.GetRequiredService<IQueryBookmarks>();

                var result = await query.GetBookmarks();

                return 0;
            }
            catch (Exception ex)
            {
                var msg = $"Unhandled exception: {ex.Message}";
                Console.WriteLine(msg);
                return 1;
            }
        }
    }
}


using DiigoSharp.ApiClient.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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



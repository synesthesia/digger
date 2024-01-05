using Ardalis.GuardClauses;
using CommandLine;
using Digger.Infra.Diigo.Configuration;
using Digger.Infra.Files;
using Digger.Infra.Hypothesis.Configuration;
using Digger.Model.Params;
using Digger.Model.Verbs;
using Digger.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.IO.Abstractions;
using System.Reflection;

namespace Digger
{
    internal class Program
    {
        private static IHost? _app;
        private static ILogger<Program> _log = NullLogger<Program>.Instance;

        public static async Task<int> Main(string[] args)
        {
            try
            {

                var builder =
                    Host.CreateDefaultBuilder(args)
                        .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                        .ConfigureLogging((ctx, logging) =>
                        {
                            logging.AddConfiguration(ctx.Configuration.GetSection("Logging"));
                            logging.ClearProviders();
                            logging.AddConsole();
                        });


                builder.ConfigureServices((ctx, services) =>
                {
                    var configurationRoot = ctx.Configuration;

                    services.Configure<DiigoOptions>(
                        configurationRoot.GetSection("Diigo"));

                    services.Configure<HypothesisOptions>(
                        configurationRoot.GetSection("Hypothesis"));

                    services.UseDiigo();

                    services.UseHypothesis();

                    services.AddSingleton<IFileSystem, FileSystem>();

                    services.AddSingleton<IWriteFiles, BookmarkFileWriter>();
                });

                _app = builder.Build();

                _log = _app.Services.GetRequiredService<ILogger<Program>>();

                var parser = new Parser(with =>
                {
                    with.CaseSensitive = false;
                    with.HelpWriter = Console.Out;
                    with.IgnoreUnknownArguments = true;
                });

                // tell the parser what command line options to expect
                // map the given arguments to the correct action method
                return await parser
                    .ParseArguments<DiigoExportOptions, HypothesisExportOptions, object>(args)
                    .MapResult(
                        (DiigoExportOptions opts) => RunDiigoExportAndReturnExitCode(opts),
                        (HypothesisExportOptions opts) => RunHypothesisExportAndReturnExitCode(opts),
                        errors => Task.FromResult(1)
                    );
            }
            catch (Exception ex)
            {
                var msg = $"Unhandled exception: {ex.Message}";
                _log.LogCritical(msg);

                return 1;
            }
        }

        /// <summary>
        /// Runs the Diigo export and returns the exit code.
        /// </summary>
        /// <param name="options">The Diigo export options.</param>
        /// <returns>The exit code.</returns>
        private static async  Task<int> RunDiigoExportAndReturnExitCode(DiigoExportOptions options)
        {
            try
            {
                Guard.Against.Null(nameof(_app));

                #pragma warning disable CS8602 // We've just checked that _app is not null!
                var query = _app.Services.GetRequiredService<IQueryBookmarks>();
                #pragma warning restore CS8602

                var result = await query.GetBookmarks(new DiigoExportParams(options));

                return 0;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error in retrieving bookmarks from Diigo");

                return 1;
            }
        }

        /// <summary>
        /// Runs the hypothesis export and returns the exit code.
        /// </summary>
        /// <param name="opts">The hypothesis export options.</param>
        /// <returns>The exit code.</returns>
        private static async Task<int> RunHypothesisExportAndReturnExitCode(HypothesisExportOptions opts)
        {
             try
            {
                Guard.Against.Null(nameof(_app));

                #pragma warning disable CS8602 // We've just checked that _app is not null!
                var query = _app.Services.GetRequiredService<IQueryAnnotations>();
                #pragma warning restore CS8602

                var result = await query.SearchAnnotations(new HypothesisExportParams(opts));

                return 0;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error in retrieving annotations from Hypothesis");

                return 1;
            }
        }
    }
}

using Ardalis.GuardClauses;
using CommandLine;
using CommandLine.Text;
using Digger.Infra.Diigo;
using Digger.Infra.Diigo.Configuration;
using Digger.Infra.Files;
using Digger.Infra.Markdown;
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
        static IHost? _app;
        static ILogger<Program> _log = NullLogger<Program>.Instance;

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

                    services.AddDiigoClient();
                    services.AddSingleton<IFileSystem, FileSystem>();
                    services.AddSingleton<IWriteFiles, BookmarkFileWriter>();
                    services.AddSingleton<IQueryBookmarks, ExportDiigoBookmarks>();
                    services.AddSingleton<IMarkdownNoteConverter, DiigoMarkdownConverter>();
                });

                _app = builder.Build();

                _log = _app.Services.GetRequiredService<ILogger<Program>>();

                var verbs = LoadVerbs();
                //var parser = Parser.Default;
                //var parser = new CommandLine.Parser(with => with.HelpWriter = null);
                var parser = new Parser(with =>
                {
                    with.CaseSensitive = false;
                    with.HelpWriter = Console.Out;
                    with.IgnoreUnknownArguments = true;
                });

                return await parser
                    .ParseArguments<DiigoExportOptions, object>(args)
                    .MapResult(
                        (DiigoExportOptions opts) => RunDiigoExportAndReturnExitCode(opts),
                        errors => Task.FromResult(1)
                    );

                    /*
                return await parser.ParseArguments(args, verbs)
                    .WithParsed(Run)
                    .WithNotParsed(DisplayHelp);
                    */

            }
            catch (Exception ex)
            {
                var msg = $"Unhandled exception: {ex.Message}";
                _log.LogCritical(msg);
                return 1;
            }
        }

        //load all types using Reflection
        private	static Type[] LoadVerbs()
        {
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
        }

        private static async Task<int> Run(object obj)
        {
        switch (obj)
        {
            case DiigoExportOptions d:
                return await RunDiigoExportAndReturnExitCode(d);
            default:
                return 1;
        }
        }

        static async  Task<int> RunDiigoExportAndReturnExitCode(DiigoExportOptions options)
        {
            try
            {
                Guard.Against.Null(nameof(_app));

                var query = _app.Services.GetRequiredService<IQueryBookmarks>();

                var result = await query.GetBookmarks(new DiigoExportParams(options));

                return 0;
            }
            catch (Exception ex)
            {
                _log.LogError("Error in retrieving bookmarks from Diigo");
                return 1;
            }

        }

        /*
        static Task<int> DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs)
        {
            HelpText helpText = null;
            if (errs.IsVersion())  //check if error is version request
                helpText = HelpText.AutoBuild(result);
                return Task.FromResult(1);
            else
            {
                var name = Assembly.GetEntryAssembly()?.GetName()?.ToString();
                var version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString();
                helpText = HelpText.AutoBuild(result, h =>
                {
                    //configure help
                    h.AdditionalNewLineAfterOption = false;
                    h.Heading = $"{name ?? "Digger"} {version}";
                    h.Copyright = "Copyright (c) 2022 Julian Elve";
                    return HelpText.DefaultParsingErrorsHandler(result, h);
                },
                e => e,
                verbsIndex:true );
            }
            Console.WriteLine(helpText);
            return Task.FromResult(1);
        }
        */
    }
}

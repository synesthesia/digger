using Ardalis.GuardClauses;
using Digger.Infra.Hypothesis;
using Digger.Infra.Hypothesis.Configuration;
using Digger.Infra.Hypothesis.Models;
using Digger.Infra.Markdown;
using Digger.Model.Params;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Digger.Services
{
    public class ExportHypothesisAnnotations: IQueryAnnotations
    {
        private readonly IHypothesisClient _client;
        private readonly IHypothesisMarkdownConverter _mdConverter;
        private readonly HypothesisOptions _settings;
        private ILogger<ExportHypothesisAnnotations> _log;

        public ExportHypothesisAnnotations(
            IHypothesisClient client,
            IOptions<HypothesisOptions> opts,
            IHypothesisMarkdownConverter mdConverter,
            ILogger<ExportHypothesisAnnotations> log
            )
        {
            _ = Guard.Against.Null(nameof(client));
            _client = client;

            _ = Guard.Against.Null(nameof(opts));
            _settings = opts.Value;

            Guard.Against.Null(nameof(log));
            _log = log;

            Guard.Against.Null(nameof(mdConverter));
            _mdConverter = mdConverter;


        }

        public async Task<IEnumerable<string>> SearchAnnotations(HypothesisExportParams parameters)
        {
            if (parameters.HypothesisSearchParameters == null)
            {
                parameters.HypothesisSearchParameters = new SearchParameters
                {
                    Limit = 100,
                    Uri = parameters.Url
                };
            }

            var annotations = await _client.SearchAnnotations(parameters.HypothesisSearchParameters);

            _log.LogInformation("Retrieved {numAnnotations} annotations matching the filter", annotations.Total);

            var mdTexts = _mdConverter.ConvertAnnotationCollection(annotations);
            //var title = bmk.Title ?? DateTime.UtcNow.ToString();

            return mdTexts;
        }
    }

}

using Ardalis.GuardClauses;
using Digger.Infra.Diigo;
using Digger.Infra.Diigo.Models;
using Digger.Infra.Diigo.Configuration;
using Digger.Infra.Files;
using Digger.Infra.Markdown;
using Digger.Model.Params;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Digger.Services
{
    public interface IQueryBookmarks
    {
        public Task<BookmarksCollection> GetBookmarks(DiigoExportParams parameters);

    }
    public class ExportDiigoBookmarks : IQueryBookmarks
    {
        private DiigoOptions _settings;
        private readonly IDiigoClient _client;
        private ILogger<ExportDiigoBookmarks> _log;
        private IDiigoMarkdownNoteConverter _mdConverter;
        private readonly IWriteFiles _writer;

        public ExportDiigoBookmarks(
            IDiigoClient client,
            IDiigoMarkdownNoteConverter mdConverter,
            IOptions<DiigoOptions> opts,
            IWriteFiles writer,
            ILogger<ExportDiigoBookmarks> log)
        {

            Guard.Against.Null(nameof(client));
            _client = client;
            Guard.Against.Null(nameof(opts));
            _settings = opts.Value;
            Guard.Against.Null(mdConverter);
            _mdConverter = mdConverter;
            Guard.Against.Null(writer);
            _writer = writer;
            Guard.Against.Null(nameof(log));
            _log = log;
        }

        public async Task<BookmarksCollection> GetBookmarks(DiigoExportParams parameters)
        {
            if (parameters.DiigoSearchParameters == null)
            {
                Guard.Against.NullOrEmpty(_settings.UserName, "username");
                parameters.DiigoSearchParameters = new SearchParameters
                {
                    User = _settings.UserName,
                    Count = 100,
                    Filter = Visibility.All,
                    Tags = new TagCollection(new[] { "#toprocess" })

                };
            }
            parameters.DiigoSearchParameters.User = _settings.UserName;

            var result = await _client.GetBookmarks(parameters.DiigoSearchParameters);

            _log.LogInformation("Retrieved {numBookmarks} bookmarks matching the filter", result.Count);

            var i = 0;
            var n = result.Bookmarks.Count();

            foreach (var bmk in result.Bookmarks)
            {
                bmk.RemoveTag(parameters.InputTag);
                var mdText = _mdConverter.ConvertBookmark(bmk);
                var title = bmk.Title ?? DateTime.UtcNow.ToString();
                await _writer.WriteFileSlugified(parameters.OutputPath, title, mdText);
                var saveResult = await MarkBookmarkAsProcessed(bmk, parameters.InputTag, parameters.OutputTag);
                i++;
            }

            return result;

        }
        private async Task<SaveBookmarkResponse?> MarkBookmarkAsProcessed(BookmarkItem bmk, string inputTag, string outputTag)
        {
            bmk.RemoveTag(inputTag);
            bmk.AddTag(outputTag);

            var toUpdate = new Bookmark
            {
                Title = bmk.Title,
                Url = bmk.Url,
                Desc = bmk.Desc,
                Tags = bmk.Tags,
                Shared = bmk.Shared != null ? (bool)bmk.Shared : false,
                ReadLater = false,
                Merge = false
            };

            var saveResult = await _client.SaveBookmark(toUpdate);

            if (saveResult is not null)
            {
                _log.LogInformation("Marked bookmark as processed in Diigo - {url}", bmk.Url);

            }

            return saveResult;
        }
    }
}

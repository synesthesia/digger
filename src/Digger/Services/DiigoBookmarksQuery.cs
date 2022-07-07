using Ardalis.GuardClauses;
using Digger.Infra.Diigo;
using Digger.Infra.Diigo.Models;
using Digger.Infra.Diigo.Configuration;
using Digger.Infra.Markdown;
using Digger.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public interface IQueryBookmarks
{
    public Task<BookmarksCollection> GetBookmarks(JobParameters parameters);

}
public class DiigoBookmarksQuery : IQueryBookmarks
{
    private DiigoOptions _settings;
    private readonly IDiigoClient _client;

    private ILogger<DiigoBookmarksQuery> _log;
    private IMarkdownNoteConverter _mdConverter;

    public DiigoBookmarksQuery(IDiigoClient client, IMarkdownNoteConverter mdConverter, IOptions<DiigoOptions> opts, ILogger<DiigoBookmarksQuery> log)
    {

        Guard.Against.Null(nameof(client));
        _client = client;
        Guard.Against.Null(nameof(opts));
        _settings = opts.Value;
        Guard.Against.Null(mdConverter);
        _mdConverter = mdConverter;
        Guard.Against.Null(nameof(log));
        _log = log;
    }

    public async Task<BookmarksCollection> GetBookmarks(JobParameters parameters)
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

        var result = await _client.GetBookmarks(parameters.DiigoSearchParameters);

        _log.LogInformation("Retrieved {numBookmarks} bookmarks", result.Count);

        var i = 0;
        var path = parameters.OutputPath;
        Directory.CreateDirectory(path);
        foreach (var bmk in result.Bookmarks)
        {
            var mdText = _mdConverter.ConvertBookmark(bmk);
            var title = $"{Slugify(bmk.Title)}.md";
            await File.WriteAllTextAsync($"{path}/{title}", mdText);
            i++;

        }

        return result;

    }

    private string Slugify(string src, int maxLength = 20)
    {
        var slug = src.Replace(' ', '-').ToLower().Substring(0, maxLength);
        /*
        foreach (char c in System.IO.Path.GetInvalidFileNameChars())
        {
            slug = slug.Replace(c, '_');
        }
        */
        //var illegals = string.Concat(System.IO.Path.GetInvalidFileNameChars()) ;
        //var test = $"[{illegals}]|[-]{2}";
        /*
        var test = Regex.Escape(@"[:;,.]");
        var pattern = new Regex(test);
        pattern.Replace(slug, "-");
        */
        slug = slug.Replace(':', '-').Replace(';', '-').Replace('\\', '-').Replace('/', '-');
        slug = slug.TrimEnd('-');
        return slug;
    }
}

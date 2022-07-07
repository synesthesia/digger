using Ardalis.GuardClauses;
using Digger.Infra.Diigo;
using Digger.Infra.Diigo.Models;
using Digger.Infra.Diigo.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Digger.Model;

public interface IQueryBookmarks
{
    public Task<BookmarksCollection> GetBookmarks(JobParameters parameters);

}
public class  DiigoBookmarksQuery : IQueryBookmarks
{
    private DiigoOptions _settings;
    private readonly IDiigoClient _client;

    private ILogger<DiigoBookmarksQuery> _log;

    public DiigoBookmarksQuery(IDiigoClient client, IOptions<DiigoOptions> opts, ILogger<DiigoBookmarksQuery> log)
    {

        Guard.Against.Null(nameof(client));
        _client = client;
        Guard.Against.Null(nameof(opts));
        _settings = opts.Value;
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
                Tags = new TagCollection(new [] {"#toprocess"} )

            };
        }

        var result = await _client.GetBookmarks(parameters.DiigoSearchParameters);

        _log.LogInformation("Retrieved {numBookmarks} bookmarks", result.Count);

        return result;

    }
}

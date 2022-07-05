using Ardalis.GuardClauses;
using DiigoSharp.ApiClient;
using DiigoSharp.ApiClient.Models;
using DiigoSharp.ApiClient.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public interface IQueryBookmarks
{
    public Task<BookmarksCollection> GetBookmarks(SearchParameters? parameters = null);

}
public class BookmarksQuery : IQueryBookmarks
{
    private DiigoOptions _settings;
    private readonly IDiigoClient _client;

    private ILogger<BookmarksQuery> _log;

    public BookmarksQuery(IDiigoClient client, IOptions<DiigoOptions> opts, ILogger<BookmarksQuery> log)
    {

        Guard.Against.Null(nameof(client));
        _client = client;
        Guard.Against.Null(nameof(opts));
        _settings = opts.Value;
        Guard.Against.Null(nameof(log));
        _log = log;
    }

    public async Task<BookmarksCollection> GetBookmarks(SearchParameters? parameters = null)
    {
        if (parameters == null)
        {
            Guard.Against.NullOrEmpty(_settings.UserName, "username");
            parameters = new SearchParameters
            {
                User = _settings.UserName,
                Count = 100,
                Filter = Visibility.All
            };
        }

        var result = await _client.GetBookmarks(parameters);

        _log.LogInformation("Retrieved {numBookmarks} bookmarks", result.Count);

        return result;

    }
}

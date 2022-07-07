using Digger.Infra.Diigo.Models;

namespace Digger.Infra.Diigo
{
    public interface IDiigoClient
    {
        public Task<BookmarksCollection> GetBookmarks(SearchParameters parameters);

        public Task<SaveBookmarkResponse?> SaveBookmark(Bookmark bookmark);
    }
}

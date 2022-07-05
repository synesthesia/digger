namespace DiigoSharp.ApiClient.Models
{
    public class BookmarksCollection
    {
        public IEnumerable<BookmarkItem> Bookmarks { get; set; }

        public long Count { get; set; }
    }
}
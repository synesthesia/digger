namespace Digger.Infra.Diigo.Models
{
    public class BookmarksCollection
    {
        public BookmarksCollection()
        {
            Bookmarks = new List<BookmarkItem>();
            Count = 0;
        }

        public IEnumerable<BookmarkItem> Bookmarks { get; set; }

        public long Count { get; set; }


    }
}

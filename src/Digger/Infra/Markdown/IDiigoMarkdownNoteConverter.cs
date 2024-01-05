using Digger.Infra.Diigo.Models;

namespace Digger.Infra.Markdown
{
    public interface IDiigoMarkdownNoteConverter
    {
        string ConvertBookmark(BookmarkItem bookmark);
    }
}

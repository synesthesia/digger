using Digger.Infra.Diigo.Models;

namespace Digger.Infra.Markdown
{
    public interface IMarkdownNoteConverter
    {
        string ConvertBookmark(BookmarkItem bookmark);
    }
}

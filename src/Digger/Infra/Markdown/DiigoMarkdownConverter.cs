using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digger.Infra.Diigo.Models;
using Grynwald.MarkdownGenerator;
using Microsoft.Extensions.Logging;
using Html2Markdown;

namespace Digger.Infra.Markdown
{
    public class DiigoMarkdownConverter : IMarkdownNoteConverter
    {
        private readonly ILogger<DiigoMarkdownConverter> logger;

        public DiigoMarkdownConverter(ILogger<DiigoMarkdownConverter> logger)
        {
            this.logger = logger;
        }
        public string ConvertBookmark(BookmarkItem bookmark)
        {
            var document = new MdDocument();

            document.Root.Add(new MdHeading($"{bookmark.Title}", 1));

            document.Root.Add(new MdHeading("Summary", 2));

            if (!string.IsNullOrEmpty(bookmark.Url))
            {
                document.Root.Add(new MdParagraph(new MdLinkSpan("Link", ((string)bookmark.Url))));
            }

            //document.Root.Add(new MdParagraph(new MdLinkSpan("Archive Link", "")));

            document.Root.Add(new MdParagraph($"Note captured: {DateTime.UtcNow.ToShortDateString()}"));

            if (bookmark.CreatedAt != null)
            {
                document.Root.Add(new MdParagraph($"Bookmark created: {((DateTime)(bookmark.CreatedAt)).ToShortDateString()}"));
            }

            if (bookmark.Annotations.Any())
            {
                document.Root.Add(new MdHeading("Notes", 2));

                foreach (var ann in bookmark.Annotations)
                {
                    // annotations can contain HTML
                    string mdContent = HtmlStringToMarkdown(ann.Content);
                    document.Root.Add(new MdBlockQuote(new MdRawMarkdownSpan(mdContent)));

                    foreach (var c in ann.Comments)
                    {
                        document.Root.Add(new MdParagraph(c.Content));
                    }

                }
            }

            var mdOptions = new MdSerializationOptions();
            mdOptions.BulletListStyle = MdBulletListStyle.Dash;

            var result = document.ToString(mdOptions);

            return result;
        }

        private string HtmlStringToMarkdown(string content)
        {

            var converter = new Converter();
            var markdown = converter.Convert(content);
            return markdown;
        }
    }
}

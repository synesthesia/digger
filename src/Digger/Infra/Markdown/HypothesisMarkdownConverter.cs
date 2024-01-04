using Grynwald.MarkdownGenerator;
using Digger.Infra.Hypothesis.Models;
using Html2Markdown;
using Microsoft.Extensions.Logging;


namespace Digger.Infra.Markdown
{
    public class HypothesisMarkdownConverter : IHypothesisMarkdownConverter
    {
        private readonly ILogger<HypothesisMarkdownConverter> _logger;

        public HypothesisMarkdownConverter(ILogger<HypothesisMarkdownConverter> logger)
        {
            _logger = logger;
        }

        public IEnumerable<string> ConvertAnnotationCollection(AnnotationsCollection annotations)
        {
            var result = new List<string>();

            if (annotations.Rows == null || !annotations.Rows.Any())
            {
                return result;
            }

            var pageGroups = annotations.Rows.GroupBy(a => a.Uri);

            foreach(var sourcePageGroup in pageGroups)
            {
                var document = new MdDocument();

                var title = GenerateTitle(sourcePageGroup);

                document.Root.Add(new MdHeading($"{title}", 1));

                document.Root.Add(new MdHeading("Summary", 2));

                if (!string.IsNullOrEmpty(sourcePageGroup.Key))
                {
                    document.Root.Add(new MdParagraph(new MdLinkSpan("Source Link", ((string)sourcePageGroup.Key))));
                }

                document.Root.Add(new MdParagraph($"Annotations captured: {DateTime.UtcNow.ToShortDateString()}"));

                document.Root.Add(new MdHeading("See also", 2));

                document.Root.Add(new MdParagraph("*TODO add references*"));

                if (sourcePageGroup.Any(a => a.Text != null))
                {
                    document.Root.Add(new MdHeading("Highlights from source page", 2));

                    foreach (var annotation in sourcePageGroup.Where(a => a.Text != null))
                    {
                        // annotations can contain HTML
                        if (annotation.Text != null)
                        {
                            string mdContent = HtmlStringToMarkdown(annotation.Text);
                            document.Root.Add(new MdBlockQuote(new MdRawMarkdownSpan(mdContent)));
                        }
                        if (annotation.Tags == null)
                        {
                            continue;
                        }
                        foreach (var t in annotation.Tags)
                        {
                            document.Root.Add(new MdParagraph($"*{t}*"));
                        }
                    }
                }

                var mdText = document.ToString();

                result.Add(mdText);
            }
            return result;

        }

        private string GenerateTitle(IGrouping<string?, Annotation> sourcePageGroup)
        {

            var title = sourcePageGroup.Key ?? DateTime.UtcNow.ToString();

            if (sourcePageGroup.Any(a => a.Text != null))
            {
                var firstAnnotation = sourcePageGroup.First(a => a.Text != null);
                if (firstAnnotation.Document != null)
                {
                    title = firstAnnotation.Document.Title?.FirstOrDefault() ?? title;
                }
            }

            return title;

        }


        private string HtmlStringToMarkdown(string content)
        {

            var converter = new Converter();
            var markdown = converter.Convert(content);
            return markdown;
        }
    }
}

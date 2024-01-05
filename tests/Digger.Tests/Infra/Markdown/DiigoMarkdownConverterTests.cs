using Digger.Infra.Diigo.Models;
using Digger.Infra.Markdown;
using Grynwald.MarkdownGenerator;

namespace Digger.Tests.Infra.Markdown
{
    public class DiigoMarkdownConverterTests
    {
        ITestOutputHelper _output;
        IDiigoMarkdownNoteConverter _sut;

        ILogger<DiigoMarkdownConverter> _logger;

        public DiigoMarkdownConverterTests(ITestOutputHelper output)
        {
            _output = output;
            _logger = _output.ToLogger<DiigoMarkdownConverter>();
            _sut = new DiigoMarkdownConverter(_logger);
        }

        [Theory]
        [AutoData]
        public void DiigoMarkdownConverter_happy_path(BookmarkItem bookMark)
        {

            /*
                we want to escape the title in our final document
                so we need to compare an escaped version of our random input
                title with the result
            */
            #pragma warning disable CS8604
            var expectedTitle = new MdTextSpan(bookMark.Title).ToString();
            #pragma warning restore CS8604

            var result = _sut.ConvertBookmark(bookMark);

            result.Should().Contain($"# {expectedTitle}");
        }


        [Fact]
        public void DiigoMarkdownConverter_handles_html_with_link_in_annotations()
        {
            var fixture = new Fixture();

            var sampleAnnotation = fixture
                .Build<Annotation>()
                .With(p => p.Content, @"Text with <a href=""https://nowhere.com"">link</a>")
                .Create();

            var expectedAnnotation = "> Text with [link](https://nowhere.com)";

            var bookmark = fixture
                .Build<BookmarkItem>()
                .With(p => p.Annotations, new List<Annotation> { sampleAnnotation })
                .Create();

            var result = _sut.ConvertBookmark(bookmark);

            result.Should().Contain(expectedAnnotation);
        }

        [Fact]
        public void DiigoMarkdownConverter_handles_html_with_ul_in_annotations()
        {
            var fixture = new Fixture();

            var inputAnnotation = @"<ul><li>Item 1</li><li>Item 2</li></ul>";

            var sampleAnnotation = fixture
                .Build<Annotation>()
                .With(p => p.Content, inputAnnotation)
                .Create();

            var bookmark = fixture
                .Build<BookmarkItem>()
                .With(p => p.Annotations, new List<Annotation> { sampleAnnotation })
                .Create();

            var result = _sut.ConvertBookmark(bookmark);

            // we are stuck with * as list seperator
            // until we implement a custom IScheme
            // for Html2Markdown

            result.Should().Contain("> *   Item 1");
            result.Should().Contain("> *   Item 2");
        }
    }
}

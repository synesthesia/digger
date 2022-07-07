using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digger.Infra.Diigo.Models;
using Digger.Infra.Markdown;
using Grynwald.MarkdownGenerator;

namespace Digger.Tests.Infra.Markdown
{
    public class DiigoMarkdownConverterTests
    {
        ITestOutputHelper _output;
        IMarkdownNoteConverter _sut;

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
                so we need to compare an escaped version oif our random input
                title with the result
            */
            var expectedTitle = new MdTextSpan(bookMark.Title).ToString();

            var result = _sut.ConvertBookmark(bookMark);

            result.Should().Contain($"# {expectedTitle}");
        }


        [Fact]
        public void DiigoMarkdownConverter_handles_html_in_annotations()
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
    }
}

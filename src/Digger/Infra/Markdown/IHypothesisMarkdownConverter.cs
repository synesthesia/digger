using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digger.Infra.Hypothesis.Models;

namespace Digger.Infra.Markdown
{
    public interface IHypothesisMarkdownConverter
    {
        IEnumerable<MdTextResult> ConvertAnnotationCollection(AnnotationsCollection annotations);
    }
}

using Digger.Infra.Hypothesis.Models;
using Digger.Infra.Markdown;
using Digger.Model.Params;

namespace Digger.Services
{
    public interface IQueryAnnotations
    {
        Task<IEnumerable<MdTextResult>> SearchAnnotations(HypothesisExportParams hypothesisExportParams);
    }
}

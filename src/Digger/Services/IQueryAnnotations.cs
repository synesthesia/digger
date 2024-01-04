using Digger.Infra.Hypothesis.Models;
using Digger.Model.Params;

namespace Digger.Services
{
    public interface IQueryAnnotations
    {
        Task<IEnumerable<string>> SearchAnnotations(HypothesisExportParams hypothesisExportParams);
    }
}

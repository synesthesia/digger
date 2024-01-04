using Digger.Infra.Hypothesis.Models;
using Digger.Model.Params;

namespace Digger.Services
{
    public interface IQueryAnnotations
    {
        Task<AnnotationsCollection> SearchAnnotations(HypothesisExportParams hypothesisExportParams);
    }
}
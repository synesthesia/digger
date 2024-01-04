using Digger.Infra.Hypothesis.Models;

namespace Digger.Infra.Hypothesis
{
    public interface IHypothesisClient
    {
        Task<AnnotationsCollection> SearchAnnotations(SearchParameters searchParameters);
    }
}

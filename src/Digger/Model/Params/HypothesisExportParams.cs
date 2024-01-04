using Digger.Infra.Hypothesis.Models;
using Digger.Model.Verbs;

namespace  Digger.Model.Params
{


    public class HypothesisExportParams
    {

        public string OutputPath { get; set; }
        public string Url { get; set; }

        public SearchParameters? HypothesisSearchParameters { get; set; }

        public HypothesisExportParams()
        {
            OutputPath = "./output";
            Url = string.Empty;
            HypothesisSearchParameters = new SearchParameters
            {
                Limit = 20,
                Uri = string.Empty
            };


        }

        public HypothesisExportParams(HypothesisExportOptions commandOpts)
        {
            OutputPath = commandOpts.OutputDirectory ?? "./output";
            Url = commandOpts.Url;
            HypothesisSearchParameters = new SearchParameters
            {
                Limit = 20,
                Uri = commandOpts.Url
            };

        }

    }
}

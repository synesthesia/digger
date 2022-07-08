using Digger.Infra.Diigo.Models;
using Digger.Model.Verbs;

namespace Digger.Model.Params
{
    public class DiigoExportParams
    {

        // tag we serach for
        public string InputTag { get; set; }

        // tag we use to mark processed bookmarks
        public string OutputTag { get; set; }
        public SearchParameters? DiigoSearchParameters { get; set; }
        public string OutputPath { get; set; }

        public DiigoExportParams()
        {
            OutputPath = "./output";
            InputTag = "#toprocess";
            OutputTag = "#processed";
            DiigoSearchParameters = new SearchParameters
            {
                Count = 100,
                Filter = Visibility.All,
                Tags = new TagCollection(new[] { InputTag })

            };

        }

        public DiigoExportParams(DiigoExportOptions commandOpts)
        {
            InputTag = "#toprocess";
            OutputTag = "#processed";
            OutputPath = commandOpts.OutputDirectory ?? "./output";


            DiigoSearchParameters = new SearchParameters
            {
                Count = 100,
                Filter = Visibility.All,
                Tags = new TagCollection(new[] { InputTag })

            };

        }


    }
}

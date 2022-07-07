using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digger.Infra.Diigo.Models;

namespace Digger.Model
{
    public class JobParameters
    {

        // tag we serach for
        public string InputTag { get; set; }

        // tag we use to mark processed bookmarks
        public string OutputTag { get; set; }
        public SearchParameters? DiigoSearchParameters { get; set; }
        public string OutputPath { get; set; }

        public JobParameters()
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


    }
}

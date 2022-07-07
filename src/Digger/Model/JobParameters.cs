using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digger.Infra.Diigo.Models;

namespace Digger.Model
{
    public class JobParameters
    {
        public SearchParameters? DiigoSearchParameters { get; set; }
        public string OutputPath { get; set; }

        public JobParameters()
        {
            OutputPath = "./output";

        }


    }
}

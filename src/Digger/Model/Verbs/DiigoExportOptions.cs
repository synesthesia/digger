using System;
using CommandLine;
using Digger.Infra.Diigo.Models;

namespace Digger.Model.Verbs
{
    [Verb("diigo", isDefault: true, HelpText = "Extract notes from Diigo")]
    public class DiigoExportOptions
    {

        [Option('o', "output-directory", HelpText = "Relative path to receive notes, defaults to ./output. If directory is below current directory, must start './'")]
	    public string? OutputDirectory { get; set; }



    }
}

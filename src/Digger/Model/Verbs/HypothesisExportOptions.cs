using CommandLine;

namespace Digger.Model.Verbs
{

    [Verb("hypothesis", HelpText = "Extract notes from Hypothesis")]
    public class HypothesisExportOptions
    {
        [Option('u', "url", HelpText = "URL of the web page for which you want to extract Hypothes.is annotations")]
        public string Url { get; set; } = string.Empty; 
    
        [Option('o', "output-directory", HelpText = "Relative path to receive notes, defaults to ./output. If directory is below current directory, must start './'")]
        public string? OutputDirectory { get; set; }
    }
}
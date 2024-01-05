using CommandLine;

namespace Digger.Model.Verbs
{
        /// <summary>
    /// Configuration options for exporting data from Diigo, which define the command line arguments.
    /// </summary>
    [Verb("diigo", isDefault: true, HelpText = "Extract notes from Diigo")]
    public class DiigoExportOptions
    {
        [Option('o', "output-directory", HelpText = "Relative path to receive notes, defaults to ./output. If directory is below current directory, must start './'")]
	    public string? OutputDirectory { get; set; }
    }
}

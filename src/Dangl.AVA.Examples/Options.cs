using CommandLine;

namespace Dangl.AVA.Examples
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Relative or absolute path to a GAEB file")]
        public string InputFilePath { get; set; }

        [Option('o', "output", Required = true, HelpText = "Relative or absolute path to the output file")]
        public string OutputFilePath { get; set; }

        [Option('s', "stripPrices", Required = false, Default = false, HelpText = "Remove prices from the output")]
        public bool StripPrices { get; set; }

        [Option('t', "target", Required = true, HelpText = "The transformation target. Values: Gaeb90, Gaeb2000, GaebXml or Excel")]
        public Target Target { get; set; }

        [Option('p', "printPositions", Required = false, HelpText = "Print out all positions to the console")]
        public bool PrintPositions { get; set; }

        [Option('e', "excludeBranding", Required = false, Default = false, HelpText = "Exclude branding comments in serialized GAEB files")]
        public bool ExcludeBranding { get; set; }
    }
}

using CommandLine;

namespace Dangl.AVA.Examples
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Relative or absolute path to a GAEB file")]
        public string InputFilePath { get; set; }

        [Option('o', "output", Required = true, HelpText = "Relative or absolute path to the output file")]
        public string OutputFilePath { get; set; }

        [Option('k', "keepPrices", Required = false, Default = true, HelpText = "Keeps prices in the output")]
        public bool KeepPrices { get; set; }

        [Option('t', "target", Required = true, HelpText = "The transformation target")]
        public Target Target { get; set; }
    }
}

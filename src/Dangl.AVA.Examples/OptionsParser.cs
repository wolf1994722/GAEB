using CommandLine;

namespace Dangl.AVA.Examples
{
    public class OptionsParser
    {
        public OptionsParser(string[] commandLineArguments)
        {
            _commandLineArguments = commandLineArguments;
            ParseOptions();
        }

        private readonly string[] _commandLineArguments;
        public bool IsValid { get; private set; }
        public Options Result { get; private set; }

        private void ParseOptions()
        {
            var parsedOptions = Parser.Default.ParseArguments<Options>(_commandLineArguments);
            if (parsedOptions is Parsed<Options> parserResult)
            {
                IsValid = true;
                Result = parserResult.Value;
            }
        }
    }
}

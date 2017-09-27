using System;
using CommandLine.Text;

namespace Dangl.AVA.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionsParser = new OptionsParser(args);
            if (optionsParser.IsValid)
            {
                Console.WriteLine(HeadingInfo.Default);
                Console.WriteLine(CopyrightInfo.Default);
                try
                {
                    TransformGaeb(optionsParser.Result);
                    Console.WriteLine("Finished GAEB transformation");
                }
                catch (Exception e)
                {
                    DisplayExceptionDetails(e);
                }
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static void DisplayExceptionDetails(Exception e)
        {
            Console.Write(e.ToString());
            Console.WriteLine();
        }

        private static void TransformGaeb(Options options)
        {
            var transformator = new GaebTransformator(options);
            transformator.TransformGaeb();
        }
    }
}

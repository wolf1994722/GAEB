using System.IO;
using System.Linq;
using Dangl.GAEB;
using Dangl.GAEB.Reader;

namespace Dangl.AVA.Examples
{
    public class GaebTransformator
    {
        private readonly Options _options;

        public GaebTransformator(Options options)
        {
            _options = options;
        }

        private Project _readProject;

        public void TransformGaeb()
        {
            ReadInputFile();
            RemovePricesIfRequested();
            PrintPositionsIfRequested();
            TransformProject();
        }

        private void ReadInputFile()
        {
            if (_options.InputFilePath.ToUpperInvariant().EndsWith("xlsx".ToUpperInvariant()))
            {
                ReadInputExcelFile();
            }
            else
            {
                ReadInputGaebFile();
            }
        }

        private void ReadInputGaebFile()
        {
            var filePath = Path.GetFullPath(_options.InputFilePath);
            using (var fileStream = File.OpenRead(filePath))
            {
                var gaebFile = GAEBReader.ReadGaeb(fileStream);
                _readProject = Converter.Converter.ConvertFromGaeb(gaebFile);
            }
        }

        private void ReadInputExcelFile()
        {
            var filePath = Path.GetFullPath(_options.InputFilePath);
            using (var fileStream = File.OpenRead(filePath))
            {
                _readProject = Converter.Excel.Reader.ReadStream(fileStream);
            }
        }

        private void RemovePricesIfRequested()
        {
            if (_options.StripPrices)
            {
                var priceStripper = new PriceStripper(_readProject);
                priceStripper.StripPrices();
            }
        }

        private void PrintPositionsIfRequested()
        {
            if (_options.PrintPositions)
            {
                var positionsPrinter = new PositionsPrinter(_readProject);
                positionsPrinter.PrintAllPositionsToConsole();
            }
        }

        private void TransformProject()
        {
            if (_options.Target == Target.Excel)
            {
                TransformToExcel();
                return;
            }
            TransformToGaeb();
        }

        private void TransformToExcel()
        {
            using (var excelStream = Converter.Excel.Writer.GetStream(_readProject))
            {
                var outputPath = Path.GetFullPath(_options.OutputFilePath + ".xlsx");
                using (var fileStream = File.Create(outputPath))
                {
                    excelStream.CopyTo(fileStream);
                }
            }
        }

        private void TransformToGaeb()
        {
            var gaebVersion = MapOptionsToGaebDestination();
            var convertedProject = Converter.Converter.ConvertToGaeb(_readProject, destinationType: gaebVersion);
            using (var gaebStream = GAEB.Writer.GAEBWriter.GetStream(convertedProject, includeBrandingComment: !_options.ExcludeBranding))
            {
                var exchangePhase = _readProject.ServiceSpecifications.First().ExchangePhase;
                var fileEnding = GaebFileEndingFactory.GetFileEndingForGaebFile(convertedProject, exchangePhase);
                var outputPath = Path.GetFullPath(_options.OutputFilePath + fileEnding);
                using (var fileStream = File.Create(outputPath))
                {
                    gaebStream.CopyTo(fileStream);
                }
            }
        }

        private Converter.DestinationGAEBType MapOptionsToGaebDestination()
        {
            switch (_options.Target)
            {
                case Target.Gaeb90:
                    return Converter.DestinationGAEBType.GAEB90;
                case Target.Gaeb2000:
                    return Converter.DestinationGAEBType.GAEB2000;
                case Target.GaebXml:
                default:
                    return Converter.DestinationGAEBType.GAEBXML_V3_2;
            }
        }
    }
}

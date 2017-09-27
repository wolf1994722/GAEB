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
            var readGaeb = ReadInputGaebFile();
            _readProject = Converter.Converter.ConvertFromGaeb(readGaeb);
            RemovePricesIfRequested();
            TransformProject();
        }

        private GAEB_File ReadInputGaebFile()
        {
            var filePath = Path.GetFullPath(_options.InputFilePath);
            using (var fileStream = File.OpenRead(filePath))
            {
                var gaebFile = GAEBReader.ReadGaeb(fileStream);
                return gaebFile;
            }
        }

        private void RemovePricesIfRequested()
        {
            if (_options.KeepPrices)
            {
                return;
            }
            var priceStripper = new PriceStripper(_readProject);
            priceStripper.StripPrices();
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
            using (var gaebStream = GAEB.Writer.GAEBWriter.GetStream(convertedProject))
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

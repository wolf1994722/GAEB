using System.Collections.Generic;
using System.Linq;
using Dangl.AVA.Contents.ServiceSpecificationContents;

namespace Dangl.AVA.Examples
{
    public class PositionsPrinter
    {
        private readonly Project _project;

        public PositionsPrinter(Project project)
        {
            _project = project;
        }

        private int _padLength;

        public void PrintAllPositionsToConsole()
        {
            var positions = _project.ServiceSpecifications
                .First()
                .RecursiveElements()
                .OfType<Position>()
                .ToList();
            GetMaxLengthOfItemNumbers(positions);
            foreach (var position in positions)
            {
                PrintPosition(position);
            }
        }

        private void GetMaxLengthOfItemNumbers(IList<Position> positions)
        {
            var maxLength = positions
                .Max(p => p.ItemNumber.StringRepresentation.Length);
            _padLength = maxLength;
        }

        private void PrintPosition(Position position)
        {
            var itemNumber = position.ItemNumber.ToString().PadLeft(_padLength, ' ');
            var normalizedShortText = string.IsNullOrWhiteSpace(position.ShortText)
                ? "Unnamed Position"
                : System.Text.RegularExpressions.Regex.Replace(position.ShortText, "\r\n?|\n", string.Empty);
            System.Console.WriteLine($"{itemNumber} - {normalizedShortText}");
        }
    }
}

using System.Linq;
using Dangl.AVA.Contents;
using Dangl.AVA.Contents.ServiceSpecificationContents;

namespace Dangl.AVA.Examples
{
    public class PriceStripper
    {
        private readonly Project _project;

        public PriceStripper(Project project)
        {
            _project = project;
        }

        public void StripPrices()
        {
            foreach (var servSpec in _project.ServiceSpecifications)
            {
                RemoveGlobalPriceData(servSpec);
                var groups = servSpec.RecursiveElements().OfType<ServiceSpecificationGroup>();
                groups.ToList().ForEach(RemoveGroupDeductions);
                var positions = servSpec.RecursiveElements().OfType<Position>();
                positions.ToList().ForEach(RemovePricesFromPosition);
            }
        }

        private void RemoveGlobalPriceData(ServiceSpecification servSpec)
        {
            servSpec.PriceInformation.DeductionFactor = 0m;
            servSpec.PriceInformation.FlatSum = 0m;
            servSpec.PriceInformation.HourlyWage = 0m;
            servSpec.PriceInformation.TaxRate = 0m;
            servSpec.DeductionFactor = 0m;
        }

        private void RemoveGroupDeductions(ServiceSpecificationGroup group)
        {
            group.DeductionFactor = 0m;
        }

        private void RemovePricesFromPosition(Position position)
        {
            position.DeductionFactor = 0m;
            position.LabourComponents.HourlyWage = 0m;
            position.LabourComponents.UseOwnHourlyWage = false;
            position.LabourComponents.Values.Clear();
            foreach (var priceComponent in position.PriceComponents)
            {
                priceComponent.Values.Clear();
            }
            position.TaxRate = 0m;
            position.UseDifferentTaxRate = false;
        }
    }
}

using Dangl.AVA.Contents.ServiceSpecificationContents;
using Dangl.GAEB;
using Dangl.GAEB.GAEB2000;
using Dangl.GAEB.GAEB90;

namespace Dangl.AVA.Examples
{
    public static class GaebFileEndingFactory
    {
        public static string GetFileEndingForGaebFile(GAEB_File gaebFile, ExchangePhase exchangePhase)
        {
            var fileEnding = ".";
            if (gaebFile is GAEB_File_90)
            {
                fileEnding += "D";
            }
            if (gaebFile is GAEB_File_2000)
            {
                fileEnding += "P";
            }
            if (gaebFile is GAEB.GAEBXML.Schemas.V3_2.Y2013.tgGAEB)
            {
                fileEnding += "X";
            }
            fileEnding += GetExchangePhaseEnding(exchangePhase);
            return fileEnding;
        }

        private static string GetExchangePhaseEnding(ExchangePhase exchangePhase)
        {
            switch (exchangePhase)
            {
                case ExchangePhase.Base:
                    return "81";
                case ExchangePhase.CostEstimate:
                    return "82";
                case ExchangePhase.OfferRequest:
                    return "83";
                case ExchangePhase.Offer:
                    return "84";
                case ExchangePhase.SideOffer:
                    return "85";
                case ExchangePhase.Grant:
                case ExchangePhase.Undefined:
                    return "86";
                default:
                    return string.Empty;
            }
        }

    }
}

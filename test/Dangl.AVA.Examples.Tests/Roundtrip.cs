using Dangl.AVA.Converter;
using Dangl.GAEB.GAEB2000;
using Dangl.GAEB.GAEB90;
using Xunit;

namespace Dangl.AVA.Examples.Tests
{
    public class Roundtrip
    {
        [Fact]
        public void CanReadAsGaeb90AndTransformToGaeb2000()
        {
            using (var gaeb90Stream = TestFilesFactory.GetGaeb90Stream())
            {
                var gaeb90 = Dangl.GAEB.Reader.GAEBReader.ReadGaeb(gaeb90Stream);
                Assert.IsType<GAEB_File_90>(gaeb90);
                var project = Dangl.AVA.Converter.Converter.ConvertFromGaeb(gaeb90);
                var gaeb2000 = Dangl.AVA.Converter.Converter.ConvertToGaeb(project, destinationType: AVA.Converter.DestinationGAEBType.GAEB2000);
                Assert.IsType<GAEB_File_2000>(gaeb2000);
            }
        }

        [Fact]
        public void CanReadAsGaebXmlAndTransformToGaeb2000WithOfferExchangePhase()
        {
            using (var gaebXmlStream = TestFilesFactory.GetGaebXmlStream())
            {
                var gaebXml = Dangl.GAEB.Reader.GAEBReader.ReadGaeb(gaebXmlStream);
                Assert.IsType<Dangl.GAEB.GAEBXML.Schemas.V3_2.Y2013.tgGAEB>(gaebXml);
                var project = Dangl.AVA.Converter.Converter.ConvertFromGaeb(gaebXml);
                var gaeb2000 = Dangl.AVA.Converter.Converter.ConvertToGaeb(project, GAEBTargetExchangePhase.Offer, destinationType: AVA.Converter.DestinationGAEBType.GAEB2000);
                Assert.IsType<GAEB_File_2000>(gaeb2000);
                var gaeb2000ExchangePhase = (gaeb2000 as GAEB_File_2000).GAEB.Vergabe.DP.Value;
                Assert.Equal(GAEB.GAEB2000.GAEB.Elements.Vergabe.DP_Field.EnumerationItems._84, gaeb2000ExchangePhase);
            }
        }
    }
}

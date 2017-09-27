using System;
using System.Linq;
using Xunit;

namespace Dangl.AVA.Examples.Tests
{
    public class ReaderTests
    {
        [Fact]
        public void CanReadGaeb90()
        {
            using (var gaebStream = TestFilesFactory.GetGaeb90Stream())
            {
                var gaebFile = Dangl.GAEB.Reader.GAEBReader.ReadGaeb(gaebStream);
                Assert.NotNull(gaebFile);
            }
        }

        [Fact]
        public void CanReadGaeb2000()
        {
            using (var gaebStream = TestFilesFactory.GetGaeb2000Stream())
            {
                var gaebFile = Dangl.GAEB.Reader.GAEBReader.ReadGaeb(gaebStream);
                Assert.NotNull(gaebFile);
            }
        }

        [Fact]
        public void CanReadGaebXml()
        {
            using (var gaebStream = TestFilesFactory.GetGaebXmlStream())
            {
                var gaebFile = Dangl.GAEB.Reader.GAEBReader.ReadGaeb(gaebStream);
                Assert.NotNull(gaebFile);
            }
        }

        [Fact]
        public void CanReadAsGaebAndConvertToProject()
        {
            using (var gaebStream = TestFilesFactory.GetGaebXmlStream())
            {
                var gaebFile = Dangl.GAEB.Reader.GAEBReader.ReadGaeb(gaebStream);
                var convertedProject = Dangl.AVA.Converter.Converter.ConvertFromGaeb(gaebFile);
                Assert.NotNull(convertedProject);
                Assert.Equal(Contents.ServiceSpecificationContents.Origin.GaebXml, convertedProject.ServiceSpecifications.First().Origin);
            }
        }
    }
}

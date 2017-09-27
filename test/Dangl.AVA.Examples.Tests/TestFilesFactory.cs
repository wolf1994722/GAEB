using System.IO;

namespace Dangl.AVA.Examples.Tests
{
    public static class TestFilesFactory
    {
        public static Stream GetGaeb90Stream()
        {
            return GetResourceStreamByName("gaeb90.d83");
        }

        public static Stream GetGaeb2000Stream()
        {
            return GetResourceStreamByName("gaeb2000.p83");
        }

        public static Stream GetGaebXmlStream()
        {
            return GetResourceStreamByName("gaebxml.x83");
        }

        private static Stream GetResourceStreamByName(string filename)
        {
            var resourcePath = $"Dangl.AVA.Examples.Tests.Resources.{filename}";
            var resourceStream = typeof(TestFilesFactory).Assembly.GetManifestResourceStream(resourcePath);
            return resourceStream;
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.BusinessLayer.Builders;

namespace SD.UnitTest.BuildersTests
{
    [TestClass]
    public class DownloadBuilderTest
    {
        private DownloadBuilder builder = new DownloadBuilder();

        [TestMethod]
        public void GetFileTest()
        {
            var file = builder.GetFile();

            Assert.IsNotNull(file);
        }
    }
}

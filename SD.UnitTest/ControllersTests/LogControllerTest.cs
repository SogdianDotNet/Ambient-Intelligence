using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD_Ambience.Controllers;
using System.Web.Mvc;

namespace SD.UnitTest.ControllersTests
{
    [TestClass]
    public class LogControllerTest
    {
        private LogController controller = new LogController();

        [TestMethod]
        public void ListViewTest()
        {
            var result = (ViewResult)controller.ListView() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DetailsTest()
        {
            Guid id = Guid.Parse("");

            var result = (ViewResult)controller.Details(id) as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}

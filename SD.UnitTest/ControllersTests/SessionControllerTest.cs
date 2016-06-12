using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD_Ambience.Controllers;
using System.Web.Mvc;

namespace SD.UnitTest.ControllersTests
{
    [TestClass]
    public class SessionControllerTest
    {
        private SessionController controller = new SessionController();

        [TestMethod]
        public void ListViewTest()
        {
            string email = "//////";

            var result = (ViewResult)controller.ListView(email) as ViewResult;

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

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD_Ambience.Controllers;
using System.Web.Mvc;
using SD.Commons.Shared;

namespace SD.UnitTest.ControllersTests
{
    [TestClass]
    public class UserControllerTest
    {
        private UserController controller = new UserController();

        [TestMethod]
        public void ListViewTest()
        {
            string email = "///////";

            var result = (ViewResult)controller.ListView() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD_Ambience.Controllers;
using System.Web.Mvc;

namespace SD.UnitTest.ControllersTests
{
    [TestClass]
    public class TeacherControllerTest
    {
        private TeacherController controller = new TeacherController();

        [TestMethod]
        public void DetailsTest()
        {
            string email = "/////////";

            var result = (ViewResult)controller.Details(email) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteTest()
        {
            string email = "";

            var result = (ViewResult)controller.Delete(email) as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.BusinessLayer.API_Builders;

namespace SD.UnitTest.APIBuilderTests
{
    [TestClass]
    public class LoginAPIBuilderTest
    {
        private LoginAPIBuilder builder = new LoginAPIBuilder();

        [TestMethod]
        public void LoginTest()
        {
            string username = "";
            int pincode = 2002;

            bool isLoggedIn = builder.Login(username, pincode);

            Assert.IsTrue(isLoggedIn);
        }
    }
}

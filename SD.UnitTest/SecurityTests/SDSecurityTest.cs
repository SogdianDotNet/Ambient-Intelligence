using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Security.Login;

namespace SD.UnitTest.SecurityTests
{
    [TestClass]
    public class SDSecurityTest
    {
        private SDSecurity sdSecurity = new SDSecurity();

        [TestMethod]
        public void LoginTest()
        {
            string username = "////";
            string password = "//////";

            bool isLoggedIn = sdSecurity.Login(username, password);

            Assert.IsTrue(isLoggedIn);
        }
    }
}

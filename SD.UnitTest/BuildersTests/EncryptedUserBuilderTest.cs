using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.BusinessLayer.Builders;

namespace SD.UnitTest.BuildersTests
{
    [TestClass]
    public class EncryptedUserBuilderTest
    {
        private EncryptedUserBuilder builder = new EncryptedUserBuilder();

        [TestMethod]
        public void DeleteTest()
        {
            string userId = "44477e94-5037-48c5-a451-aca1de25389f";
            bool isDeleted = builder.Delete(userId);

            Assert.IsTrue(isDeleted);
        }
    }
}

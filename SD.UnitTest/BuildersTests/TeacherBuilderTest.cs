using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.BusinessLayer.Builders;

namespace SD.UnitTest.BuildersTests
{
    [TestClass]
    public class TeacherBuilderTest
    {
        private TeacherBuilder builder = new TeacherBuilder();

        [TestMethod]
        public void GetByIdTest()
        {
            string teacherEmail = "//////";
            var teacher = builder.GetById(teacherEmail);

            Assert.AreEqual(teacherEmail, teacher.Email);
        }

        [TestMethod]
        public void DeleteTest()
        {
            string teacherEmail = "/////////";
            var teacher = builder.GetById(teacherEmail);

            bool isDeleted = builder.Delete(teacher.Id);

            Assert.IsTrue(isDeleted);
        }
    }
}

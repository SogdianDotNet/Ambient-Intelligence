using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.BusinessLayer.Builders;
using SD.Commons.Shared.Models;
using System.Collections.Generic;

namespace SD.UnitTest.BuildersTests
{
    [TestClass]
    public class SessionBuilderTest
    {
        private SessionBuilder builder = new SessionBuilder();

        [TestMethod]
        public void GetListTest()
        {
            string teacherEmail = "///////";
            IEnumerable<SessionModel> list = builder.GetList(teacherEmail);

            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void GetSessionDataTest()
        {
            string teacherEmail = "//////";
            string course = "INFSEN01-2";
            string klas = "INF3C";
            SessionModel model = builder.GetSessionData(teacherEmail, course, klas);

            //Assert.AreEqual(teacherEmail, model.TeacherEmail);
            //Assert.AreEqual(course, model.Course);
            Assert.IsNotNull(model);
        }
    }
}

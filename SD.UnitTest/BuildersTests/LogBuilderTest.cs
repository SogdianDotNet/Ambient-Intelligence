using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.BusinessLayer.Builders;
using System.Collections;
using System.Collections.Generic;
using SD.Commons.Shared.Models;

namespace SD.UnitTest.BuildersTests
{
    [TestClass]
    public class LogBuilderTest
    {
        private LogBuilder builder = new LogBuilder();

        [TestMethod]
        public void GetListTest()
        {
            IEnumerable<LogModel> list = builder.GetList();

            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Guid id = Guid.Parse("08F42124-ED9D-4C8F-9176-4F7CBAE4F648");

            var log = builder.GetById(id);

            Assert.AreEqual(id, log.Id);
        }
    }
}

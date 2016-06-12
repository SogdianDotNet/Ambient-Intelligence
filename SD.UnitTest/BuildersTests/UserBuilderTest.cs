using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.BusinessLayer.Builders;
using SD.Commons.Shared;
using System.Collections;
using SD.Commons.Shared.Models;
using System.Collections.Generic;

namespace SD.UnitTest.BuildersTests
{
    [TestClass]
    public class UserBuilderTest
    {
        private UserBuilder builder = new UserBuilder();

        [TestMethod]
        public void GetListTest()
        {
            //arrange


            //act
            IEnumerable<UserViewModel> users = builder.GetList(UserRoles.Teacher);

            //assert
            Assert.IsNotNull(users);
        }

        [TestMethod]
        public void GetById()
        {
            string id = "0fc71edb-d529-4a87-af0f-b706497fd299";

            UserViewModel user = builder.GetById(id);

            Assert.AreEqual(id, user.UserId);
        }

        [TestMethod]
        public void GetApplicationUserByIdTest()
        {
            string id = "0fc71edb-d529-4a87-af0f-b706497fd299";

            var user = builder.GetApplicationUserById(id);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void GetApplicationUserByEmail()
        {
            string email = "///////";

            var user = builder.GetApplicationUserByEmail(email);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void InsertTest()
        {
            RegisterViewModel model = new RegisterViewModel()
            {
                Email = "///////",
                Password = "testtest",
                ConfirmPassword = "testtest",
                Firstname = "tester",
                Lastname = "testertester",
                PhoneNumber = "0643434343"
            };

            string userId = "";

            bool check = builder.Insert(model, userId);

            Assert.IsTrue(check);
        }

        [TestMethod]
        public void IsTeacherTest()
        {
            string email = "///////";

            bool check = builder.IsTeacher(email);

            Assert.IsTrue(check);
        }

        [TestMethod]
        public void IsAdministrator()
        {
            string email = "///////";

            bool check = builder.IsAdministrator(email);

            Assert.IsTrue(check);
        }


    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apps.Business;
using Apps.Entity;

namespace Apps.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FindByCodeUserNotExist()
        {
            BUser business = new BUser();
            EUser user = business.FindByCodeUser("xxx");
            Assert.AreEqual(user, null);
        }

        [TestMethod]
        public void InsertUser()
        {
            BUser business = new BUser();
            EUser user = new EUser();

            user.CodeUser = "motero";
            user.Name = "Miguel Angel";
            user.Password = "123456";
            user.Email = "miguel-otero@hotmail.com";
            user.Profile = "adm";
            user.State = 1;
            business.Insert(user);
            Assert.AreEqual(user, user);
        }

        [TestMethod]
        public void DeletetUser()
        {
            BUser business = new BUser();
            EUser user = new EUser();
            user.CodeUser = "motero";
            business.Delete(user);
            Assert.AreEqual(user, user);
        }

        [TestMethod]
        public void UpdateUser()
        {
            BUser business = new BUser();
            EUser user = new EUser();

            user.CodeUser = "motero";
            user.Name = "Miguel Angel";
            user.Password = "123456";
            user.Email = "miguel-otero@hotmail.com";
            user.Profile = "adm";
            user.State = 1;
            business.Update(user);
            Assert.AreEqual(user, user);
        }
    }
}

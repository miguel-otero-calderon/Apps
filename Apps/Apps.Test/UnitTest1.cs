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
        public void AppsBusinessFindByCodeUserNotExist()
        {
            BUser business = new BUser();
            EUser user = business.FindByCodeUser("xxx");
            Assert.AreEqual(user, null);
        }

        [TestMethod]
        public void AppsBusinessInsertUser()
        {
            BUser business = new BUser();
            EUser user = new EUser();
            user.CodeUser = "motero";
            user.Name = "Miguel Angel";
            user.Password = "123456";
            user.Estado = 1;
            try
            {
                business.Insert(user);
                Assert.AreEqual(user, user);
            }
            catch
            {
                Assert.AreEqual(user, null);
            }
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apps.Business;
using Apps.Entity;
using System.Transactions;

namespace Apps.Test
{
    [TestClass]
    public class TUser
    {  
        [TestMethod]
        public void Login()
        {
            bool result;
            short routes = 0;
            BUser b = new BUser();
            BAudit b2 = new BAudit();
            EUser user = new EUser();
            user.CodeUser = "motero";
            user.Name = "Miguel Angel";
            user.Password = "123456";
            user.Email = "miguel-otero@hotmail.com";
            user.Profile = "adm";
            user.State = 1;
            user.Audit.UserRegister = "uni test";

            TransactionScope ts = new TransactionScope();

            b.Delete(user);

            result = b.Login(user);
            if (result == false
                && b.Message.Contains("usuario")
                && b.Message.Contains("no existe"))
                routes++;

            user.State = 0;
            b.Insert(user);
            result = b.Login(user);
            if (result == false
                && b.Message.Contains("El usuario")
                && b.Message.Contains("no se encuentra 'Activo'."))
                routes++;

            user.State = 1;
            b.Update(user);
            result = b.Login(user);
            if (result == true && b.Message =="Ok")
                routes++;

            b.Delete(user);
            user.Password = "123456";
            b.Insert(user);
            user.Password = "password";
            result = b.Login(user);
            if (result == false && b.Message == "Password incorrecto.")
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 4);
        }

        [TestMethod]
        public void Select()
        {
            short routes = 0;
            BUser b = new BUser();

            if (b.Select(new EUser { CodeUser = "abc" }) == null)
                routes++;

            if (b.Select(new EUser { CodeUser = "motero" }) != null)
                routes++;

            Assert.AreEqual(routes, 2);
        }

        [TestMethod]
        public void Insert()
        {
            short routes = 0;
            BUser b = new BUser();
            BAudit b2 = new BAudit();
            EUser user = new EUser();
            user.CodeUser = "motero";
            user.Name = "Miguel Angel";
            user.Password = "123456";
            user.Email = "miguel-otero@hotmail.com";
            user.Profile = "adm";
            user.State = 1;
            user.Audit.UserRegister = "uni test";           

            TransactionScope ts = new TransactionScope();

            if (b.Select(user) != null)
                b.Delete(user);

            if (b.Select(user) == null)
                routes++;

            b.Insert(user);

            EUser insert = b.Select(user);

            if (insert != null)
                routes++;

            string hash = b.CalculateHash(user);
            if (insert.Password == hash)
                routes++;

            user.Audit = b2.Select(user.Audit)[0];
            if (user.Audit.UserRegister == "uni test"
                && user.Audit.TypeEvent.ToLower() == "insert")
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 4);
        }

        [TestMethod]
        public void Delete()
        {
            short routes = 0;
            BUser b = new BUser();
            BAudit b2 = new BAudit();
            EUser user = new EUser();
            user.CodeUser = "motero";
            user.Audit.UserRegister = "uni test";

            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);

            if (b.Select(user) != null)
            {
                b.Delete(user);
                routes++;                
            }

            if (b.Select(user) == null)
                routes++;

            user.Audit = b2.Select(user.Audit)[0];
            if (user.Audit.UserRegister == "uni test"
                && user.Audit.TypeEvent.ToLower() == "delete")
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 3);
        }

        [TestMethod]
        public void Update()
        {
            short routes = 0;
            BUser b = new BUser();
            BAudit b2 = new BAudit();
            EUser user = new EUser();
            user.CodeUser = "motero";
            user.Name = "Miguel Angel";
            user.Password = "123456";
            user.Email = "miguel-otero@hotmail.com";
            user.Profile = "adm";
            user.State = 1;
            user.Audit.UserRegister = "uni test";

            TransactionScope ts = new TransactionScope();

            b.Delete(user);
            b.Insert(user);

            EUser original = b.Select(user);
            if (original != null)
                routes++;

            original.Name = "Update Name";
            original.Email = "Update Email";
            original.Audit.UserRegister = "uni test";

            b.Update(original);

            EUser update = b.Select(original);

            if (update != null)
                routes++;

            if (original.Password == update.Password)
                routes++;

            if (update.Name == "Update Name" && update.Email == "Update Email")
                routes++;

            update.Audit = b2.Select(update.Audit)[0];
            if (update.Audit.UserRegister == "uni test"
                && update.Audit.TypeEvent.ToLower() == "update")
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 5);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apps.Business;
using Apps.Entity;
using Apps.Util;
using System.Transactions;
using System.Linq;
using System.Collections.Generic;

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
            BAudit bAudit = new BAudit();
            BUser bUser = new BUser();            
            EUser eUser = new EUser();
            eUser.CodeUser = Aleatory.GetString(8);
            eUser.Name = Aleatory.GetString(8);
            eUser.Password = Aleatory.GetString(8);
            eUser.Email = Aleatory.GetString(15);
            eUser.State = Aleatory.GetShort();
            eUser.Audit.UserRegister = Aleatory.GetString(8);

            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);

            bUser.Delete(eUser);

            result = bUser.Login(eUser);
            if (result == false
                && bUser.Message.Contains("usuario")
                && bUser.Message.Contains("no existe"))
                routes++;

            eUser.State = 0;
            bUser.Insert(eUser);
            result = bUser.Login(eUser);
            if (result == false
                && bUser.Message.Contains("El usuario")
                && bUser.Message.Contains("no se encuentra 'Activo'."))
                routes++;

            eUser.State = 1;
            bUser.Update(eUser);
            result = bUser.Login(eUser);
            if (result == true && bUser.Message =="Ok")
                routes++;

            bUser.Delete(eUser);
            eUser.Password = "123456";
            bUser.Insert(eUser);
            eUser.Password = "1234567890";
            result = bUser.Login(eUser);
            if (result == false && bUser.Message == "Password incorrecto.")
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 4);
        }

        [TestMethod]
        public void Select()
        {
            bool result = false;
            BUser bUser = new BUser();
            EUser eUser = new EUser();
            EUser selectedUser = null;
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);
            eUser.CodeUser = Aleatory.GetString(8);
            eUser.Name = Aleatory.GetString(8);
            eUser.Password = Aleatory.GetString(8);
            eUser.Email = Aleatory.GetString(15);
            eUser.State = Aleatory.GetShort();
            eUser.Audit.UserRegister = Aleatory.GetString(8);

            selectedUser = bUser.Select(eUser);
            if (selectedUser == null)
            {
                bUser.Insert(eUser);
                selectedUser = bUser.Select(eUser);
            }

            if (selectedUser != null
                && selectedUser.CodeUser == eUser.CodeUser
                && selectedUser.Name == eUser.Name
                && selectedUser.State == eUser.State)
                result = true;

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void Insert()
        {
            short routes = 0;            
            BAudit bAudit = new BAudit();
            EAudit eAudit = null;
            BUser bUser = new BUser();
            EUser eUser = new EUser();
            EUser insertedUser = null;
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);
            eUser.CodeUser = Aleatory.GetString(8);
            eUser.Name = Aleatory.GetString(8);
            eUser.Password = Aleatory.GetString(8);
            eUser.Email = Aleatory.GetString(15);
            eUser.State = Aleatory.GetShort();
            eUser.Audit.UserRegister = Aleatory.GetString(8);            

            if (bUser.Select(eUser) != null)
                bUser.Delete(eUser);

            if (bUser.Select(eUser) == null)
                routes++;

            bUser.Insert(eUser);

            insertedUser = bUser.Select(eUser);

            if (insertedUser != null)
                routes++;

            string hash = bUser.CalculateHash(eUser);
            if (insertedUser.Password == hash)
                routes++;

            eAudit = bAudit.Select(eUser.Audit).Where(x => x.UserRegister == eUser.Audit.UserRegister && x.TypeEvent == "Insert").FirstOrDefault();
            if (eAudit != null)
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 4);
        }

        [TestMethod]
        public void Delete()
        {
            short routes = 0;
            BAudit bAudit = new BAudit();
            EAudit eAudit = null;
            BUser bUser = new BUser();
            EUser eUser = new EUser();
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);
            eUser.CodeUser = Aleatory.GetString(8);
            eUser.Name = Aleatory.GetString(8);
            eUser.Password = Aleatory.GetString(8);
            eUser.Email = Aleatory.GetString(15);
            eUser.State = Aleatory.GetShort();
            eUser.Audit.UserRegister = Aleatory.GetString(8);

            bUser.Insert(eUser);
            bUser.Delete(eUser);

            if (bUser.Select(eUser) == null)
                routes++;

            eAudit = bAudit.Select(eUser.Audit).Where(x=>x.UserRegister == eUser.Audit.UserRegister && x.TypeEvent == "Delete").FirstOrDefault();
            if (eAudit != null)
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 2);
        }

        [TestMethod]
        public void Update()
        {
            short routes = 0;
            BAudit bAudit = new BAudit();
            EAudit eAudit = null;
            BUser bUser = new BUser();
            EUser eUser = new EUser();
            EUser insertedUser = null;
            EUser updatedUser = null;
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);
            eUser.CodeUser = Aleatory.GetString(8);
            eUser.Name = Aleatory.GetString(8);
            eUser.Password = Aleatory.GetString(8);
            eUser.Email = Aleatory.GetString(15);
            eUser.State = Aleatory.GetShort();
            eUser.Audit.UserRegister = Aleatory.GetString(8);

            bUser.Insert(eUser);

            insertedUser = bUser.Select(eUser);
            if (insertedUser != null)
                routes++;

            insertedUser.Name = Aleatory.GetString(8);
            insertedUser.Email = Aleatory.GetString(15);
            insertedUser.State = Aleatory.GetShort();
            insertedUser.Audit.UserRegister = Aleatory.GetString(8);

            bUser.Update(insertedUser);

            updatedUser = bUser.Select(insertedUser);

            if (updatedUser != null
                && updatedUser.Name != eUser.Name 
                && updatedUser.Email != eUser.Email
                && updatedUser.State != eUser.State
                && updatedUser.CodeUser == eUser.CodeUser)
                routes++;

            eAudit = bAudit.Select(insertedUser.Audit).Where(x=>x.UserRegister == insertedUser.Audit.UserRegister && x.TypeEvent == "Update").FirstOrDefault();
            if (eAudit != null)
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 3);
        }
    }
}

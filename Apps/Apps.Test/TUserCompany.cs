﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apps.Business;
using Apps.Entity;
using System.Transactions;
using Apps.Util;

namespace Apps.Test
{
    [TestClass]
    public class TUserCompany
    {
        [TestMethod]
        public void Select()
        {
            short routes = 0;
            BUserCompany b = new BUserCompany();
            
            if (b.Select(new EUserCompany { CodeUser="abc", CodeCompany = "abc" }) == null)
                routes++;

            if (b.Select(new EUserCompany { CodeUser = "motero", CodeCompany = "xx" }) != null)
                routes++;

            Assert.AreEqual(routes, 2);
        }

        [TestMethod]
        public void Insert()
        {
            short routes = 0;
            BUserCompany b = new BUserCompany();
            BAudit b2 = new BAudit();
            EUserCompany usercompany = new EUserCompany();
            usercompany.CodeUser = "motero";
            usercompany.CodeCompany = "xx";

            TransactionScope ts = new TransactionScope();

            if (b.Select(usercompany) != null)
                b.Delete(usercompany);

            if (b.Select(usercompany) == null)
                routes++;

            b.Insert(usercompany);

            EUserCompany insert = b.Select(usercompany);

            if (insert != null)
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 2);
        }

        [TestMethod]
        public void Delete()
        {            
            EUser eUser = new EUser();
            eUser.CodeUser = Aleatory.GetString(4);
            eUser.Name = Aleatory.GetString(8);

            

            ECompany eCompany = new ECompany();
            eCompany.CodeCompany = Aleatory.GetString(2);
            eCompany.LongName = Aleatory.GetString(8);

        }

        [TestMethod]
        public void SelectByUser()
        {
            short routes = 0;
            BUserCompany b = new BUserCompany();

            if (b.SelectByUser(new EUser { CodeUser = "abc"}).Count == 0)
                routes++;

            if (b.SelectByUser(new EUser{ CodeUser = "motero" }).Count > 0)
                routes++;

            Assert.AreEqual(routes, 2);
        }

        [TestMethod]
        public void DeleteByUser()
        {
            bool result = false;
            BUserCompany b = new BUserCompany();

            TransactionScope ts = new TransactionScope();

            b.DeleteByUser(new EUser { CodeUser = "abc" });

            b.DeleteByUser(new EUser { CodeUser = "motero" });

            if (b.SelectByUser(new EUser { CodeUser = "motero" }).Count == 0)
                result = true;

            ts.Dispose();

            Assert.IsTrue(result);
        }
    }
}
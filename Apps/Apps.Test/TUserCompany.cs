using System;
using Apps.Util;
using Apps.Business;
using Apps.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System.Linq;
using System.Collections.Generic;

namespace Apps.Test
{
    [TestClass]
    public class TUserCompany
    {
        [TestMethod]
        public void Select()
        {
            bool result = false;
            BUserCompany bUserCompany = new BUserCompany();
            EUserCompany eUserCompany = new EUserCompany();
            BCorporation bCorporation = new BCorporation();
            ECorporation eCorporation = new ECorporation();
            ECorporation insertedCorporation = new ECorporation();
            BCompany bCompany = new BCompany();
            ECompany eCompany = new ECompany();
            BUser bUser = new BUser();
            EUser eUser = new EUser();
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);

            eCorporation.CodeCorporation = Aleatory.GetString(2);
            eCorporation.Name = Aleatory.GetString(15);
            eCorporation.State = Aleatory.GetShort();
            eCorporation.Audit.UserRegister = Aleatory.GetString(8);
            bCorporation.Insert(eCorporation);

            insertedCorporation = bCorporation.Select(eCorporation);

            eCompany.CodeCompany = Aleatory.GetString(2);
            eCompany.CodeCorporation = insertedCorporation.CodeCorporation;
            eCompany.LongName = Aleatory.GetString(15);
            eCompany.State = Aleatory.GetShort();
            eCompany.Audit.UserRegister = Aleatory.GetString(8);
            bCompany.Insert(eCompany);

            eUser.CodeUser = Aleatory.GetString(8);
            eUser.Name = Aleatory.GetString(8);
            eUser.State = Aleatory.GetShort();
            eUser.Audit.UserRegister = Aleatory.GetString(8);
            bUser.Insert(eUser);            

            eUserCompany.CodeUser = eUser.CodeUser;
            eUserCompany.CodeCompany = eCompany.CodeCompany;
            bUserCompany.Insert(eUserCompany);

            eUserCompany = bUserCompany.Select(eUserCompany);

            if (eUserCompany != null
                && eUserCompany.CodeUser == eUser.CodeUser
                && eUserCompany.CodeCompany == eCompany.CodeCompany)
                result = true;

            ts.Dispose();

            Assert.IsTrue(result);            
        }

        [TestMethod]
        public void Insert()
        {
            bool result = false;
            BUserCompany bUserCompany = new BUserCompany();
            EUserCompany eUserCompany = new EUserCompany();
            EUserCompany insertedUserCompany = new EUserCompany();
            BCorporation bCorporation = new BCorporation();
            ECorporation eCorporation = new ECorporation();
            BCompany bCompany = new BCompany();
            ECompany eCompany = new ECompany();
            BUser bUser = new BUser();
            EUser eUser = new EUser();
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);

            eCorporation.CodeCorporation = Aleatory.GetString(2);
            eCorporation.Name = Aleatory.GetString(15);
            eCorporation.State = Aleatory.GetShort();
            eCorporation.Audit.UserRegister = Aleatory.GetString(8);
            bCorporation.Insert(eCorporation);

            eCompany.CodeCompany = Aleatory.GetString(2);
            eCompany.CodeCorporation = eCorporation.CodeCorporation;
            eCompany.LongName = Aleatory.GetString(15);
            eCompany.State = Aleatory.GetShort();
            eCompany.Audit.UserRegister = Aleatory.GetString(8);
            bCompany.Insert(eCompany);

            eUser.CodeUser = Aleatory.GetString(8);
            eUser.Name = Aleatory.GetString(8);
            eUser.State = Aleatory.GetShort();
            eUser.Audit.UserRegister = Aleatory.GetString(8);
            bUser.Insert(eUser);

            eUserCompany.CodeUser = eUser.CodeUser;
            eUserCompany.CodeCompany = eCompany.CodeCompany;
            bUserCompany.Insert(eUserCompany);

            insertedUserCompany = bUserCompany.Select(eUserCompany);

            if (insertedUserCompany != null
                && insertedUserCompany.CodeUser == eUserCompany.CodeUser
                && insertedUserCompany.CodeCompany == eCompany.CodeCompany)
                result = true;

            ts.Dispose();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Delete()
        {
            bool result = false;
            BUserCompany bUserCompany = new BUserCompany();
            EUserCompany eUserCompany = new EUserCompany();
            EUserCompany insertedUserCompany = new EUserCompany();
            EUserCompany deletedUserCompany = new EUserCompany();
            BCorporation bCorporation = new BCorporation();
            ECorporation eCorporation = new ECorporation();
            BCompany bCompany = new BCompany();
            ECompany eCompany = new ECompany();
            BUser bUser = new BUser();
            EUser eUser = new EUser();
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);

            eCorporation.CodeCorporation = Aleatory.GetString(2);
            eCorporation.Name = Aleatory.GetString(15);
            eCorporation.State = Aleatory.GetShort();
            eCorporation.Audit.UserRegister = Aleatory.GetString(8);
            bCorporation.Insert(eCorporation);

            eCompany.CodeCompany = Aleatory.GetString(2);
            eCompany.CodeCorporation = eCorporation.CodeCorporation;
            eCompany.LongName = Aleatory.GetString(15);
            eCompany.State = Aleatory.GetShort();
            eCompany.Audit.UserRegister = Aleatory.GetString(8);
            bCompany.Insert(eCompany);

            eUser.CodeUser = Aleatory.GetString(8);
            eUser.Name = Aleatory.GetString(8);
            eUser.State = Aleatory.GetShort();
            eUser.Audit.UserRegister = Aleatory.GetString(8);
            bUser.Insert(eUser);

            eUserCompany.CodeUser = eUser.CodeUser;
            eUserCompany.CodeCompany = eCompany.CodeCompany;
            bUserCompany.Insert(eUserCompany);

            insertedUserCompany = bUserCompany.Select(eUserCompany);

            if (insertedUserCompany != null)
                bUserCompany.Delete(eUserCompany);

            deletedUserCompany = bUserCompany.Select(eUserCompany);

            if (deletedUserCompany == null)
                result = true;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SelectByUser()
        {
            bool result = false;
            List<ECompany> listECompanies = new List<ECompany>();
            BUserCompany bUserCompany = new BUserCompany();
            EUserCompany eUserCompany = new EUserCompany();
            EUserCompany insertedOneUserCompany = new EUserCompany();
            EUserCompany insertedTwoUserCompany = new EUserCompany();
            BCorporation bCorporation = new BCorporation();
            ECorporation eCorporation = new ECorporation();
            BCompany bCompany = new BCompany();
            ECompany eCompany = new ECompany();
            BUser bUser = new BUser();
            EUser eUser = new EUser();
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);

            eCorporation.CodeCorporation = Aleatory.GetString(2);
            eCorporation.Name = Aleatory.GetString(15);
            eCorporation.State = Aleatory.GetShort();
            eCorporation.Audit.UserRegister = Aleatory.GetString(8);
            bCorporation.Insert(eCorporation);

            eCompany.CodeCompany = Aleatory.GetString(2);
            eCompany.CodeCorporation = eCorporation.CodeCorporation;
            eCompany.LongName = Aleatory.GetString(15);
            eCompany.State = Aleatory.GetShort();
            eCompany.Audit.UserRegister = Aleatory.GetString(8);
            bCompany.Insert(eCompany);

            eUser.CodeUser = Aleatory.GetString(8);
            eUser.Name = Aleatory.GetString(8);
            eUser.State = Aleatory.GetShort();
            eUser.Audit.UserRegister = Aleatory.GetString(8);
            bUser.Insert(eUser);

            eUserCompany.CodeUser = eUser.CodeUser;
            eUserCompany.CodeCompany = eCompany.CodeCompany;
            bUserCompany.Insert(eUserCompany);

            insertedOneUserCompany = bUserCompany.Select(eUserCompany);

            eCompany.CodeCompany = Aleatory.GetString(2);
            eCompany.CodeCorporation = eCorporation.CodeCorporation;
            eCompany.LongName = Aleatory.GetString(15);
            eCompany.State = Aleatory.GetShort();
            eCompany.Audit.UserRegister = Aleatory.GetString(8);
            bCompany.Insert(eCompany);

            eUserCompany.CodeUser = eUser.CodeUser;
            eUserCompany.CodeCompany = eCompany.CodeCompany;
            bUserCompany.Insert(eUserCompany);

            insertedTwoUserCompany = bUserCompany.Select(eUserCompany);

            listECompanies = bUserCompany.SelectByUser(eUser);

            if (listECompanies.Count == 2
                && listECompanies.Exists(x=>x.CodeCompany == insertedOneUserCompany.CodeCompany)
                && listECompanies.Exists(x=>x.CodeCompany == insertedTwoUserCompany.CodeCompany))
                result = true;

            ts.Dispose();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteByUser()
        {
            bool result = false;
            List<ECompany> listECompanies = new List<ECompany>();
            BUserCompany bUserCompany = new BUserCompany();
            EUserCompany eUserCompany = new EUserCompany();
            EUserCompany insertedOneUserCompany = new EUserCompany();
            EUserCompany insertedTwoUserCompany = new EUserCompany();
            BCorporation bCorporation = new BCorporation();
            ECorporation eCorporation = new ECorporation();
            BCompany bCompany = new BCompany();
            ECompany eCompany = new ECompany();
            BUser bUser = new BUser();
            EUser eUser = new EUser();
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);

            eCorporation.CodeCorporation = Aleatory.GetString(2);
            eCorporation.Name = Aleatory.GetString(15);
            eCorporation.State = Aleatory.GetShort();
            eCorporation.Audit.UserRegister = Aleatory.GetString(8);
            bCorporation.Insert(eCorporation);

            eCompany.CodeCompany = Aleatory.GetString(2);
            eCompany.CodeCorporation = eCorporation.CodeCorporation;
            eCompany.LongName = Aleatory.GetString(15);
            eCompany.State = Aleatory.GetShort();
            eCompany.Audit.UserRegister = Aleatory.GetString(8);
            bCompany.Insert(eCompany);

            eUser.CodeUser = Aleatory.GetString(8);
            eUser.Name = Aleatory.GetString(8);
            eUser.State = Aleatory.GetShort();
            eUser.Audit.UserRegister = Aleatory.GetString(8);
            bUser.Insert(eUser);

            eUserCompany.CodeUser = eUser.CodeUser;
            eUserCompany.CodeCompany = eCompany.CodeCompany;
            bUserCompany.Insert(eUserCompany);

            insertedOneUserCompany = bUserCompany.Select(eUserCompany);

            eCompany.CodeCompany = Aleatory.GetString(2);
            eCompany.CodeCorporation = eCorporation.CodeCorporation;
            eCompany.LongName = Aleatory.GetString(15);
            eCompany.State = Aleatory.GetShort();
            eCompany.Audit.UserRegister = Aleatory.GetString(8);
            bCompany.Insert(eCompany);

            eUserCompany.CodeUser = eUser.CodeUser;
            eUserCompany.CodeCompany = eCompany.CodeCompany;
            bUserCompany.Insert(eUserCompany);

            insertedTwoUserCompany = bUserCompany.Select(eUserCompany);

            listECompanies = bUserCompany.SelectByUser(eUser);

            if (listECompanies.Count == 2
                && listECompanies.Exists(x => x.CodeCompany == insertedOneUserCompany.CodeCompany)
                && listECompanies.Exists(x => x.CodeCompany == insertedTwoUserCompany.CodeCompany))
                bUserCompany.DeleteByUser(eUser);

            listECompanies = bUserCompany.SelectByUser(eUser);

            if (listECompanies.Count == 0)
                result = true;

            Assert.IsTrue(result);
        }
    }
}

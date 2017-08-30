using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apps.Business;
using Apps.Entity;
using Apps.Util;
using System.Transactions;

namespace Apps.Test
{
    [TestClass]
    public class TCompany
    {
        [TestMethod]
        public void Select()
        {
            short routes = 0;
            BCompany bCompany = new BCompany();
            ECompany eCompany = new ECompany();
            BCorporation bCorporation = new BCorporation();
            ECorporation eCorporation = new ECorporation();
            ECompany selectCompany = new ECompany();
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);
            
            eCompany.CodeCompany = Aleatory.GetString(2);
            eCompany.LongName = Aleatory.GetString(8);
            eCompany.ShortName = Aleatory.GetString(8);
            eCompany.State = 1;
            eCompany.Audit.UserRegister = Aleatory.GetString(8);

            selectCompany = bCompany.Select(eCompany);

            if (selectCompany == null)
                routes++;

            eCorporation.CodeCorporation = Aleatory.GetString(2);
            eCorporation.Name = Aleatory.GetString(8);
            eCorporation.State = 1;
            eCorporation.Audit.UserRegister = "uni test";
            bCorporation.Insert(eCorporation);

            eCompany.CodeCorporation = eCorporation.CodeCorporation;
            bCompany.Insert(eCompany);

            selectCompany = bCompany.Select(eCompany);

            if (selectCompany != null
                && selectCompany.LongName == eCompany.LongName
                && selectCompany.ShortName == eCompany.ShortName)
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 2);
        }

        [TestMethod]
        public void Delete()
        {
            short routes = 0;
            BAudit bAudit = new BAudit();
            EAudit eAudit = null;
            BCompany bCompany = new BCompany();
            ECompany eCompany = new ECompany();
            BCorporation bCorporation = new BCorporation();
            ECorporation eCorporation = new ECorporation();
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);

            eCorporation.CodeCorporation = Aleatory.GetString(2);
            eCorporation.Name = Aleatory.GetString(8);
            eCorporation.State = 1;
            eCorporation.Audit.UserRegister = Aleatory.GetString(8);
            bCorporation.Insert(eCorporation);

            eCompany.CodeCompany = Aleatory.GetString(2);
            eCompany.CodeCorporation = eCorporation.CodeCorporation;
            eCompany.LongName = Aleatory.GetString(8);
            eCompany.State = Aleatory.GetShort();
            eCompany.Audit.UserRegister = Aleatory.GetString(8);
            bCompany.Insert(eCompany);

            if (bCompany.Select(eCompany) != null)
                routes++;

            bCompany.Delete(eCompany);

            if (bCompany.Select(eCompany) == null)
                routes++;

            eAudit = bAudit.Select(eCompany.Audit).Where(x => x.UserRegister == eCompany.Audit.UserRegister && x.TypeEvent == "Delete").FirstOrDefault();
            if (eAudit != null)
                routes++;

            ts.Dispose();           

            Assert.AreEqual(routes, 3);
        }

        [TestMethod]
        public void Insert()
        {
            short routes = 0;
            BAudit bAudit = new BAudit();
            EAudit eAudit = null;
            BCompany bCompany = new BCompany();
            ECompany eCompany = new ECompany();
            ECompany insertedCompany = new ECompany();
            BCorporation bCorporation = new BCorporation();
            ECorporation eCorporation = new ECorporation();
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);

            eCorporation.CodeCorporation = Aleatory.GetString(2);
            eCorporation.Name = Aleatory.GetString(8);
            eCorporation.State = Aleatory.GetShort();
            eCorporation.Audit.UserRegister = Aleatory.GetString(8);
            bCorporation.Insert(eCorporation);

            eCompany.CodeCompany = Aleatory.GetString(2);
            eCompany.LongName = Aleatory.GetString(8);
            eCompany.State = Aleatory.GetShort();
            eCompany.Audit.UserRegister = Aleatory.GetString(8);
            bCompany.Insert(eCompany);

            insertedCompany = bCompany.Select(eCompany);
            if (insertedCompany != null
                && insertedCompany.CodeCompany == eCompany.CodeCompany
                && insertedCompany.LongName == eCompany.LongName
                && insertedCompany.State == eCompany.State)
                routes++;

            eAudit = bAudit.Select(eCompany.Audit).Where(x => x.UserRegister == eCompany.Audit.UserRegister && x.TypeEvent == "Insert").FirstOrDefault();
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
            BCompany bCompany = new BCompany();
            ECompany eCompany = new ECompany();
            ECompany updatedCompany = new ECompany();
            BCorporation bCorporation = new BCorporation();
            ECorporation eCorporation = new ECorporation();
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);

            eCorporation.CodeCorporation = Aleatory.GetString(2);
            eCorporation.Name = Aleatory.GetString(8);
            eCorporation.State = Aleatory.GetShort();
            eCorporation.Audit.UserRegister = Aleatory.GetString(8);
            bCorporation.Insert(eCorporation);

            eCompany.CodeCompany = Aleatory.GetString(2);
            eCompany.CodeCorporation = eCorporation.CodeCorporation;
            eCompany.LongName = Aleatory.GetString(8);
            eCompany.State = Aleatory.GetShort();
            eCompany.Audit.UserRegister = Aleatory.GetString(8);
            bCompany.Insert(eCompany);

            eCompany.LongName = Aleatory.GetString(8);
            eCompany.State = Aleatory.GetShort();
            eCompany.Audit.UserRegister = Aleatory.GetString(8);
            bCompany.Update(eCompany);

            updatedCompany = bCompany.Select(eCompany);

            if (updatedCompany != null
                && updatedCompany.CodeCompany == eCompany.CodeCompany
                && updatedCompany.CodeCorporation == eCompany.CodeCorporation
                && updatedCompany.LongName != eCompany.LongName
                && updatedCompany.State == eCompany.State)
                routes++;

            eAudit = bAudit.Select(eCompany.Audit).Where(x=>x.UserRegister == eCompany.Audit.UserRegister && x.TypeEvent == "Update").FirstOrDefault();

            if (eAudit != null)
                routes++;

            ts.Dispose();

        }
    }
}

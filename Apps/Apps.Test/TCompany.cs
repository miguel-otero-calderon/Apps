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
            ECorporation eCorporation = new ECorporation();
            BCorporation bCorporation = new BCorporation();
            TransactionScope ts = new TransactionScope();

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

            if (bCompany.Select(eCompany) != null)
                routes++;

            bCompany.Delete(eCompany);

            if (bCompany.Select(eCompany) == null)
                routes++;

            eAudit = bAudit.Select(eCompany.Audit)[0];
            if (eAudit != null
                && eAudit.CodeCompany == eCompany.Audit.CodeCompany
                && eAudit.CodeEntity == eCompany.Audit.CodeEntity
                && eAudit.Code == eCompany.Audit.Code
                && eAudit.UserRegister == eCompany.Audit.UserRegister
                && eAudit.TypeEvent == "Delete")
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 3);
        }

        [TestMethod]
        public void Insert()
        {
            short routes = 0;
            BCompany b = new BCompany();
            BAudit b2 = new BAudit();
            ECompany company = new ECompany();
            company.CodeCompany = "xx";
            company.CodeCorporation = "01";
            company.LongName = "Test LongName";
            company.ShortName = "Test ShortName";
            company.Ruc = "Ruc";
            company.Address = "Address";
            company.PageWeb = "PageWeb";
            company.Phone = "Phone";
            company.Fax = "Fax";
            company.Logo = "Logo";
            company.State = 1;
            company.Audit.UserRegister = "uni test";

            TransactionScope ts = new TransactionScope();

            b.Insert(company);

            ECompany insert = b.Select(company);

            if (insert != null)
                routes++;

            company.Audit = b2.Select(company.Audit)[0];
            if (company.Audit.UserRegister == "uni test"
                && company.Audit.TypeEvent.ToLower() == "insert")
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 2);
        }

        [TestMethod]
        public void Update()
        {
            short routes = 0;
            BCompany b = new BCompany();
            BAudit b2 = new BAudit();
            ECompany company = new ECompany();
            company.CodeCompany = "xx";
            company.CodeCorporation = "01";
            company.LongName = "Test LongName";
            company.ShortName = "Test ShortName";
            company.Ruc = "Ruc";
            company.Address = "Address";
            company.PageWeb = "PageWeb";
            company.Phone = "Phone";
            company.Fax = "Fax";
            company.Logo = "Logo";
            company.State = 1;
            company.Audit.UserRegister = "uni test";

            TransactionScope ts = new TransactionScope();

            b.Delete(company);
            b.Insert(company);

            ECompany original = b.Select(company);
            if (original != null)
                routes++;

            original.LongName = "Update Name";
            original.PageWeb = "Update Page Web";
            original.Audit.UserRegister = "uni test";

            b.Update(original);

            ECompany update = b.Select(original);

            if (update != null)
                routes++;

            if (update.LongName == "Update Name" && update.PageWeb == "Update Page Web")
                routes++;

            update.Audit = b2.Select(update.Audit)[0];
            if (update.Audit.UserRegister == "uni test"
                && update.Audit.TypeEvent.ToLower() == "update")
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 4);
        }
    }
}

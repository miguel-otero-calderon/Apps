using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apps.Business;
using Apps.Entity;
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
            BCompany b = new BCompany();

            if (b.Select(new ECompany { CodeCompany = "abc" }) == null)
                routes++;

            if (b.Select(new ECompany { CodeCompany = "01" }) != null)
                routes++;

            Assert.AreEqual(routes, 2);
        }

        [TestMethod]
        public void Delete()
        {
            short routes = 0;
            BCompany b = new BCompany();
            BAudit b2 = new BAudit();
            ECompany company = new ECompany();
            company.CodeCompany = "01";
            company.Audit.UserRegister = "uni test";

            TransactionScope ts = new TransactionScope();

            if (b.Select(company) != null)
            {
                b.Delete(company);
                routes++;
            }

            if (b.Select(company) == null)
                routes++;

            company.Audit = b2.Select(company.Audit)[0];
            if (company.Audit.UserRegister == "uni test"
                && company.Audit.TypeEvent.ToLower() == "delete")
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

            if (b.Select(company) != null)
                b.Delete(company);

            if (b.Select(company) == null)
                routes++;

            b.Insert(company);

            ECompany insert = b.Select(company);

            if (insert != null)
                routes++;

            company.Audit = b2.Select(company.Audit)[0];
            if (company.Audit.UserRegister == "uni test"
                && company.Audit.TypeEvent.ToLower() == "insert")
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 3);
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

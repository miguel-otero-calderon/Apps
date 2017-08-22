using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apps.Business;
using Apps.Entity;
using System.Transactions;

namespace Apps.Test
{
    [TestClass]
    public class TAudit
    {
        [TestMethod]
        public void Insert()
        {
            bool result = false; ;
            BAudit b = new BAudit();
            string code = DateTime.Now.ToString();  
            EAudit audit = new EAudit(CodeCompany:"00",CodeEntity:"test",Code:code);
            audit.TypeEvent = "uni test";
            audit.UserRegister = "user test";

            TransactionScope ts = new TransactionScope();

            b.Insert(audit);

            var list = b.Select(audit);

            var insert = list.Where(x => x.CodeCompany == "00" && x.CodeEntity == "test" && x.Code == code).FirstOrDefault();

            ts.Dispose();

            if (insert != null)
                result = true;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Select()
        {
            bool result = false; ;
            BAudit b = new BAudit();
            string code = DateTime.Now.ToString();
            EAudit audit = new EAudit(CodeCompany: "00", CodeEntity: "test", Code: code);
            audit.TypeEvent = "uni test";
            audit.UserRegister = "user test";

            TransactionScope ts = new TransactionScope();

            b.Insert(audit);

            var list = b.Select(audit);

            var insert = list.Where(x => x.CodeCompany == "00" && x.CodeEntity == "test" && x.Code == code).FirstOrDefault();

            ts.Dispose();

            if (insert != null)
                result = true;

            Assert.IsTrue(result);
        }
    }
}

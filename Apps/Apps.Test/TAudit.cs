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
    public class TAudit
    {
        [TestMethod]
        public void Insert()
        {
            bool result = false;
            string codeCompany = Aleatory.GetString(2);
            string codeEntity = Aleatory.GetString(8);
            string code = Aleatory.GetString(8);
            BAudit bAudit = new BAudit();
            EAudit eAudit = new EAudit(
                CodeCompany:codeCompany,
                CodeEntity:codeEntity,
                Code:code);
            eAudit.TypeEvent = "Insert";
            eAudit.UserRegister = Aleatory.GetString(8);
            bAudit.Insert(eAudit);
            EAudit insertedAudit = bAudit.Select(eAudit)[0];

            if (insertedAudit != null
                && insertedAudit.CodeCompany == eAudit.CodeCompany
                && insertedAudit.CodeEntity == eAudit.CodeEntity
                && insertedAudit.Code == eAudit.Code
                && insertedAudit.UserRegister == eAudit.UserRegister
                && insertedAudit.TypeEvent == eAudit.TypeEvent)
                result = true;
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Select()
        {
            short routes = 0;
            string codeCompany = Aleatory.GetString(2);
            string codeEntity = Aleatory.GetString(8);
            string code = Aleatory.GetString(8);
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);
            EAudit selectedAudit = null;
            BAudit bAudit = new BAudit();            
            EAudit eAudit = new EAudit(
                CodeCompany:codeCompany,
                CodeEntity:codeEntity,
                Code:code
                );
            eAudit.UserRegister = Aleatory.GetString(8);
            eAudit.TypeEvent = "Insert";

            if (bAudit.Select(eAudit).Count == 0)
                routes++;
            bAudit.Insert(eAudit);
            selectedAudit = bAudit.Select(eAudit)[0];
            if (selectedAudit != null
                && selectedAudit.CodeCompany == eAudit.CodeCompany
                && selectedAudit.CodeEntity == eAudit.CodeEntity
                && selectedAudit.Code == eAudit.Code
                && selectedAudit.UserRegister == eAudit.UserRegister
                && selectedAudit.TypeEvent == eAudit.TypeEvent)
                routes++;
            ts.Dispose();
            Assert.AreEqual(routes, 2);
        }
    }
}

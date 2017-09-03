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
    public class TClient
    {
        [TestMethod]
        public void Select()
        {
            bool result = false;
            BAudit bAudit = new BAudit();
            List<EAudit> listAudit = new List<EAudit>();
            BClient bClient = new BClient();
            EClient eClient = new EClient();
            BCompany bCompany = new BCompany();
            ECompany eCompany = new ECompany();
            BCorporation bCorporation = new BCorporation();
            ECorporation eCorporation = new ECorporation();
            EClient selectedEClient = new EClient();
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

            eClient.CodeCompany = eCompany.CodeCompany;
            eClient.CodeTypeDocumentIdentity = "0";
            eClient.FirstName = Aleatory.GetString(8);
            eClient.SecondName = Aleatory.GetString(8);
            eClient.FatherLastName = Aleatory.GetString(8);
            eClient.MotherLastName = Aleatory.GetString(8);
            eClient.Audit.UserRegister = Aleatory.GetString(8);
            EClient insertedEClient = bClient.Insert(eClient);

            selectedEClient = bClient.Select(insertedEClient);

            if(selectedEClient != null 
                && selectedEClient.CodeClient == insertedEClient.CodeClient
                && selectedEClient.SearchName == insertedEClient.SearchName
                && selectedEClient.State == insertedEClient.State)
                result = true;

            ts.Dispose();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Insert()
        {
            bool result = false;
            BAudit bAudit = new BAudit();
            List<EAudit> listAudit = new List<EAudit>();
            BClient bClient = new BClient();
            EClient eClient = new EClient();
            BCompany bCompany = new BCompany();
            ECompany eCompany = new ECompany();
            BCorporation bCorporation = new BCorporation();
            ECorporation eCorporation = new ECorporation();
            BSequence bSequence = new BSequence();
            ESequence eSequence = null;
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

            eClient.CodeCompany = eCompany.CodeCompany;            
            eClient.CodeTypeDocumentIdentity = "0";
            eClient.FirstName = Aleatory.GetString(8);
            eClient.SecondName = Aleatory.GetString(8);
            eClient.FatherLastName = Aleatory.GetString(8);
            eClient.MotherLastName = Aleatory.GetString(8);
            eClient.Audit.UserRegister = Aleatory.GetString(8);
            EClient insertedEClient = bClient.Insert(eClient);

            eSequence = new ESequence(eClient);
            eSequence = bSequence.Select(eSequence);

            listAudit = bAudit.Select(eClient.Audit);

            if (insertedEClient != null
                && listAudit.Exists(x => x.UserRegister == eClient.Audit.UserRegister 
                && x.TypeEvent == "Insert" 
                && x.Code == insertedEClient.CodeClient.ToString())
                && eSequence.Correlative == insertedEClient.CodeClient + 1)
                result = true;
                
            ts.Dispose();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Update()
        {
            bool result = false;
            BAudit bAudit = new BAudit();
            List<EAudit> listAudit = new List<EAudit>();
            BClient bClient = new BClient();
            EClient eClient = new EClient();
            BCompany bCompany = new BCompany();
            ECompany eCompany = new ECompany();
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

            eClient.CodeCompany = eCompany.CodeCompany;
            eClient.CodeTypeDocumentIdentity = "0";
            eClient.FirstName = Aleatory.GetString(8);
            eClient.SecondName = Aleatory.GetString(8);
            eClient.FatherLastName = Aleatory.GetString(8);
            eClient.MotherLastName = Aleatory.GetString(8);
            eClient.Audit.UserRegister = Aleatory.GetString(8);
            EClient insertedEClient = bClient.Insert(eClient);

            insertedEClient.FirstName = Aleatory.GetString(8);
            insertedEClient.SecondName = Aleatory.GetString(8);
            insertedEClient.FatherLastName = Aleatory.GetString(8);
            insertedEClient.MotherLastName = Aleatory.GetString(8);
            insertedEClient.Audit.UserRegister = Aleatory.GetString(8);
            EClient updatedEClient = bClient.Update(insertedEClient);

            listAudit = bAudit.Select(eClient.Audit);

            if (updatedEClient != null
                && updatedEClient.SearchName != eClient.SearchName
                && listAudit.Count > 0
                && listAudit.Exists(
                    x=>x.UserRegister == insertedEClient.Audit.UserRegister
                    && x.Code == updatedEClient.CodeClient.ToString()
                    && x.TypeEvent == "Update"))
                result = true;

            ts.Dispose();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Delete()
        {
            bool result = false;
            BAudit bAudit = new BAudit();
            List<EAudit> listAudit = new List<EAudit>();
            BClient bClient = new BClient();
            EClient eClient = new EClient();
            BCompany bCompany = new BCompany();
            ECompany eCompany = new ECompany();
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

            eClient.CodeCompany = eCompany.CodeCompany;
            eClient.CodeTypeDocumentIdentity = "0";
            eClient.FirstName = Aleatory.GetString(8);
            eClient.SecondName = Aleatory.GetString(8);
            eClient.FatherLastName = Aleatory.GetString(8);
            eClient.MotherLastName = Aleatory.GetString(8);
            eClient.Audit.UserRegister = Aleatory.GetString(8);
            EClient insertedEClient = bClient.Insert(eClient);            
            if(insertedEClient != null)
            {
                insertedEClient.Audit.UserRegister = Aleatory.GetString(8);
                bClient.Delete(insertedEClient);

                EClient deletedEClient = bClient.Select(eClient);

                listAudit = bAudit.Select(eClient.Audit);

                if (deletedEClient == null                    
                    && listAudit.Count > 0
                    && listAudit.Exists(
                        x => x.UserRegister == insertedEClient.Audit.UserRegister
                        && x.Code == insertedEClient.CodeClient.ToString()
                        && x.TypeEvent == "Delete"))
                    result = true;
            }                         

            ts.Dispose();

            Assert.IsTrue(result);
        }
    }
}

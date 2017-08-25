using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apps.Business;
using Apps.Entity;
using Apps.Util ;
using System.Transactions;

namespace Apps.Test
{
    [TestClass]
    public class TCorporation
    {
        [TestMethod]
        public void Select()
        {
            ECorporation eCorporation = new ECorporation();
            BCorporation bCorporation = new BCorporation();            
            BAudit bAudit = new BAudit();
            short routes = 0;

            eCorporation.CodeCorporation = Aleatory.GetString(2);
            eCorporation.Name = Aleatory.GetString(8);
            eCorporation.State = 1;
            eCorporation.Audit.UserRegister = Aleatory.GetString(8);

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                if (bCorporation.Select(eCorporation) == null)
                    routes++;

                bCorporation.Insert(eCorporation);

                ECorporation insertedCorporation = bCorporation.Select(eCorporation);

                if (insertedCorporation != null && insertedCorporation.Name == eCorporation.Name && insertedCorporation.State == eCorporation.State)
                    routes++;

                EAudit lastAudit = bAudit.Select(eCorporation.Audit)[0];

                if (lastAudit != null && lastAudit.UserRegister == eCorporation.Audit.UserRegister 
                    && lastAudit.TypeEvent.ToLower() == "insert")
                    routes++;
            }

            Assert.AreEqual(routes, 3);
        }

        [TestMethod]
        public void Insert()
        {
            ECorporation eCorporation = new ECorporation();
            BCorporation bCorporation = new BCorporation();
            BAudit bAudit = new BAudit();
            short routes = 0;

            eCorporation.CodeCorporation = Aleatory.GetString(2);
            eCorporation.Name = Aleatory.GetString(8);
            eCorporation.State = Aleatory.GetShort();
            eCorporation.Audit.UserRegister = Aleatory.GetString(8);

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                bCorporation.Insert(eCorporation);

                ECorporation insertedCorporation = bCorporation.Select(eCorporation);

                if (insertedCorporation != null 
                    && insertedCorporation.Name == eCorporation.Name 
                    && insertedCorporation.State == eCorporation.State)
                    routes++;

                EAudit lastAudit = bAudit.Select(eCorporation.Audit)[0];

                if (lastAudit != null 
                    && lastAudit.UserRegister == eCorporation.Audit.UserRegister
                    && lastAudit.TypeEvent.ToLower() == "insert")
                    routes++;
            }

            Assert.AreEqual(routes, 2);
        }

        [TestMethod]
        public void Update()
        {
            ECorporation eCorporation = new ECorporation();
            BCorporation bCorporation = new BCorporation();
            BAudit bAudit = new BAudit();
            short routes = 0;
            bool negative = true;

            eCorporation.CodeCorporation =Aleatory.GetString(2);
            eCorporation.Name = Aleatory.GetString(8);
            eCorporation.State = Aleatory.GetShort();
            eCorporation.Audit.UserRegister = Aleatory.GetString(8);

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                bCorporation.Insert(eCorporation);

                ECorporation originalCorporation = bCorporation.Select(eCorporation);

                if (originalCorporation != null 
                    && originalCorporation.Name == eCorporation.Name 
                    && originalCorporation.State == eCorporation.State)
                    routes++;

                EAudit lastAudit = bAudit.Select(eCorporation.Audit)[0];

                if (lastAudit != null 
                    && lastAudit.UserRegister == eCorporation.Audit.UserRegister
                    && lastAudit.TypeEvent.ToLower() == "insert")
                    routes++;

                eCorporation.Name = string.Format("{0} {1}", eCorporation.Name,"update");                
                eCorporation.State = Aleatory.GetShort(negative);
                eCorporation.Audit.UserRegister = Aleatory.GetString(8);

                bCorporation.Update(eCorporation);

                ECorporation updatedCorporation = bCorporation.Select(eCorporation);

                if (updatedCorporation != null 
                    && updatedCorporation.Name == eCorporation.Name                     
                    && updatedCorporation.State == eCorporation.State
                    && updatedCorporation.Name != originalCorporation.Name
                    && updatedCorporation.State != originalCorporation.State)
                    routes++;

                lastAudit = bAudit.Select(updatedCorporation.Audit)[0];
                if (lastAudit != null
                    && lastAudit.UserRegister == eCorporation.Audit.UserRegister
                    && lastAudit.TypeEvent.ToLower() == "update")
                    routes++;
            }

            Assert.AreEqual(routes, 4);
        }
    }
}

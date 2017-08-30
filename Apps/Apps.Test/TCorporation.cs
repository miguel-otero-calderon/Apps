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

                EAudit lastAudit = bAudit.Select(eCorporation.Audit).Where(x => x.UserRegister == eCorporation.Audit.UserRegister && x.TypeEvent == "Insert").FirstOrDefault();

                if (lastAudit != null)
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

                EAudit lastAudit = bAudit.Select(eCorporation.Audit).Where(x => x.UserRegister == eCorporation.Audit.UserRegister && x.TypeEvent == "Insert").FirstOrDefault();

                if (lastAudit != null)
                    routes++;

                eCorporation.Name = string.Format("{0} {1}", eCorporation.Name,"update");                
                eCorporation.State = Aleatory.GetShort();
                eCorporation.Audit.UserRegister = Aleatory.GetString(8);

                bCorporation.Update(eCorporation);

                ECorporation updatedCorporation = bCorporation.Select(eCorporation);

                if (updatedCorporation != null 
                    && updatedCorporation.Name == eCorporation.Name                     
                    && updatedCorporation.State == eCorporation.State
                    && updatedCorporation.Name != originalCorporation.Name
                    && updatedCorporation.State != originalCorporation.State)
                    routes++;

                lastAudit = bAudit.Select(updatedCorporation.Audit).Where(x=>x.UserRegister == eCorporation.Audit.UserRegister && x.TypeEvent == "Update").FirstOrDefault();
                if (lastAudit != null)
                    routes++;
            }

            Assert.AreEqual(routes, 4);
        }

        [TestMethod]
        public void Delete()
        {
            short routes = 0;
            BAudit bAudit = new BAudit();
            EAudit eAudit = null;
            BCorporation bCorporation = new BCorporation();
            ECorporation eCorporation = new ECorporation();
            ECorporation insertedCorporation = new ECorporation();
            BCompany bCompany = new BCompany();
            ECompany eCompany = new ECompany();
            ECompany insertedCompany = new ECompany();
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);

            eCorporation.CodeCorporation = Aleatory.GetString(2);
            eCorporation.Name = Aleatory.GetString(8);
            eCorporation.State = Aleatory.GetShort();
            eCorporation.Audit.UserRegister = Aleatory.GetString(8);
            bCorporation.Insert(eCorporation);

            insertedCorporation = bCorporation.Select(eCorporation);
            if (insertedCorporation != null)
                routes++;

            insertedCorporation.Audit.UserRegister = eCorporation.Audit.UserRegister;
            bCorporation.Delete(insertedCorporation);

            if (bCorporation.Select(insertedCorporation) == null)
                routes++;

            eAudit = bAudit.Select(eCorporation.Audit).Where(x => x.UserRegister == eCorporation.Audit.UserRegister && x.TypeEvent == "Delete").FirstOrDefault();

            if (eAudit != null)
                routes++;

            bCorporation.Insert(eCorporation);
            eCompany.CodeCorporation = eCorporation.CodeCorporation;
            eCompany.CodeCompany = Aleatory.GetString(2);
            eCompany.LongName = Aleatory.GetString(8);
            eCompany.State = Aleatory.GetShort();
            eCompany.Audit.UserRegister = eCorporation.Audit.UserRegister;
            bCompany.Insert(eCompany);

            insertedCompany = bCompany.Select(eCompany);

            if(insertedCompany != null)
            {
                try
                {
                    eCorporation.Audit.UserRegister = Aleatory.GetString(9);
                    bCorporation.Delete(eCorporation);
                }
                catch
                {

                }

                if (bCorporation.Message.Contains("La Corporación")
                    && bCorporation.Message.Contains("tiene referencias en el Sistema, no se puede eliminar el registro."))
                    routes++;

                eAudit = bAudit.Select(eCorporation.Audit).Where(x => x.UserRegister == eCorporation.Audit.UserRegister && x.TypeEvent == "Delete").FirstOrDefault();
                if (eAudit == null)
                    routes++;
            }

            ts.Dispose();

            Assert.AreEqual(routes,5);
        }
    }
}

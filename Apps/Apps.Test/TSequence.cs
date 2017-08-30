using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apps.Entity;
using Apps.Util;
using Apps.Business;
using System.Transactions;

namespace Apps.Test
{
    [TestClass]
    public class TSequence
    {
        [TestMethod]
        public void Select()
        {
            short routes = 0;
            string codeSequence = Aleatory.GetString(8);
            int correlativo = Aleatory.GetShort(); 
            BSequence bSequence = new BSequence();
            ESequence eSequence = new ESequence(codeSequence, correlativo);
            ESequence selectedSequence = null;
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);
            selectedSequence = bSequence.Select(eSequence);

            if (selectedSequence == null)
                routes++;
            
            bSequence.Insert(eSequence);
            selectedSequence = bSequence.Select(eSequence);

            if (selectedSequence != null
                && selectedSequence.CodeSequence == eSequence.CodeSequence
                && selectedSequence.Correlative == eSequence.Correlative)
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 2);
        }

        [TestMethod]
        public void Insert()
        {
            bool result = false;
            string codeSequence = Aleatory.GetString(8);
            int correlativo = Aleatory.GetShort();
            BSequence bSequence = new BSequence();
            ESequence eSequence = new ESequence(codeSequence, correlativo);
            ESequence insertedSequence = null;
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);

            bSequence.Insert(eSequence);
            insertedSequence = bSequence.Select(eSequence);

            if (insertedSequence != null
                && insertedSequence.CodeSequence == eSequence.CodeSequence
                && insertedSequence.Correlative == eSequence.Correlative)
                result = true;

            ts.Dispose();

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void Delete()
        {
            bool result = false;
            string codeSequence = Aleatory.GetString(8);
            int correlativo = Aleatory.GetShort();
            BSequence bSequence = new BSequence();
            ESequence eSequence = new ESequence(codeSequence, correlativo);
            ESequence deletedSequence = null;
            ESequence seletedSequence = null;
            TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew);

            bSequence.Insert(eSequence);
            seletedSequence = bSequence.Select(eSequence);
            if(seletedSequence  != null)
            {
                bSequence.Delete(eSequence);
                deletedSequence = bSequence.Select(eSequence);
                if (deletedSequence == null)
                    result = true;
            }

            Assert.AreEqual(result, true);
        }
    }
}

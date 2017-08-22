using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apps.Entity;
using Apps.Business;
using System.Transactions;

namespace Apps.Test
{
    [TestClass]
    public class TSequence
    {
        [TestMethod]
        public void GetCorrelative()
        {
            BSequence b = new BSequence();
            ESequence test = new ESequence(CodeSequence: "test", Correlative: 100);
            TransactionScope ts = new TransactionScope();
            ESequence result;
            short routes = 0;
            int correlative = 0;

            b.Delete(test);

            if (b.Select(test) == null)
                routes++;

            correlative = b.GetCorrelative(test);
            result = b.Select(test);
            if (result != null && correlative == 1 && result.Correlative == 1)
                routes++;

            result.Correlative = 100;
            b.Update(result);

            correlative = b.GetCorrelative(test);
            result = b.Select(test);
            if (result != null && correlative == 100 && result.Correlative == 100)
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 3);
        }

        [TestMethod]
        public void SetCorrelative()
        {
            BSequence b = new BSequence();
            ESequence test = new ESequence(CodeSequence: "test", Correlative: 100);
            TransactionScope ts = new TransactionScope();
            ESequence result;
            short routes = 0;

            b.Delete(test);
            b.SetCorrelativo(test);
            result = b.Select(test);
            
            if (result != null && result.Correlative == 1)
                routes++;

            result.Correlative = 200;
            b.Update(result);
            result = b.Select(test);

            if (result != null && result.Correlative == 200)
                routes++;
            b.SetCorrelativo(test);

            result = b.Select(test);

            if (result != null && result.Correlative == 201)
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 3);
        }

        [TestMethod]
        public void Select()
        {
            BSequence b = new BSequence();
            ESequence sequence = new ESequence(CodeSequence: "Client", Correlative: 0);
            ESequence select = b.Select(sequence);
            Assert.IsNotNull(select);
        }

        [TestMethod]
        public void Insert()
        {
            BSequence b = new BSequence();
            ESequence test = new ESequence(CodeSequence: "test", Correlative: 1);
            TransactionScope ts = new TransactionScope();
            short routes = 0;
            b.Insert(test);

            ESequence insert = b.Select(test);
            if (insert != null)
                routes++;

            if (insert.CodeSequence == test.CodeSequence && insert.Correlative == test.Correlative)
                routes++;            

            ts.Dispose();

            Assert.AreEqual(routes, 2);
        }

        [TestMethod]
        public void Update()
        {
            BSequence b = new BSequence();
            ESequence test = new ESequence(CodeSequence: "test", Correlative: 1);
            TransactionScope ts = new TransactionScope();
            short routes = 0;

            b.Delete(test);
            b.Insert(test);

            ESequence original = b.Select(test);
            if (original != null && original.Correlative == 1)
                routes++;

            original.Correlative = 100;
            b.Update(original);

            ESequence update = b.Select(test);
            if (update != null)
                routes++;

            if (original.CodeSequence == update.CodeSequence && original.Correlative == update.Correlative)
                routes++;

            ts.Dispose();

            Assert.AreEqual(routes, 3);
        }
    }
}

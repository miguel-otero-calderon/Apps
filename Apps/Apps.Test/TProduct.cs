using System;
using Apps.Entity;
using Apps.Business;
using Apps.Util;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apps.Test
{
    [TestClass]
    public class TProduct
    {
        [TestMethod]
        public void Select()
        {
            bool result = false;
            TransactionScope ts = new TransactionScope();
            EProduct eProduct = new EProduct();
            EProduct insertedProduct = new EProduct();
            BProduct bProduct = new BProduct();
            eProduct.DescriptionForSale = Aleatory.GetString(60);
            eProduct.DescriptionForStore = Aleatory.GetString(60);
            eProduct.Audit.UserRegister = Aleatory.GetString(8);
            insertedProduct = bProduct.Insert(eProduct);

            if (insertedProduct != null)
                result = true;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Insert()
        {

        }

        [TestMethod]
        public void Update()
        {

        }

        [TestMethod]
        public void Delete()
        {

        }
    }
}

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
            BClient bClient = new BClient();
        }
    }
}

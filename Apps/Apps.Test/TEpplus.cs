using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apps.Util;

namespace Apps.Test
{
    [TestClass]
    public class TEpplus
    {
        [TestMethod]
        public void ToDataTable()
        {
            string file = @"D:\Miguel\Escritorio\Apps Load\ProductType.xlsx";
            DataTable table = null;
            table = Epplus.ToDataTable(file);
            if (table != null
                && table.Columns.Count > 0
                && table.Rows.Count > 0)
                Assert.IsTrue(true);
        }
    }
}

﻿using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apps.Entity;
using Apps.Business;
using Apps.Extension;
using Apps.Load;
using Apps.Util;

namespace Apps.Test
{
    [TestClass]
    public class TProductType
    {
        [TestMethod]
        public void Load()
        {
            string file = @"D:\Miguel\Documentos\Visual Studio 2015\Projects\Apps\Apps\Apps.Load\Files\ProductType.xlsx";
            LProductType lProductType = new LProductType();
            lProductType.Load(file);
        }
    }
}

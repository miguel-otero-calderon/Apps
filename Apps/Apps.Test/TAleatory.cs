using System;
using System.Linq;
using System.Collections.Generic;
using Apps.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apps.Test
{
    [TestClass]
    public class TAleatory
    {
        [TestMethod]
        public void GetString()
        {
            bool result = false;
            List<string> list = new List<string>();
            List<string> dist = new List<string>();
            string value = string.Empty;
            for (int i = 1; i <= 100; i++)
            {
                value = Aleatory.GetString(5);
                list.Add(value);
            }

            dist = list.Distinct().ToList();

            if (list.Count == dist.Count)
                result = true;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetShort()
        {
            bool result = false;
            List<short> list = new List<short>();
            List<short> dist = new List<short>();
            short value = 0;
            for (int i = 1; i <= 10; i++)
            {
                value = Aleatory.GetShort();
                list.Add(value);
            }

            dist = list.Distinct().ToList();

            if (list.Count == dist.Count)
                result = true;

            Assert.IsTrue(result);
        }
    }
}

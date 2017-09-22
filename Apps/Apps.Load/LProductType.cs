using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Util;
using Apps.Extension;
using Apps.Entity;
using Apps.Business;
using System.Transactions;

namespace Apps.Load
{
    public static class LProductType
    {
        public static int Load(string file)
        {
            int rows = 0;
            EProductType eProductType = null;
            BProductType bProductType = new BProductType();
            DataTable table = Epplus.ToDataTable(file);
            if (table != null)
            {
                List<string> columns = table.GetColumns();
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (DataRow datarow in table.Rows)
                    {
                        eProductType = new EProductType(datarow, columns);
                        bProductType.Insert(eProductType);
                        rows++;
                    }
                    ts.Complete();
                }
            }
            return rows;
        }
    }
}

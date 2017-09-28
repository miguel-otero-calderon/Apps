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
    public class LProductType
    {
        public int Load(string file)
        {
            int rows = 0;
            EProductType eProductType = null;
            BProductType bProductType = new BProductType();
            List<string> columns = new List<string>();
            DataTable table = Epplus.ToDataTable(file);
            if (table != null)
            {
                table.Columns["Código"].ColumnName = "CodeProductType";
                table.Columns["Descripción"].ColumnName = "Description";
                table.Columns["Código Sunat"].ColumnName = "CodeSunatExistence";
                table.Columns["Estado"].ColumnName = "State";

                columns = table.GetColumns();

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (DataRow datarow in table.Rows)
                    {
                        eProductType = new EProductType(datarow, columns);
                        eProductType.Audit.UserRegister = "User Load";
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

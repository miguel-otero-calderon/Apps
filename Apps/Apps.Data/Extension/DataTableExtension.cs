using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Data.Extension
{
    public static class DataTableExtension
    {
        public static List<string> GetColumns(this DataTable table)
        {
            List<string> Columns = new List<string>();
            foreach (DataColumn column in table.Columns)
            {
                Columns.Add(column.ColumnName);
            }
            return null;
        }
    }
}

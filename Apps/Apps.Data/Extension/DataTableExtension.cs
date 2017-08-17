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
            return Columns;
        }

        public static List<string> GetValues(this DataTable table, string column)
        {
            List<string> values = new List<string>();
            int last = table.Rows.Count - 1;
            object value;

            for (int i = 0; i <= last; i++)
            {
                value = table.Rows[i][column];
                if (value != null && value != DBNull.Value)
                    values.Add(value.ToString());
                else
                    values.Add(string.Empty);
            }
            return values;
        }
    }
}

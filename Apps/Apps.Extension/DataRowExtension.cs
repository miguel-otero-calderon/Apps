using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Extension
{
    public static class DataRowExtension
    {
        public static bool Validate(this System.Data.DataRow dr, string column)
        {
            if (dr[column] != null && dr[column] != DBNull.Value)
                return true;
            else
                return false;
        }

        public static List<string> GetColumns(this System.Data.DataRow row)
        {
            return row.Table.GetColumns();
        }
    }
}

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Entity.Extension
{
    public static class DataRowExtension
    {
        public static bool Validate(this DataRow dr, string column)
        {
            if (dr[column] != null && dr[column] != DBNull.Value)
                return true;
            else
                return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Apps.Data
{
    public class DataFactory
    {
        private DataFactory() { }
        private static DataSqlServer instanceSqlServer;
        private static string stringConnection;
        public static Data CreateData()
        {
            if (string.IsNullOrEmpty(stringConnection))
                stringConnection = ConfigurationManager.ConnectionStrings[0].ConnectionString;

            if (string.IsNullOrEmpty(stringConnection))
                throw new Exception("String Connection not exists.");

            if (stringConnection.ToLower().Contains("sqlserver"))
            {
                if(instanceSqlServer == null)
                    instanceSqlServer = new DataSqlServer(stringConnection);
                return instanceSqlServer;
            }
               
            return null;
        }
    }
}

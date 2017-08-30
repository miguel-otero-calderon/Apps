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
        public DataFactory() { }
        private DataSqlServer instanceSqlServer;
        private static ConnectionStringSettings stringConnection = new ConnectionStringSettings();
        public Data CreateData()
        {
            if (string.IsNullOrEmpty(stringConnection.ConnectionString))
            {
                foreach (ConnectionStringSettings connection in ConfigurationManager.ConnectionStrings)
                {
                    if (connection.Name.ToLower().Contains("connection"))
                    {
                        stringConnection = connection;
                        break;
                    }                        
                }                
            }

            if (string.IsNullOrEmpty(stringConnection.ConnectionString))
            {
                throw new Exception("Not exists connectionString, begin 'connection[SqlServer or Oracle]'.");
            }

            if (stringConnection.Name.ToLower().Contains("sqlserver"))
            {
                if(instanceSqlServer == null)
                    instanceSqlServer = new DataSqlServer(stringConnection.ConnectionString);
                return instanceSqlServer;
            }
               
            return null;
        }
    }
}

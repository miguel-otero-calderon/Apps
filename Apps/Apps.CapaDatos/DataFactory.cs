using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.CapaDatos
{
    public class DataFactory
    {
        protected string stringConnection;
        public DataFactory(string stringConnection)
        {
            this.stringConnection = stringConnection;
        }

        public Data CreateData()
        {
            if (this.stringConnection.ToLower().Contains("sqlserver"))
                return new DataSqlServer(this.stringConnection);
            return null;
        }
    }
}

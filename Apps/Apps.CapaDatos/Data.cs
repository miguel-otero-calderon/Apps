using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;

namespace Apps.CapaDatos
{
    public abstract class Data
    {
        public Data() { }
        protected abstract DbConnection GetConnection();
        public abstract void ExecuteCommand(DbCommand Command);
        public abstract IDataReader ExecuteDataReader(DbCommand Command);
    }
}

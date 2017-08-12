using System.Data.Common;
using System.Data;

namespace Apps.Data
{
    public abstract class Data
    {
        public Data() { }
        protected abstract DbConnection GetConnection();
        public abstract void ExecuteCommand(DbCommand Command);
        public abstract IDataReader ExecuteDataReader(DbCommand Command);
    }
}

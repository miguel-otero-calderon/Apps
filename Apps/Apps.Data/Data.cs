using Apps.Data;
using System;
using System.Data;

namespace Apps.Data
{
    public abstract class Data
    {
        protected Data() { }
        public abstract void ExecuteNonQuery(DaCommand Command);
        public abstract DataTable ExecuteDataTable(DaCommand Command);
        public abstract DataRow ExecuteDataRow(DaCommand Command);
        public abstract object ExecuteScalar(DaCommand Command);
        public abstract Exception Exception { get; }
        public abstract bool ExistsReference();
        public abstract bool ExistsPrimaryKey();
    }
}

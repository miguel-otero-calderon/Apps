using Apps.Data;
using System.Data;

namespace Apps.Data
{
    public abstract class Data
    {
        protected Data() { }
        public abstract void ExecuteCommand(DaCommand Command);
        public abstract IDataReader ExecuteDataReader(DaCommand Command);
    }
}

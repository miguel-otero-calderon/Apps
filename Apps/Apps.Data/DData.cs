using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Apps.Data
{
    public class DData:Data
    {
        private static Data layerData;
        public DData()
        {
            if (layerData == null)
                layerData = DataFactory.CreateData();
        }

        public override void ExecuteCommand(DaCommand Command)
        {
            layerData.ExecuteCommand(Command);
        }

        public override IDataReader ExecuteDataReader(DaCommand Command)
        {
            return layerData.ExecuteDataReader(Command);
        }
    }
}

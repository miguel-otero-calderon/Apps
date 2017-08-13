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
        public override void ExecuteCommand(DaCommand Command)
        {
            if (layerData == null)
                layerData = DataFactory.CreateData();
            layerData.ExecuteCommand(Command);
        }

        public override IDataReader ExecuteDataReader(DaCommand Command)
        {
            if (layerData == null)
                layerData = DataFactory.CreateData();
            return layerData.ExecuteDataReader(Command);
        }
    }
}

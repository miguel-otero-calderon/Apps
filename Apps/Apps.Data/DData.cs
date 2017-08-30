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
        private DataFactory dataFactory = new DataFactory();
        private Data _layerData;
        private Data layerData
        {
            get
            {
                if (_layerData == null)
                    _layerData = dataFactory.CreateData();
                return _layerData;
            }
        }

        public override Exception Exception
        {
            get
            {
                return layerData.Exception;
            }
        }

        public override bool ExistsReference()
        {
            return layerData.ExistsReference();
        }

        public override bool ExistsPrimaryKey()
        {
            return layerData.ExistsPrimaryKey();
        }

        public override void ExecuteNonQuery(DaCommand Command)
        {
            layerData.ExecuteNonQuery(Command);
        }

        public override DataRow ExecuteDataRow(DaCommand Command)
        {
            return layerData.ExecuteDataRow(Command);
        }

        public override DataTable ExecuteDataTable(DaCommand Command)
        {
            return layerData.ExecuteDataTable(Command);
        }

        public override object ExecuteScalar(DaCommand Command)
        {
            return layerData.ExecuteScalar(Command);
        }
    }
}

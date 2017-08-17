﻿using System;
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
        private static Data _layerData;
        private static Data layerData
        {
            get
            {
                if (_layerData == null)
                    _layerData = DataFactory.CreateData();
                return _layerData;
            }
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

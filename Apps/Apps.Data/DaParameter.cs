﻿using System;
using System.Data;

namespace Apps.Data
{
    public class DaParameter : System.Data.Common.DbParameter
    {
        public DaParameter() { }
        public DaParameter(string ParameterName, DbType DbType, object Value)
        {
            this.ParameterName = ParameterName;
            this.DbType = DbType;
            this.Value = Value;
        }

        public override DbType DbType { get; set; }

        public override ParameterDirection Direction { get; set; }

        public override bool IsNullable { get; set; }

        public override string ParameterName { get; set; }

        public override int Size { get; set; }

        public override string SourceColumn { get; set; }

        public override bool SourceColumnNullMapping { get; set; }

        public override object Value { get; set; }

        public override void ResetDbType() { }        
    }
}
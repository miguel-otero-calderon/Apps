using System;
using System.Collections.Generic;
using System.Data;

namespace Apps.Data
{
    public class DaCommand
    {
        private List<DaParameter> parameters;
        public DaCommand() {parameters = new List<DaParameter>();}
        public DaCommand(string CommandText)
        {
            this.CommandText = CommandText;
            parameters = new List<DaParameter>();
        }            

        public string CommandText { get; set; }

        public CommandType CommandType { get; set; }

        public List<DaParameter> Parameters { get { return parameters; }}

        public void AddInParameter(string ParameterName, DbType DbType, object Value)
        {
            DaParameter parameter = new DaParameter(ParameterName, DbType, Value);
            parameter.Direction = ParameterDirection.Input;
            Parameters.Add(parameter);
        }

        public void AddOutParameter(string ParameterName, DbType DbType, object Value)
        {
            DaParameter parameter = new DaParameter(ParameterName, DbType, Value);
            parameter.Direction = ParameterDirection.Output;
            Parameters.Add(parameter);
        }
    }
}

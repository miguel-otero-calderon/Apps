using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Apps.Data
{
    public class DataSqlServer : Data
    {
        private static SqlConnection Connection;
        private static SqlCommand Command;
        private DataSqlServer() { }
        public DataSqlServer(string stringConnection)
        {
            if (DataSqlServer.Connection == null)
                DataSqlServer.Connection = new SqlConnection(stringConnection);

            if (DataSqlServer.Command == null)
                DataSqlServer.Command = new SqlCommand();
        }

        public override void ExecuteCommand(DaCommand Command)
        {
            try
            {
                SqlCommand store = GetCommand(Command);
                store.Connection = GetConnection();
                store.ExecuteNonQuery();
            }
            finally
            {
                GetConnection().Close();
            }
        }

        public override IDataReader ExecuteDataReader(DaCommand Command)
        {
            try
            {
                SqlCommand store = GetCommand(Command);
                store.Connection = GetConnection();
                return store.ExecuteReader();
            }
            finally
            {
                GetConnection().Close();
            }
        }

        protected SqlConnection GetConnection()
        {
            if(DataSqlServer.Connection.State != ConnectionState.Open)
                DataSqlServer.Connection.Open();

            return DataSqlServer.Connection;
        }   

        protected SqlCommand GetCommand()
        {
            if (DataSqlServer.Command == null)
                DataSqlServer.Command = new SqlCommand();

            return DataSqlServer.Command;
        }

        protected SqlCommand GetCommand(DaCommand Command)
        {
            SqlCommand store = DataSqlServer.Command;
            store.CommandText = Command.CommandText;
            store.CommandType = CommandType.StoredProcedure;
            SetParameters(ref store,Command);
            return store;
        }

        private void SetParameters(ref SqlCommand store, DaCommand command)
        {
            store.Parameters.Clear();
            foreach (var par in command.Parameters)
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = par.ParameterName;
                parameter.Direction = par.Direction;
                parameter.DbType = par.DbType;
                parameter.Value = par.Value;
                store.Parameters.Add(parameter);
            }
        }
    }
}

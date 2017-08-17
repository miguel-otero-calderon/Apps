using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Apps.Data
{
    public class DataSqlServer : Data
    {
        private SqlConnection Connection;        
        private DataSqlServer() { }
        public DataSqlServer(string stringConnection)
        {
            if (Connection == null)
                Connection = new SqlConnection(stringConnection);
        }

        public override void ExecuteNonQuery(DaCommand Command)
        {
            try
            {
                SqlCommand store = GetCommand(Command);
                store.Connection = GetConnection();
                store.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GetConnection().Close();
            }
        }

        public override DataTable ExecuteDataTable(DaCommand Command)
        {
            try
            {
                SqlCommand store = GetCommand(Command);
                store.Connection = GetConnection();
                DataTable table = new DataTable();
                using (SqlDataReader reader = store.ExecuteReader())
                {
                    table.Load(reader);
                    reader.Close();
                }
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GetConnection().Close();
            }
        }

        protected SqlConnection GetConnection()
        {
            if(Connection.State != ConnectionState.Open)
                Connection.Open();

            return Connection;
        }   

        protected SqlCommand GetCommand(DaCommand Command)
        {
            SqlCommand store = new SqlCommand();
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

        public override DataRow ExecuteDataRow(DaCommand Command)
        {
            DataTable table = ExecuteDataTable(Command);
            if (table.Rows.Count > 0)
                return table.Rows[0];
            else
                return null;
        }
    }
}

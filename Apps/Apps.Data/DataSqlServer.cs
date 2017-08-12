using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Apps.Data
{
    public class DataSqlServer : Data
    {
        private string stringConnection;
        public DataSqlServer(string stringConnection)
        {
            this.stringConnection = stringConnection;
        }

        public override void ExecuteCommand(DbCommand command)
        {
            SqlCommand store = (SqlCommand)command;
            store.Connection = (SqlConnection)GetConnection();
            store.ExecuteNonQuery();
        }

        public override IDataReader ExecuteDataReader(DbCommand command)
        {
            SqlCommand store = (SqlCommand)command;
            store.Connection = (SqlConnection)GetConnection();
            return store.ExecuteReader();
        }

        protected override DbConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(stringConnection);
            connection.Open();
            return connection;
        }
    }
}

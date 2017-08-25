using System.Data;
using Apps.Entity;

namespace Apps.Data
{
    public class DCorporation : DData
    {
        public DataRow Select(ECorporation corporation)
        {
            DaCommand command = new DaCommand("dbo.CorporationSelect");
            command.AddInParameter("@CodeCorporation", DbType.String, corporation.CodeCorporation);
            return ExecuteDataRow(command);
        }

        public void Delete(ECorporation corporation)
        {
            DaCommand command = new DaCommand("dbo.CorporationDelete");
            command.AddInParameter("@CodeCorporation", DbType.String, corporation.CodeCorporation);
            ExecuteNonQuery(command);
        }

        public void Insert(ECorporation corporation)
        {
            DaCommand command = new DaCommand("dbo.CorporationInsert");
            command.AddInParameter("@CodeCorporation", DbType.String, corporation.CodeCorporation);
            command.AddInParameter("@Name", DbType.String, corporation.Name);
            command.AddInParameter("@State", DbType.Int16, corporation.State);
            ExecuteNonQuery(command);
        }

        public void Update(ECorporation corporation)
        {
            DaCommand command = new DaCommand("dbo.CorporationUpdate");
            command.AddInParameter("@CodeCorporation", DbType.String, corporation.CodeCorporation);
            command.AddInParameter("@Name", DbType.String, corporation.Name);
            command.AddInParameter("@State", DbType.Int16, corporation.State);
            ExecuteNonQuery(command);
        }
    }
}

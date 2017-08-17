using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Entity;

namespace Apps.Data
{
    public class DClient:DData
    {
        public void Insert(EClient client)
        {
            DaCommand command = new DaCommand("ClientInsert");
            command.AddInParameter("@CodeUser", DbType.Int32, client.CodeClient);
            command.AddInParameter("@FirstName", DbType.String, client.FirstName);
            command.AddInParameter("@SecondName", DbType.String, client.SecondName);
            command.AddInParameter("@FatherLastName", DbType.String, client.FatherLastName);
            command.AddInParameter("@MotherLastName", DbType.String, client.MotherLastName);
            command.AddInParameter("@LongName", DbType.String, client.LongName);
            command.AddInParameter("@ShortName", DbType.String, client.ShortName);
            command.AddInParameter("@SearchName", DbType.String, client.SearchName);
            command.AddInParameter("@TypeIdentity", DbType.String, client.TypeIdentity);
            command.AddInParameter("@NumberIdentity", DbType.String, client.NumberIdentity);
            command.AddInParameter("@Address", DbType.String, client.Address);
            command.AddInParameter("@Phone", DbType.String, client.Phone);
            command.AddInParameter("@Fax", DbType.String, client.Fax);
            command.AddInParameter("@Email", DbType.String, client.Email);
            command.AddInParameter("@State", DbType.String, client.State);
            ExecuteNonQuery(command);
        }

        public DataRow FindByCodeClient(int codeClient)
        {            
            DaCommand command = new DaCommand("ClientFindByCodeClient");
            command.AddInParameter("@CodeUser", DbType.Int32, codeClient);
            return ExecuteDataRow(command);
        }
    }
}

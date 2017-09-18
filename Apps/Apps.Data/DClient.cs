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
        public DataRow Select(EClient eClient)
        {
            DaCommand command = new DaCommand("ClientSelect");
            command.AddInParameter("@CodeClient", DbType.Int32, eClient.CodeClient);
            return ExecuteDataRow(command);
        }

        public void Insert(EClient eClient)
        {
            DaCommand command = new DaCommand("ClientInsert");
            command.AddInParameter("@CodeClient", DbType.Int32, eClient.CodeClient);
            command.AddInParameter("@FirstName", DbType.String, eClient.FirstName);
            command.AddInParameter("@SecondName", DbType.String, eClient.SecondName);
            command.AddInParameter("@FatherLastName", DbType.String, eClient.FatherLastName);
            command.AddInParameter("@MotherLastName", DbType.String, eClient.MotherLastName);
            command.AddInParameter("@LongName", DbType.String, eClient.LongName);
            command.AddInParameter("@ShortName", DbType.String, eClient.ShortName);
            command.AddInParameter("@SearchName", DbType.String, eClient.SearchName);
            command.AddInParameter("@CodeTypeDocumentIdentity", DbType.String, eClient.CodeTypeDocumentIdentity);
            command.AddInParameter("@NumberIdentity", DbType.String, eClient.NumberIdentity);
            command.AddInParameter("@Address", DbType.String, eClient.Address);
            command.AddInParameter("@Phone", DbType.String, eClient.Phone);
            command.AddInParameter("@Fax", DbType.String, eClient.Fax);
            command.AddInParameter("@Email", DbType.String, eClient.Email);
            command.AddInParameter("@State", DbType.String, eClient.State);
            ExecuteNonQuery(command);
        }        

        public void Update(EClient eClient)
        {
            DaCommand command = new DaCommand("ClientUpdate");
            command.AddInParameter("@CodeClient", DbType.Int32, eClient.CodeClient);
            command.AddInParameter("@FirstName", DbType.String, eClient.FirstName);
            command.AddInParameter("@SecondName", DbType.String, eClient.SecondName);
            command.AddInParameter("@FatherLastName", DbType.String, eClient.FatherLastName);
            command.AddInParameter("@MotherLastName", DbType.String, eClient.MotherLastName);
            command.AddInParameter("@LongName", DbType.String, eClient.LongName);
            command.AddInParameter("@ShortName", DbType.String, eClient.ShortName);
            command.AddInParameter("@SearchName", DbType.String, eClient.SearchName);
            command.AddInParameter("@CodeTypeDocumentIdentity", DbType.String, eClient.CodeTypeDocumentIdentity);
            command.AddInParameter("@NumberIdentity", DbType.String, eClient.NumberIdentity);
            command.AddInParameter("@Address", DbType.String, eClient.Address);
            command.AddInParameter("@Phone", DbType.String, eClient.Phone);
            command.AddInParameter("@Fax", DbType.String, eClient.Fax);
            command.AddInParameter("@Email", DbType.String, eClient.Email);
            command.AddInParameter("@State", DbType.String, eClient.State);
            ExecuteNonQuery(command);
        }

        public void Delete(EClient eClient)
        {
            DaCommand command = new DaCommand("ClientDelete");
            command.AddInParameter("@CodeClient", DbType.Int32, eClient.CodeClient);
            ExecuteNonQuery(command);
        }


    }
}

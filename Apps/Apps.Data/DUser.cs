using System;
using System.Data;
using Apps.Entity;

namespace Apps.Data
{
    public class DUser:DData
    {
        public bool Login(EUser user)
        {           
            return true;
        }

        public IDataReader FindByCodeUser(string CodeUser)
        {
            DaCommand command = new DaCommand("usp_select_User");
            command.AddInParameter("@CodeUser", DbType.String, CodeUser);
            return ExecuteDataReader(command);
        }

        public void Insert(EUser user)
        {
            DaCommand command = new DaCommand("usp_insert_User");
            command.AddInParameter("@CodeUser", DbType.String, user.CodeUser);
            command.AddInParameter("@Name", DbType.String, user.Name);
            command.AddInParameter("@Password", DbType.String, user.Password);
            command.AddInParameter("@Estado", DbType.String, user.Estado);
            ExecuteCommand(command);
        }
    }
}

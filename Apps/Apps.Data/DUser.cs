using System;
using System.Data;
using Apps.Entity;

namespace Apps.Data
{
    public class DUser:DData
    {
        public DataRow FindByCodeUser(string CodeUser)
        {
            DaCommand command = new DaCommand("UserFindByCodeUser");
            command.AddInParameter("@CodeUser", DbType.String, CodeUser);
            return ExecuteDataRow(command);
        }

        public void Insert(EUser user)
        {
            DaCommand command = new DaCommand("UserInsert");
            command.AddInParameter("@CodeUser", DbType.String, user.CodeUser);
            command.AddInParameter("@Name", DbType.String, user.Name);
            command.AddInParameter("@Password", DbType.String, user.Password);
            command.AddInParameter("@Email", DbType.String, user.Email);
            command.AddInParameter("@Profile", DbType.String, user.Profile);
            command.AddInParameter("@State", DbType.String, user.State);
            ExecuteCommand(command);
        }

        public void Delete(string CodeUser)
        {
            DaCommand command = new DaCommand("UserDelete");
            command.AddInParameter("@CodeUser", DbType.String, CodeUser);
            ExecuteCommand(command);
        }

        public void Update(EUser user)
        {
            DaCommand command = new DaCommand("UserUpdate");
            command.AddInParameter("@CodeUser", DbType.String, user.CodeUser);
            command.AddInParameter("@Name", DbType.String, user.Name);            
            command.AddInParameter("@Email", DbType.String, user.Email);
            command.AddInParameter("@Profile", DbType.String, user.Profile);
            command.AddInParameter("@State", DbType.String, user.State);
            ExecuteCommand(command);
        }
    }
}

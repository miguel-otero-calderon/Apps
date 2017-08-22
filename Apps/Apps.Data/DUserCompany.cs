using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Apps.Entity;

namespace Apps.Data
{
    public class DUserCompany:DData
    {
        public DataRow Select(EUserCompany userCompany)
        {
            DaCommand command = new DaCommand("UserCompanySelect");
            command.AddInParameter("@CodeUser", DbType.String, userCompany.CodeUser);
            command.AddInParameter("@CodeCompany", DbType.String, userCompany.CodeCompany);
            return ExecuteDataRow(command);
        }

        public void Insert(EUserCompany userCompany)
        {
            DaCommand command = new DaCommand("UserCompanyInsert");
            command.AddInParameter("@CodeUser", DbType.String, userCompany.CodeUser);
            command.AddInParameter("@CodeCompany", DbType.String, userCompany.CodeCompany);
            ExecuteNonQuery(command);
        }

        public void Delete(EUserCompany userCompany)
        {
            DaCommand command = new DaCommand("UserCompanyDelete");
            command.AddInParameter("@CodeUser", DbType.String, userCompany.CodeUser);
            command.AddInParameter("@CodeCompany", DbType.String, userCompany.CodeCompany);
            ExecuteNonQuery(command);
        }

        public DataTable SelectByUser(EUser user)
        {
            DaCommand command = new DaCommand("UserCompanySelectByUser");
            command.AddInParameter("@CodeUser", DbType.String, user.CodeUser);
            return ExecuteDataTable(command);
        }

        public void DeleteByUser(EUser user)
        {
            string CodeCompanyList = string.Join(",", user.Companies);
            DaCommand command = new DaCommand("UserCompanyDeleteByUser");
            command.AddInParameter("@CodeUser", DbType.String, user.CodeUser);            
            ExecuteDataTable(command);
        }
    }
}

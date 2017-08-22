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
            command.AddInParameter("@CodeCompany", DbType.String, userCompany.CodeCompanny);
            return ExecuteDataRow(command);
        }

        public void Insert(EUserCompany userCompany)
        {
            DaCommand command = new DaCommand("UserCompanyInsert");
            command.AddInParameter("@CodeUser", DbType.String, userCompany.CodeUser);
            command.AddInParameter("@CodeCompany", DbType.String, userCompany.CodeCompanny);
            ExecuteNonQuery(command);
        }

        public void Delete(EUserCompany userCompany)
        {
            DaCommand command = new DaCommand("UserCompanyDelete");
            command.AddInParameter("@CodeUser", DbType.String, userCompany.CodeUser);
            command.AddInParameter("@CodeCompany", DbType.String, userCompany.CodeCompanny);
            ExecuteNonQuery(command);
        }
    }
}

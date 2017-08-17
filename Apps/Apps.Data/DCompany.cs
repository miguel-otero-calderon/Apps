using System.Data;
using Apps.Entity;

namespace Apps.Data
{
    public class DCompany:DData
    {        
        public DataTable GetCompaniesByUser(EUser user)
        {
            DaCommand command = new DaCommand("UserGetCompanies");
            command.AddInParameter("@CodeUser", DbType.String, user.CodeUser);
            return ExecuteDataTable(command);
        }

        public void InsertUserCompany(EUser user)
        {
            string CodeCompanyList = string.Join(",", user.Companies);
            DaCommand command = new DaCommand("UserCompanyInsert");
            command.AddInParameter("@CodeUser", DbType.String, user.CodeUser);
            command.AddInParameter("@CodeCompanyList", DbType.String, CodeCompanyList);
            ExecuteNonQuery(command);
        }

        public void DeletetUserCompany(EUser user)
        {
            string CodeCompanyList = string.Join(",", user.Companies);
            DaCommand command = new DaCommand("UserCompanyDelete");
            command.AddInParameter("@CodeUser", DbType.String, user.CodeUser);
            command.AddInParameter("@CodeCompanyList", DbType.String, CodeCompanyList);
            ExecuteDataTable(command);
        }
    }
}

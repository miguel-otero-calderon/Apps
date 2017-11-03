using System.Data;
using Apps.Entity;

namespace Apps.Data
{
    public class DCompany:DData
    {               
        public DataRow Select(ECompany company)
        {
            DaCommand command = new DaCommand("dbo.CompanySelect");
            command.AddInParameter("@CodeCompany", DbType.String, company.CodeCompany);
            return ExecuteDataRow(command);
        }

        public void Delete(ECompany company)
        {
            DaCommand command = new DaCommand("dbo.CompanyDelete");
            command.AddInParameter("@CodeCompany", DbType.String, company.CodeCompany);
            ExecuteNonQuery(command);
        }

        public void Insert(ECompany company)
        {
            DaCommand command = new DaCommand("dbo.CompanyInsert");
            command.AddInParameter("@CodeCompany", DbType.String, company.CodeCompany);
            command.AddInParameter("@CodeCorporation", DbType.String, company.CodeCorporation);
            command.AddInParameter("@LongName", DbType.String, company.LongName);
            command.AddInParameter("@ShortName", DbType.String, company.ShortName);
            command.AddInParameter("@Ruc", DbType.String, company.Ruc);
            command.AddInParameter("@Address", DbType.String, company.Address);
            command.AddInParameter("@PageWeb", DbType.String, company.PageWeb);
            command.AddInParameter("@Phone", DbType.String, company.Phone);
            command.AddInParameter("@Fax", DbType.String, company.Fax);
            command.AddInParameter("@Logo", DbType.String, company.Logo);
            command.AddInParameter("@State", DbType.Int16, company.State);
            ExecuteNonQuery(command);
        }

        public void Update(ECompany company)
        {
            DaCommand command = new DaCommand("dbo.CompanyUpdate");
            command.AddInParameter("@CodeCompany", DbType.String, company.CodeCompany);
            command.AddInParameter("@CodeCorporation", DbType.String, company.CodeCorporation);
            command.AddInParameter("@LongName", DbType.String, company.LongName);
            command.AddInParameter("@ShortName", DbType.String, company.ShortName);
            command.AddInParameter("@Ruc", DbType.String, company.Ruc);
            command.AddInParameter("@Address", DbType.String, company.Address);
            command.AddInParameter("@PageWeb", DbType.String, company.PageWeb);
            command.AddInParameter("@Phone", DbType.String, company.Phone);
            command.AddInParameter("@Fax", DbType.String, company.Fax);
            command.AddInParameter("@Logo", DbType.String, company.Logo);
            command.AddInParameter("@State", DbType.Int16, company.State);
            ExecuteNonQuery(command);
        }

        public DataTable List()
        {
            DaCommand command = new DaCommand("dbo.CompanyList");
            return ExecuteDataTable(command);
        }
    }
}

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Data;
using Apps.Entity;
using Apps.Extension;

namespace Apps.Business
{
    public class BCompany
    {
        DCompany data = new DCompany();
        BAudit audit = new BAudit();

        //public List<ECompany> GetCompaniesByUser(EUser user)
        //{
        //    System.Data.DataTable table = data.GetCompaniesByUser(user);
        //    int rows = table.Rows.Count;
        //    List<string> columns = table.GetColumns();
        //    List<ECompany> companies = new List<ECompany>();
        //    for (int i = 0; i <= rows - 1; i++)
        //    {
        //        System.Data.DataRow row = table.Rows[i];
        //        ECompany item = new ECompany(row, columns);
        //        companies.Add(item);
        //    }
        //    return companies;
        //}

        //public void SetCompaniesByUser(EUser user)
        //{
        //    List<string> companies = user.Companies;
        //    List<string> database = data.GetCompaniesByUser(user).GetList("CodeCompany");            
        //    List<string> news = new List<string>();
        //    foreach (var item in companies)
        //    {
        //        if (database.Exists(c=>c.Equals(item)) == false)
        //            news.Add(item);
        //    }
        //    List<string> deletes = new List<string>();
        //    foreach (var item in database)
        //    {
        //        if (companies.Exists(c => c.Equals(item)) == false)
        //            deletes.Add(item);
        //    }
        //    user.Companies = deletes;
        //    DeletetUserCompany(user);

        //    user.Companies = news;
        //    InsertUserCompany(user);
        //}

        //public void InsertUserCompany(EUser user)
        //{
        //    data.InsertUserCompany(user);
        //}

        //public void DeletetUserCompany(EUser user)
        //{
        //    data.DeletetUserCompany(user);
        //}

        public ECompany Select(ECompany company)
        {
            ECompany result = null;
            DataRow row = data.Select(company);
            if(row != null)
                result = new ECompany(row, row.GetColumns());
            return result;
        }

        public void Delete(ECompany company)
        {
            data.Delete(company);
            company.Audit.TypeEvent = "Delete";
            audit.Insert(company.Audit);
        }

        public void Insert(ECompany company)
        {            
            data.Insert(company);
            company.Audit.TypeEvent = "Insert";
            audit.Insert(company.Audit);
        }

        public void Update(ECompany company)
        {
            data.Update(company);
            company.Audit.TypeEvent = "Update";
            audit.Insert(company.Audit);
        }
    }
}

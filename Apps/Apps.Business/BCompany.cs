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

        public List<ECompany> GetCompaniesByUser(EUser user)
        {
            System.Data.DataTable table = data.GetCompaniesByUser(user);
            int rows = table.Rows.Count;
            List<string> columns = table.GetColumns();
            List<ECompany> companies = new List<ECompany>();
            for (int i = 0; i <= rows - 1; i++)
            {
                System.Data.DataRow row = table.Rows[i];
                ECompany item = new ECompany(row, columns);
                companies.Add(item);
            }
            return companies;
        }

        public void SetCompaniesByUser(EUser user)
        {
            List<string> companies = user.Companies;
            List<string> database = data.GetCompaniesByUser(user).GetValues("CodeCompany");            
            List<string> news = new List<string>();
            foreach (var item in companies)
            {
                if (!database.Exists(c=>c.Equals(item)))
                    news.Add(item);
            }
            List<string> deletes = new List<string>();
            foreach (var item in database)
            {
                if (!companies.Exists(c => c.Equals(item)))
                    deletes.Add(item);
            }
            user.Companies = deletes;
            DeletetUserCompany(user);

            user.Companies = news;
            InsertUserCompany(user);
        }

        public void InsertUserCompany(EUser user)
        {
            data.InsertUserCompany(user);
        }

        public void DeletetUserCompany(EUser user)
        {
            data.DeletetUserCompany(user);
        }
    }
}

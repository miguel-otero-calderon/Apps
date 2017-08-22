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
    public class BUserCompany
    {
        DUserCompany d = new DUserCompany();

        public EUserCompany Select(EUserCompany userCompany)
        {
            EUserCompany result = null;
            DataRow row = d.Select(userCompany);
            if(row != null)
                result = new EUserCompany(row);

            return result;
        }

        public void Insert(EUserCompany userCompany)
        {
            d.Insert(userCompany);
        }

        public void Delete(EUserCompany userCompany)
        {
            d.Delete(userCompany);
        }

        public List<ECompany> SelectByUser(EUser user)
        {
            DataTable table = d.SelectByUser(user);
            int rows = table.Rows.Count;
            List<string> columns = table.GetColumns();
            List<ECompany> companies = new List<ECompany>();
            for (int i = 0; i <= rows - 1; i++)
            {
                DataRow row = table.Rows[i];
                ECompany item = new ECompany(row, columns);
                companies.Add(item);
            }
            return companies;
        }

        public void DeleteByUser(EUser user)
        {
            d.DeleteByUser(user);
        }
    }
}

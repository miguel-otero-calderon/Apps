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

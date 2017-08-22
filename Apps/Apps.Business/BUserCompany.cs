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
            DataRow row = d.Select(userCompany);
            EUserCompany result = new EUserCompany(row);
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
    }
}

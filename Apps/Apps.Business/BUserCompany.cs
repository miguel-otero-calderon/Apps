using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Data;
using Apps.Entity;
using Apps.Extension;
using System.Transactions;

namespace Apps.Business
{
    public class BUserCompany
    {
        DUserCompany dUserCompany = new DUserCompany();

        public EUserCompany Select(EUserCompany userCompany)
        {
            EUserCompany result = null;
            DataRow row = dUserCompany.Select(userCompany);
            if(row != null)
                result = new EUserCompany(row);

            return result;
        }

        public void Insert(EUserCompany userCompany)
        {
            dUserCompany.Insert(userCompany);
        }

        public void Delete(EUserCompany userCompany)
        {
            dUserCompany.Delete(userCompany);
        }

        public List<ECompany> SelectByUser(EUser eUser)
        {
            List<ECompany> listECompanies = new List<ECompany>();
            List<string> listColumns;
            DataTable dataTable;
            int rowsCount;
            DataRow dataRow;
            ECompany eCompany;
            dataTable = dUserCompany.SelectByUser(eUser);
            rowsCount = dataTable.Rows.Count;
            listColumns = dataTable.GetColumns();            
            for (int i = 0; i <= rowsCount - 1; i++)
            {
                dataRow = dataTable.Rows[i];
                eCompany = new ECompany(dataRow, listColumns);
                listECompanies.Add(eCompany);
            }
            return listECompanies;
        }

        public void DeleteByUser(EUser eUser)
        {
            EUserCompany eUserCompany;
            List<ECompany> listECompanies;
            short listCount;
            using (TransactionScope ts = new TransactionScope())
            {
                listECompanies = SelectByUser(eUser);
                listCount = Convert.ToInt16(listECompanies.Count);

                for (short i = 0; i <= listCount - 1; i++)
                {
                    eUserCompany = new EUserCompany();
                    eUserCompany.CodeUser = eUser.CodeUser;
                    eUserCompany.CodeCompany = listECompanies[i].CodeCompany;
                    Delete(eUserCompany);
                }
                ts.Complete();
            }
        }

        public void UpdateByUser(EUser eUser)
        {
            EUserCompany eUserCompany;
            List<string> Companies;
            short listCount;
            using (TransactionScope ts = new TransactionScope())
            {                
                DeleteByUser(eUser);
                Companies = eUser.Companies;
                listCount = Convert.ToInt16(Companies.Count);

                for (short i = 0; i <= listCount - 1; i++)
                {
                    eUserCompany = new EUserCompany();
                    eUserCompany.CodeUser = eUser.CodeUser;
                    eUserCompany.CodeCompany = Companies[i];
                    Insert(eUserCompany);
                }
                ts.Complete();
            }
        }
    }
}

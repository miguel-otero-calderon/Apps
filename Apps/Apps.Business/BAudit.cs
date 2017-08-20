using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Entity;
using Apps.Extension;
using Apps.Data;
using System.Data;

namespace Apps.Business
{
    public class BAudit
    {
        DAudit d = new DAudit();
        public void Insert(EAudit audit)
        {
            d.Insert(audit);
        }

        public List<EAudit> Select(EAudit audit, short top = 10)
        {
            DataTable table = d.Select(audit, top);
            List<string> columns = table.GetColumns();
            int rows = table.Rows.Count;
            List<EAudit> list = new List<EAudit>();
            for (int i = 0; i <= rows - 1; i++)
            {
                DataRow row = table.Rows[i];
                EAudit item = new EAudit(row, columns);
                list.Add(item);
            }
            return list;
        }
    }
}

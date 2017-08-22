using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Apps.Entity
{
    public class EUserCompany
    {
        public string CodeUser { get; set; }
        public string CodeCompany { get; set; }
        public EUserCompany() { }
        public EUserCompany(DataRow row)
        {
            CodeUser = Convert.ToString( row["CodeUser"]);
            CodeCompany = Convert.ToString(row["CodeCompany"]);
        }
    }
}

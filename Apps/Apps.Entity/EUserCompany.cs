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
        public string CodeCompanny { get; set; }
        private EUserCompany() { }
        public EUserCompany(DataRow row)
        {
            CodeUser = Convert.ToString( row["CodeUser"]);
            CodeCompanny = Convert.ToString(row["CodeCompanny"]);
        }
    }
}

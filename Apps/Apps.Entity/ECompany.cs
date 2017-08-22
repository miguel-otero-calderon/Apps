using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Extension;

namespace Apps.Entity
{
    public class ECompany : EEntity
    {
        private EAudit audit;
        public override EAudit Audit
        {
            get
            {
                if (audit == null)
                    audit = new EAudit(CodeCompany: this.CodeCompany, CodeEntity: "Company", Code: CodeCompany, Sequence: 0);
                return audit;
            }
            set
            {
                audit = value;
            }
        }
        public string CodeCompany { get; set; }
        public string CodeCorporation { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string Ruc { get; set; }
        public string Address { get; set; }
        public string PageWeb { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Logo { get; set; }
        public short State { get; set; }
        public ECompany() { }
        public ECompany(DataRow row, List<string> columns)
        {
            if (columns.Contains("CodeCompany") && row.Validate("CodeCompany"))
                CodeCompany = Convert.ToString(row["CodeCompany"]);

            if (columns.Contains("CodeCorporation") && row.Validate("CodeCorporation"))
                CodeCorporation = Convert.ToString(row["CodeCorporation"]);

            if (columns.Contains("LongName") && row.Validate("LongName"))
                LongName = Convert.ToString(row["LongName"]);

            if (columns.Contains("ShortName") && row.Validate("ShortName"))
                ShortName = Convert.ToString(row["ShortName"]);

            if (columns.Contains("Ruc") && row.Validate("Ruc"))
                Ruc = Convert.ToString(row["Ruc"]);

            if (columns.Contains("Address") && row.Validate("Address"))
                Address = Convert.ToString(row["Address"]);

            if (columns.Contains("PageWeb") && row.Validate("PageWeb"))
                PageWeb = Convert.ToString(row["PageWeb"]);

            if (columns.Contains("Phone") && row.Validate("Phone"))
                Phone = Convert.ToString(row["Phone"]);

            if (columns.Contains("Fax") && row.Validate("Fax"))
                Fax = Convert.ToString(row["Fax"]);

            if (columns.Contains("Logo") && row.Validate("Logo"))
                Logo = Convert.ToString(row["Logo"]);

            if (columns.Contains("State") && row.Validate("State"))
                State = Convert.ToInt16(row["State"]);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Apps.Extension;

namespace Apps.Entity
{
    public class ECorporation : EEntity
    {
        private EAudit audit;
        public override EAudit Audit
        {
            get
            {
                if (audit == null)
                    audit = new EAudit(CodeCompany: "00", CodeEntity: "Corporation", Code: CodeCorporation, Sequence: 0);
                return audit;
            }
            set
            {
                audit = value;
            }
        }
        public string CodeCorporation { get; set; }
        public string Name { get; set; }
        public short State { get; set; }
        public ECorporation() { }
        public ECorporation (DataRow row, List<string> columns)
        {
            if (columns.Contains("CodeCorporation") && row.Validate("CodeCorporation"))
                CodeCorporation = Convert.ToString(row["CodeCorporation"]);

            if (columns.Contains("Name") && row.Validate("Name"))
                Name = Convert.ToString(row["Name"]);

            if (columns.Contains("State") && row.Validate("State"))
                State = Convert.ToInt16(row["State"]);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Apps.Extension;

namespace Apps.Entity
{
    public class EClient : Entity
    {
        public int CodeClient { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FatherLastName { get; set; }
        public string MotherLastName { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string SearchName { get; set; }
        public string TypeIdentity { get; set; }
        public string NumberIdentity { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public short State { get; set; }
        private EAudit audit;
        public override EAudit Audit
        {
            get
            {
                if (audit == null)
                    audit = new EAudit();
                audit.CodeCompany = "00";
                audit.CodeEntity = "Client";
                audit.Code = CodeClient.ToString();
                audit.Sequence = 0;
                return audit;
            }
            set
            {
                audit = value;
            }
        }
        public EClient(DataRow row, List<string> columns)
        {
            if (columns.Contains("CodeClient") && row.Validate("CodeClient"))
                CodeClient = Convert.ToInt32(row["CodeClient"]);

            if (columns.Contains("FirstName") && row.Validate("FirstName"))
                FirstName = Convert.ToString(row["FirstName"]);

            if (columns.Contains("SecondName") && row.Validate("SecondName"))
                SecondName = Convert.ToString(row["SecondName"]);

            if (columns.Contains("FatherLastName") && row.Validate("FatherLastName"))
                FatherLastName = Convert.ToString(row["FatherLastName"]);

            if (columns.Contains("MotherLastName") && row.Validate("MotherLastName"))
                MotherLastName = Convert.ToString(row["MotherLastName"]);

            if (columns.Contains("LongName") && row.Validate("LongName"))
                LongName = Convert.ToString(row["LongName"]);

            if (columns.Contains("ShortName") && row.Validate("ShortName"))
                ShortName = Convert.ToString(row["ShortName"]);

            if (columns.Contains("SearchName") && row.Validate("SearchName"))
                SearchName = Convert.ToString(row["SearchName"]);

            if (columns.Contains("TypeIdentity") && row.Validate("TypeIdentity"))
                TypeIdentity = Convert.ToString(row["TypeIdentity"]);

            if (columns.Contains("NumberIdentity") && row.Validate("NumberIdentity"))
                NumberIdentity = Convert.ToString(row["NumberIdentity"]);

            if (columns.Contains("Address") && row.Validate("Address"))
                Address = Convert.ToString(row["Address"]);

            if (columns.Contains("Phone") && row.Validate("Phone"))
                Phone = Convert.ToString(row["Phone"]);

            if (columns.Contains("Fax") && row.Validate("Fax"))
                Fax = Convert.ToString(row["Fax"]);

            if (columns.Contains("Email") && row.Validate("Email"))
                Email = Convert.ToString(row["Email"]);

            if (columns.Contains("State") && row.Validate("State"))
                State = Convert.ToInt16(row["State"]);
        }
    }
}

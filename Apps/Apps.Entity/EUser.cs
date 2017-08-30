using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Apps.Extension;
using Apps.Entity;


namespace Apps.Entity
{
    public class EUser : EEntity
    {
        public string CodeUser { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public short State { get; set; }
        private EAudit audit;
        private List<string> companies;
        public List<string> Companies
        {
            get
            {
                if (companies == null)
                    companies = new List<string>();
                return companies;
            }
            set
            {
                companies = value;
            }
        }
        public override EAudit Audit
        {
            get {
                if (audit == null)
                    audit = new EAudit(CodeCompany :string.Empty, CodeEntity : "User", Code : CodeUser, Sequence : 0);
                return audit;
            }
            set
            {
                audit = value;
            }
        }
        public EUser() { }
        public EUser(DataRow row,List<string>columns)
        {
            if (columns.Contains("CodeUser") && row.Validate("CodeUser"))
                CodeUser = Convert.ToString(row["CodeUser"]);

            if (columns.Contains("Name") && row.Validate("Name"))
                Name = Convert.ToString(row["Name"]);

            if (columns.Contains("Password") && row.Validate("Password"))
                Password = Convert.ToString(row["Password"]);

            if (columns.Contains("Email") && row.Validate("Email"))
                Email = Convert.ToString(row["Email"]);

            if (columns.Contains("State") && row.Validate("State"))
                State = Convert.ToInt16(row["State"]);
        }
    }
}

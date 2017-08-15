using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Apps.Entity.Extension;
using Apps.Entity;


namespace Apps.Entity
{
    public class EUser : Entity
    {
        public string CodeUser { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Profile { get; set; }
        public short State { get; set; }
        private EAudit audit;
        public override EAudit Audit
        {
            get {
                if (audit == null)
                    audit = new EAudit();
                audit.CodeCompany = "00";
                audit.CodeEntity = "User";
                audit.Code = CodeUser;
                audit.Sequence = 0;
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

            if (columns.Contains("Profile") && row.Validate("Profile"))
                Profile = Convert.ToString(row["Profile"]);

            if (columns.Contains("State") && row.Validate("State"))
                State = Convert.ToInt16(row["State"]);
        }
    }
}

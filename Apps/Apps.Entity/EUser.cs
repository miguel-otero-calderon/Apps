using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Apps.Entity.Extension;


namespace Apps.Entity
{
    public class EUser:Entity
    {
        public string CodeUser { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public short Estado { get; set; }
        public EUser() { }
        public EUser(DataRow row,List<string>columns)
        {
            if (columns.Contains("CodeUser") && row.Validate("CodeUser"))
                CodeUser = Convert.ToString(row["CodeUser"]);

            if (columns.Contains("Name") && row.Validate("Name"))
                Name = Convert.ToString(row["Name"]);

            if (columns.Contains("Password") && row.Validate("Password"))
                Password = Convert.ToString(row["Password"]);

            if (columns.Contains("Estado") && row.Validate("Estado"))
                Estado = Convert.ToInt16(row["Estado"]);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Entity;
using Apps.Data;
using Apps.Data.Extension;
using System.Data;

namespace Apps.Business
{
    public class BUser
    {
        DUser data = new DUser();
        BAudit audit = new BAudit();
        public bool Login(EUser user)
        {            
            return false;
        }

        public EUser FindByCodeUser(string CodeUser)
        {
            IDataReader reader = data.FindByCodeUser(CodeUser);
            DataTable table = new DataTable();
            table.Load(reader);
            reader.Close();
            if (table.Rows.Count == 1)
            {
                DataRow row = table.Rows[0];
                List<string> columns = table.GetColumns();
                EUser user = new EUser(row, columns);
                return user;
            }
            else
            {
                return null;
            }                                    
        }

        public void Insert(EUser user)
        {
            data.Insert(user);
            user.Audit.TypeEvent = "New";
            audit.Insert(user.Audit);
        }

        public void Delete(EUser user)
        {
            data.Delete(user.CodeUser);
            user.Audit.TypeEvent = "Delete";
            audit.Insert(user.Audit);
        }

        public void Update(EUser user)
        {
            data.Update(user);
            user.Audit.TypeEvent = "Update";
            audit.Insert(user.Audit);
        }
    }
}

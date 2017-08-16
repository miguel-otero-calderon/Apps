using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Entity;
using Apps.Data;
using Apps.Data.Extension;
using Apps.Util;
using System.Data;

namespace Apps.Business
{
    public class BUser
    {
        DUser data = new DUser();
        BAudit audit = new BAudit();
        public bool Login(EUser user)
        {
            EUser find = FindByCodeUser(user.CodeUser);
            if (find == null)
                throw new Exception("El usuario no existe.");            

            if (user.Password == CalculateHash(user))
                return true;
            else
                return false;
        }

        public EUser FindByCodeUser(string CodeUser)
        {
            EUser user;
            DataRow row = data.FindByCodeUser(CodeUser);
            if (row != null)
            {
                List<string> columns = row.Table.GetColumns();
                user = new EUser(row, columns);
                return user;
            }
            else
                return null;                     
        }

        private string CalculateHash(EUser user)
        {
            string text = string.Concat(user.CodeUser.ToLower().Trim(), user.Password.Trim());
            return Security.CalculateHash(text);
        }

        public void Insert(EUser user)
        {
            user.Password = CalculateHash(user);
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

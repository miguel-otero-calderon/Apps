using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Entity;
using Apps.Data;
using Apps.Extension;
using Apps.Util;
using System.Data;

namespace Apps.Business
{
    public class BUser:BBusiness
    {
        DUser data = new DUser();
        BAudit audit = new BAudit();
        public bool Login(EUser user)
        {
            EUser _user = Select(user);
            if (_user == null)
            {
                Message = String.Format("El usuario '{0}' no existe.",user.CodeUser);
                return false;
            }

            if (_user.Password == CalculateHash(user))
            {
                if(_user.State == 0)
                {
                    Message = String.Format("El usuario '{0}' no se encuentra 'Activo'.", user.CodeUser);
                    return false;
                }
                return true;
            }                
            else
            {
                Message = "Password incorrecto.";
                return false;
            }                
        }

        public EUser Select(EUser user)
        {
            EUser _user;
            DataRow row = data.Select(user);
            if (row != null)
            {
                List<string> columns = row.Table.GetColumns();
                _user = new EUser(row, columns);
                return _user;
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

        public List<ECompany> GetCompanies(EUser user)
        {
            BCompany bCompany = new BCompany();
            return bCompany.GetCompaniesByUser(user);
        }
    }
}

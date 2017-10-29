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
        DUser dUser = new DUser();
        BAudit bAudit = new BAudit();
        BUserCompany bUserCompany = new BUserCompany();

        public bool Login(EUser user)
        {
            EUser eUser = Select(user);
            if (eUser == null)
            {
                Message = String.Format("El usuario '{0}' no existe.",user.CodeUser);
                return false;
            }

            if (eUser.Password == CalculateHash(user))
            {
                if(eUser.State == 0)
                {
                    Message = String.Format("El usuario '{0}' no se encuentra 'Activo'.", user.CodeUser);
                    return false;
                }
                Message = "Ok";
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
            EUser eUser;
            DataRow row = dUser.Select(user);
            if (row != null)
            {
                List<string> columns = row.Table.GetColumns();
                eUser = new EUser(row, columns);
                return eUser;
            }
            else
                return null;                     
        }

        public string CalculateHash(EUser user)
        {
            if (user.Password == null)
                user.Password = string.Empty;
            string text = string.Concat(user.CodeUser.ToLower().Trim(), user.Password.Trim());
            return Security.CalculateHash(text);
        }

        public void Insert(EUser eUser)
        {
            eUser.PasswordHash = CalculateHash(eUser);
            eUser.Validar();
            dUser.Insert(eUser);
            eUser.Audit.TypeEvent = "Insert";
            bAudit.Insert(eUser.Audit);
        }

        public void Delete(EUser eUser)
        {
            bUserCompany.DeleteByUser(eUser);
            dUser.Delete(eUser);
            eUser.Audit.TypeEvent = "Delete";
            bAudit.Insert(eUser.Audit);
        }

        public void Update(EUser eUser)
        {
            eUser.Validar();
            dUser.Update(eUser);
            eUser.Audit.TypeEvent = "Update";
            bAudit.Insert(eUser.Audit);
        }

        public List<EUser> List()
        {
            EUser eUser = null;
            DataTable table = null;
            List<EUser> list = null;
            List<string> columns = null;
            int rowsCount = 0;

            list = new List<EUser>();
            table = dUser.List();
            rowsCount = table.Rows.Count;
            columns = table.GetColumns();

            foreach(DataRow row in table.Rows)
            {
                eUser = new EUser(row, columns);
                list.Add(eUser);
            }

            return list;
        }
    }
}

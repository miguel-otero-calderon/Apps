using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Data;
using Apps.Entity;
using Apps.Extension;

namespace Apps.Business
{
    public class BCompany : BBusiness
    {
        DCompany dCompany = new DCompany();
        BAudit bAudit = new BAudit();

        public ECompany Select(ECompany company)
        {
            ECompany result = null;
            DataRow row = dCompany.Select(company);
            if(row != null)
                result = new ECompany(row, row.GetColumns());
            return result;
        }

        public void Delete(ECompany company)
        {
            dCompany.Delete(company);
            if (dCompany.ExistsReference())
            {
                Message = string.Format("La empresa '{0}' tiene referencias en el Sistema, no se puede eliminar el registro.",company.LongName);
                throw new Exception(Message);
            }
            company.Audit.TypeEvent = "Delete";
            bAudit.Insert(company.Audit);
        }

        public void Insert(ECompany eCompany)
        {
            eCompany.Validar();
            dCompany.Insert(eCompany);
            if (dCompany.ExistsPrimaryKey())
            {
                Message = string.Format("El código de empresa '{0}' ya existe en el Sistema, no se puede crear el registro.", eCompany.CodeCompany);
                throw new Exception(Message);
            }
            if (dCompany.ExistsReference())
            {
                Message = string.Format("El código de Corporation '{0}' no existe en el Sistema", eCompany.CodeCorporation);
                throw new Exception(Message);
            }
            eCompany.Audit.TypeEvent = "Insert";
            bAudit.Insert(eCompany.Audit);
        }

        public void Update(ECompany eCompany)
        {
            eCompany.Validar();
            dCompany.Update(eCompany);
            eCompany.Audit.TypeEvent = "Update";
            bAudit.Insert(eCompany.Audit);
        }
    }
}

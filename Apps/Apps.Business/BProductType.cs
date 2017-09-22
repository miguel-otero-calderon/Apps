using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Entity;
using Apps.Data;
using Apps.Extension;
using System.Transactions;

namespace Apps.Business
{
    public class BProductType: BBusiness
    {
        DProductType dProductType = new DProductType();
        BAudit bAudit = new BAudit();

        public EProductType Select(EProductType eProductType)
        {
            EProductType eResult = null;
            DataRow dataRow;
            List<string> listColumns;
            dataRow = dProductType.Select(eProductType);
            if (dataRow != null)
            {
                listColumns = dataRow.GetColumns();
                eResult = new EProductType(dataRow, listColumns);
            }
            return eResult;
        }

        public void Insert(EProductType eProductType)
        {
            eProductType.Validar();
            dProductType.Insert(eProductType);
            if (dProductType.ExistsPrimaryKey())
            {
                Message = string.Format("El código de tipo de producto '{0}' ya existe en el Sistema, no se puede crear el registro.", eProductType.CodeProductType);
                throw new Exception(Message);
            }
            if (dProductType.ExistsReference())
            {
                Message = string.Format("El código de Sunat '{0}' no existe en el Sistema", eProductType.CodeSunatExistence);
                throw new Exception(Message);
            }
            eProductType.Audit.TypeEvent = "Insert";
            bAudit.Insert(eProductType.Audit);
        }

        public void Update(EProductType eProductType)
        {
            eProductType.Validar();
            dProductType.Update(eProductType);
            eProductType.Audit.TypeEvent = "Update";
            bAudit.Insert(eProductType.Audit);
        }

        public void Delete(EProductType eProductType)
        {
            dProductType.Delete(eProductType);
            if (dProductType.ExistsReference())
            {
                Message = string.Format("El tipo de Producto '{0}' tiene referencias en el Sistema, no se puede eliminar el registro.", eProductType.Description);
                throw new Exception(Message);
            }
            eProductType.Audit.TypeEvent = "Delete";
            bAudit.Insert(eProductType.Audit);
        }
    }
}

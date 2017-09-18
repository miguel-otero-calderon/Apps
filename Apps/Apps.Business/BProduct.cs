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
    public class BProduct : BBusiness
    {
        DProduct dProduct = new DProduct();
        BAudit bAudit = new BAudit();

        public EProduct Select(EProduct eProduct)
        {
            EProduct eResult = null;
            DataRow dataRow;
            List<string> listColumns;
            dataRow = dProduct.Select(eProduct);
            if (dataRow != null)
            {
                listColumns = dataRow.GetColumns();
                eResult = new EProduct(dataRow, listColumns);
            }
            return eResult;
        }

        public EProduct Insert(EProduct eProduct)
        {
            EProduct eResult = null;
            BSequence bSequence = new BSequence();
            ESequence eSequence = new ESequence(eProduct);
            int correlative = 0;
            correlative = bSequence.GetCorrelative(eSequence);
            eProduct.CodeProduct = correlative.ToString("0000000000");
            eProduct.Validar();
            dProduct.Insert(eProduct);

            if (dProduct.ExistsPrimaryKey())
            {
                Message = string.Format("El código de Producte '{0}' ya existe en el Sistema, no se puede crear el registro.", eProduct.CodeProduct);
                throw new Exception(Message);
            }
            if (dProduct.ExistsReference())
            {
                Message = string.Format("Se intento grabar una llave foranea incorrecta '{0}', no se puede crear el registro.", eProduct.CodeProduct);
                throw new Exception(Message);
            }
            correlative++;
            eSequence.Correlative = correlative;
            bSequence.SetCorrelativo(eSequence);

            eResult = Select(eProduct);

            eProduct.Audit.Code = eResult.CodeProduct.ToString();
            eProduct.Audit.TypeEvent = "Insert";
            bAudit.Insert(eProduct.Audit);

            return eResult;
        }

        public EProduct Update(EProduct eProduct)
        {
            EProduct eResult;
            eProduct.Validar();
            dProduct.Update(eProduct);

            eProduct.Audit.TypeEvent = "Update";
            bAudit.Insert(eProduct.Audit);

            eResult = Select(eProduct);
            return eResult;
        }

        public void Delete(EProduct eProduct)
        {
            dProduct.Delete(eProduct);
            if (dProduct.ExistsReference())
            {
                Message = string.Format("El Producte '{0}' tiene referencias en el Sistema, no se puede eliminar el registro.", eProduct.CodeProduct);
                throw new Exception(Message);
            }
            eProduct.Audit.TypeEvent = "Delete";
            bAudit.Insert(eProduct.Audit);
        }
    }
}

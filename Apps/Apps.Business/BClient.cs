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
    public class BClient:BBusiness
    {
        DClient dClient = new DClient();
        BAudit bAudit = new BAudit();

        public EClient Select(EClient eClient)
        {
            EClient eResult = null;
            DataRow dataRow;
            List<string> listColumns;
            dataRow = dClient.Select(eClient);
            if (dataRow != null)
            {
                listColumns = dataRow.GetColumns();
                eResult = new EClient(dataRow, listColumns);
            }
            return eResult;
        }

        public EClient Insert(EClient eClient)
        {
            EClient eResult = null;
            BSequence bSequence = new BSequence();
            ESequence eSequence = new ESequence(eClient);
            int correlative = 0;
            correlative = bSequence.GetCorrelative(eSequence);

            eClient.CodeClient = correlative;
            dClient.Insert(eClient);

            if (dClient.ExistsPrimaryKey())
            {
                Message = string.Format("El código de Cliente '{0}' ya existe en el Sistema, no se puede crear el registro.", eClient.CodeClient);
                throw new Exception(Message);
            }

            eClient.Audit.TypeEvent = "Insert";
            bAudit.Insert(eClient.Audit);

            correlative++;
            eSequence.Correlative = correlative;
            bSequence.SetCorrelativo(eSequence);

            eResult = Select(eClient);

            return eResult;
        }

        public EClient Update(EClient eClient)
        {
            EClient eResult;
            dClient.Update(eClient);

            eClient.Audit.TypeEvent = "Update";
            bAudit.Insert(eClient.Audit);

            eResult = Select(eClient);
            return eResult;
        }

        public void Delete(EClient eClient)
        {
            dClient.Delete(eClient);
            if (dClient.ExistsReference())
            {
                Message = string.Format("El Cliente '{0}' tiene referencias en el Sistema, no se puede eliminar el registro.", eClient.SearchName);
                throw new Exception(Message);
            }
            eClient.Audit.TypeEvent = "Delete";
            bAudit.Insert(eClient.Audit);
        }
    }
}

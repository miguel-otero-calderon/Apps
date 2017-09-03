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
            eClient.SearchName = ProcessSearchName(eClient);
            correlative = bSequence.GetCorrelative(eSequence);
            eClient.CodeClient = correlative;
            dClient.Insert(eClient);

            if (dClient.ExistsPrimaryKey())
            {
                Message = string.Format("El código de Cliente '{0}' ya existe en el Sistema, no se puede crear el registro.", eClient.CodeClient);
                throw new Exception(Message);
            }
            if (dClient.ExistsReference())
            {
                Message = string.Format("El código de Empresa '{0}' no existe en el Sistema, no se puede crear el registro.", eClient.CodeClient);
                throw new Exception(Message);
            }
            correlative++;
            eSequence.Correlative = correlative;
            bSequence.SetCorrelativo(eSequence);

            eResult = Select(eClient);

            eClient.Audit.Code = eResult.CodeClient.ToString();
            eClient.Audit.TypeEvent = "Insert";
            bAudit.Insert(eClient.Audit);

            return eResult;
        }

        public EClient Update(EClient eClient)
        {
            EClient eResult;
            eClient.SearchName = ProcessSearchName(eClient);
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

        public string ProcessSearchName(EClient eClient)
        {
            string result = string.Empty;
            if (eClient.CodeTypeDocumentIdentity == "6")
                result = eClient.LongName;
            else
            {
                string firstName = string.Empty;
                string secondName = string.Empty;
                string fatherLastName = string.Empty;
                string motherLastName = string.Empty;
                if (!string.IsNullOrEmpty(eClient.FirstName))
                    firstName = eClient.FirstName.Trim();
                if (!string.IsNullOrEmpty(eClient.SecondName))
                    secondName = " " + eClient.SecondName;
                if (!string.IsNullOrEmpty(eClient.FatherLastName))
                    fatherLastName = " " + eClient.FatherLastName;
                if (!string.IsNullOrEmpty(eClient.MotherLastName))
                    motherLastName = " " + eClient.MotherLastName;
                result = string.Format("{0}{1}{2}{3}", firstName, secondName, fatherLastName, motherLastName);
            }
            return result;
        }
    }            
}

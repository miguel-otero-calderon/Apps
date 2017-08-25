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
    public class BCorporation : BBusiness
    {
        DCorporation data = new DCorporation();
        BAudit audit = new BAudit();

        public ECorporation Select(ECorporation corporation)
        {
            ECorporation result = null;
            DataRow row = data.Select(corporation);
            if (row != null)
                result = new ECorporation(row, row.GetColumns());
            return result;
        }

        public void Delete(ECorporation corporation)
        {
            data.Delete(corporation);
            if (data.ExistsReference())
            {
                string msg = string.Format("La Corporación '{0}' tiene referencias en el Sistema, no se puede eliminar el registro.", corporation.Name);
                throw new Exception(msg);
            }
            corporation.Audit.TypeEvent = "Delete";
            audit.Insert(corporation.Audit);
        }

        public void Insert(ECorporation corporation)
        {
            data.Insert(corporation);
            if (data.ExistsPrimaryKey())
            {
                string msg = string.Format("El código de Corporación '{0}' ya existe en el Sistema, no se puede crear el registro.", corporation.CodeCorporation);
                throw new Exception(msg);
            }
            corporation.Audit.TypeEvent = "Insert";
            audit.Insert(corporation.Audit);
        }

        public void Update(ECorporation corporation)
        {
            data.Update(corporation);
            corporation.Audit.TypeEvent = "Update";
            audit.Insert(corporation.Audit);
        }
    }
}

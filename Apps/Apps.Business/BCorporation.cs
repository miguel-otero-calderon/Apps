﻿using System;
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
        DCorporation dCorporation = new DCorporation();
        BAudit bAudit = new BAudit();

        public ECorporation Select(ECorporation corporation)
        {
            ECorporation result = null;
            DataRow row = dCorporation.Select(corporation);
            if (row != null)
                result = new ECorporation(row, row.GetColumns());
            return result;
        }

        public void Delete(ECorporation corporation)
        {
            dCorporation.Delete(corporation);
            if (dCorporation.ExistsReference())
            {
                Message = string.Format("La Corporación '{0}' tiene referencias en el Sistema, no se puede eliminar el registro.", corporation.Name);
                throw new Exception(Message);
            }
            corporation.Audit.TypeEvent = "Delete";
            bAudit.Insert(corporation.Audit);
        }

        public void Insert(ECorporation eCorporation)
        {
            eCorporation.Validar();
            dCorporation.Insert(eCorporation);
            if (dCorporation.ExistsPrimaryKey())
            {
                Message = string.Format("El código de Corporación '{0}' ya existe en el Sistema, no se puede crear el registro.", eCorporation.CodeCorporation);
                throw new Exception(Message);
            }
            eCorporation.Audit.TypeEvent = "Insert";
            bAudit.Insert(eCorporation.Audit);
        }

        public void Update(ECorporation eCorporation)
        {
            eCorporation.Validar();
            dCorporation.Update(eCorporation);
            eCorporation.Audit.TypeEvent = "Update";
            bAudit.Insert(eCorporation.Audit);
        }
    }
}

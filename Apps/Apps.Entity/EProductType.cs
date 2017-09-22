using Apps.Extension;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Apps.Entity
{
    public class EProductType : EEntity
    {
        public string CodeProductType { get; set; }
        public string Description { get; set; }
        public string CodeSunatExistence { get; set; }
        public short State { get; set; }
        private EAudit audit;
        public override EAudit Audit
        {
            get
            {
                if (audit == null)
                    audit = new EAudit("00", "ProductType", CodeProductType);
                return audit;
            }
            set
            {
                audit = value;
            }
        }

        public EProductType(DataRow dataRow, List<string> listColumns)
        {
            if (listColumns.Contains("CodeProductType") && dataRow.Validate("CodeProductType"))
                CodeProductType = Convert.ToString(dataRow["CodeProductType"]);

            if (listColumns.Contains("Description") && dataRow.Validate("Description"))
                Description = Convert.ToString(dataRow["Description"]);

            if (listColumns.Contains("CodeSunatExistence") && dataRow.Validate("CodeSunatExistence"))
                CodeSunatExistence = Convert.ToString(dataRow["CodeSunatExistence"]);

            if (listColumns.Contains("State") && dataRow.Validate("State"))
                State = Convert.ToInt16(dataRow["State"]);

        }

        public override void Validar()
        {
            if (string.IsNullOrEmpty(CodeProductType))
                throw new Exception("El Código de tipo de Producto[CodeProductType] es requerido, ingrese un valor.[ProductType]");

            if (string.IsNullOrEmpty(Description))
                throw new Exception("La descripción [Description] es requerida, ingrese un valor.[ProductType]");

            if (string.IsNullOrEmpty(CodeSunatExistence))
                throw new Exception("El Código de Sunat[CodeSunatExistence] es requerido, ingrese un valor.[ProductType]");
        }
    }
}

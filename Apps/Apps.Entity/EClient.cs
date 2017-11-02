using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Apps.Extension;


namespace Apps.Entity
{
    public class EClient : EEntity
    {
        public int CodeClient { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FatherLastName { get; set; }
        public string MotherLastName { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string SearchName { get; set; }
        public string CodeTypeDocumentIdentity { get; set; }
        public string NumberIdentity { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public short State { get; set; }
        private EAudit audit;
        public override EAudit Audit
        {
            get
            {
                if (audit == null)
                {
                    audit = new EAudit("00", this.CodeEntity, CodeClient.ToString());
                }                    
                return audit;
            }
            set
            {
                audit = value;
            }
        }

        public override string CodeEntity
        {
            get
            {
                return "Client";
            }
        }

        public EClient() { }
        public EClient(DataRow dataRow, List<string> listColumns)
        {
            if (listColumns.Contains("CodeClient") && dataRow.Validate("CodeClient"))
                CodeClient = Convert.ToInt32(dataRow["CodeClient"]);

            if (listColumns.Contains("FirstName") && dataRow.Validate("FirstName"))
                FirstName = Convert.ToString(dataRow["FirstName"]);

            if (listColumns.Contains("SecondName") && dataRow.Validate("SecondName"))
                SecondName = Convert.ToString(dataRow["SecondName"]);

            if (listColumns.Contains("FatherLastName") && dataRow.Validate("FatherLastName"))
                FatherLastName = Convert.ToString(dataRow["FatherLastName"]);

            if (listColumns.Contains("MotherLastName") && dataRow.Validate("MotherLastName"))
                MotherLastName = Convert.ToString(dataRow["MotherLastName"]);

            if (listColumns.Contains("LongName") && dataRow.Validate("LongName"))
                LongName = Convert.ToString(dataRow["LongName"]);

            if (listColumns.Contains("ShortName") && dataRow.Validate("ShortName"))
                ShortName = Convert.ToString(dataRow["ShortName"]);

            if (listColumns.Contains("SearchName") && dataRow.Validate("SearchName"))
                SearchName = Convert.ToString(dataRow["SearchName"]);

            if (listColumns.Contains("CodeTypeDocumentIdentity") && dataRow.Validate("CodeTypeDocumentIdentity"))
                CodeTypeDocumentIdentity = Convert.ToString(dataRow["CodeTypeDocumentIdentity"]);

            if (listColumns.Contains("NumberIdentity") && dataRow.Validate("NumberIdentity"))
                NumberIdentity = Convert.ToString(dataRow["NumberIdentity"]);

            if (listColumns.Contains("Address") && dataRow.Validate("Address"))
                Address = Convert.ToString(dataRow["Address"]);

            if (listColumns.Contains("Phone") && dataRow.Validate("Phone"))
                Phone = Convert.ToString(dataRow["Phone"]);

            if (listColumns.Contains("Fax") && dataRow.Validate("Fax"))
                Fax = Convert.ToString(dataRow["Fax"]);

            if (listColumns.Contains("Email") && dataRow.Validate("Email"))
                Email = Convert.ToString(dataRow["Email"]);

            if (listColumns.Contains("State") && dataRow.Validate("State"))
                State = Convert.ToInt16(dataRow["State"]);
        }
        public override void Validar()
        {
            if (CodeClient <= 0)
                throw new Exception("El Código de Cliente[CodeClient] es incorrecto.[Client]");

            if (string.IsNullOrEmpty(SearchName))
                throw new Exception("El Campo Busqueda[SearchName] no puede ser vacio.[Client]");

            if (string.IsNullOrEmpty(CodeTypeDocumentIdentity))
                throw new Exception("El Código de Tipo de documento de Identidad[CodeTypeDocumentIdentity] no puede ser vacio.[Client]");

            if (string.IsNullOrEmpty(NumberIdentity))
                throw new Exception("El Número de Identidad[NumberIdentity] no puede ser vacio.[Client]");

        }
    }
}

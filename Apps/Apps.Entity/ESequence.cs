using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Apps.Extension;

namespace Apps.Entity
{
    public class ESequence:EEntity
    {
        public string CodeSequence { get; set; }
        public int Correlative { get; set; }

        public override EAudit Audit
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override string CodeEntity
        {
            get
            {
                return "Sequence";
            }
        }

        public ESequence(EEntity entity)
        {
            CodeSequence = entity.Audit.CodeEntity;
        }
        public ESequence(string CodeSequence,int Correlative)
        {
            this.CodeSequence = CodeSequence;
            this.Correlative = Correlative;
        }
        public ESequence(DataRow row, List<string> columns)
        {
            if (columns.Contains("CodeSequence") && row.Validate("CodeSequence"))
                CodeSequence = Convert.ToString(row["CodeSequence"]);

            if (columns.Contains("Correlative") && row.Validate("Correlative"))
                Correlative = Convert.ToInt32(row["Correlative"]);
        }

        public override void Validar()
        {
            if (string.IsNullOrEmpty(CodeSequence))
                throw new Exception("El Código de Secuencia[CodeSequence] no puede ser vacio.[Sequence]");

            if (Correlative <= 0)
                throw new Exception("El Correlativo[Correlative] no puede ser menor o igual a cero [Sequence]");
        }
    }
}

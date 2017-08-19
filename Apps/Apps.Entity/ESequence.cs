using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Apps.Extension;

namespace Apps.Entity
{
    public class ESequence
    {
        public string CodeSequence { get; set; }
        public int Correlative { get; set; }
        public ESequence(EEntity entity)
        {
            CodeSequence = entity.Audit.CodeEntity;
        }
        public ESequence(DataRow row, List<string> columns)
        {
            if (columns.Contains("CodeSequence") && row.Validate("CodeSequence"))
                CodeSequence = Convert.ToString(row["CodeSequence"]);

            if (columns.Contains("Correlative") && row.Validate("Correlative"))
                Correlative = Convert.ToInt32(row["Correlative"]);
        }
    }
}

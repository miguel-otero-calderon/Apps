using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Entity
{
    public class ESequence
    {
        public string CodeSequence { get; set; }
        public int Correlative { get; set; }
        public ESequence(Entity entity)
        {
            CodeSequence = entity.Audit.CodeEntity;            
        }
    }
}

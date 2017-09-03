using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Entity
{
    public abstract class EEntity
    {
        public abstract EAudit Audit { get; set; }
        public abstract void Validar();        
    }
}

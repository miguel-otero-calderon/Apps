using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Entity
{
    public abstract class Entity
    {
        public abstract EAudit Audit { get; set; }
    }
}

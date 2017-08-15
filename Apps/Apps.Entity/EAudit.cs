using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Entity
{
    public class EAudit
    {
        public int AuditId { get; set;}
        public string CodeCompany { get; set;}
        public string CodeEntity { get; set;}
        public string Code { get; set;}
        public short Sequence { get; set;}
        public string TypeEvent { get; set;}
        public string UserRegister { get; set;}
        public DateTime DateRegister { get; set;}
        public string HostRegister { get; set;}
    }
}

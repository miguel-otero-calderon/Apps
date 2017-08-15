using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Entity;
using Apps.Data;

namespace Apps.Business
{
    public class BAudit
    {
        DAudit d = new DAudit();
        public void Insert(EAudit entity)
        {
            d.Insert(entity);
        }
    }
}

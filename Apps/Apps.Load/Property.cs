using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Load
{
    public class Property
    {
        public string Column { get; set; }
        public short Position { get; set; }
        public DbType Type { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Entity
{
    public class EUser:Entity
    {
        public string CodeUser { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public short Estado { get; set; } 
    }
}

using System.Data.Common;
using Apps.Entity;

namespace Apps.Data
{
    public class DUser:DData
    {
        public bool Login(EUser user)
        {           
            return true;
        }

        public EUser FindByCodeUser(string CodeUser)
        {           
            return null;
        }
    }
}

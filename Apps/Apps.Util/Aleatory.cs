using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Util
{
    public static class Aleatory
    {
        private static Random random = new Random();
        private static List<string> list = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "q", "l", "m", "n", "o", "p", "q","r","s","t","u","v","w","x","y","z"};
        public static string GetString(short quantityString)
        {
            string result = string.Empty;
            short minValue = 0;
            short maxValue = Convert.ToInt16(quantityString - 1);
            for (short i = minValue; i <= maxValue; i++)
            {
                result = result + GetString();
            }
            return result;
        }
        private static string GetString()
        {            
            int minValue = 0;
            int maxValue = list.Count - 1;
            random.Next(minValue, maxValue);
            random.Next(minValue, maxValue);
            int ranValue = random.Next(minValue,maxValue);
            return list[ranValue];
        }
        public static short GetShort(bool negative = false)
        {
            int minValue = 1;
            int maxValue = 99;
            random.Next(minValue, maxValue);
            random.Next(minValue, maxValue);
            short ranValue = Convert.ToInt16(random.Next(minValue, maxValue));
            if (negative)
                return Convert.ToInt16(ranValue * (-1));
            else
                return ranValue;
        }
    }
}

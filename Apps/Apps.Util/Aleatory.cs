using System;
using System.Collections.Generic;
using Apps.Extension;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Util
{
    public static class Aleatory
    {
        private static Random random = new Random(DateTime.Now.Millisecond);
        public static string GetString(short quantityString)
        {
            string options = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int length = options.Length;
            char character;
            string text = string.Empty;
            for (int i = 0; i < quantityString; i++)
            {
                character = options[random.Next(length)];
                text += character.ToString();
            }
            return text;
        }
        public static short GetShort()
        {           
            int minValue = 1;
            int maxValue = 9999;
            short ranValue = Convert.ToInt16(random.Next(minValue, maxValue));
            return ranValue;
        }
    }
}

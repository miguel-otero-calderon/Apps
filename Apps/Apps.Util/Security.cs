using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Apps.Util
{
    public static class Security
    {
        public static string CalculateHash(string text)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(text);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Extension
{
    public static class StringExtension
    {
        public static string Right(this string text, int length)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return text.Trim().Substring(text.Length - length);
        }

        public static string Left(this string text, int length)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return text.Trim().Substring(0, length);
        }

        public static string LastCharacter(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return text.Right(1);
        }
    }
}

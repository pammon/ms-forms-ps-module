using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FormsPowerShellModule
{
    internal static class Extensions
    {
        internal static string FirstLetterToLowerCase(this string str)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (var i = 0; i < str.Length; i++)
            {
                if (i == 0)
                {
                    stringBuilder.Append(Char.ToLower(str[i]));
                }
                else
                {
                    stringBuilder.Append(str[i]);
                }
            }

            return stringBuilder.ToString();
        }
    }
}

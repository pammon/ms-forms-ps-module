using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FormsPowerShellModule.Test
{
    internal static class Extensions
    {
        internal static SecureString ToSecureString(this string str)
        {
            SecureString secureString = new SecureString();

            for (var i = 0; i < str.Length; i++)
            {
                    secureString.AppendChar(str[i]);
            }

            return secureString;
        }
    }
}

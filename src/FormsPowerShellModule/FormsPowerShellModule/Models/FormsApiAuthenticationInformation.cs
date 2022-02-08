using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;

namespace FormsPowerShellModule.Models
{
    public class FormsApiAuthenticationInformation
    {
        public FormsApiAuthenticationInformation(string antiForgeryToken, CoreWebView2Cookie requestVerificationToken, CoreWebView2Cookie aadAuthForms)
        {
            AntiForgeryToken = antiForgeryToken;
            RequestVerificationToken = requestVerificationToken;
            AadAuthForms = aadAuthForms;
        }

        public string AntiForgeryToken { get; }
        public CoreWebView2Cookie RequestVerificationToken  { get;}

        public CoreWebView2Cookie AadAuthForms { get; }
    }
}

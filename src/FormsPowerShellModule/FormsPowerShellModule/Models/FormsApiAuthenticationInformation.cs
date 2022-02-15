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

        public FormsApiAuthenticationInformation(string antiForgeryToken, CoreWebView2Cookie requestVerificationToken,
            CoreWebView2Cookie aadAuthForms, string tenantId)
        {
            TenantId = tenantId;
            AntiForgeryToken = antiForgeryToken;
            RequestVerificationToken = requestVerificationToken;
            AadAuthForms = aadAuthForms;
        }

        public string TenantId { get; }

        public string AntiForgeryToken { get; }
        public CoreWebView2Cookie RequestVerificationToken  { get;}

        public CoreWebView2Cookie AadAuthForms { get; }
    }
}

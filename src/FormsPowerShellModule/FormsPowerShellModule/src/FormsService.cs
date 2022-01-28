using System;
using System.Management.Automation;
using System.Security;
using System.Threading.Tasks;
using FormsPowerShellModule.Models;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FormsPowerShellModule
{
    internal class FormsService
    {
        private static IPublicClientApplication _app;
        private static PSCredential _credentials;
        private static AuthenticationResult _result;
        private static string _tenantId;

        internal static void Connect(string tenantId, string clientId, PSCredential credentials)
        {
            _tenantId = tenantId;
            _credentials = credentials;
            _app = PublicClientApplicationBuilder.Create(clientId)
                .WithAuthority($"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token")
                .Build();

            AcquireToken().GetAwaiter().GetResult();
        }

        internal static Forms[] Get(string userId, string[] fields)
        {
            if (_result.ExpiresOn < DateTime.Now)
            {
                AcquireToken().GetAwaiter().GetResult();
            }
            
            string url = $"https://forms.office.com/formapi/api/{_tenantId}/users/{userId}/light/forms";
            if (fields != null && fields.Length > 0)
            {
                url = string.Concat(url, "?$select=", string.Join(",", fields));
            }
            var webRequest = System.Net.WebRequest.Create(url);
            webRequest.Method = "GET";
            webRequest.Timeout = 12000;
            webRequest.ContentType = "application/json";
            string cookie = $"AADAuth.forms={_result.AccessToken};";
            webRequest.Headers.Add("cookie", cookie);
            webRequest.Headers.Add("x-ms-forms-isdelegatemode", "true");

            using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                {
                    var result = JsonConvert.DeserializeObject<FormsResult>(sr.ReadToEnd(), new JsonSerializerSettings()
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    return result.Forms;
                }
            }

        }


        private static async Task AcquireToken()
        {
            _result = await _app.AcquireTokenByUsernamePassword(new[] {"api://forms.office.com/c9a559d2-7aab-4f13-a6ed-e7e9c52aec87/Forms.Read"},
                    _credentials.UserName, _credentials.Password)
                .ExecuteAsync();
        }

        

    }
}
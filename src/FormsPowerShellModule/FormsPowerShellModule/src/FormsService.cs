using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Security;
using System.Threading.Tasks;
using FormsPowerShellModule.Models;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FormsPowerShellModule
{
    public class FormsService : Service
    {
        private UserService _userService;
        public FormsService(string tenantId, string clientId, string userName, SecureString password) : base(tenantId, clientId, userName, password, new[] {"api://forms.office.com/c9a559d2-7aab-4f13-a6ed-e7e9c52aec87/Forms.Read"})
        {
            _userService = new UserService(tenantId, clientId, userName, password);
        }

        public override void Connect()
        {
            _userService.Connect();
            base.Connect();
            Instance = this;
        }


        public Forms[] GetForms(string userId, string[] fields = null)
        {
            if (Result.ExpiresOn < DateTime.Now)
            {
                AcquireToken().GetAwaiter().GetResult();
            }
            
            string url = $"https://forms.office.com/formapi/api/{TenantId}/users/{userId}/light/forms";
            if (fields != null && fields.Length > 0)
            {
                url = string.Concat(url, "?$select=", string.Join(",", fields.Select(f => f.FirstLetterToLowerCase())));
            }
            var webRequest = System.Net.WebRequest.Create(url);
            webRequest.Method = "GET";
            webRequest.Timeout = 12000;
            webRequest.ContentType = "application/json";
            string cookie = $"AADAuth.forms={Result.AccessToken};";
            webRequest.Headers.Add("cookie", cookie);
            webRequest.Headers.Add("x-ms-forms-isdelegatemode", "true");

            try
            {
                using (var response = webRequest.GetResponse())
                {

                    using (System.IO.Stream s = response.GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                        {
                            return JsonConvert.DeserializeObject<FormsResult>(sr.ReadToEnd(),
                                new JsonSerializerSettings()
                                {
                                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                    NullValueHandling = NullValueHandling.Ignore
                                })
                                ?.Forms;
                        }
                    }
                }
            }
            catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
            {
                // handle 404 exceptions
            }

            return new Forms[]{};


        }

        public Forms[] GetFormsFromDeletedUsers(string[] fields = null)
        {
            return GetFormsByUserList(_userService.GetDeletedUsers(), fields);
        }

        public Forms[] GetForms(string[] fields = null)
        {
            return GetFormsByUserList(_userService.GetUsers(), fields);
        }

        private Forms[] GetFormsByUserList(User[] users, string[] fields = null)
        {
            List<Forms> result = new List<Forms>();

            foreach (User user in users)
            {
                result.AddRange(GetForms(user.Id, fields).Select(u =>
                {
                    u.UserId = user.Id;
                    u.UserPrincipalName = user.UserPrincipalName;
                    u.DisplayName = user.DisplayName;
                    u.Mail = user.Mail;
                    return u;
                }));
            }

            return result.ToArray();
        }

        public static FormsService Instance;
    }
}
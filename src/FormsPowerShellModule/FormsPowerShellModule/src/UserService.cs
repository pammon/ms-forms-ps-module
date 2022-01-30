using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Security;
using System.Threading.Tasks;
using FormsPowerShellModule.Models;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FormsPowerShellModule
{
    public class UserService : Service
    {

        public UserService(string tenantId, string clientId, string userName, SecureString password) : base(tenantId, clientId, userName, password, new[] { "https://graph.microsoft.com/User.Read.All" })
        {
        }

        public User[] GetDeletedUsers(int top = 100)
        {
            if (Result.ExpiresOn < DateTime.Now)
            {
                AcquireToken().GetAwaiter().GetResult();
            }

            string url = $"https://graph.microsoft.com/v1.0/directory/deletedItems/microsoft.graph.user?$select=userPrincipalName,id,displayName,mail&$top={top}";

            List<User> users = new List<User>();

            while (!string.IsNullOrEmpty(url))
            {
                var webRequest = System.Net.WebRequest.Create(url);
                webRequest.Method = "GET";
                webRequest.Timeout = 12000;
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("Authorization", $"{Result.TokenType} {Result.AccessToken}");

                using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        var result = JsonConvert.DeserializeObject<UsersResult>(sr.ReadToEnd(), new JsonSerializerSettings()
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver(),
                            NullValueHandling = NullValueHandling.Ignore
                        });
                        url = result.NextLink;
                        users.AddRange(result.Users);
                    }
                }
            }
            return users.ToArray();
        }

        public User[] GetUsers(int top = 100)
        {
            if (Result.ExpiresOn < DateTime.Now)
            {
                AcquireToken().GetAwaiter().GetResult();
            }

            string url = $"https://graph.microsoft.com/v1.0/users?$select=userPrincipalName,id,displayName,mail&$top={top}";

            List<User> users = new List<User>();

            while (!string.IsNullOrEmpty(url))
            {
                var webRequest = System.Net.WebRequest.Create(url);
                webRequest.Method = "GET";
                webRequest.Timeout = 12000;
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("Authorization", $"{Result.TokenType} {Result.AccessToken}");

                using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        var result = JsonConvert.DeserializeObject<UsersResult>(sr.ReadToEnd(), new JsonSerializerSettings()
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver(),
                            NullValueHandling = NullValueHandling.Ignore
                        });
                        url = result.NextLink;
                        users.AddRange(result.Users);
                    }
                }
            }
            return users.ToArray();
        }
    }
}
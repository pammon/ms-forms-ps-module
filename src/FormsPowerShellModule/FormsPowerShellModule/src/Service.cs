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
    public class Service
    {
        private readonly string[] _scopes;
        private static IPublicClientApplication _app;
        private readonly string _userName;
        private readonly SecureString _password;
        protected AuthenticationResult Result;
        protected string TenantId;
        private readonly string _clientId;

        public Service(string tenantId, string clientId, string[] scopes, string userName = null, SecureString password = null)
        {
            TenantId = tenantId;
            _userName = userName;
            _password = password;
            _scopes = scopes;
            _clientId = clientId;
            if (_app == null && !string.IsNullOrEmpty(_clientId))
            {
                _app = PublicClientApplicationBuilder.Create(_clientId)
                    .WithAuthority($"https://login.microsoftonline.com/{TenantId}/oauth2/v2.0/token")
                    .WithDefaultRedirectUri()
                    .Build();
            }
        }



        public virtual void Connect()
        {   
            AcquireToken().GetAwaiter().GetResult();
        }


        protected async Task AcquireToken()
        {
            if (_app != null)
            {
                if (string.IsNullOrEmpty(_userName) || _password == null)
                {
                    var accounts = await _app.GetAccountsAsync();

                    try
                    {
                        Result = await _app.AcquireTokenSilent(_scopes, accounts.FirstOrDefault()).ExecuteAsync();
                    }
                    catch (MsalUiRequiredException ex)
                    {

                        Result = await _app.AcquireTokenInteractive(_scopes).ExecuteAsync();
                    }
                }
                else
                {
                    Result = await _app.AcquireTokenByUsernamePassword(_scopes,
                            _userName, _password)
                        .ExecuteAsync();
                }
            }
        }
    }
}
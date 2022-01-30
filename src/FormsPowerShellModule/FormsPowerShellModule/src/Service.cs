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
        private IPublicClientApplication _app;
        private readonly string _userName;
        private readonly SecureString _password;
        protected AuthenticationResult Result;
        protected readonly string TenantId;
        private readonly string _clientId;

        public Service(string tenantId, string clientId, string userName, SecureString password, string[] scopes)
        {
            TenantId = tenantId;
            _userName = userName;
            _password = password;
            _scopes = scopes;
            _clientId = clientId;
        }



        public virtual void Connect()
        {
            _app = PublicClientApplicationBuilder.Create(_clientId)
                .WithAuthority($"https://login.microsoftonline.com/{TenantId}/oauth2/v2.0/token")
                .Build();
            AcquireToken().GetAwaiter().GetResult();
        }


        protected async Task AcquireToken()
        {
            Result = await _app.AcquireTokenByUsernamePassword(_scopes,
                    _userName, _password)
                .ExecuteAsync();
        }

        

    }
}
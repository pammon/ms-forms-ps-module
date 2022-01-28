using System.Management.Automation;
using System.Security;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace FormsPowerShellModule
{
    internal class FormsService
    {
        private static IPublicClientApplication _app;
        private static PSCredential _credentials;
        private static AuthenticationResult _result;

        internal static void Connect(string tenantId, string clientId, PSCredential credentials)
        {
            _credentials = credentials;
            _app = PublicClientApplicationBuilder.Create(clientId)
                .WithAuthority($"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token")
                .Build();

            AcquireToken().GetAwaiter().GetResult();
        }

        private static async Task AcquireToken()
        {
            _result = await _app.AcquireTokenByUsernamePassword(
                    new[] {"api://forms.office.com/c9a559d2-7aab-4f13-a6ed-e7e9c52aec87/Forms.Read"},
                    _credentials.UserName, _credentials.Password)
                .ExecuteAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace FormsPowerShellModule
{
    [Cmdlet(VerbsCommunications.Connect, "MsForms")]
    public class ConnectMsFormsCmdlet : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public string TenantId { get; set; }


        [Parameter(Mandatory = true)]
        public string ClientId { get; set; }


        [Parameter(Mandatory = true)]
        public PSCredential Credentials { get; set; }
        
        protected override void ProcessRecord()
        {
            FormsService.Connect(TenantId, ClientId, Credentials);
            WriteVerbose("connected");
        }
    }
}

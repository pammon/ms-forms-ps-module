using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using FormsPowerShellModule.Models;

namespace FormsPowerShellModule
{
    [Cmdlet(VerbsCommon.Set, "MsForms")]
    [OutputType(typeof(Forms[]))]
    public class SetMsFormsCmdlet : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public string UserId { get; set; }
        
        [Parameter(Mandatory = true)]
        public string FormId { get; set; }
        
        [Parameter(Mandatory = true)]
        public bool FormClosed { get; set; }

        [Parameter(Mandatory = true)]
        public string FormClosedMessage { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(FormsService.Instance.UpdateFormSettings(UserId, FormId, FormClosed, FormClosedMessage));
        }
    }
}

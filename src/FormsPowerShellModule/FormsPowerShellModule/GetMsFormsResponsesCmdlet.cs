using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using FormsPowerShellModule.Models;

namespace FormsPowerShellModule
{
    [Cmdlet(VerbsCommon.Get, "MsFormsResponses")]
    [OutputType(typeof(Forms[]))]
    public class GetMsFormsResponsesCmdlet : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public string UserId { get; set; }
        
        [Parameter(Mandatory = true)]
        public string FormId { get; set; }
        
        protected override void ProcessRecord()
        {
            WriteObject(FormsService.Instance.GetFormResponses(UserId, FormId));
        }
    }
}

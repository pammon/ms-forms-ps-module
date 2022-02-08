using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using FormsPowerShellModule.Models;

namespace FormsPowerShellModule
{
    [Cmdlet(VerbsCommon.Move, "MsFormsToUser")]
    [OutputType(typeof(Forms[]))]
    public class MoveMsFormsToUser : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public string UserId { get; set; }
        
        [Parameter(Mandatory = true)]
        public string FormId { get; set; }
        
        [Parameter(Mandatory = true)]
        public string NewOwnerId { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(FormsService.Instance.MoveFormToUser(UserId, FormId, NewOwnerId));
        }
    }
}

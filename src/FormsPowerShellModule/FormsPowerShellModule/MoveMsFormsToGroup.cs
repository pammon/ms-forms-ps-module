using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using FormsPowerShellModule.Models;

namespace FormsPowerShellModule
{
    [Cmdlet(VerbsCommon.Move, "MsFormsToGroup")]
    [OutputType(typeof(Forms[]))]
    public class MoveMsFormsToGroup : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public string UserId { get; set; }
        
        [Parameter(Mandatory = true)]
        public string FormId { get; set; }
        
        [Parameter(Mandatory = true)]
        public string GroupId { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(FormsService.Instance.MoveFormToGroup(UserId, FormId, GroupId));
        }
    }
}

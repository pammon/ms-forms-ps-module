using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using FormsPowerShellModule.Models;

namespace FormsPowerShellModule
{
    [Cmdlet(VerbsCommon.Get, "MsForms")]
    [OutputType(typeof(Forms[]))]
    public class GetMsFormsCmdlet : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public string UserId { get; set; }


        [Parameter(Mandatory = false)]
        public string[] Fields { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(FormsService.Get(UserId, Fields));
        }
    }
}

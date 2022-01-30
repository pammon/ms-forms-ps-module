using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using FormsPowerShellModule.Models;

namespace FormsPowerShellModule
{
    [Cmdlet(VerbsCommon.Get, "AllMsForms")]
    [OutputType(typeof(Forms[]))]
    public class GetAllMsForms : Cmdlet
    {
        [Parameter(Mandatory = false)]
        public string[] Fields { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(FormsService.Instance.GetForms(Fields?.ToList()));
        }
    }
}

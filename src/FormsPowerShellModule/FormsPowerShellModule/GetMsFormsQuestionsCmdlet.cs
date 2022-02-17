using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using FormsPowerShellModule.Models;

namespace FormsPowerShellModule
{
    [Cmdlet(VerbsCommon.Get, "MsFormsQuestions")]
    [OutputType(typeof(Forms[]))]
    public class GetMsFormsQuestionsCmdlet : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public string UserId { get; set; }
        
        [Parameter(Mandatory = true)]
        public string FormId { get; set; }
        
        protected override void ProcessRecord()
        {
            WriteObject(FormsService.Instance.GetFormQuestions(UserId, FormId));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using FormsPowerShellModule.Models;

namespace FormsPowerShellModule
{
    [Cmdlet(VerbsCommon.Get, "MsFormsResults")]
    [OutputType(typeof(Forms[]))]
    public class DownloadMsFormsResults : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public string Id { get; set; }

        [Parameter(Mandatory = true)]
        public string Path { get; set; }


        [Parameter(Mandatory = false)]
        public int MaxResponseId { get; set; }


        [Parameter(Mandatory = false)]
        public int MinResponseId { get; set; }
        

        protected override void ProcessRecord()
        {
            FormsService.Instance.DownloadDownloadExcelFile(Id, Path, MinResponseId, MaxResponseId);
            WriteObject("Completed.");
        }
    }
}

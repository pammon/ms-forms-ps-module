using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FormsPowerShellModule.Models
{
    public class FormsBackground
    {
        public object AltText { get; set; }
        public object ContentType { get; set; }
        public object FileIdentifier { get; set; }
        public object OriginalFileName { get; set; }
        public object ResourceId { get; set; }
        public object ResourceUrl { get; set; }
        public object Height { get; set; }
        public object Width { get; set; }
        public object Size { get; set; }
    }
}

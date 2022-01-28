using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FormsPowerShellModule.Models
{
    internal class FormsResult
    {
        [JsonProperty("value")]
        public Forms[] Forms { get; set; }
    }
}

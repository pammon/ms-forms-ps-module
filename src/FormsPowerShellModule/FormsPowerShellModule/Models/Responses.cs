using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FormsPowerShellModule.Models
{
    public class Responses
    {
        [JsonProperty("value")]
        public Response[] Items { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsPowerShellModule.Models
{
    public class User
    {
        public string UserPrincipalName { get; set; }
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Mail { get; set; }
    }
}

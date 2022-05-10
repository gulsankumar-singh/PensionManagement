using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionModule.Models
{
    public class APIResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Response { get; set; }
    }
}

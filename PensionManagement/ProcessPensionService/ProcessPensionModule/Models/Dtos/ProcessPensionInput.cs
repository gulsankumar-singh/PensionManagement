using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionModule.Models.Dtos
{
    public class ProcessPensionInput
    {
        //public string UserName { get; set; }
        [Required]
        public long AadhaarNumber { get; set; }
        //public string PanNumber { get; set; }
        //public int PensionType { get; set; }
    }
}

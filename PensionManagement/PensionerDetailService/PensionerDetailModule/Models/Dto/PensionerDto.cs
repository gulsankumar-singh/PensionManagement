using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailModule.Models.Dto
{
    public class PensionerDto
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PanNumber { get; set; }
        public long AadharNumber { get; set; }
        public long SalaryEarned { get; set; }
        public long Allowances { get; set; }
        public string PensionType { get; set; }
        //public long AccountNumber { get; set; }
        //public BankDetailDto BankDetail { get; set; }
    }
}

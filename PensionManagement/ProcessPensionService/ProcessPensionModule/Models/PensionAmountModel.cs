using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionModule.Models
{
    public class PensionAmountModel
    {
        public long SalaryEarned { get; set; }
        public long Allowance { get; set; }
        public PensionTypes PensionType { get; set; }
        public BankTypes BankType { get; set; }
    }
}

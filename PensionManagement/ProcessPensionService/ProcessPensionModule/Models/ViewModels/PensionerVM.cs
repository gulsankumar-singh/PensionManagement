﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionModule.Models.ViewModels
{
    public class PensionerVM
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PanNumber { get; set; }
        public long AadharNumber { get; set; }
        public long SalaryEarned { get; set; }
        public long Allowances { get; set; }
        public PensionTypes PensionType { get; set; }
        public long AccountNumber { get; set; }
        public BankDetailVM BankDetail { get; set; }
    }
}

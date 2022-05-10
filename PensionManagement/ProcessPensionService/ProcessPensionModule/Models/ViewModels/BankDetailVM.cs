using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionModule.Models.ViewModels
{
    public class BankDetailVM
    {
        public string BankName { get; set; }
        public BankTypes BankType { get; set; }
    }

}

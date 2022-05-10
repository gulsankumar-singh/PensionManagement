using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailModule.Models
{
    public class BankDetailDto
    {
        public string BankName { get; set; }
        public BankTypes BankType { get; set; }
    }

}

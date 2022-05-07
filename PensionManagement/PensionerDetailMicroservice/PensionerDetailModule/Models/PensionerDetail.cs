using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionerDetailModule.Models
{
    public class PensionerDetail
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PAN { get; set; }
        public long AadharNumber { get; set; }
        public long SalaryEarned { get; set; }
        public long Allowances { get; set; }
        public PensionTypes PensionType { get; set; }
        public long AccountNumber { get; set; }
        public string BankName { get; set; }
        public BankTypes BankType { get; set; }
    }
    public enum BankTypes { Public, Private };

    public enum PensionTypes { Self, Family };
}

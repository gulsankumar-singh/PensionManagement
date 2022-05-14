using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionModule.Models
{
    public class PensionDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PanNumber { get; set; }
        [Required]
        public long AadharNumber { get; set; }
        [Required]
        public long SalaryEarned { get; set; }
        [Required]
        public long Allowances { get; set; }
        [Required]
        public string PensionType { get; set; }
        [Required]
        public long AccountNumber { get; set; }
        [Required]
        public double PensionAmount { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public string BankType { get; set; }

        [Required]
        public double BankServiceCharge { get; set; }

        [Required]
        public string TransectionId { get; set; }
    }

    public enum PensionTypes { Self, Family };
    public enum BankTypes { Public, Private };


}

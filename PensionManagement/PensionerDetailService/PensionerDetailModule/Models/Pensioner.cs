using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionerDetailModule.Models
{
    public class Pensioner
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string PanNumber { get; set; }
        [Required]
        public long AadharNumber { get; set; }
        [Required]
        public long SalaryEarned { get; set; }
        [Required]
        public long Allowances { get; set; }
        [Required]
        public PensionTypes PensionType { get; set; }
        [Required]
        public long AccountNumber { get; set; }
        public int BankId { get; set; }
        [ForeignKey("BankId")]
        public Bank BankDetail { get; set; }
    }
    public enum PensionTypes { Self, Family };
}

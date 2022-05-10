using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailModule.Models
{
    public class BankDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public BankTypes BankType { get; set; }
    }

    public enum BankTypes { Public, Private };


}

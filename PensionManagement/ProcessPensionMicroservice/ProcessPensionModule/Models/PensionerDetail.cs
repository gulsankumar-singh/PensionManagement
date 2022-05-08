using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPensionModule.Models
{
    public class PensionerDetail
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PAN { get; set; }
        public long AadharNumber { get; set; }
        public double PensionAmount { get; set; }
        public string PensionType { get; set; }
        public string BankType { get; set; }
    }
    public enum BankTypes { Public, Private };

    public enum PensionTypes { Self, Family };
}

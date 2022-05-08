using ProcessPensionModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionModule.Utility.CalculatePension
{
    public interface ICalculatePensionAmount
    {
        double GetPensionAmount(PensionAmountModel pensionAmountModel);
    }
}

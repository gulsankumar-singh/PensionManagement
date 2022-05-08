using log4net;
using ProcessPensionModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionModule.Utility.CalculatePension
{
    public class CalculatePensionAmount : ICalculatePensionAmount
    {
        private readonly ILog _logger;

        public CalculatePensionAmount()
        {
            _logger = LogManager.GetLogger(typeof(CalculatePensionAmount));
        }

        public double GetPensionAmount(PensionAmountModel pensionAmountModel)
        {
            double pensionAmount = 0;
            try
            {
                int pensionType = (int)pensionAmountModel.PensionType;
                int bankType = (int)pensionAmountModel.BankType;
                int serviceCharge = bankType == 0 ? 500 : 550;
                if(pensionType == 0)
                {
                    pensionAmount = 0.8 * pensionAmountModel.SalaryEarned + pensionAmountModel.Allowance + serviceCharge;
                }
                else
                {
                    pensionAmount = 0.5 * pensionAmountModel.SalaryEarned + pensionAmountModel.Allowance + serviceCharge;

                }
                pensionAmount = Math.Round(pensionAmount, 2);
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            return pensionAmount;
        }
    }
}

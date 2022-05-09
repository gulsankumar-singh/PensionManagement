using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProcessPensionModule.Models;
using ProcessPensionModule.Services.CallPensionerDetailService;
using ProcessPensionModule.Utility;
using System;
using System.Threading.Tasks;

namespace ProcessPensionModule.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPensionController : ControllerBase
    {
        private readonly ILogger<ProcessPensionController> _logger;
        private readonly IGetPensionerDetail _getPensionerDetail;
        public ProcessPensionController(ILogger<ProcessPensionController> logger, IGetPensionerDetail getPensionerDetail)
        {
            _logger = logger;
            _getPensionerDetail = getPensionerDetail;
        }

        /// <summary>
        /// Process Pension based on the aadhar Number
        /// </summary>
        /// <param name="aadharNumber"></param>
        /// <returns>Pensioner Detail with Pension Amount</returns>
        [Route("ProcessPension")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PensionerDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PensionerDetail>> ProcessPension(long aadharNumber)
        {
            _logger.LogInformation("ProcessPension method execution started...");
            var authenticationInfo = await HttpContext.AuthenticateAsync();
            string token = authenticationInfo.Properties.GetTokenValue(StaticData.ACCESS_TOKEN);

            PensionerInfo pensionerInfo = await _getPensionerDetail.GetPensionerDetailAsync(aadharNumber, token);
            //if pensioner not found return null
            if(pensionerInfo == null)
            {
                return null;
            }

            PensionAmountModel amountModel = new PensionAmountModel
            {
                Allowance = pensionerInfo.Allowances,
                SalaryEarned = pensionerInfo.SalaryEarned,
                PensionType = pensionerInfo.PensionType,
                BankType = pensionerInfo.BankType
            };

            double pensionAmount = CalculatePensionAmount(amountModel);

            PensionerDetail pensionerDetail = new PensionerDetail
            {
                Name = pensionerInfo.Name,
                DateOfBirth = pensionerInfo.DateOfBirth,
                PAN = pensionerInfo.PAN,
                AadharNumber = pensionerInfo.AadharNumber,
                PensionAmount = pensionAmount,
                PensionType = Enum.GetName(typeof(PensionTypes), pensionerInfo.PensionType),
                BankType = Enum.GetName(typeof(BankTypes), pensionerInfo.BankType),

            };
            return Ok(pensionerDetail); 
        }

        /// <summary>
        /// Helper for calculating the pension amount
        /// </summary>
        /// <param name="pensionAmountModel"></param>
        /// <returns></returns>
        private static double CalculatePensionAmount(PensionAmountModel pensionAmountModel)
        {

            int pensionType = (int)pensionAmountModel.PensionType;
            int bankType = (int)pensionAmountModel.BankType;
            int serviceCharge = bankType == 0 ? 500 : 550;
            double salaryPercentage = pensionType == 0 ? 0.8 : 0.5;

            double pensionAmount = salaryPercentage * pensionAmountModel.SalaryEarned + pensionAmountModel.Allowance + serviceCharge;
                
            pensionAmount = Math.Round(pensionAmount, 2);
            
            return pensionAmount;
        }
    }
}

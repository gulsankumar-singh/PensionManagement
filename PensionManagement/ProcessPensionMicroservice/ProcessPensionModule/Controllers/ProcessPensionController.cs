using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessPensionModule.Models;
using ProcessPensionModule.Services.CallPensionerDetailService;
using ProcessPensionModule.Utility.CalculatePension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionModule.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPensionController : ControllerBase
    {
        private readonly ILog _logger;
        private readonly ICalculatePensionAmount _calculatePensionAmount;
        private readonly IGetPensionerDetail _getPensionerDetail;
        public ProcessPensionController(ICalculatePensionAmount calculatePensionAmount, IGetPensionerDetail getPensionerDetail)
        {
            _logger = LogManager.GetLogger(typeof(ProcessPensionController));
            _calculatePensionAmount = calculatePensionAmount;
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
            try
            {
                PensionerInfo pensionerInfo = await _getPensionerDetail.GetPensionerDetailAsync(aadharNumber);
                if(pensionerInfo == null)
                {
                    return BadRequest();
                }

                PensionAmountModel amountModel = new PensionAmountModel
                {
                    Allowance = pensionerInfo.Allowances,
                    SalaryEarned = pensionerInfo.SalaryEarned,
                    PensionType = pensionerInfo.PensionType,
                    BankType = pensionerInfo.BankType
                };

                double pensionAmount = _calculatePensionAmount.GetPensionAmount(amountModel);

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
            catch(Exception ex)
            {
                _logger.Error(ex);
                return NotFound();
            }
        } 
    }
}

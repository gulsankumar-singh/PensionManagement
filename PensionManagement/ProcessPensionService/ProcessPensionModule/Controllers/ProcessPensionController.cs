using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProcessPensionModule.Models;
using ProcessPensionModule.Models.Dtos;
using ProcessPensionModule.Models.ViewModels;
using ProcessPensionModule.Repository.IRepository;
using ProcessPensionModule.Services.GetPensionerDetailQueries;
using ProcessPensionModule.Utility;
using System;
using System.Threading.Tasks;

namespace ProcessPensionModule.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPensionController : ControllerBase
    {
        private readonly ILogger<ProcessPensionController> _logger;
        private readonly IGetPensionerDetail _getPensionerDetail;
        private readonly IPensionDetailRepository _pensionDetailRepository;
        private readonly IMapper _mapper;
        public ProcessPensionController(ILogger<ProcessPensionController> logger, IMapper mapper, IGetPensionerDetail getPensionerDetail, 
            IPensionDetailRepository pensionDetailRepository)
        {
            _logger = logger;
            _getPensionerDetail = getPensionerDetail;
            _mapper = mapper;
            _pensionDetailRepository = pensionDetailRepository;
        }

        /// <summary>
        /// Process Pension based on the aadhar Number
        /// </summary>
        /// <param name="aadharNumber"></param>
        /// <returns>Pensioner Detail with Pension Amount</returns>
        [Route("ProcessPension")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> ProcessPension(long aadharNumber)
        {
            _logger.LogInformation("ProcessPension method execution started...");
            APIResponse apiResponse = new APIResponse();
            PensionerVM pensionerInfo = await _getPensionerDetail.GetPensionerDetailByAadhaar(aadharNumber);
            //if pensioner not found return null
            if(pensionerInfo == null)
            {
                apiResponse.Status = "Error";
                apiResponse.Message = "No Record Found Please enter valid aadhaar number";
                return apiResponse;
            }

            PensionAmountModel amountModel = new PensionAmountModel
            {
                Allowance = pensionerInfo.Allowances,
                SalaryEarned = pensionerInfo.SalaryEarned,
                PensionType = pensionerInfo.PensionType,
                BankType = pensionerInfo.BankDetail.BankType
            };

            double pensionAmount = CalculatePensionAmount(amountModel);

            PensionDetail pensionerDetail = new PensionDetail
            {
                Name = pensionerInfo.Name,
                PAN = pensionerInfo.PAN,
                AadharNumber = pensionerInfo.AadharNumber,
                SalaryEarned = pensionerInfo.SalaryEarned,
                Allowances = pensionerInfo.Allowances,
                PensionAmount = pensionAmount,
                PensionType = Enum.GetName(typeof(PensionTypes), pensionerInfo.PensionType),
                BankType = Enum.GetName(typeof(BankTypes), pensionerInfo.BankDetail.BankType),
                AccountNumber = pensionerInfo.AccountNumber,
                BankName = pensionerInfo.BankDetail.BankName,
                TransectionId = Guid.NewGuid().ToString(),
            };
            
            await _pensionDetailRepository.CreatePensionDetail(pensionerDetail);
            PensionDetailDto pensionDto = _mapper.Map<PensionDetailDto>(pensionerDetail);
            apiResponse.Status = "Success";
            apiResponse.Message = "Pension Calculated Successfully!!";
            apiResponse.Response = pensionDto;
            return Ok(apiResponse); 
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

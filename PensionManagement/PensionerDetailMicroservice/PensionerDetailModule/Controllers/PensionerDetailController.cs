using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PensionerDetailModule.Models;
using PensionerDetailModule.Services.DataSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailModule.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionerDetailController : ControllerBase
    {
        private readonly IApplicationDataSetup _applicationDataSetup;
        private readonly ILog _logger;

        public PensionerDetailController()
        {
            _logger = LogManager.GetLogger(typeof(PensionerDetailController));
            _applicationDataSetup = new ApplicationDataSetup();
        }

        /// <summary>
        /// Get List of Pensioner
        /// </summary>
        /// <returns></returns>
        [Route("GetAllPensioner")]
        [HttpGet]
        public List<PensionerDetail> GetPensionerList()
        {
            _logger.Info("Calling GetPensionerList method");
            return _applicationDataSetup.GetPensionerDetails();
        }

        
        /// <summary>
        /// Get Pensioner Detail by aadhaarNumber
        /// </summary>
        /// <param name="aadhaarNumber"></param>
        /// <returns></returns>
        [Route("PensionerDetailByAadhaar")]
        [HttpGet]
        public PensionerDetail PensionerDetailByAadhaar(long aadhaarNumber)
        {
            PensionerDetail pensioner = null;
            try{
                _logger.Info("Calling PensionerDetailByAadhaar method");
                List<PensionerDetail> pensionerList = _applicationDataSetup.GetPensionerDetails();
                pensioner = pensionerList.FirstOrDefault(i => i.AadharNumber == aadhaarNumber);
            }catch(Exception ex){
                _logger.Error(ex);
            }
            return pensioner;
        }
    }
}

using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PensionerDetailModule.Infrastructure.Exceptions;
using PensionerDetailModule.Models;
using PensionerDetailModule.Services.DataSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailModule.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PensionerDetailController : ControllerBase
    {
        private readonly IApplicationDataSetup _applicationDataSetup;
        //private readonly ILog _logger;
        private readonly ILogger<PensionerDetailController> _logger;

        public PensionerDetailController(IApplicationDataSetup applicationDataSetup, ILogger<PensionerDetailController> logger)
        {
            _logger = logger;
            _applicationDataSetup = applicationDataSetup;
        }

        /// <summary>
        /// Get List of Pensioner
        /// </summary>
        /// <returns></returns>
        [Route("GetAllPensioner")]
        [HttpGet]
        public List<PensionerDetail> GetPensionerList()
        {
            _logger.LogInformation("Calling GetPensionerList method");
            return _applicationDataSetup.GetPensionerDetails();
        }

        
        /// <summary>
        /// Get Pensioner Detail by aadhaarNumber
        /// </summary>
        /// <param name="aadhaarNumber"></param>
        /// <returns>Pensioner detail associated to the aadhaarNumber</returns>
        [Route("PensionerDetailByAadhaar")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PensionerDetail))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<PensionerDetail> PensionerDetailByAadhaar(long aadhaarNumber)
        {
            _logger.LogInformation("Calling PensionerDetailByAadhaar method");
            List<PensionerDetail> pensionerList = _applicationDataSetup.GetPensionerDetails();
            PensionerDetail pensioner = pensionerList.FirstOrDefault(i => i.AadharNumber == aadhaarNumber);
            if(pensioner == null)
                throw new EntityNotFoundException();

            return Ok(pensioner);
          
        }
    }
}

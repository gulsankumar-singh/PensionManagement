using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PensionerDetailModule.Common.Exceptions;
using PensionerDetailModule.Models;
using PensionerDetailModule.Models.Dto;
using PensionerDetailModule.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace PensionerDetailModule.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PensionerDetailController : ControllerBase
    {
        private readonly ILogger<PensionerDetailController> _logger;
        private readonly IPensionerRepository _pensionerRepository;
        private readonly IMapper _mapper;


        public PensionerDetailController(ILogger<PensionerDetailController> logger, IMapper mapper, IPensionerRepository pensionerRepository)
        {
            _logger = logger;
            _pensionerRepository = pensionerRepository;
            _mapper = mapper;
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
            return _pensionerRepository.GetAllPensioner();
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
        public ActionResult<PensionerDetailDto> PensionerDetailByAadhaar(long aadhaarNumber)
        {
            _logger.LogInformation("Starting PensionerDetailByAadhaar method.....");
            PensionerDetail pensioner = _pensionerRepository.GetPensionerByAadhaar(aadhaarNumber);

            if (pensioner == null)
                throw new EntityNotFoundException();

            PensionerDetailDto pensionerDto = _mapper.Map<PensionerDetailDto>(pensioner);

            return Ok(pensionerDto);

        }
    }
}

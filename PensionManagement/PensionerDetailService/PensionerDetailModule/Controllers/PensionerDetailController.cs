﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PensionerDetailModule.Models;
using PensionerDetailModule.Models.Dto;
using PensionerDetailModule.Repository.IRepository;
using PensionerDetailModule.Utility;
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
        /// <returns>List of pensioner</returns>
        [Route("GetAllPensioner")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PensionerDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PensionerDto>>> GetPensionerList()
        {
            _logger.LogInformation("Starting GetPensionerList method....");

            var pensionerList = await _pensionerRepository.GetAllPensioner();
            var pensionerDTO = new List<PensionerDto>();

            foreach (var item in pensionerList)
            {
                pensionerDTO.Add(_mapper.Map<PensionerDto>(item));
            }

            return Ok(pensionerDTO);
        }


        /// <summary>
        /// Method for Getting Pensioner Detail by aadhaarNumber
        /// </summary>
        /// <param name="aadhaarNumber">Pensioner Aadhaar number</param>
        /// <returns>Pensioner detail associated with the aadhaarNumber</returns>
        [Route("PensionerDetailByAadhaar")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PensionerDetailDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PensionerDetailDto>> PensionerDetailByAadhaar(long aadhaarNumber)
        {
            _logger.LogInformation("Starting PensionerDetailByAadhaar method.....");
            Pensioner pensioner = await _pensionerRepository.GetPensionerByAadhaar(aadhaarNumber);

            if (pensioner == null)
                return NotFound(new { message = StaticData.PensionerNotFound});

            PensionerDetailDto pensionerDto = _mapper.Map<PensionerDetailDto>(pensioner);

            return Ok(pensionerDto);

        }
    }
}

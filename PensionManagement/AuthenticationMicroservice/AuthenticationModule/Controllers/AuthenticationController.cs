using AuthenticationModule.Models;
using AuthenticationModule.Services.AuthenticationService;
using AuthenticationModule.Utility;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace AuthenticationModule.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenAuth _authenticate;
        private readonly ILogger<AuthenticationController> _logger;
        public AuthenticationController(IJwtTokenAuth authenticate, IConfiguration configuration, ILogger<AuthenticationController> logger)
        {
            _configuration = configuration;
            _authenticate = authenticate;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TokenDetail> GetAuthenticationToken([FromBody] User user)
        {
            TokenDetail token = null;
            
            _logger.LogInformation("GetAuthenticationToken method execution started...");

            if(user.UserName != StaticData.USER || user.Password != StaticData.PASSWORD)
                return BadRequest(new { message = "Username or password is incorrect" });

            token = _authenticate.GenerateToken(user.UserName);

            return Ok(token);

        }
    }
}

using AuthenticationModule.Models;
using AuthenticationModule.Services.AuthenticationService;
using AuthenticationModule.Utility;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly ILog _logger; 
        public AuthenticationController(IJwtTokenAuth authenticate, IConfiguration configuration)
        {
            _configuration = configuration;
            _authenticate = new JwtTokenAuth(configuration);
            _logger = LogManager.GetLogger(typeof(AuthenticationController));
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TokenDetail> GetAuthenticationToken([FromBody] User user)
        {
            TokenDetail token = null;
            try
            {
                _logger.Info("GetAuthenticationToken method execution started");

                if(user.UserName != Constants.USER || user.Password != Constants.PASSWORD)
                    return BadRequest(new { message = "Username or password is incorrect" });

                token = _authenticate.GenerateToken(user.UserName);

                _logger.Info("GetAuthenticationToken method execution ended");
                return Ok(token);
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
                return NotFound();
            }

        }
    }
}

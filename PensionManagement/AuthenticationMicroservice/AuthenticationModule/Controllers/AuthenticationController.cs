using AuthenticationModule.Models;
using AuthenticationModule.Services.AuthenticationService;
using log4net;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IAuthenticate _authenticate;
        private readonly ILog _logger; 
        public AuthenticationController(IAuthenticate authenticate, IConfiguration configuration)
        {
            _configuration = configuration;
            _authenticate = new Authenticate(configuration);
            _logger = LogManager.GetLogger(typeof(AuthenticationController));
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult GetAuthenticationToken([FromBody] User user)
        {
            TokenDetail token = null;
            try
            {
                _logger.Info("GetAuthenticationToken method execution started");
                token = _authenticate.Authentication(user.UserName, user.Password);

                if(token == null)
                    return BadRequest(new { message = "Username or password is incorrect" });
                
                _logger.Info("GetAuthenticationToken method execution ended");
                return Ok(token);
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
                throw;
            }

        }
    }
}

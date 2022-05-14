using AuthenticationModule.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationModule.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationController> _logger;
        public AuthenticationController(IConfiguration configuration, ILogger<AuthenticationController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Get JWT Authentication token for 
        /// Pension Management System
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Token Detail</returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> GetAuthenticationToken([FromBody] User user)
        {
            APIResponse response = new APIResponse();
            _logger.LogInformation("GetAuthenticationToken method started...");

            if (user.UserName != StaticData.USER || user.Password != StaticData.PASSWORD)
            {
                response.Status = StaticData.ERROR;
                response.Message = StaticData.INVALIDDATAMSG;
                response.Response = null;
                return NotFound(response);
            }

            TokenDetail token = GenerateToken(user.UserName);

            response.Status = StaticData.SUCCESS;
            response.Message = StaticData.LOGINSUCCESSMSG;
            response.Response = token;
            return Ok(response);

        }

        /// <summary>
        /// Helper method to generate the JWT token
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private TokenDetail GenerateToken(string userName)
        {
           
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration.GetSection(StaticData.JWT_DETAIL).GetSection(StaticData.SUBJECT).Value),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.Name, userName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection(StaticData.JWT_DETAIL).GetSection(StaticData.KEY).Value));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                    _configuration.GetSection(StaticData.JWT_DETAIL).GetSection(StaticData.ISSUER).Value,
                        _configuration.GetSection(StaticData.JWT_DETAIL).GetSection(StaticData.AUDIENCE).Value,
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(StaticData.EXPIRESAFTER),
                    signingCredentials: signIn);
            var tokenHandler = new JwtSecurityTokenHandler();

            TokenDetail tokenDetail = new TokenDetail
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = token.ValidTo
            };

            return tokenDetail; 
        }
    }
}

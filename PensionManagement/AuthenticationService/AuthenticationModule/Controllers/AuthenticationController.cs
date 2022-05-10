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

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TokenDetail> GetAuthenticationToken([FromBody] User user)
        {
            
            _logger.LogInformation("GetAuthenticationToken method started...");

            if(user.UserName != StaticData.USER || user.Password != StaticData.PASSWORD)
                return BadRequest(new { message = "Username or password is incorrect" });

            TokenDetail token = GenerateToken(user.UserName);

            return Ok(token);

        }

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

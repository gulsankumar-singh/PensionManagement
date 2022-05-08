using AuthenticationModule.Models;
using AuthenticationModule.Utility;
using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationModule.Services.AuthenticationService
{
    public class JwtTokenAuth : IJwtTokenAuth
    {
        private readonly IConfiguration _configuration;
        private readonly ILog _logger;
        public JwtTokenAuth(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = LogManager.GetLogger(typeof(JwtTokenAuth));
        }

        public TokenDetail GenerateToken(string userName)
        {
            try
            {
                _logger.Info("Authentication method execution started");

                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration.GetSection(Constants.JWT_DETAIL).GetSection(Constants.SUBJECT).Value),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Name, userName)
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection(Constants.JWT_DETAIL).GetSection(Constants.KEY).Value));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                var token = new JwtSecurityToken(
                        _configuration.GetSection(Constants.JWT_DETAIL).GetSection(Constants.ISSUER).Value,
                         _configuration.GetSection(Constants.JWT_DETAIL).GetSection(Constants.AUDIENCE).Value,
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: signIn);
                var tokenHandler = new JwtSecurityTokenHandler();

                //string secret = _configuration.GetSection(Constants.SECRET).Value;

                //var key = Encoding.ASCII.GetBytes(secret);

                //var tokenDescriptor = new SecurityTokenDescriptor
                //{
                //    Subject = new ClaimsIdentity(new Claim[] {
                //    new Claim(ClaimTypes.Name, userName),
                //    new Claim(JwtRegisteredClaimNames.Iss, "https://localhost:44303")
                //}),
                //    Expires = DateTime.UtcNow.AddMinutes(30),
                //    Issuer = "https://localhost:44303",
                //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                //};

                //var token = tokenHandler.CreateToken(tokenDescriptor);

                TokenDetail tokenDetail = new TokenDetail {
                    Token = tokenHandler.WriteToken(token),
                    Expiration = token.ValidTo
                };
       
                _logger.Info("Authentication method execution ended");

                return tokenDetail;
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
                throw;
            }

        }
    }
}

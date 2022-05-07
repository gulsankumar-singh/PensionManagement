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
    public class Authenticate : IAuthenticate
    {
        private readonly IConfiguration _configuration;
        private readonly ILog _logger;
        public Authenticate(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = LogManager.GetLogger(typeof(Authenticate));
        }

        public TokenDetail Authentication(string userName, string password)
        {
            try
            {
                _logger.Info("Authentication method execution started");

                if (userName != Constants.USER || password != Constants.PASSWORD)
                    return null;

                var tokenHandler = new JwtSecurityTokenHandler();

                string secret = _configuration.GetSection(Constants.SECRET).Value;

                var key = Encoding.ASCII.GetBytes(secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, userName)
                }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

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

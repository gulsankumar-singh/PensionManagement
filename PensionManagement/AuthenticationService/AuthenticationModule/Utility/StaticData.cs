using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationModule
{
    public static class StaticData
    {
        public const string LogConfigFile = "log4net.config";
        public const string UserName = "AppUser";
        public const string Password = "Pass@123";
        public const string JwtDetail = "JwtDetail";
        public const string Subject = "Subject";
        public const string Issuer = "Issuer";
        public const string Audience = "Audience";
        public const string Key = "Key";
        public const string ContentType = "application/json";
        public const int ExpiryTime = 30;
        public const string Success = "Success";
        public const string Error = "Error";
        public const string InvalidDataMsg = "Username or password is incorrect";
        public const string TokenGenerated = "Token Generated Successfully";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationModule
{
    public static class StaticData
    {
        public const string LOG_CONFIG_FILE = "log4net.config";
        public const string USER = "AppUser";
        public const string PASSWORD = "Pass@123";
        public const string JWT_DETAIL = "JwtDetail";
        public const string SUBJECT = "Subject";
        public const string ISSUER = "Issuer";
        public const string AUDIENCE = "Audience";
        public const string KEY = "Key";
        public const string CONTENTTYPE = "application/json";
        public const int EXPIRESAFTER = 30;
    }
}

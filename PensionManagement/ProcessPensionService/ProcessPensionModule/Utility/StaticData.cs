using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionModule.Utility
{
    public static class StaticData
    {
        public const string LogConfigFile = "log4net.config";
        public const string JwtDetail = "JwtDetail";
        public const string Subject = "Subject";
        public const string Issuer = "Issuer";
        public const string Audience = "Audience";
        public const string Key = "Key";
        public const string PensionerDetailApiUrl = "PensionerDetailAPIURL";
        public const string ContentType = "application/json";
        public const string GetPensionerDetail = "/api/PensionerDetail/PensionerDetailByAadhaar?aadhaarNumber=";
        public const string AccessToken = "access_token";
        public const string Authorization = "Authorization";
        public const string Bearer = "Bearer ";
        public const string Success = "Success";
        public const string Error = "Error";
        public const string PensionerNotFound = "Invalid pensioner detail provided, please provide valid detail";
        public const string PensionCalculated = "Pension Calculated Successfully!";


    }
}

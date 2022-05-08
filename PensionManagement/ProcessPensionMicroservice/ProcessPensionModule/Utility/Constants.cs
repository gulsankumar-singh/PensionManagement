using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionModule.Utility
{
    public static class Constants
    {
        public const string LOG_CONFIG_FILE = "log4net.config";
        public const string DATA_SET = @"Services\DataSetup\PensionerList.csv";
        public const string GET_PENSIONER_DETAIL = "/api/PensionerDetail/PensionerDetailByAadhaar?aadhaarNumber=";
        public const string PENSIONER_DETAIL_API_URL = "PensionerDetailAPIURL";
        public const string JWT_DETAIL = "JwtDetail";
        public const string SUBJECT = "Subject";
        public const string ISSUER = "Issuer";
        public const string AUDIENCE = "Audience";
        public const string KEY = "Key";
    }
}

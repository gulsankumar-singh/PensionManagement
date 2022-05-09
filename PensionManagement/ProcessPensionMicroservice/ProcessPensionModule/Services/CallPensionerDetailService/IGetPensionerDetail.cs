using ProcessPensionModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionModule.Services.CallPensionerDetailService
{
    public interface IGetPensionerDetail
    {
        Task<PensionerInfo> GetPensionerDetailAsync(long aadhaarNumber, string accessToken);
    }
}

using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProcessPensionModule.Models;
using ProcessPensionModule.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProcessPensionModule.Services.CallPensionerDetailService
{
    /// <summary>
    /// Class for getting the Pensioner detail from 
    /// the PensionerDetailMicroservice
    /// </summary>
    public class GetPensionerDetail : IGetPensionerDetail
    {
        private string PensionerDetailAPIURL { get; set; }
        private readonly ILog _logger;

        /// <summary>
        /// GetPensionerDetail Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public GetPensionerDetail(IConfiguration configuration)
        {
            _logger = LogManager.GetLogger(typeof(GetPensionerDetail));
            PensionerDetailAPIURL = configuration.GetSection(Constants.PENSIONER_DETAIL_API_URL).Value;
        }

        /// <summary>
        /// Get Pensioner detail from PensionerDetailMicroservice
        /// </summary>
        /// <param name="aadhaarNumber"></param>
        /// <returns></returns>
        public async Task<PensionerInfo> GetPensionerDetailAsync(long aadhaarNumber)
        {
            PensionerInfo pensioner = null;
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(PensionerDetailAPIURL);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage responseMessage = await client.GetAsync(Constants.GET_PENSIONER_DETAIL + aadhaarNumber);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var result = responseMessage.Content.ReadAsStringAsync().Result;
                        pensioner = JsonConvert.DeserializeObject<PensionerInfo>(result);
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            return pensioner;
        }
    }
}

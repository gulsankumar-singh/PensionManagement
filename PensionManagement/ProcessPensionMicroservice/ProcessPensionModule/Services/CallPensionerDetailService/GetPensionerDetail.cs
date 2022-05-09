using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProcessPensionModule.Models;
using ProcessPensionModule.Utility;
using System;
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
        //private readonly ILog _logger;
        private readonly ILogger<GetPensionerDetail> _logger;

        /// <summary>
        /// GetPensionerDetail Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public GetPensionerDetail(IConfiguration configuration, ILogger<GetPensionerDetail> logger)
        {
            _logger = logger;//LogManager.GetLogger(typeof(GetPensionerDetail));
            PensionerDetailAPIURL = configuration.GetSection(StaticData.PENSIONER_DETAIL_API_URL).Value;
        }

        /// <summary>
        /// Get Pensioner detail from PensionerDetailMicroservice
        /// </summary>
        /// <param name="aadhaarNumber"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<PensionerInfo> GetPensionerDetailAsync(long aadhaarNumber, string accessToken)
        {
            PensionerInfo pensioner = null;
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(PensionerDetailAPIURL);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(StaticData.CONTENT_TYPE));
                  
                    client.DefaultRequestHeaders.Add(StaticData.AUTHORIZATION, StaticData.BEARER + accessToken);
                    HttpResponseMessage responseMessage = await client.GetAsync(StaticData.GET_PENSIONER_DETAIL + aadhaarNumber);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var result = responseMessage.Content.ReadAsStringAsync().Result;
                        pensioner = JsonConvert.DeserializeObject<PensionerInfo>(result);
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return pensioner;
        }
    }
}

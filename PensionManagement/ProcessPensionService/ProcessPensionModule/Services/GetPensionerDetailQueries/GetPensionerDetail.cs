using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProcessPensionModule.Models.ViewModels;
using ProcessPensionModule.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProcessPensionModule.Services.GetPensionerDetailQueries
{
    public class GetPensionerDetail : IGetPensionerDetail
    {
        private string PensionerDetailAPIURL { get; set; }
        private readonly ILogger<GetPensionerDetail> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// GetPensionerDetail Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public GetPensionerDetail(IConfiguration configuration, ILogger<GetPensionerDetail> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            PensionerDetailAPIURL = configuration.GetSection(StaticData.PENSIONER_DETAIL_API_URL).Value;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get Pensioner detail from PensionerDetailMicroservice
        /// </summary>
        /// <param name="aadhaarNumber"></param>
        /// <returns></returns>
        public async Task<PensionerVM> GetPensionerDetailByAadhaar(long aadhaarNumber)
        {
            PensionerVM pensioner = null;
            try
            {
                HttpContext httpContext = _httpContextAccessor.HttpContext;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(PensionerDetailAPIURL);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(StaticData.CONTENT_TYPE));

                    //client.DefaultRequestHeaders.Add(StaticData.AUTHORIZATION, StaticData.BEARER + accessToken);
                    HttpResponseMessage responseMessage = await client.GetAsync(StaticData.GET_PENSIONER_DETAIL + aadhaarNumber);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var result = responseMessage.Content.ReadAsStringAsync().Result;
                        pensioner = JsonConvert.DeserializeObject<PensionerVM>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return pensioner;
        }
    }
}

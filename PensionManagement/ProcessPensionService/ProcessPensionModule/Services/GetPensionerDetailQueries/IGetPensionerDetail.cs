using ProcessPensionModule.Models.ViewModels;
using System.Threading.Tasks;

namespace ProcessPensionModule.Services.GetPensionerDetailQueries
{
    public interface IGetPensionerDetail
    {
        Task<PensionerVM> GetPensionerDetailByAadhaar(long aadhaarNumber);
    }
}

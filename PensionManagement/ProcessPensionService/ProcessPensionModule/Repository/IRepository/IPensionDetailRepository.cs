using ProcessPensionModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionModule.Repository.IRepository
{
    public interface IPensionDetailRepository
    {
        ICollection<PensionDetail> GetAllPensionDetail();

        PensionDetail GetPensionDetail(int pensionId);
        Task<bool> CreatePensionDetail(PensionDetail pensionDetail);
        Task<bool> Save();
    }
}

using ProcessPensionModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionModule.Repository.IRepository
{
    /// <summary>
    /// Repository for Pension Detail
    /// </summary>
    public interface IPensionDetailRepository
    {
        /// <summary>
        /// Methods declaration for Adding 
        /// Pension Detail
        /// </summary>
        /// <param name="pensionDetail"></param>
        /// <returns></returns>
        Task CreatePensionDetail(PensionDetail pensionDetail);
    }
}

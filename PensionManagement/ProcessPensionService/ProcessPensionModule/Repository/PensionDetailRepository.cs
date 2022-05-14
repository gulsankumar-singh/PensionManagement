using ProcessPensionModule.AppDbContext;
using ProcessPensionModule.Models;
using ProcessPensionModule.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionModule.Repository
{
    public class PensionDetailRepository : IPensionDetailRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PensionDetailRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Add the Pension Detail to the database
        /// </summary>
        /// <param name="pensionDetail"></param>
        /// <returns></returns>
        public async Task CreatePensionDetail(PensionDetail pensionDetail)
        {
            await _dbContext.PensionDetails.AddAsync(pensionDetail);
            await _dbContext.SaveChangesAsync();
        }

    }
}

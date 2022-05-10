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

        public async Task<bool> CreatePensionDetail(PensionDetail pensionDetail)
        {
            await _dbContext.PensionDetails.AddAsync(pensionDetail);
            return await Save();
        }

        public ICollection<PensionDetail> GetAllPensionDetail()
        {
            return _dbContext.PensionDetails.ToList();
        }

        public PensionDetail GetPensionDetail(int pensionId)
        {
            return _dbContext.PensionDetails.FirstOrDefault(p => p.Id == pensionId);
        }

        public async Task<bool> Save()
        {
            return await _dbContext.SaveChangesAsync() >= 0;
        }
    }
}

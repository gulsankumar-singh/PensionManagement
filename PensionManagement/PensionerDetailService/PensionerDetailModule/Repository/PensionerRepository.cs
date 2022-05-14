using Microsoft.EntityFrameworkCore;
using PensionerDetailModule.AppDbContext;
using PensionerDetailModule.Models;
using PensionerDetailModule.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailModule.Repository
{
    public class PensionerRepository : IPensionerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PensionerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Pensioner>> GetAllPensioner()
        {
            return await _dbContext.Pensioners.Include(i => i.BankDetail).ToListAsync();
        }

        public async Task<Pensioner> GetPensionerByAadhaar(long aadharNumber)
        {
            return await _dbContext.Pensioners.Include(i => i.BankDetail).FirstOrDefaultAsync(i => i.AadharNumber == aadharNumber);
        }
    }
}

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
        public List<PensionerDetail> GetAllPensioner()
        {
            return _dbContext.PensionerDetails.Include(i => i.BankDetail).ToList();
        }

        public PensionerDetail GetPensionerByAadhaar(long aadharNumber)
        {
            return _dbContext.PensionerDetails.Include(i => i.BankDetail).FirstOrDefault(i => i.AadharNumber == aadharNumber);
        }
    }
}

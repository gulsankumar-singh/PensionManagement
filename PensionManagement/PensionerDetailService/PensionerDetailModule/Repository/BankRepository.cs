using PensionerDetailModule.AppDbContext;
using PensionerDetailModule.Models;
using PensionerDetailModule.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailModule.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BankRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Bank> GetAllBankAsync()
        {
            return _dbContext.Banks.ToList();
        }
    }
}

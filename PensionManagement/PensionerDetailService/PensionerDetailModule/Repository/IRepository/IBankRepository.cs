using PensionerDetailModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailModule.Repository.IRepository
{
    public interface IBankRepository
    {
        List<Bank> GetAllBankAsync();
    }
}

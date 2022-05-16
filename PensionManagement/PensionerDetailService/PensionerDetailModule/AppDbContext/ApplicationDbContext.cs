using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PensionerDetailModule.Models;
using PensionerDetailModule.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailModule.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Pensioner> Pensioners { get; set; }
        public DbSet<Bank> Banks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pensioner>()
                .ToTable(StaticData.Pensioners);

            modelBuilder.Entity<Bank>()
                .ToTable(StaticData.Banks);

            modelBuilder.Entity<Pensioner>()
                .HasData(GetPensionerDataFromCsv());

            modelBuilder.Entity<Bank>()
                .HasData(GetBankDataFromCsv());
        }

        private static List<Pensioner> GetPensionerDataFromCsv()
        {
            List<Pensioner> pensionerDetails = null;
            try
            {
                pensionerDetails = new List<Pensioner>();
                using (StreamReader sr = new StreamReader(StaticData.PensionerDataSet))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');

                        pensionerDetails.Add(new Pensioner()
                        {
                            Id = Convert.ToInt32(values[0]),
                            Name = values[1],
                            DateOfBirth = Convert.ToDateTime(values[2]),
                            PanNumber = values[3],
                            AadharNumber = Convert.ToInt64(values[4]),
                            SalaryEarned = Convert.ToInt64(values[5]),
                            Allowances = Convert.ToInt64(values[6]),
                            PensionType = (PensionTypes)Enum.Parse(typeof(PensionTypes), values[7]),
                            AccountNumber = Convert.ToInt64(values[8]),
                            BankId = Convert.ToInt32(values[9]),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                 throw;
            }
            return pensionerDetails;
        }

        private static List<Bank> GetBankDataFromCsv()
        {
            List<Bank> bankList = null;
            try
            {
                bankList = new List<Bank>();
                using (StreamReader sr = new StreamReader(StaticData.BankDataSet))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');

                        bankList.Add(new Bank()
                        {
                            Id = Convert.ToInt32(values[0]),
                            BankName = values[1],
                            BankType = (BankTypes)Enum.Parse(typeof(BankTypes), values[2])
                        });
                    }
                }
                return bankList;
            }
            catch (Exception ex)
            {
                throw;
            }
 
        }

    }
}

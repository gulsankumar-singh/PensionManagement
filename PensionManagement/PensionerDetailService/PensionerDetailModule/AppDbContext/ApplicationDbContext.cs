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

        public DbSet<PensionerDetail> PensionerDetails { get; set; }
        public DbSet<BankDetail> BankDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PensionerDetail>()
                .ToTable("PensionerDetails");

            modelBuilder.Entity<PensionerDetail>()
                .ToTable("BankDetail");

            modelBuilder.Entity<PensionerDetail>()
                .HasData(GetPensionerDataFromCsv());

            modelBuilder.Entity<BankDetail>()
                .HasData(GetBankDataFromCsv());
        }

        private static List<PensionerDetail> GetPensionerDataFromCsv()
        {
            List<PensionerDetail> pensionerDetails = null;
            try
            {
                pensionerDetails = new List<PensionerDetail>();
                using (StreamReader sr = new StreamReader(StaticData.PENSIONER_DATA_SET))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');

                        pensionerDetails.Add(new PensionerDetail()
                        {
                            Id = Convert.ToInt32(values[0]),
                            Name = values[1],
                            DateOfBirth = Convert.ToDateTime(values[2]),
                            PAN = values[3],
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

        private static List<BankDetail> GetBankDataFromCsv()
        {
            List<BankDetail> bankList = null;
            try
            {
                bankList = new List<BankDetail>();
                using (StreamReader sr = new StreamReader(StaticData.BANK_DATA_SET))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');

                        bankList.Add(new BankDetail()
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

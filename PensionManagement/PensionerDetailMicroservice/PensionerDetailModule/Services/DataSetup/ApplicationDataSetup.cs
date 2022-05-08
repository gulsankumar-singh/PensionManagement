using log4net;
using Microsoft.Extensions.Logging;
using PensionerDetailModule.Models;
using PensionerDetailModule.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PensionerDetailModule.Services.DataSetup
{
    public class ApplicationDataSetup : IApplicationDataSetup
    { 
        private static List<PensionerDetail> _pensionerDetails = null;
        private static ILog _logger = null;

        public ApplicationDataSetup()
        {
            _logger = LogManager.GetLogger(typeof(ApplicationDataSetup));
        }

        public List<PensionerDetail> GetPensionerDetails()
        {
            _logger.Info("Starting Get Pensioner Details method");

            try{
                if (_pensionerDetails != null && _pensionerDetails.Any())
                {
                    return _pensionerDetails;
                }
            
                _pensionerDetails = new List<PensionerDetail>();
                //string path = Path.Combine(Directory.GetCurrentDirectory(), Constants.DATA_SET);
                using (StreamReader sr = new StreamReader("PensionerList.csv"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');

                        _pensionerDetails.Add(new PensionerDetail()
                        {
                            Name = values[0],
                            DateOfBirth = Convert.ToDateTime(values[1]),
                            PAN = values[2],
                            AadharNumber = Convert.ToInt64(values[3]),
                            SalaryEarned = Convert.ToInt64(values[4]),
                            Allowances = Convert.ToInt64(values[5]),
                            PensionType = (PensionTypes)Enum.Parse(typeof(PensionTypes), values[6]),
                            AccountNumber = Convert.ToInt64(values[7]),
                            BankName = values[8],
                            BankType = (BankTypes)Enum.Parse(typeof(BankTypes), values[9])
                        });
                    }
                }
            }
            catch(Exception ex){
                _logger.Error(ex);
                return null;
            }

            _logger.Info("Ending Get Pensioner Details method");
            return _pensionerDetails;
        }
    }
}

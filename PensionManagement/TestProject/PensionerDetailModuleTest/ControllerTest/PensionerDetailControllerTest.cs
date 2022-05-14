using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PensionerDetailModule.API.Controllers;
using PensionerDetailModule.Models;
using PensionerDetailModule.Models.Dto;
using PensionerDetailModule.Repository;
using PensionerDetailModule.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailModuleTest.ControllersTest
{
    [TestFixture]
    public class PensionerDetailControllerTest
    {
        private readonly IConfiguration _configuration;
        private readonly Mock<ILogger<PensionerDetailController>> _loggerMock;
        private readonly Mock<IPensionerRepository> _pensionerRepositoryMock;
        //private readonly IPensionerRepository _pensionerRepositoryMock;
        private static IMapper _mapper;
        private readonly PensionerDetailController _pensionerDetailController; 

        public PensionerDetailControllerTest()
        {
            _configuration = PensionerDetailModuleConfig.GetConfiguration();
            _loggerMock = new Mock<ILogger<PensionerDetailController>>();
            _pensionerRepositoryMock = new Mock<IPensionerRepository>();
            //_pensionerRepositoryMock = new PensionerRepository(PensionerDetailModuleConfig.GetApplicationDbContext());
            if (_mapper == null)
            {
                _mapper = PensionerDetailModuleConfig.GetAutoMapperConfiguration();

            }
            _pensionerDetailController = new PensionerDetailController(_loggerMock.Object, _mapper, _pensionerRepositoryMock.Object);

        }

        [Test]
        public async Task GetPensionerList_ReturnsListOfPensioner()
        {
            //Arrange
            _pensionerRepositoryMock.Setup(i => i.GetAllPensioner()).ReturnsAsync(GetListOfPensioners());

            //Act
            var actionResult = await _pensionerDetailController.GetPensionerList();
            var actionResultValue = (List<PensionerDto>)((OkObjectResult)actionResult.Result).Value;
           

            //Assert
            Assert.IsNotNull(_pensionerDetailController);
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResultValue);
            Assert.IsInstanceOf<List<PensionerDto>>(actionResultValue);
            Assert.AreEqual(20, actionResultValue.Count);
        }

        [Test]
        public async Task PensionerDetailByAadhaar_ReturnsPensioner_WhenCorrectAadhaar()
        {
            //Arrange
            long aadhaarNumber = 791714947214;
            var pensionerDto = new PensionerDetailDto()
            {
                Name = "Ajay",
                DateOfBirth = new DateTime(2000, 10, 02, 00, 00, 00),
                PanNumber = "KSMOC9374L",
                AadharNumber = 791714947214,
                AccountNumber = 968557297810,
                SalaryEarned = 436986,
                Allowances = 19292,
                PensionType = (PensionTypes)0,
                BankDetail = new BankDto()
                {
                    BankName = "Axis Bank Ltd.",
                    BankType = (BankTypes)1,
                }
            };
            _pensionerRepositoryMock.Setup(i => i.GetPensionerByAadhaar(aadhaarNumber)).Returns(Task.FromResult(GetPensionerByAadhaarHelper(aadhaarNumber)));

            //Act
            var actionResult = await _pensionerDetailController.PensionerDetailByAadhaar(aadhaarNumber);
            PensionerDetailDto actionResultValue = (PensionerDetailDto)((OkObjectResult)actionResult.Result).Value;


            //Assert
            Assert.IsNotNull(_pensionerDetailController);
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResultValue);
            Assert.IsInstanceOf<PensionerDetailDto>(actionResultValue);
        }

        [Test]
        public async Task PensionerDetailByAadhaar_ReturnsNotFound_WhenBadAadhaar()
        {
            //Arrange
            long aadhaarNumber = 791714947344;

            //Act
            var actionResult = await _pensionerDetailController.PensionerDetailByAadhaar(aadhaarNumber);
            var actionResultValue = (NotFoundObjectResult)actionResult.Result;


            //Assert
            Assert.IsNotNull(_pensionerDetailController);
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResultValue);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResultValue);
        }

        private static List<Pensioner> GetListOfPensioners()
        {
            List<Bank> bankList = GetAllBank();
            List<Pensioner> pensionerList = new List<Pensioner>()
            {
                new Pensioner()
                {
                    Id = 1,
                    AadharNumber = 791714947214L,
                    AccountNumber = 968557297810L,
                    Allowances = 19292L,
                    DateOfBirth = new DateTime(2000, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Ajay",
                    PanNumber = "KSMOC9374L",
                    PensionType = (PensionTypes)0,
                    SalaryEarned = 436986L,
                    BankDetail = new Bank
                    {
                        Id=5,
                        BankName= "Axis Bank Ltd.",
                        BankType= (BankTypes)1
                    }
                },
                new Pensioner()
                {
                    Id = 2,
                    AadharNumber = 854728326906L,
                    AccountNumber = 81730197274L,
                    Allowances = 26704L,
                    BankId = 1,
                    DateOfBirth = new DateTime(1991, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Sourav",
                    PanNumber = "KDRTI7666A",
                    PensionType = (PensionTypes)Enum.Parse(typeof(PensionTypes), "0"),
                    SalaryEarned = 468418L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 1)
                },
                new Pensioner()
                {
                    Id = 3,
                    AadharNumber = 974455078114L,
                    AccountNumber = 40740232906L,
                    Allowances = 36476L,
                    BankId = 9,
                    DateOfBirth = new DateTime(1974, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Sameer",
                    PanNumber = "ZWKOF2689R",
                    PensionType = (PensionTypes)1,
                    SalaryEarned = 865912L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 9)
                },
                new Pensioner()
                {
                    Id = 4,
                    AadharNumber = 461755449180L,
                    AccountNumber = 467028762867L,
                    Allowances = 31476L,
                    BankId = 5,
                    DateOfBirth = new DateTime(1980, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Vivaan",
                    PanNumber = "IXSUT5186S",
                    PensionType = (PensionTypes)0,
                    SalaryEarned = 925642L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 5)
                },
                new Pensioner()
                {
                    Id = 5,
                    AadharNumber = 389444809498L,
                    AccountNumber = 560710767357L,
                    Allowances = 1406L,
                    BankId = 7,
                    DateOfBirth = new DateTime(2002, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Saket",
                    PanNumber = "UOYCV0783O",
                    PensionType = (PensionTypes)0,
                    SalaryEarned = 885577L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 7)
                },
                new Pensioner()
                {
                    Id = 6,
                    AadharNumber = 937162602552L,
                    AccountNumber = 315827677945L,
                    Allowances = 38035L,
                    BankId = 4,
                    DateOfBirth = new DateTime(1991, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Rahul",
                    PanNumber = "GPPVI6213L",
                    PensionType = (PensionTypes)1,
                    SalaryEarned = 619564L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 4)
                },
                new Pensioner()
                {
                    Id = 7,
                    AadharNumber = 287244420393L,
                    AccountNumber = 2473327567L,
                    Allowances = 45100L,
                    BankId = 3,
                    DateOfBirth = new DateTime(1957, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Rohan",
                    PanNumber = "BXLEY3454O",
                    PensionType = (PensionTypes)1,
                    SalaryEarned = 593845L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 3)
                },
                new Pensioner()
                {
                    Id = 8,
                    AadharNumber = 874313859213L,
                    AccountNumber = 779371944069L,
                    Allowances = 29185L,
                    BankId = 4,
                    DateOfBirth = new DateTime(1987, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Pragya",
                    PanNumber = "GOWUN8949C",
                    PensionType = (PensionTypes)0,
                    SalaryEarned = 461227L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 4)
                },
                new Pensioner()
                {
                    Id = 9,
                    AadharNumber = 789953042547L,
                    AccountNumber = 265723038859L,
                    Allowances = 5890L,
                    BankId = 10,
                    DateOfBirth = new DateTime(1994, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Kunal",
                    PanNumber = "QKSXL5968C",
                    PensionType = (PensionTypes)0,
                    SalaryEarned = 520820L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 10)
                },
                new Pensioner()
                {
                    Id = 10,
                    AadharNumber = 337877837167L,
                    AccountNumber = 639415907726L,
                    Allowances = 14845L,
                    BankId = 8,
                    DateOfBirth = new DateTime(1995, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Anjali",
                    PanNumber = "VOVUQ4952B",
                    PensionType = (PensionTypes)1,
                    SalaryEarned = 593084L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 8)
                },
                new Pensioner()
                {
                    Id = 11,
                    AadharNumber = 888861335475L,
                    AccountNumber = 297580783520L,
                    Allowances = 39151L,
                    BankId = 2,
                    DateOfBirth = new DateTime(1994, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Ranjeet",
                    PanNumber = "BIOCM9234K",
                    PensionType = (PensionTypes)1,
                    SalaryEarned = 752833L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 2)
                },
                new Pensioner()
                {
                    Id = 12,
                    AadharNumber = 602310740678L,
                    AccountNumber = 943925554740L,
                    Allowances = 4419L,
                    BankId = 1,
                    DateOfBirth = new DateTime(1957, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Shubham",
                    PanNumber = "AJNZV5132D",
                    PensionType = (PensionTypes)1,
                    SalaryEarned = 507340L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 1)
                },
                new Pensioner()
                {
                    Id = 13,
                    AadharNumber = 869852758857L,
                    AccountNumber = 571881193550L,
                    Allowances = 3807L,
                    BankId = 5,
                    DateOfBirth = new DateTime(1984, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Sunidhi",
                    PanNumber = "OYGXW4443E",
                    PensionType = (PensionTypes)0,
                    SalaryEarned = 866978L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 5)
                },
                new Pensioner()
                {
                    Id = 14,
                    AadharNumber = 150242523686L,
                    AccountNumber = 657059136087L,
                    Allowances = 41707L,
                    BankId = 3,
                    DateOfBirth = new DateTime(1963, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Rohit",
                    PanNumber = "YIUGX7931I",
                    PensionType = (PensionTypes)0,
                    SalaryEarned = 810046L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 3)
                },
                new Pensioner()
                {
                    Id = 15,
                    AadharNumber = 960272640642L,
                    AccountNumber = 162572811085L,
                    Allowances = 14068L,
                    BankId = 6,
                    DateOfBirth = new DateTime(1976, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Aditya",
                    PanNumber = "UNVHI8166X",
                    PensionType = (PensionTypes)0,
                    SalaryEarned = 710003L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 6)
                },
                new Pensioner()
                {
                    Id = 16,
                    AadharNumber = 471714613539L,
                    AccountNumber = 204687301318L,
                    Allowances = 37704L,
                    BankId = 9,
                    DateOfBirth = new DateTime(1996, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Gulshan",
                    PanNumber = "ZEOSA8100V",
                    PensionType = (PensionTypes)1,
                    SalaryEarned = 869460L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 9)
                },
                new Pensioner()
                {
                    Id = 17,
                    AadharNumber = 653515346579L,
                    AccountNumber = 23368227618L,
                    Allowances = 24871L,
                    BankId = 1,
                    DateOfBirth = new DateTime(1978, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Ram",
                    PanNumber = "YOLLZ3272S",
                    PensionType = (PensionTypes)1,
                    SalaryEarned = 606992L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 1)
                },
                new Pensioner()
                {
                    Id = 18,
                    AadharNumber = 930139439179L,
                    AccountNumber = 713766531809L,
                    Allowances = 4976L,
                    BankId = 2,
                    DateOfBirth = new DateTime(1975, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Ujjwal",
                    PanNumber = "QAQBL8395S",
                    PensionType = (PensionTypes)0,
                    SalaryEarned = 575598L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 2)
                },
                new Pensioner()
                {
                    Id = 19,
                    AadharNumber = 280225974218L,
                    AccountNumber = 349195618403L,
                    Allowances = 28821L,
                    BankId = 8,
                    DateOfBirth = new DateTime(1999, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Akash",
                    PanNumber = "YEBSQ6580R",
                    PensionType = (PensionTypes)0,
                    SalaryEarned = 629579L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 8)
                },
                new Pensioner()
                {
                    Id = 20,
                    AadharNumber = 643359853704L,
                    AccountNumber = 972016531683L,
                    Allowances = 40949L,
                    BankId = 9,
                    DateOfBirth = new DateTime(1991, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    Name = "Sunita",
                    PanNumber = "OKOKS6038K",
                    PensionType = (PensionTypes)1,
                    SalaryEarned = 871959L,
                    BankDetail = bankList.FirstOrDefault(i => i.Id == 9)
                }
            };

            return pensionerList;
        }

        private static Pensioner GetPensionerByAadhaarHelper(long aadhar)
        {
            List<Pensioner> pensioners = GetListOfPensioners();

            return pensioners.FirstOrDefault(i => i.AadharNumber == aadhar);
        }

        public static List<Bank> GetAllBank()
        {
            List<Bank> bankList = new List<Bank>()
            {
                new Bank()
                        {
                            Id = 1,
                            BankName = "HDFC Bank Ltd.",
                            BankType = (BankTypes)1
                        },
                new Bank()
                {
                    Id = 2,
                    BankName = "State Bank of India",
                    BankType = (BankTypes)0
                },
                new Bank()
                {
                    Id = 3,
                    BankName = "ICICI Bank Ltd.",
                    BankType = (BankTypes)1
                },
                new Bank()
                {
                    Id = 4,
                    BankName = "Kotak Mahindra Bank",
                    BankType = (BankTypes)1
                },
                new Bank()
                {
                    Id = 5,
                    BankName = "Axis Bank Ltd.",
                    BankType = (BankTypes)1
                },
                new Bank()
                {
                    Id = 6,
                    BankName = "Indusland Bank Ltd.",
                    BankType = (BankTypes)1
                },
                new Bank()
                {
                    Id = 7,
                    BankName = "Yes Bank Ltd.",
                    BankType = (BankTypes)1
                },
                new Bank()
                {
                    Id = 8,
                    BankName = "Panjab National Bank",
                    BankType = (BankTypes)0
                },
                new Bank()
                {
                    Id = 9,
                    BankName = "Bank of Baroda",
                    BankType = (BankTypes)0
                },
                new Bank()
                {
                    Id = 10,
                    BankName = "Bank of India",
                    BankType = (BankTypes)0
                }
            };

            return bankList;
        }
    
    }
}

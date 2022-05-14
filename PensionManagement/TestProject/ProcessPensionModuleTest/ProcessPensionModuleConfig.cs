using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProcessPensionModule.AppDbContext;
using ProcessPensionModule.Utility.AutoMapperConfig;
using System.IO;

namespace ProcessPensionModuleTest
{
    public static class ProcessPensionModuleConfig
    {
        private static IConfiguration _configuration = null;
        private static string _connectionString = null;
        private static ApplicationDbContext _context;

        public static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        public static string GetConnectionString()
        {
            if (_configuration == null)
                _configuration = GetConfiguration();
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            return _connectionString;
        }

        public static ApplicationDbContext GetApplicationDbContext()
        {
            if (_configuration == null)
                _configuration = GetConfiguration();

            if (_connectionString == null)
                _connectionString = GetConnectionString();

            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_connectionString)
                .Options;

            _context = new ApplicationDbContext(dbContextOptions);
            return _context;
        }

        public static IMapper GetAutoMapperConfiguration()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingConfig());
            });
            return mappingConfig.CreateMapper();
            
        }
    }
}

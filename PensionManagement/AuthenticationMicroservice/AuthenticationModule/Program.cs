using AuthenticationModule.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AuthenticationModule
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var log4netRepository = log4net.LogManager.GetRepository(Assembly.GetEntryAssembly());
            //log4net.Config.XmlConfigurator.Configure(log4netRepository, new FileInfo(StaticData.LOG_CONFIG_FILE));
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddLog4Net(StaticData.LOG_CONFIG_FILE);
                });
    }
}

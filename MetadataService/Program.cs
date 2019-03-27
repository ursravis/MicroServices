using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace MetadataService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostContext, config) =>
                 {
                     var env = hostContext.HostingEnvironment;
                     // delete all default configuration providers
                     config.Sources.Clear();
                     config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                     config.AddEnvironmentVariables();
                 });
                //   .ConfigureLogging(logging =>
                //     {
                //         logging.ClearProviders();
                //         logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                //     })
                //     .UseNLog();  // NLog: setup NLog for Dependency injection
    }
}

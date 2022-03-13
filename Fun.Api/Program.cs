using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using NLog.Web;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog;

namespace Fun.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));
            var logger = NLogBuilder.ConfigureNLog(LogManager.Configuration).GetCurrentClassLogger();

            try
            {
                logger.Debug("Init main");
                CreateWebHostBuilder(args, config).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfigurationRoot config)
        {
            return WebHost.CreateDefaultBuilder(args)
                    .UseConfiguration(config)
                    .UseStartup<Startup>()
                    .ConfigureLogging(logging => logging.ClearProviders())
                    .UseNLog();
        }
    }
}
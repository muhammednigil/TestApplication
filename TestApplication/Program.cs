using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                                       .SetBasePath(Directory.GetCurrentDirectory())
                                       .AddJsonFile("appsettings.json");
#if DEBUG
            configurationBuilder.AddJsonFile($"appsettings.Development.json", true);
#endif
            var configuration = configurationBuilder.Build();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

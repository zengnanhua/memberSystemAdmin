using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdminSystem.Api.Extensions;
using AdminSystem.Api.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using AdminSystem.Infrastructure;
using Serilog;

namespace AdminSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var configuration = GetConfiguration();
            var host = CreateWebHostBuilder(args).Build();
            host.MigrateDbContext<ApplicationDbContext>((context, services) =>
            {
                var env = services.GetService<IHostingEnvironment>();
                new ApplicationDbContextSeed()
                    .SeedAsync(context, env)
                    .Wait();
            });
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog(InitSerilog)
                .UseStartup<Startup>();

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            return builder.Build();
        }

        /// <summary>
        /// 初始化  Serilog
        /// </summary>
        /// <param name="hostingContext"></param>
        /// <param name="loggerConfiguration"></param>
        private static void InitSerilog(WebHostBuilderContext hostingContext, LoggerConfiguration loggerConfiguration)
        {
            var outPutTempLate = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level}] {HttpRequestId} {NewLine} {SourceContext} {NewLine} {Message}{NewLine}{Exception}{NewLine}";
            var Namespace = typeof(Program).Namespace;
            loggerConfiguration
            .Enrich.WithProperty("ApplicationContext", Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1))
            .WriteTo.RollingFile("./log/log-{Date}.txt",outputTemplate: outPutTempLate)
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: outPutTempLate);
        }
    }
}

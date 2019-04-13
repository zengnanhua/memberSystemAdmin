using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UserIdentity.Data;
using UserIdentity.Extensions;

namespace UserIdentity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();

            var host= BuildWebHost(args);
            host.MigrateDbContext<ConfigurationDbContext>((context, services) =>
            {
                new ConfigurationDbContextSeed()
                    .SeedAsync(context, configuration)
                    .Wait();
            });
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();


        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            return builder.Build();
        }
    }
}

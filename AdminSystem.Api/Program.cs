﻿using System;
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
    }
}

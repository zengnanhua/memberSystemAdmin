using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UserIdentity.AuthenticationValidator;

namespace UserIdentity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("MysqlConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()
               .AddDeveloperSigningCredential()
               .AddExtensionGrantValidator<UserPwdValidator>()
               .AddConfigurationStore(options =>
               {
                   options.ConfigureDbContext = builder => builder.UseMySql(connectionString,
                   mySqlOptionsAction: sqlOptions =>
                   {
                       sqlOptions.MigrationsAssembly(migrationsAssembly);
                   });
               })
               .AddOperationalStore(options =>
               {
                   options.ConfigureDbContext = builder => builder.UseMySql(connectionString,
                       mySqlOptionsAction: sqlOptions =>
                       {
                           sqlOptions.MigrationsAssembly(migrationsAssembly);
                       });
               });
               //.AddDeveloperSigningCredential()
               //.AddInMemoryClients(Config.GetClients())
               //.AddInMemoryIdentityResources(Config.GetIdentityResources())
               //.AddInMemoryApiResources(Config.GetApiResources())
               ;


            services.AddMvc();
        }

      
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}

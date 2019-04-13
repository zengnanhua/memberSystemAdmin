using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UserIdentity.AuthenticationValidator;
using UserIdentity.Services;

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
                       sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                   });
               })
               .AddOperationalStore(options =>
               {
                   options.ConfigureDbContext = builder => builder.UseMySql(connectionString,
                       mySqlOptionsAction: sqlOptions =>
                       {
                           sqlOptions.MigrationsAssembly(migrationsAssembly);
                           sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                       });
               })
               .Services.AddScoped<IProfileService, ProfileService>();
               //.AddDeveloperSigningCredential()
               //.AddInMemoryClients(Config.GetClients())
               //.AddInMemoryIdentityResources(Config.GetIdentityResources())
               //.AddInMemoryApiResources(Config.GetApiResources())
               ;

            #region 允许所有跨域
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            #endregion

            services.AddMvc();
        }

      
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("cors");
            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}

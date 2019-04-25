using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AdminSystem.Api.Infrastructure.AutofacModules;
using AdminSystem.Api.Infrastructure.Filters;
using AdminSystem.Application.Services;
using AdminSystem.Infrastructure;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace AdminSystem.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

      
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {


            services
              .AddSystemRegisterType(Configuration)
              .AddCustomSwagger(Configuration)
              .AddCustomDbContext(Configuration)
              .AddAuthorization()
              .AddMvc(options=> 
              {
                  options.Filters.Add(typeof(HttpGlobalExceptionFilter));
              }).AddControllersAsServices()
              .AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });


            #region 允许跨域访问
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

            #region 验证权限
            services.AddAuthentication("Bearer")
           .AddIdentityServerAuthentication(options =>
           {
               options.Authority = Configuration["AuthorizationUrl"];
               options.RequireHttpsMetadata = false;
               options.ApiName = "admin_api";
           });
            #endregion

            #region 使用 autofac
            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule(Configuration.GetConnectionString("MysqlConnection")));

            #endregion

            return new AutofacServiceProvider(container.Build());
        }

       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");//允许跨域

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint($"/swagger/v1/swagger.json", "adminSystem.Api");
                   c.DocExpansion(DocExpansion.None);
                   //c.OAuthClientId("orderingswaggerui");
                   //c.OAuthAppName("Ordering Swagger UI");
               });
        }
    }
    static class CustomExtensionsMethods
    {

        public static IServiceCollection AddSystemRegisterType(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdentityService, IdentityService>();
            return services;
        }
        /// <summary>
        /// 添加数据库
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkMySql()
                   .AddDbContext<ApplicationDbContext>(options =>
                   {
                       options.UseMySql(configuration.GetConnectionString("MysqlConnection"),
                           mySqlOptionsAction: sqlOptions =>
                           {
                               //sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                               sqlOptions.MigrationsAssembly("AdminSystem.Api");
                               sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                           });
                   },
                       ServiceLifetime.Scoped
                    );

            //services.AddDbContext<IntegrationEventLogContext>(options =>
            //{
            //    options.UseSqlServer(configuration["ConnectionString"],
            //                         sqlServerOptionsAction: sqlOptions =>
            //                         {
            //                             sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
            //                             //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
            //                             sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            //                         });
            //});

            return services;
        }

        
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                //options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "adminSystem.Api",
                    Version = "v1",
                    Description = "The adminSystem Service HTTP API",
                    TermsOfService = "Terms Of Service"
                });

               

                //options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                //{
                //    Description = "Authorization format : Bearer {token}",
                //    Name = "Authorization",
                //    In = "header",
                //    Type = "apiKey"
                //});
                options.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, "AdminSystem.Api.xml"));

                //options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                //{
                //    Type = "oauth2",
                //    Flow = "implicit",
                //    AuthorizationUrl = $"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize",
                //    TokenUrl = $"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/token",
                //    Scopes = new Dictionary<string, string>()
                //    {
                //        { "orders", "Ordering API" }
                //    }
                //});

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return services;
        }
    }
}

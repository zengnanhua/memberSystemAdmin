using System;
using AdminSystem.Api.Infrastructure.AutofacModules;
using AdminSystem.Api.Infrastructure.Filters;
using AdminSystem.Application.Services;
using AdminSystem.Infrastructure;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasyCaching.Core;
using EasyCaching.InMemory;
using EasyCaching.Redis;
using EasyCaching.Serialization.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.SignalR;
using AdminSystem.Application.Hubs;

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

            services.AddSignalR().AddStackExchangeRedis(options=> 
            {
                options.Configuration.DefaultDatabase = 3;
                options.Configuration.ChannelPrefix = "adminSystem";
                options.Configuration.EndPoints.Add("www.zengnanhua.club",6379);
            });

            services
              .AddCache(Configuration)
              .AddSystemRegisterType(Configuration)
              .AddCustomSwagger(Configuration)
              .AddCustomDbContext(Configuration)
              .AddAuthorization()
              .AddMvc(options=> 
              {
                  options.Filters.Add(typeof(HttpGlobalExceptionFilter));
              }).AddControllersAsServices()
              .AddJsonOptions(
                  options => 
                  {
                      options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                      options.SerializerSettings.DateFormatString = "yyyy-MM-dd hh:mm:ss";
                  }
              );


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
            //app.UseDefaultFiles();

            app.UseFileServer(new FileServerOptions());
            app.UseCors("CorsPolicy");//允许跨域

            //处理signalr权限认证问题
            app.UseSignalrAuthentication();

            app.UseAuthentication();
            app.UseEasyCaching();
            
            app.UseMvc();

            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint($"/swagger/v1/swagger.json", "adminSystem.Api");
                   c.DocExpansion(DocExpansion.None);
                   //c.OAuthClientId("orderingswaggerui");
                   //c.OAuthAppName("Ordering Swagger UI");
               });
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/api/chatHub");
            });
  
        }
    }
    static class CustomExtensionsMethods
    {
        #region Configure 扩展
        public static IApplicationBuilder UseSignalrAuthentication(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                // 这里从url中获取token参数，实际应用请实际考虑，加一些过滤条件
                if (context.Request.Query.TryGetValue("access_token", out var token))
                {
                    // 从url中拿到header，再添加到header中，一定要在UseAuthentication之前
                    context.Request.Headers.Add("Authorization", $"Bearer {token}");
                }
                await next.Invoke();
            });
            return app;
        }
        #endregion

        #region ConfigureServices扩展
        public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEasyCaching(option =>
            {
                option.UseInMemory("m1");
                option.UseRedis(config =>
                {
                    config.DBConfig.Endpoints.Add(new EasyCaching.Core.Configurations.ServerEndPoint("www.zengnanhua.club", 6379));
                    config.DBConfig.Database = 5;
                }, "redis1").WithJson();
            });
            return services;
        }

        public static IServiceCollection AddSystemRegisterType(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //登录信息
            services.AddTransient<IIdentityService, IdentityService>();
            //缓存
            services.AddScoped<ICachingService, CachingService>();
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
        #endregion
    }
}

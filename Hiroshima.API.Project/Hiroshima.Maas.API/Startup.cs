using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using Hiroshima.Maas.API.Infrastructure;
using Hiroshima.Maas.API.Infrastructure.Auth;
using Hiroshima.Maas.Common.Infrastructure.Middlewares;
using Hiroshima.Maas.DAL.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog;
using Swashbuckle.AspNetCore.Swagger;

namespace Hiroshima.Maas.API
{
    public partial class Startup
    {

        private const string _defaultCorsPolicyName = "_myAllowSpecificOrigins";
        private readonly IConfigurationRoot _appConfiguration;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);
            //if (env.IsEnvironment("Development"))
            //{
            //    // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
            //    builder.UseDeveloperExceptionPage(true);
            //}

            builder.AddEnvironmentVariables();
            _appConfiguration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            DependencyInjectionConfig.ConfigureServices(services);
            JwtTokenConfig.AddAuthentication(services, _appConfiguration);
            DBContextConfig.Initialize(services, _appConfiguration);
           
            services.AddAutoMapper();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Hiroshima API", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddPolicy(_defaultCorsPolicyName,
                builder =>
                {
                    builder.WithOrigins(_appConfiguration["App:CorsOrigins"])
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory(_defaultCorsPolicyName));
            });

            // override modelstate
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var error = context.ModelState.Values.First().Errors.First().ErrorMessage;
                    var result = new
                    {
                        Title = "Validation errors",
                        Message = error
                    };
                    return new BadRequestObjectResult(result);
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddConsole(_appConfiguration.GetSection("Logging"));
            loggerFactory.AddDebug();
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Hiroshima API V1");
                //options.IndexStream = () => Assembly.GetExecutingAssembly()
                //    .GetManifestResourceStream("EventCloud.Web.Host.wwwroot.swagger.ui.index.html");
            }); // URL: /swagger

            // global cors policy
            app.UseCors(_defaultCorsPolicyName);
        }
    }
}

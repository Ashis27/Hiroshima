using Hiroshima.Maas.DAL.Contexts;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiroshima.Maas.API.Infrastructure
{
    public class DBContextConfig
    {
        //public static void Initialize(IConfiguration configuration, IHostingEnvironment env, IServiceProvider svp)

        //{
        //    var optionsBuilder = new DbContextOptionsBuilder();
        //    if (env.IsDevelopment()) optionsBuilder.UseMySQL(configuration.GetConnectionString("Default"));
        //    else if (env.IsStaging()) optionsBuilder.UseMySQL(configuration.GetConnectionString("Default"));
        //    else if (env.IsProduction()) optionsBuilder.UseMySQL(configuration.GetConnectionString("Default"));
        //    var context = new IdentityDataContext(optionsBuilder.Options);
        //    if (context.Database.EnsureCreated())
        //    {
        //        IUserMap service = svp.GetService(typeof(IUserMap)) as IUserMap;
        //        new DBInitializeConfig(service).DataTest();
        //    }
        //}

        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HiroshimaMaaSDBContext>(options =>
              options.UseMySQL(configuration.GetConnectionString("Default")));
        }

    }
}

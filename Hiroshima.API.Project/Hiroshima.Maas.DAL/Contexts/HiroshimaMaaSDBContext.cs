using Hiroshima.Maas.DL.Entities;
using Hiroshima.Maas.DL.Entities.AdminUserModel;
using Hiroshima.Maas.DL.Entities.AppConfigurationModel;
using Hiroshima.Maas.DL.Entities.CurrencyConfigurationModel;
using Hiroshima.Maas.DL.Entities.LanguageConfigurationModel;
using Hiroshima.Maas.DL.Entities.PassInformationModel;
using Hiroshima.Maas.DL.Entities.PGTransactionInformationModel;
using Hiroshima.Maas.DL.Entities.PTOInformationModel;
using Hiroshima.Maas.DL.Entities.PTOInformationModel;
using Hiroshima.Maas.DL.Entities.QRCodeConfigModel;
using Hiroshima.Maas.DL.Entities.QRCodeModel;
using Hiroshima.Maas.DL.Entities.TravellerFeedbackModel;
using Hiroshima.Maas.DL.Entities.TravellerModel;
using Hiroshima.Maas.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hiroshima.Maas.DAL.Contexts
{
    public class HiroshimaMaaSDBContext : DbContext
    {
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<PassInformation> PassInformations { get; set; }
        public DbSet<PassDescription> PassDescriptions { get; set; }
        public DbSet<PTOInformation> PTOInformations { get; set; }
        public DbSet<PTODescription> PTODescriptions { get; set; }
        public DbSet<PassActivePTO> PassActivePTOs { get; set; }
        public DbSet<Traveller> Travellers { get; set; }
        public DbSet<BookedPassInformation> BookedPassInformations { get; set; }
        public DbSet<QRCode> QRCodes { get; set; }
        public DbSet<PGTransactionInformation> PGTransactionInformations { get; set; }
        public DbSet<TravellerFeedback> TravellerFeedback { get; set; }
        public DbSet<AppConfiguration> AppConfigurations { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<CurrencyConfiguration> CurrencyConfigurations { get; set; }
        public DbSet<QRCodeConfiguration> QRCodeConfigurations { get; set; }

        private bool _disposed = false;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public HiroshimaMaaSDBContext(DbContextOptions<HiroshimaMaaSDBContext> options, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Microsoft.EntityFrameworkCore.Proxies if require proxy. 
            optionsBuilder.UseMySQL(_configuration.GetConnectionString("Default"));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PassActivePTO>()
           .HasKey(t => new { t.PassInformationId, t.PTOInformationId });

            //builder.Entity<PassActivePTO>()
            //     .HasOne(pt => pt.PassInformation)
            //     .WithMany(p => p.PassActivePTOs)
            //     .HasForeignKey(pt => pt.PassInformationId);
            //builder.Entity<PassActivePTO>()
            //    .HasOne(pt => pt.PTOInformation)
            //    .WithMany(t => t.PassActivePTOs)
            //    .HasForeignKey(pt => pt.PTOInformationId);
            builder.Entity<BookedPassInformation>()
                 .HasOne<QRCode>(q => q.QRCode)
                 .WithOne(b => b.BookedPassInformation)
                 .HasForeignKey<QRCode>(f => f.BookedPassInformationId);
            builder.Entity<BookedPassInformation>()
                .HasOne<PGTransactionInformation>(q => q.PGTransactionInformation)
                .WithOne(b => b.BookedPassInformation)
                .HasForeignKey<PGTransactionInformation>(f => f.BookedPassInformationId);
            builder.Entity<PTOInformation>()
               .HasMany<PTODescription>(q => q.PTODescription);

            base.OnModelCreating(builder);
        }

        protected void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    base.Dispose();
                }

                this._disposed = true;
            }
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

       
    }

}

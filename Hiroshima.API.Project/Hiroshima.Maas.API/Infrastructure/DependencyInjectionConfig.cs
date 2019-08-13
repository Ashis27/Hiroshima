
using Hiroshima.Maas.API.Infrastructure.Auth;
using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.Common.Infrastructure.Logger;
using Hiroshima.Maas.DAL.Contexts;
using Hiroshima.Maas.DAL.Interfaces;
using Hiroshima.Maas.DAL.Repositories;
using Hiroshima.Maas.DAL.Repositories.Interfaces;
using Hiroshima.Maas.Services.Auth;
using Hiroshima.Maas.Services.Interfaces;
using Hiroshima.Maas.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Hiroshima.Maas.Services.Services.PaymentGatewayTransaction;

namespace Hiroshima.Maas.API
{
    internal static class DependencyInjectionConfig
    {

        public static void ConfigureServices(IServiceCollection services)
        {
          
            #region Admin_User
            services.AddScoped<IAdminUserService, AdminUserService>();
            services.AddScoped<IAdminUserRepository, AdminUserRepository>();
            #endregion

            #region Pass_Information
            services.AddScoped<IPassService, PassService>();
            services.AddScoped<IPassRepository, PassRepository>();
            #endregion

            #region PTO_Information
            services.AddScoped<IPTOService, PTOService>();
            services.AddScoped<IPTORepository, PTORepository>();
            #endregion

            #region PTO_Description
            services.AddScoped<IPTODescriptionRepository, PTODescriptionRepository>();
            #endregion

            #region Pass_Description
            services.AddScoped<IPassDescriptionRepository, PassDescriptionRepository>();
            #endregion

            #region Pass_PTO_Mapper
            services.AddScoped<IPassActivePTOMapper, PassActivePTOMapper>();
            #endregion

            #region Traveller_Pass_Information
            services.AddScoped<ITravellerService, TravellerService>();
            services.AddScoped<ITravellerRepository, TravellerRepository>();
            #endregion

            #region DB_Context
            services.AddDbContext<HiroshimaMaaSDBContext>();
            #endregion

            #region Error Handler
            services.AddScoped<IMessageHandler, MessageHandler>();
            #endregion

            #region Unit_Of_Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region JWT_Authentication_Token
            services.AddSingleton<IJwtFactory, JwtFactory>();
            #endregion

            #region Logger
            services.AddSingleton<ILoggerManager, LoggerManager>();
            #endregion

            #region Configuration
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            #endregion

            #region PG_Transaction
            services.AddScoped<IPaymentGatewayTransactionService, PaymentGatewayTransactionService>();
            services.AddScoped<IPaymentGatewayTransactionRepository, PaymentGatewayTransactionRepository>();
            #endregion

            services.AddSingleton<IHttpContextAccessorExtension, HttpContextAccessorExtension>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

    }
}


using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.DAL.Contexts;
using Hiroshima.Maas.DAL.Interfaces;
using Hiroshima.Maas.DL.Entities.AppConfigurationModel;
using Hiroshima.Maas.DL.Entities.CurrencyConfigurationModel;
using Hiroshima.Maas.DL.Entities.LanguageConfigurationModel;
using Hiroshima.Maas.DL.Entities.PassInformationModel;
using Hiroshima.Maas.DL.Entities.PGTransactionInformationModel;
using Hiroshima.Maas.DL.Utility;
using Hiroshima.Maas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Hiroshima.Maas.DL.Entities.TravellerFeedbackModel;
using Hiroshima.Maas.DL.Entities.QRCodeConfigModel;

namespace Hiroshima.Maas.DAL.Repositories
{
    public class ConfigurationRepository : BaseRepository<AppConfiguration>, IConfigurationRepository
    {
        public ConfigurationRepository(HiroshimaMaaSDBContext context, IMessageHandler messageHandler, IUnitOfWork unitOfWork) : base(context, messageHandler, unitOfWork) { }

        #region Get_App_Configuration
        public async Task<AppConfiguration> GetAppConfiguration(string deviceType)
        {
            //Get app configuration based on device type
            return await this._context.AppConfigurations.Where(p => p.ConfigurationType == deviceType && p.IsActive).FirstOrDefaultAsync();
        }
        #endregion

        #region Get_Active_Languages
        public async Task<PaginatedList<Language>> GetActiveLanguages(PassInfoSearchParams searchParams)
        {
            //Get active languages
            IQueryable<Language> activeLangs = this._context.Languages.Where(p => p.IsActive);
            if (activeLangs == null)
                throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.NullException), _messageHandler.GetMessage(ErrorMessagesEnum.NotValidInformations)));

            //This is used to create a PaginatedList class that uses Skip and Take statements to filter data on the server instead of always retrieving all rows of the table
            var filteredResult = await PaginatedList<Language>.CreateAsync(activeLangs.AsNoTracking(), searchParams.PageIndex, searchParams.PageSize);
            return filteredResult;
        }
        #endregion

        #region Get_Active_Curriencies
        public async Task<PaginatedList<CurrencyConfiguration>> GetActiveCurriencies(PassInfoSearchParams searchParams)
        {
            //Get active curriencies
            IQueryable<CurrencyConfiguration> activeCurriencies = this._context.CurrencyConfigurations.Where(p => p.IsActive);
            if (activeCurriencies == null)
                throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.NullException), _messageHandler.GetMessage(ErrorMessagesEnum.NotValidInformations)));

            //This is used to create a PaginatedList class that uses Skip and Take statements to filter data on the server instead of always retrieving all rows of the table
            var filteredResult = await PaginatedList<CurrencyConfiguration>.CreateAsync(activeCurriencies.AsNoTracking(), searchParams.PageIndex, searchParams.PageSize);
            return filteredResult;
        }
        #endregion

        #region Get_Booking_Information
        public async Task<PaginatedList<BookedPassInformation>> GetBookingInformation(PassInfoSearchParams searchParams)
        {
            object[] queryString = searchParams.GetSearchQuery(searchParams);
            ArrayList searchArgs = (ArrayList)queryString[1];

            //Get booking details based on search param
            IQueryable<BookedPassInformation> bookingInfo = this.GetDbSet<BookedPassInformation>().Where(queryString[0].ToString(), searchArgs.ToArray()).Include(p => p.PassInformation).ThenInclude(q => q.PassDescription).AsQueryable();
            if (bookingInfo == null)
                throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.NullException), _messageHandler.GetMessage(ErrorMessagesEnum.NotValidInformations)));

            //This is used to create a PaginatedList class that uses Skip and Take statements to filter data on the server instead of always retrieving all rows of the table
            var filteredResult = await PaginatedList<BookedPassInformation>.CreateAsync(bookingInfo.AsNoTracking(), searchParams.PageIndex, searchParams.PageSize);
            return filteredResult;
        }
        #endregion

        #region Get_Traveller_feedback
        public async Task<PaginatedList<TravellerFeedback>> GetTravellerFeedback(PassInfoSearchParams searchParams)
        {
            //Get active curriencies
            IQueryable<TravellerFeedback> activeFeedback = this._context.TravellerFeedback.Where(p => p.IsActive);
            if (activeFeedback == null)
                throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.NullException), _messageHandler.GetMessage(ErrorMessagesEnum.NotValidInformations)));

            //This is used to create a PaginatedList class that uses Skip and Take statements to filter data on the server instead of always retrieving all rows of the table
            var filteredResult = await PaginatedList<TravellerFeedback>.CreateAsync(activeFeedback.AsNoTracking(), searchParams.PageIndex, searchParams.PageSize);
            return filteredResult;
        }
        #endregion

        #region Get_QR_Code_configuration_By_Id
        public QRCodeConfiguration GetQRConfigurationById(int id)
        {
            //Get active config based on id
            return this._context.QRCodeConfigurations.Where(p => p.Id == id).SingleOrDefault();
        }
        #endregion

        #region Update_QR_Code_configuration
        public async void UpdateQRCodeRegerateTime(QRCodeConfiguration qrConfig)
        {
            this.GetDbSet<QRCodeConfiguration>().Attach(qrConfig);
            this._context.Entry(qrConfig).State = EntityState.Modified;
            await this._unitOfWork.CompleteAsync();
        }
        #endregion

        #region Get_QR_Code_configuration
        public QRCodeConfiguration GetQRConfiguration()
        {
            //Get active configuration
            return this._context.QRCodeConfigurations.FirstOrDefault();
        }
        #endregion

    }
}

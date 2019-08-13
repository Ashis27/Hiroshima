using AutoMapper;
using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.Common.Infrastructure.Logger;
using Hiroshima.Maas.DAL.Interfaces;
using Hiroshima.Maas.DL.Entities.AppConfigurationModel;
using Hiroshima.Maas.DL.Entities.CurrencyConfigurationModel;
using Hiroshima.Maas.DL.Entities.LanguageConfigurationModel;
using Hiroshima.Maas.DL.Entities.PassInformationModel;
using Hiroshima.Maas.DL.Utility;
using Hiroshima.Maas.Services.Interfaces;
using Hiroshima.Maas.Services.Utility.Helper;
using Hiroshima.Maas.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hiroshima.Maas.DL.Entities.TravellerFeedbackModel;
using Hiroshima.Maas.Services.RequestAndResponse;
using Hiroshima.Maas.DL.Entities.QRCodeConfigModel;
using System.Linq;

namespace Hiroshima.Maas.Services.Services
{
    public class ConfigurationService : BaseService, IConfigurationService
    {
        private readonly IConfigurationRepository _configurationRepo;

        public ConfigurationService(IConfigurationRepository configurationRepo, IJwtFactory jwtFactory, IMessageHandler messageHandler, ILoggerManager logger, IMapper mapper) : base(messageHandler, mapper, logger, jwtFactory)
        {
            _configurationRepo = configurationRepo;
        }

        #region Get_App_Configuration
        public async Task<AppConfigurationViewModel> GetAppConfiguration(string deviceType)
        {
            _logger.LogInfo("Trying to get app configuration with device type " + deviceType);
            try
            {
                AppConfigurationViewModel appConfig = _mapper.Map<AppConfiguration, AppConfigurationViewModel>(await _configurationRepo.GetAppConfiguration(deviceType));
                if (appConfig == null)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidConfiguration)));
                _logger.LogInfo("Retrieved app configuration with device type " + deviceType);
                return appConfig;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return null;
            }
        }
        #endregion

        #region Get_Active_Languages
        public async Task<PagedViewModelResult<LanguageViewModel>> GetActiveLanguages(SearchParams searchParams)
        {
            _logger.LogInfo("Trying to get active languages");
            try
            {
                PagedViewModelResult<LanguageViewModel> activeLangs = _mapper.Map<PaginatedList<Language>, PagedViewModelResult<LanguageViewModel>>(await _configurationRepo.GetActiveLanguages(_mapper.Map<PassInfoSearchParams>(searchParams)));
                _logger.LogInfo("Retrieved active languages");
                return activeLangs;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return null;
            }
        }
        #endregion

        #region Get_Active_Curriencies
        public async Task<PagedViewModelResult<CurrencyViewModel>> GetActiveCurriencies(SearchParams searchParams)
        {
            _logger.LogInfo("Trying to get active curriencies");
            try
            {
                PagedViewModelResult<CurrencyViewModel> activeCurriencies = _mapper.Map<PaginatedList<CurrencyConfiguration>, PagedViewModelResult<CurrencyViewModel>>(await _configurationRepo.GetActiveCurriencies(_mapper.Map<PassInfoSearchParams>(searchParams)));
                _logger.LogInfo("Retrieved active curriencies");
                return activeCurriencies;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return null;
            }
        }
        #endregion

        #region Get_Booking_information
        public async Task<PagedViewModelResult<BookedPassInformationViewModel>> GetBookingInformation(SearchParams searchParams)
        {
            _logger.LogInfo("Trying to get booking information");
            try
            {
                PagedViewModelResult<BookedPassInformationViewModel> activeBookingInfo = _mapper.Map<PaginatedList<BookedPassInformation>, PagedViewModelResult<BookedPassInformationViewModel>>(await _configurationRepo.GetBookingInformation(_mapper.Map<PassInfoSearchParams>(searchParams)));

                //Get all booking information based on language
                var activeInfo = new List<BookedPassInformationViewModel>();
                foreach (var item in activeBookingInfo.Items)
                {
                    //Filter based on language from the paginated result.
                    if (item.PassInformation != null)
                        item.PassInformation.PassDescription = item?.PassInformation?.PassDescription.Where(p => p.SelectedLanguage == searchParams.Lang);
                }
                _logger.LogInfo("Retrieved booking information");
                return activeBookingInfo;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return null;
            }
        }
        #endregion

        #region Get_Traveller_feedback
        public async Task<PagedViewModelResult<TravellerFeedbackViewModel>> GetTravellerFeedback(SearchParams searchParams)
        {
            _logger.LogInfo("Trying to get active feedback information");
            try
            {
                PagedViewModelResult<TravellerFeedbackViewModel> activeFeedback = _mapper.Map<PaginatedList<TravellerFeedback>, PagedViewModelResult<TravellerFeedbackViewModel>>(await _configurationRepo.GetTravellerFeedback(_mapper.Map<PassInfoSearchParams>(searchParams)));
                _logger.LogInfo("Retrieved feedback information");
                return activeFeedback;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return null;
            }
        }
        #endregion

        #region Update_QR_Code_configuration
        public QRCodeConfigResponse UpdateQRCodeRegerateTime(QRCodeConfigurationViewModel configInfo)
        {
            _logger.LogInfo("Trying to update configuration time");
            try
            {
                //Get QR Code configuration based on id
                QRCodeConfiguration qrConfig = _configurationRepo.GetQRConfigurationById(configInfo.Id);

                //Throw exception if null
                if (qrConfig == null)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidQRConfiguration)));

                //Updated Time
                qrConfig.RegenerationTimeInMin = configInfo.RegenerationTimeInMin;
                _configurationRepo.UpdateQRCodeRegerateTime(qrConfig);
                _logger.LogInfo("Updated configuration time");
                QRCodeConfigResponse response = new QRCodeConfigResponse(true, "Successfully saved");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return new QRCodeConfigResponse(false, ex.Message);
            }
        }
        #endregion

        #region Get_QR_Code_configuration
        public QRCodeConfigurationViewModel GetQRConfiguration()
        {
            _logger.LogInfo("Trying to get active QR Code configuration");
            try
            {
                QRCodeConfigurationViewModel qrConfig = _mapper.Map<QRCodeConfiguration, QRCodeConfigurationViewModel>(_configurationRepo.GetQRConfiguration());
                _logger.LogInfo("Retrieved QR Code configuration information");
                return qrConfig;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return null;
            }
        }
        #endregion
    }
}

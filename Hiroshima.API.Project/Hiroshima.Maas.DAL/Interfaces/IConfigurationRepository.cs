using Hiroshima.Maas.DL.Entities.AppConfigurationModel;
using Hiroshima.Maas.DL.Entities.CurrencyConfigurationModel;
using Hiroshima.Maas.DL.Entities.LanguageConfigurationModel;
using Hiroshima.Maas.DL.Entities.PassInformationModel;
using Hiroshima.Maas.DL.Entities.QRCodeConfigModel;
using Hiroshima.Maas.DL.Entities.TravellerFeedbackModel;
using Hiroshima.Maas.DL.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.DAL.Interfaces
{
    public interface IConfigurationRepository
    {
        Task<AppConfiguration> GetAppConfiguration(string deviceType);
        Task<PaginatedList<Language>> GetActiveLanguages(PassInfoSearchParams searchParams);
        Task<PaginatedList<CurrencyConfiguration>> GetActiveCurriencies(PassInfoSearchParams searchParams);
        Task<PaginatedList<BookedPassInformation>> GetBookingInformation(PassInfoSearchParams searchParams);
        Task<PaginatedList<TravellerFeedback>> GetTravellerFeedback(PassInfoSearchParams searchParams);
        void UpdateQRCodeRegerateTime(QRCodeConfiguration qrConfig);
        QRCodeConfiguration GetQRConfigurationById(int id);
        QRCodeConfiguration GetQRConfiguration();
    }
}

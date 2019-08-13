using Hiroshima.Maas.Services.RequestAndResponse;
using Hiroshima.Maas.Services.Utility.Helper;
using Hiroshima.Maas.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.Services.Interfaces
{
    public interface IConfigurationService
    {
        Task<AppConfigurationViewModel> GetAppConfiguration(string deviceType);
        Task<PagedViewModelResult<LanguageViewModel>> GetActiveLanguages(SearchParams searchParams);
        Task<PagedViewModelResult<CurrencyViewModel>> GetActiveCurriencies(SearchParams searchParams);
        Task<PagedViewModelResult<BookedPassInformationViewModel>> GetBookingInformation(SearchParams searchParams);
        Task<PagedViewModelResult<TravellerFeedbackViewModel>> GetTravellerFeedback(SearchParams searchParams);
        QRCodeConfigResponse UpdateQRCodeRegerateTime(QRCodeConfigurationViewModel configInfo);
        QRCodeConfigurationViewModel GetQRConfiguration();
    }
}

using Hiroshima.Maas.Services.RequestAndResponse;
using Hiroshima.Maas.Services.Utility.Helper;
using Hiroshima.Maas.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.Services.Interfaces
{
    public interface ITravellerService
    {
        Task<PagedViewModelResult<BookedPassInformationViewModel>> GetAvailablePasses(SearchParams searchParams);
        Task<PagedViewModelResult<BookedPassInformationViewModel>> GetInUsePasses(SearchParams searchParams);
        Task<PagedViewModelResult<BookedPassInformationViewModel>> GetExpiredPasses(SearchParams searchParams);
        Task<BookedPassInformationViewModel> GetBookingInformation(string uId);
        Task<BookedPassInformationViewModel> GetBookingDatailsById(string uId);
        TravellerResponse SubmitFeedback(TravellerFeedbackViewModel feedbackData);
        Task<TravellerResponse> ActivatePass(string uId);
        Task<TravellerResponse> BookPass(BookedPassInformationViewModel bookingInfo);
        bool UpdateBookingInformation(BookedPassInformationViewModel passInformation, bool status, string transId);

    }
}

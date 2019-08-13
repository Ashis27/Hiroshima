using Hiroshima.Maas.DL.Entities.PassInformationModel;
using Hiroshima.Maas.DL.Entities.PGTransactionInformationModel;
using Hiroshima.Maas.DL.Entities.QRCodeModel;
using Hiroshima.Maas.DL.Entities.TravellerFeedbackModel;
using Hiroshima.Maas.DL.Entities.TravellerModel;
using Hiroshima.Maas.DL.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.DAL.Interfaces
{
    public interface ITravellerRepository
    {
        Task<PaginatedList<BookedPassInformation>> GetAvailablePasses(PassInfoSearchParams searchParams);
        Task<PaginatedList<BookedPassInformation>> GetInUsePasses(PassInfoSearchParams searchParams);
        Task<PaginatedList<BookedPassInformation>> GetExpiredPasses(PassInfoSearchParams searchParams);
        Task<PaginatedList<BookedPassInformation>> GetBookingInformations(PassInfoSearchParams searchParams);
        Task<BookedPassInformation> GetBookingDatails(string uId);
        Task<BookedPassInformation> GetBookingDatailsById(string uId);
        Traveller GetTravellerByDeviceId(string id);
        void SubmitFeedback(TravellerFeedback feedbackData);
        void ActivatePass(QRCode qrInfo);
        void AddTraveller(Traveller travellerInfo);
        void BookPass(BookedPassInformation bookingInfo);
        void UpdateBookingInformation(BookedPassInformation passInformation);
    }
}

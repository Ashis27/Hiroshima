using AutoMapper;
using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.Common.Infrastructure.Logger;
using Hiroshima.Maas.DAL.Interfaces;
using Hiroshima.Maas.DL.Entities.PassInformationModel;
using Hiroshima.Maas.DL.Entities.QRCodeModel;
using Hiroshima.Maas.DL.Entities.TravellerFeedbackModel;
using Hiroshima.Maas.DL.Entities.TravellerModel;
using Hiroshima.Maas.DL.Utility;
using Hiroshima.Maas.Services.Interfaces;
using Hiroshima.Maas.Services.RequestAndResponse;
using Hiroshima.Maas.Services.Utility.Helper;
using Hiroshima.Maas.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hiroshima.Maas.DL.Entities.PGTransactionInformationModel;

namespace Hiroshima.Maas.Services.Services
{
    public class TravellerService : BaseService, ITravellerService
    {
        private readonly ITravellerRepository _travellerRepository;
        private readonly IPassRepository _passRepository;
        public TravellerService(ITravellerRepository travellerRepository, IPassRepository passRepository, IJwtFactory jwtFactory, IMessageHandler messageHandler, ILoggerManager logger, IMapper mapper) : base(messageHandler, mapper, logger, jwtFactory)
        {
            _travellerRepository = travellerRepository;
            _passRepository = passRepository;
        }

        #region Get_Available_Passes
        public async Task<PagedViewModelResult<BookedPassInformationViewModel>> GetAvailablePasses(SearchParams searchParams)
        {
            _logger.LogInfo("Trying to get all available passes for " + searchParams.DeviceId);
            try
            {
                //Get active user based on device id
                Traveller travellerInfo = _travellerRepository.GetTravellerByDeviceId(searchParams.DeviceId);
                if (travellerInfo == null)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidTravellerInformation)));

                //Get all active booking informations
                searchParams.TravellerId = travellerInfo.Id;
                searchParams.StartDateAndTime = DateTime.Now;
                PagedViewModelResult<BookedPassInformationViewModel> passInformation = _mapper.Map<PaginatedList<BookedPassInformation>, PagedViewModelResult<BookedPassInformationViewModel>>(await _travellerRepository.GetAvailablePasses(_mapper.Map<PassInfoSearchParams>(searchParams)));

                //Get all passes which pass descriptions are available
                var activePass = new List<BookedPassInformationViewModel>();
                _logger.LogInfo("Retrieved " + passInformation.Items.Count() + " number of booking information");
                foreach (var item in passInformation.Items)
                {
                    //Get all QR code based on booking id and filter only available booking information
                    if (item.PassInformation != null && item.PassInformation.PassExpiredDate >= searchParams.StartDateAndTime)
                    {
                        //Filter based on language from the paginated result.
                        item.PassInformation.PassDescription = item.PassInformation?.PassDescription?.Where(p => p.SelectedLanguage == searchParams.Lang);
                        if (item.PassInformation.PassDescription.Count() > 0)
                            activePass.Add(item);
                    }
                }
                passInformation.Items = activePass;
                _logger.LogInfo("Retrieved " + activePass.Count() + " available passes for " + searchParams.DeviceId);
                return passInformation;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return null;
            }
        }
        #endregion

        #region Get_In_Use_Passes
        public async Task<PagedViewModelResult<BookedPassInformationViewModel>> GetInUsePasses(SearchParams searchParams)
        {
            _logger.LogInfo("Trying to get all in use passes for " + searchParams.DeviceId);
            try
            {
                //Get active user based on device id
                Traveller travellerInfo = _travellerRepository.GetTravellerByDeviceId(searchParams.DeviceId);
                if (travellerInfo == null)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidTravellerInformation)));

                //Get all active booking informations
                searchParams.TravellerId = travellerInfo.Id;
                searchParams.StartDateAndTime = DateTime.Now;
                PagedViewModelResult<BookedPassInformationViewModel> passInformation = _mapper.Map<PaginatedList<BookedPassInformation>, PagedViewModelResult<BookedPassInformationViewModel>>(await _travellerRepository.GetInUsePasses(_mapper.Map<PassInfoSearchParams>(searchParams)));

                //Get all passes which pass descriptions are available
                var activePass = new List<BookedPassInformationViewModel>();
                foreach (var item in passInformation.Items)
                {
                    if (item.PassInformation != null && (item.PassInformation.PassExpiredDate >= searchParams.StartDateAndTime))
                    {
                        //Filter based on language from the paginated result.
                        item.PassInformation.PassDescription = item.PassInformation?.PassDescription?.Where(p => p.SelectedLanguage == searchParams.Lang);
                        activePass.Add(item);
                    }
                }
                passInformation.Items = activePass;
                _logger.LogInfo("Retrieved all in use passes for " + searchParams.DeviceId);
                return passInformation;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return null;
            }
        }
        #endregion

        #region Get_Expired_Passes
        public async Task<PagedViewModelResult<BookedPassInformationViewModel>> GetExpiredPasses(SearchParams searchParams)
        {
            _logger.LogInfo("Trying to get all expired passes for " + searchParams.DeviceId);
            try
            {
                //Get active user based on device id
                Traveller travellerInfo = _travellerRepository.GetTravellerByDeviceId(searchParams.DeviceId);
                if (travellerInfo == null)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidTravellerInformation)));

                //Get all active booking informations
                searchParams.TravellerId = travellerInfo.Id;
                searchParams.StartDateAndTime = DateTime.Now;
                PagedViewModelResult<BookedPassInformationViewModel> passInformation = _mapper.Map<PaginatedList<BookedPassInformation>, PagedViewModelResult<BookedPassInformationViewModel>>(await _travellerRepository.GetExpiredPasses(_mapper.Map<PassInfoSearchParams>(searchParams)));
                _logger.LogInfo("Retrieved " + passInformation.Items.Count() + " number of booking information");
                //Get all passes which pass descriptions are available
                var activePass = new List<BookedPassInformationViewModel>();
                foreach (var item in passInformation.Items)
                {
                    if (item.PassInformation != null)
                    {
                        //Filter based on language from the paginated result.
                        item.PassInformation.PassDescription = item.PassInformation?.PassDescription?.Where(p => p.SelectedLanguage == searchParams.Lang);
                        activePass.Add(item);
                    }
                }
                passInformation.Items = activePass;
                _logger.LogInfo("Retrieved " + activePass.Count() + " expired passes for " + searchParams.DeviceId); ;
                return passInformation;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return null;
            }
        }
        #endregion

        #region Get_Booking_information
        public async Task<BookedPassInformationViewModel> GetBookingInformation(string uId)
        {
            _logger.LogInfo("Trying to get booking info with booking id " + uId);
            try
            {
                BookedPassInformationViewModel bookingInfo = _mapper.Map<BookedPassInformation, BookedPassInformationViewModel>(await _travellerRepository.GetBookingDatails(uId));
                _logger.LogInfo("Retrieved booking info with booking id " + uId);
                return bookingInfo;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return null;
            }
        }
        #endregion
        #region Get_Booking_information_By_Id
        public async Task<BookedPassInformationViewModel> GetBookingDatailsById(string uId)
        {
            _logger.LogInfo("Trying to get booking info with booking id " + uId);
            try
            {
                BookedPassInformationViewModel bookingInfo = _mapper.Map<BookedPassInformation, BookedPassInformationViewModel>(await _travellerRepository.GetBookingDatailsById(uId));
                _logger.LogInfo("Retrieved booking info with booking id " + uId);
                return bookingInfo;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return null;
            }
        }
        #endregion

        #region Submit_Traveller_Feedback
        public TravellerResponse SubmitFeedback(TravellerFeedbackViewModel feedbackData)
        {
            _logger.LogInfo("Trying to submit feedback with device id " + feedbackData.DeviceId);
            try
            {
                //Get Traveller info based on device id
                Traveller traveller = _travellerRepository.GetTravellerByDeviceId(feedbackData.DeviceId);
                if (traveller == null)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.BookingWithInvalidDeviceId)));

                //Convert view model to entity
                TravellerFeedback feedbackInfo = _mapper.Map<TravellerFeedback>(feedbackData);
                feedbackInfo.TravellerId = traveller.Id;
                feedbackInfo.IsActive = true;
                _travellerRepository.SubmitFeedback(feedbackInfo);
                _logger.LogInfo("Successfully submitted feedback with device id " + feedbackData.DeviceId);
                feedbackData.DeviceId = null;
                TravellerResponse response = new TravellerResponse(true, string.Format(_messageHandler.GetSuccessMessage(SuccessMessagesEnum.SuccessfullySaved)));
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new TravellerResponse(false, ex.Message);
            }
        }
        #endregion

        #region Book_Pass
        public async Task<TravellerResponse> BookPass(BookedPassInformationViewModel bookingInfo)
        {
            _logger.LogInfo("Trying to book pass having id " + bookingInfo.PassInformationId + "with traveller id " + bookingInfo.TravellerDeviceId);
            try
            {
                //Get selected pass information based on id
                PassInformationViewModel passInformation = _mapper.Map<PassInformation, PassInformationViewModel>(await _passRepository.GetActivePass(bookingInfo.PassInformationId));
                if (passInformation == null)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidPassInformation)));

                //Get active user based on device id
                Traveller travellerInfo = new Traveller();
                var isExistTraveller = _travellerRepository.GetTravellerByDeviceId(bookingInfo.TravellerDeviceId);
                if (isExistTraveller == null)
                {
                    //Add traveller information
                    travellerInfo.DeviceId = bookingInfo.TravellerDeviceId;
                    travellerInfo.DeviceType = bookingInfo.TravellerDeviceType;
                    travellerInfo.IsActive = true;

                    //Added new traveller
                    _travellerRepository.AddTraveller(travellerInfo);
                    _logger.LogInfo("Successfully added new traveller with device id " + bookingInfo.TravellerDeviceId);

                    //Once traveller info added then get the traveller id
                    isExistTraveller = _travellerRepository.GetTravellerByDeviceId(bookingInfo.TravellerDeviceId);
                }

                //Business logic goes here for booking pass information
                bookingInfo.TravellerId = isExistTraveller.Id;
                bookingInfo.UniqueReferrenceNumber = "HST-" + bookingInfo.TravellerId + "-" + DateTime.Now.ToString("ddMMyyhhmmssff");
                bookingInfo.TransactionNumber = DateTime.Now.ToString("ddMMyyhhmmssff");
                bookingInfo.BookingDate = DateTime.Today;
                bookingInfo.TotalAmout = (bookingInfo.Adult * passInformation.AdultPrice) + (bookingInfo.Child * passInformation.ChildPrice);
                bookingInfo.IsActive = true;
                bookingInfo.QRCode = null;
               // QR code information to be stored with booking information id
                QRCodeViewModel qrCode = new QRCodeViewModel();
                //bookingInfo.PaymentStatus = true;
                //bookingInfo.IsActive = true;
                //bookingInfo.PaymentResponse = "Success";
                qrCode.IsActive = false;
                qrCode.BookedPassInformationId = bookingInfo.Id;
                qrCode.PassExpiredDuraionDate = passInformation.PassExpiredDate;
                bookingInfo.QRCode = qrCode;
                ///////////////////////////////////////////////////////////////
                if (bookingInfo.TotalAmout == 0)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidPassengerInformation)));
                _travellerRepository.BookPass(_mapper.Map<BookedPassInformation>(bookingInfo));
                _logger.LogInfo("Successfully addded booking information");
                TravellerResponse response = new TravellerResponse(true, string.Format(_messageHandler.GetSuccessMessage(SuccessMessagesEnum.SuccessfullySaved)));
                response.Result = bookingInfo.UniqueReferrenceNumber;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new TravellerResponse(false, ex.Message);
            }
        }
        #endregion

        #region Activate_Pass
        public async Task<TravellerResponse> ActivatePass(string uId)
        {
            _logger.LogInfo("Trying to activate pass with booking id " + uId);
            try
            {
                _logger.LogInfo("Retrieved booking information with booking id " + uId);
                BookedPassInformation bookingInformation = await _travellerRepository.GetBookingDatails(uId);
                if (bookingInformation.PassInformation == null)
                {
                    _logger.LogInfo(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.NullException), " No pass information available"));
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidPassInformation)));
                }
                if (bookingInformation.QRCode == null)
                {
                    _logger.LogInfo(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.NullException), " Invalid QR code information"));
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidQRCode)));
                }
               // Check If already pass has expired and trying to activate again then throw exception
                if (bookingInformation.QRCode.IsActive)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidPassInformation)));

                bookingInformation.QRCode.PassActivatedDate = DateTime.Now;
                bookingInformation.QRCode.IsActive = true;
                bookingInformation.QRCode.ActivatedPassExpiredDate = DateTime.Now;

                if (bookingInformation.PassInformation.PassValidityInDays > 0)
                    bookingInformation.QRCode.ActivatedPassExpiredDate = bookingInformation.QRCode.ActivatedPassExpiredDate.AddDays(bookingInformation.PassInformation.PassValidityInDays);

                if (bookingInformation.PassInformation.PassValidityInHours > 0)
                    bookingInformation.QRCode.ActivatedPassExpiredDate = bookingInformation.QRCode.ActivatedPassExpiredDate.AddHours(bookingInformation.PassInformation.PassValidityInHours);

                bookingInformation.QRCode.PassExpiredDuraionDate = bookingInformation.PassInformation.PassExpiredDate;
                bookingInformation.QRCode.QRCodeImage = null;

                _travellerRepository.ActivatePass(bookingInformation.QRCode);
                _logger.LogInfo("Successfully activated pass with booking id " + uId);
                TravellerResponse response = new TravellerResponse(true, string.Format(_messageHandler.GetSuccessMessage(SuccessMessagesEnum.SuccessfullyActivated)));
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new TravellerResponse(false, ex.Message);
            }
        }
        #endregion

        #region Update_Booking_Pass_Information
        public bool UpdateBookingInformation(BookedPassInformationViewModel bookingInfo,bool pgStatus, string transId)
        {
            _logger.LogInfo("Trying to update Booking pass information after payment");
            try
            {
                BookedPassInformation bookingInformation =  _travellerRepository.GetBookingDatailsById(bookingInfo.UniqueReferrenceNumber).Result;
                bookingInformation.PaymentStatus = pgStatus;
                bookingInformation.TransactionNumber = transId;
                bookingInformation.IsActive = pgStatus;
                bookingInformation.PaymentResponse = pgStatus ?"Success":"Failed";
                _travellerRepository.UpdateBookingInformation(bookingInformation);
                _logger.LogInfo("Booking pass information updated successfully for id "+ bookingInfo.UniqueReferrenceNumber);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        #endregion
       
    }
}

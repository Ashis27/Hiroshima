using Hiroshima.Maas.Common.Infrastructure.Logger;
using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.DAL.Contexts;
using Hiroshima.Maas.DAL.Interfaces;
using Hiroshima.Maas.DL.Entities.PassInformationModel;
using Hiroshima.Maas.DL.Entities.PGTransactionInformationModel;
using Hiroshima.Maas.DL.Entities.QRCodeModel;
using Hiroshima.Maas.DL.Entities.TravellerFeedbackModel;
using Hiroshima.Maas.DL.Entities.TravellerModel;
using Hiroshima.Maas.DL.Utility;
using Hiroshima.Maas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.DAL.Repositories
{
    public class TravellerRepository : BaseRepository<Traveller>, ITravellerRepository
    {
        public TravellerRepository(HiroshimaMaaSDBContext context, IMessageHandler messageHandler, IUnitOfWork unitOfWork) : base(context, messageHandler, unitOfWork) { }


        #region Get_Booking_Information
        public async Task<PaginatedList<BookedPassInformation>> GetBookingInformations(PassInfoSearchParams searchParams)
        {
            //Get Booking Info based on traveller id
            var bookingInformation = this._context.BookedPassInformations.Include(qr => qr.QRCode).Include(pass => pass.PassInformation).ThenInclude(passDesc => passDesc.PassDescription).Where(p => p.TravellerId == searchParams.TravellerId && p.IsActive && p.PaymentStatus);
            if (bookingInformation == null)
                throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.NullException), _messageHandler.GetMessage(ErrorMessagesEnum.NotValidInformations)));

            //This is used to create a PaginatedList class that uses Skip and Take statements to filter data on the server instead of always retrieving all rows of the table
            var filteredResult = await PaginatedList<BookedPassInformation>.CreateAsync(bookingInformation.AsNoTracking(), searchParams.PageIndex, searchParams.PageSize);
            return filteredResult;
        }
        #endregion

        #region Get_Available_Passes
        public async Task<PaginatedList<BookedPassInformation>> GetAvailablePasses(PassInfoSearchParams searchParams)
        {

            //Get QR code information
            var availableQRCodes = this._context.QRCodes.Where(q => !q.IsActive && q.PassExpiredDuraionDate >= searchParams.StartDateAndTime);
            
            //Get all active QR ids
            var qrCodeIds = availableQRCodes.Select(p => p.BookedPassInformationId).ToArray();

            //Get Booking Info based on traveller id
            var bookingInformation = this._context.BookedPassInformations.Include(qr => qr.QRCode).Include(pass => pass.PassInformation).ThenInclude(passDesc => passDesc.PassDescription).Where(p => qrCodeIds.Contains(p.Id) && p.TravellerId == searchParams.TravellerId && p.IsActive && p.PaymentStatus);
            if (bookingInformation == null)
                throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.NullException), _messageHandler.GetMessage(ErrorMessagesEnum.NotValidInformations)));

            //This is used to create a PaginatedList class that uses Skip and Take statements to filter data on the server instead of always retrieving all rows of the table
            var filteredResult = await PaginatedList<BookedPassInformation>.CreateAsync(bookingInformation.AsNoTracking(), searchParams.PageIndex, searchParams.PageSize);
            return filteredResult;
        }
        #endregion

        #region Get_In_Use_Passes
        public async Task<PaginatedList<BookedPassInformation>> GetInUsePasses(PassInfoSearchParams searchParams)
        {

            //Get QR code information
            var availableQRCodes = this._context.QRCodes.Where(q => q.IsActive && q.ActivatedPassExpiredDate >= searchParams.StartDateAndTime);

            //Get all active QR ids
            var qrCodeIds = availableQRCodes.Select(p => p.BookedPassInformationId).ToArray();

            //Get Booking Info based on traveller id
            var bookingInformation = this._context.BookedPassInformations.Include(qr => qr.QRCode).Include(pass => pass.PassInformation).ThenInclude(passDesc => passDesc.PassDescription).Where(p => qrCodeIds.Contains(p.Id) && p.TravellerId == searchParams.TravellerId && p.IsActive && p.PaymentStatus);
            if (bookingInformation == null)
                throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.NullException), _messageHandler.GetMessage(ErrorMessagesEnum.NotValidInformations)));

            //This is used to create a PaginatedList class that uses Skip and Take statements to filter data on the server instead of always retrieving all rows of the table
            var filteredResult = await PaginatedList<BookedPassInformation>.CreateAsync(bookingInformation.AsNoTracking(), searchParams.PageIndex, searchParams.PageSize);
            return filteredResult;
        }
        #endregion

        #region Get_Expired_Passes
        public async Task<PaginatedList<BookedPassInformation>> GetExpiredPasses(PassInfoSearchParams searchParams)
        {
            //Get QR code information
            var availableQRCodes = this._context.QRCodes.Where(q => (q.IsActive && q.ActivatedPassExpiredDate < searchParams.StartDateAndTime) || q.PassExpiredDuraionDate < searchParams.StartDateAndTime);

            //Get all active QR ids
            var qrCodeIds = availableQRCodes.Select(p => p.BookedPassInformationId).ToArray();

            //Get Booking Info based on traveller id
            var bookingInformation = this._context.BookedPassInformations.Include(qr => qr.QRCode).Include(pass => pass.PassInformation).ThenInclude(passDesc => passDesc.PassDescription).Where(p => qrCodeIds.Contains(p.Id) && p.TravellerId == searchParams.TravellerId && p.IsActive && p.PaymentStatus);
            if (bookingInformation == null)
                throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.NullException), _messageHandler.GetMessage(ErrorMessagesEnum.NotValidInformations)));

            //This is used to create a PaginatedList class that uses Skip and Take statements to filter data on the server instead of always retrieving all rows of the table
            var filteredResult = await PaginatedList<BookedPassInformation>.CreateAsync(bookingInformation.AsNoTracking(), searchParams.PageIndex, searchParams.PageSize);
            return filteredResult;
        }
        #endregion

        #region Get_Booking_information
        public async Task<BookedPassInformation> GetBookingDatails(string uId)
        {
            //Get Booking Info based on booking id
            BookedPassInformation bookingInformation = await this._context.BookedPassInformations.Where(p => p.UniqueReferrenceNumber == uId && p.IsActive && p.PaymentStatus).Include(p => p.QRCode).Include(pi => pi.PassInformation).ThenInclude(passDesc => passDesc.PassDescription).FirstOrDefaultAsync();
            if (bookingInformation == null)
                throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.NullException), _messageHandler.GetMessage(ErrorMessagesEnum.NotValidInformations)));
            return bookingInformation;
        }
        #endregion
        #region Get_Booking_information_By_Id
        public async Task<BookedPassInformation> GetBookingDatailsById(string uId)
        {
            //Get Booking Info based on booking id
            BookedPassInformation bookingInformation = await this._context.BookedPassInformations.Where(p => p.UniqueReferrenceNumber == uId ).FirstOrDefaultAsync();
            if (bookingInformation == null)
                throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.NullException), _messageHandler.GetMessage(ErrorMessagesEnum.NotValidInformations)));
            return bookingInformation;
        }
        #endregion
        #region Submit_Traveller_Feedback
        public async void SubmitFeedback(TravellerFeedback feedbackData)
        {
            await this._context.TravellerFeedback.AddAsync(feedbackData);
            await this._unitOfWork.CompleteAsync();
        }
        #endregion

        #region Activate_Pass
        public async void ActivatePass(QRCode qrInfo)
        {
            this.GetDbSet<QRCode>().Attach(qrInfo);
            this._context.Entry(qrInfo).State = EntityState.Modified;
            await this._unitOfWork.CompleteAsync();
        }
        #endregion

        #region Get_Traveller_Information
        public async Task<Traveller> GetTraveller(int id)
        {
            //Get active user based on user id
            return await this.GetById(id);

        }
        #endregion

        #region Get_Traveller_Information_By_DeviceId
        public Traveller GetTravellerByDeviceId(string id)
        {
            //Get active user based on device id
            return this.Where(p => p.DeviceId == id).SingleOrDefault();
        }
        #endregion

        #region Add_New_Traveller
        public async void AddTraveller(Traveller travellerInfo)
        {
            //Add new traveller information
            this.Add(travellerInfo);
            await this._unitOfWork.CompleteAsync();
        }
        #endregion

        #region Add_Booking_information
        public async void BookPass(BookedPassInformation bookingInfo)
        {
            //Add booking information with selected pass
            await this._context.BookedPassInformations.AddAsync(bookingInfo);
            await this._unitOfWork.CompleteAsync();
        }
        #endregion
        #region Update_Booking_Pass_Information
        public async void UpdateBookingInformation(BookedPassInformation passInformation)
        {
            this.GetDbSet<BookedPassInformation>().Attach(passInformation);
            this._context.Entry(passInformation).State = EntityState.Modified;
            await this._unitOfWork.CompleteAsync();
        }
        #endregion
       
    }
}

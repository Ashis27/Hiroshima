using AutoMapper;
using Hiroshima.Maas.Common.Infrastructure.Logger;
using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.DAL.Interfaces;
using Hiroshima.Maas.DL.Entities.PGTransactionInformationModel;
using Hiroshima.Maas.Services.Interfaces;
using Hiroshima.Maas.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.Services.Services.PaymentGatewayTransaction
{
    public class PaymentGatewayTransactionService : BaseService, IPaymentGatewayTransactionService
    {
        private readonly IPaymentGatewayTransactionRepository _paymentGatewayRepository;

        public PaymentGatewayTransactionService(IPaymentGatewayTransactionRepository paymentGatewayRepository, IJwtFactory jwtFactory, IMessageHandler messageHandler, ILoggerManager logger, IMapper mapper) : base(messageHandler, mapper, logger, jwtFactory)
        {
            _paymentGatewayRepository = paymentGatewayRepository;
        }

        #region Add_new_transaction_information
        public bool AddTransactionInformation(BookedPassInformationViewModel bookingInfo, bool pgStatus, string transId)
        {
            _logger.LogInfo("Trying to add new PG transaction information after payment for Id " + transId);
            try
            {
                PGTransactionInformation pgInfo = new PGTransactionInformation();
                pgInfo.BookingDate = bookingInfo.BookingDate;
                pgInfo.BookedPassInformationId = bookingInfo.Id;
                pgInfo.TravellerId = bookingInfo.TravellerId;
                pgInfo.UniqueReferrenceNumber = bookingInfo.UniqueReferrenceNumber;
                pgInfo.TransactionNumber = transId;
                pgInfo.TotalAmout = bookingInfo.TotalAmout;
                pgInfo.PaymentStatus = pgStatus;
                pgInfo.PaymentResponse = pgStatus ? "Success" : "Failed";
                pgInfo.PaymentMode = "Credit Card";
                pgInfo.IsActive = pgStatus;
                _paymentGatewayRepository.AddTransactionInformation(pgInfo);
                _logger.LogInfo("PG transaction information updated successfully for trans Id " + transId);
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

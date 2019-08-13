using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Hiroshima.Maas.Common.Infrastructure.Logger;
using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.DL.Entities.PGTransactionInformationModel;
using Hiroshima.Maas.Services.Interfaces;
using Hiroshima.Maas.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Hiroshima.Maas.API.Controllers.PaymentGateway
{

    [ApiController]
    [Route("api/[Controller]")]
    public class PaymentGatewayController : BaseAPIController
    {
        private readonly IConfigurationService _configurationService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ITravellerService _travellerService;
        private readonly IPaymentGatewayTransactionService _paymentGatewayTransactionService;
        public PaymentGatewayController(IHostingEnvironment hostingEnvironment, IConfigurationService configurationService, ITravellerService travellerService, IPaymentGatewayTransactionService paymentGatewayTransactionService, IMessageHandler messageHandler, ILoggerManager logger, IHttpContextAccessorExtension _httpContextAccessorExtension) : base(_httpContextAccessorExtension, messageHandler, logger)
        {
            _configurationService = configurationService;
            _hostingEnvironment = hostingEnvironment;
            _travellerService = travellerService;
            _paymentGatewayTransactionService = paymentGatewayTransactionService;
            //_logger = Logger;
        }
        [AllowAnonymous]
        [HttpPost("ProcessOrderAfterPayment")]
        public bool ProcessOrderAfterPayment(GMOPaymentResponse content)
        {
            if (content == null)
                throw new Exception("Invalid information.");
            var response = _travellerService.GetBookingDatailsById(content.UniqueReference);
            if (response == null)
                throw new Exception("Invalid request.");
            return ProcessAppointmentAfterPaytmPayment(content, response.Result);
        }
        private bool ProcessAppointmentAfterPaytmPayment(GMOPaymentResponse response, BookedPassInformationViewModel bookingInfo)
        {
            try
            {
                if (response != null)
                {
                    bool pgStatus = false;
                    bool bookingStatus = false;
                    string Vmmp_txn = response.ShopID;
                    bool VpaymentStatus = String.IsNullOrEmpty(response.ErrorCode) ? true : false;
                    _logger.LogError("Payment gateway response status: " + VpaymentStatus);

                    _logger.LogError("Trying to update success booking pass information for id " + bookingInfo.UniqueReferrenceNumber);
                     bookingStatus = _travellerService.UpdateBookingInformation(bookingInfo, VpaymentStatus, response.TranID);
                    _logger.LogInfo("Booking pass information updated successfully for id " + bookingInfo.UniqueReferrenceNumber);

                    ///////////////////////////////// Add New PG Transation Information /////////////////////////
                    if (bookingStatus)
                    {
                        _logger.LogError("Trying to add new payment gateway transaction for id " + bookingInfo.UniqueReferrenceNumber);
                        pgStatus = _paymentGatewayTransactionService.AddTransactionInformation(bookingInfo, VpaymentStatus, response.TranID);
                        _logger.LogInfo("PG transaction information updated successfully for trans Id " + response.TranID);
                        return pgStatus;
                    }
                    return bookingStatus;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        public class GMOPaymentResponse
        {
            public string ShopID { get; set; }
            public string JobCd { get; set; }
            public string Amount { get; set; }
            public string Tax { get; set; }
            public string Currency { get; set; }
            public string AccessID { get; set; }
            public string AccessPass { get; set; }
            public string OrderID { get; set; }
            public string Forwarded { get; set; }
            public string Method { get; set; }
            public string PayTimes { get; set; }
            public string Approve { get; set; }
            public string TranID { get; set; }
            public string TranDate { get; set; }
            public string CheckString { get; set; }
            public string PayType { get; set; }
            public string CardNo { get; set; }
            public string ErrorCode { get; set; }
            public string ErrorDetails { get; set; }
            public string UniqueReference { get; set; }
            public string PGUrl { get; set; }
            public string ReturnURL { get; set; }
        }
    }


}
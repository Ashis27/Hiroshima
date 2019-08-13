using GMOPaymentGatewayDL.Entities;
using GMOPaymentgatewayServices.IServices;
using GMOPaymentgatewayServices.RequestAndResponse;
using GMOPaymentgGatewayDAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GMOPaymentgatewayServices.Services
{
    public class CreditCardPaymentGatewayService : IPaymentGatewayService, ICommonService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICommonService _commonService;
        public CreditCardPaymentGatewayService(IPaymentRepository PaymentRepository, ICommonService CommonService)
        {
            _paymentRepository = PaymentRepository;
            _commonService = CommonService;
        }

        public GMOPaymentResponse makePayment(string uniqueId, string paymentModule,string PGType)
        {
            GMOPaymentResponse response = new GMOPaymentResponse();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            CreditCardRequest creditCardRequest = new CreditCardRequest();
            try
            {
                BookedPassInformation bookedPassInformations = GetBookedPassInformations(uniqueId);
                if (bookedPassInformations == null)
                    throw new Exception("No Booking information found");

                // Get configuaration by ApplicationName,ModuleName,GroupEntityID,PaymentGateWay
                GMOPGConfiguration pgConfig = GetPGConfiguration(paymentModule, PGType);
                if (pgConfig == null)
                    throw new Exception("Payment for this module has not been enabled.");

                creditCardRequest = getPaymentRequestInfo(pgConfig, bookedPassInformations);
                parameters = GeneratePGParams(creditCardRequest);
                creditCardRequest.PGForm = GeneratePGForm(creditCardRequest.PaymentUrl, parameters);
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        private static CreditCardRequest getPaymentRequestInfo(GMOPGConfiguration pgConfig, BookedPassInformation bookedPassInformations)
        {
            Random generator = new Random();
            String randomNumber = generator.Next(0, 999999).ToString("D6");
            CreditCardRequest paymentRequest = new CreditCardRequest();
            paymentRequest.ShopID = pgConfig.ShopID;
            paymentRequest.ShopPassword = pgConfig.ShopPassword;
            paymentRequest.OrderID = randomNumber + DateTime.Now.Millisecond;
            paymentRequest.Amount = bookedPassInformations.TotalAmout.ToString(); //Convert.ToDecimal("10000").ToString("N");
            paymentRequest.Tax = "0";//Convert.ToDecimal("200").ToString("N");
            paymentRequest.DateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            paymentRequest.JobCd = pgConfig.JobCd;
            paymentRequest.UseCredit = pgConfig.UseCredit;
            paymentRequest.RetURL = pgConfig.CallbackURL + "?uniqueId=" + bookedPassInformations.UniqueReferrenceNumber;
            paymentRequest.PaymentUrl = pgConfig.GMOPGPaymentUrl;
            return paymentRequest;
        }

        private Dictionary<string, string> GeneratePGParams(CreditCardRequest paymentRequest)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ShopID", paymentRequest.ShopID);
            parameters.Add("OrderID", paymentRequest.OrderID);
            parameters.Add("Amount", paymentRequest.Amount);
            parameters.Add("Tax", paymentRequest.Tax);
            parameters.Add("DateTime", paymentRequest.DateTime);
            parameters.Add("RetURL", paymentRequest.RetURL); //This parameter is not mandatory. Use this to pass the callback url dynamically.
            parameters.Add("UseCredit", paymentRequest.UseCredit);
            parameters.Add("JobCd", paymentRequest.JobCd);
            string shop_hash_string = string.Empty;
            string member_hash_string = string.Empty;

            //[Shop ID+ Order ID+ Amount of money+ Tax/shipping+ Shop password+ Date information] in 
            // MD5 hashed character string.
            // shop information
            shop_hash_string = paymentRequest.ShopID + "|" + paymentRequest.OrderID + "|" + paymentRequest.Amount + "|" + paymentRequest.Tax + "|" + paymentRequest.ShopPassword + "|" + paymentRequest.DateTime;
            string shopPassString = GenerateMD5SignatureForGMO(shop_hash_string);

            //[Site ID+ Member ID+ Site password+ Date information] in MD5 hashed character string.
            // member information
            //member_hash_string = PaymentRequest.SiteID + PaymentRequest.MemberID + PaymentRequest.SitePassword + PaymentRequest.DateTime;
            //string memberPassString = GenerateMD5SignatureForPayU(member_hash_string).ToLower();

            paymentRequest.ShopPassString = shopPassString;
            //PaymentRequest.MemberPassString = memberPassString;
            parameters.Add("ShopPassString", shopPassString);
            return parameters;
        }

        public string GeneratePGForm(string paymentUrl, Dictionary<string, string> parameters)
        {
            return _commonService.GeneratePGForm(paymentUrl, parameters);
        }

        public string GenerateMD5SignatureForGMO(string input)
        {
            return _commonService.GenerateMD5SignatureForGMO(input).ToLower();
        }

        public BookedPassInformation GetBookedPassInformations(string uniqueId)
        {
            return _paymentRepository.GetBookedPassInformations(uniqueId.ToLower());
        }

        public GMOPGConfiguration GetPGConfiguration(string paymentModule, string PGType)
        {
            return _paymentRepository.GetPGConfiguration(paymentModule, PGType);
        }
    }
}

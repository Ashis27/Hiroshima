using GMOPaymentGatway.Models.PaymentGatwayModels;
using GMOPaymentGatway.Models.PGEnum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GMOPaymentGatway.Controllers
{
    public class PaymentGatewayController : Controller
    {
        private HiroshimaMaasDBContext _context = new HiroshimaMaasDBContext();
        //Constant value
        //private const string paymentModule = "CreditCard";
        private const string paymentGateway = "GMO";
        private const string taxAmount = "0";
        private string paymentModule = "";
        // GET: PaymentGatway
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GMOPamentGateway(string uniqueId, PGModuleEnum pgModule)
        {
            GMOPaymentRequest paymentRequest = new GMOPaymentRequest();
            try
            {
                if (String.IsNullOrEmpty(pgModule.ToString()))
                    throw new Exception("Invalid payment module selected");
                if (String.IsNullOrEmpty(uniqueId))
                    throw new Exception("Invalid unique referrence id.");
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                paymentModule = pgModule.ToString();
                BookedPassInformation bookedPassInformations = _context.BookedPassInformations.FirstOrDefault(t => t.UniqueReferrenceNumber.ToLower() == uniqueId.ToLower());
                if (bookedPassInformations == null)
                    throw new Exception("No booking information found.");
                // Get configuaration by ApplicationName,ModuleName,GroupEntityID,PaymentGateWay
                GMOPGConfiguration pgConfig = _context.GMOPGConfigurations.FirstOrDefault(p => p.PaymentGateway.ToLower() == paymentGateway.ToLower()
                && p.PaymentModule.ToLower() == paymentModule.ToLower() && p.IsActive);

                if (pgConfig == null)
                    throw new Exception("Payment for this module has not been enabled.");
                parameters = getPaymentRequestParamsInfo(pgConfig, bookedPassInformations, paymentModule);
                if (parameters == null)
                    throw new Exception("Invalid payment option selected.");
                paymentRequest.PGForm = generatePGForm(pgConfig.GMOPGPaymentUrl, parameters);
                return View(paymentRequest);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string generatePGForm(string paymentUrl, Dictionary<string, string> parameters)
        {
            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paymentUrl + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            // outputHTML += "<input type='hidden' name='SHOPPASSSTRING' value='" + shopPassString + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";
            return outputHTML;
        }


        private Dictionary<string, string> getPaymentRequestParamsInfo(GMOPGConfiguration pgConfig, BookedPassInformation bookedPassInformations, string paymentModule)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Random generator = new Random();
            String randomNumber = generator.Next(0, 999999).ToString("D6");
            GMOPaymentRequest paymentRequest = new GMOPaymentRequest();
            paymentRequest.ShopID = pgConfig.ShopID;
            paymentRequest.ShopPassword = pgConfig.ShopPassword;
            paymentRequest.OrderID = randomNumber + DateTime.Now.Millisecond;
            paymentRequest.Amount = bookedPassInformations.TotalAmout.ToString(); //Convert.ToDecimal("10000").ToString("N");
            paymentRequest.Tax = taxAmount;//Convert.ToDecimal("200").ToString("N");
            paymentRequest.DateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            paymentRequest.RetURL = pgConfig.CallbackURL + "?uniqueId=" + bookedPassInformations.UniqueReferrenceNumber;
            paymentRequest.PaymentUrl = pgConfig.GMOPGPaymentUrl;

            parameters.Add("ShopID", paymentRequest.ShopID);
            parameters.Add("OrderID", paymentRequest.OrderID);
            parameters.Add("Amount", paymentRequest.Amount);
            parameters.Add("Tax", paymentRequest.Tax);
            parameters.Add("DateTime", paymentRequest.DateTime);
            parameters.Add("RetURL", paymentRequest.RetURL); //This parameter is not mandatory. Use this to pass the callback url dynamically.

            if (paymentModule == PGModuleEnum.UseCredit.ToString())
            {
                paymentRequest.JobCd = pgConfig.JobCd;
                paymentRequest.UseCredit = pgConfig.UseCredit;
                parameters.Add("UseCredit", paymentRequest.UseCredit);
                parameters.Add("JobCd", paymentRequest.JobCd);
                string shop_hash_string = string.Empty;
                string member_hash_string = string.Empty;

                //[Shop ID+ Order ID+ Amount of money+ Tax/shipping+ Shop password+ Date information] in 
                // MD5 hashed character string.
                // shop information
                shop_hash_string = paymentRequest.ShopID + "|" + paymentRequest.OrderID + "|" + paymentRequest.Amount + "|" + paymentRequest.Tax + "|" + paymentRequest.ShopPassword + "|" + paymentRequest.DateTime;
                string shopPassString = GenerateMD5SignatureForGMO(shop_hash_string).ToLower();

                //[Site ID+ Member ID+ Site password+ Date information] in MD5 hashed character string.
                // member information
                //member_hash_string = PaymentRequest.SiteID + PaymentRequest.MemberID + PaymentRequest.SitePassword + PaymentRequest.DateTime;
                //string memberPassString = GenerateMD5SignatureForPayU(member_hash_string).ToLower();

                paymentRequest.ShopPassString = shopPassString;
                //PaymentRequest.MemberPassString = memberPassString;
                parameters.Add("ShopPassString", shopPassString);
            }
            else if (paymentModule == PGModuleEnum.UseDocomo.ToString())
            {
                paymentRequest.UseDocomo = "1";
                parameters.Add("UseDocomo", paymentRequest.UseDocomo);
               // parameters = getDocomoMobileRequestParam(paymentRequest);
            }
            else if (paymentModule == PGModuleEnum.UseAu.ToString())
            {
                string s_unicode = "バスサービス";
                // Convert a string to utf-8 bytes.
                byte[] utf8Bytes = System.Text.Encoding.UTF8.GetBytes(s_unicode);
                paymentRequest.UseAu = "1";
                paymentRequest.Commodity = utf8Bytes;
                paymentRequest.ServiceName = utf8Bytes;
                paymentRequest.ServiceTel = utf8Bytes;
                parameters.Add("UseAu", paymentRequest.UseAu);
                parameters.Add("Commodity", "");
                parameters.Add("ServiceName", "");
                parameters.Add("ServiceName", "");
                //parameters = getKantanKessaiRequestParam(paymentRequest);
            }
            else if (paymentModule == PGModuleEnum.UseSb.ToString())
            {
                paymentRequest.UseSb = "1";
                //parameters = getSoftbankMatometeShiharaiRequestParam(paymentRequest);
            }
            else
                return null;
            return parameters;
        }


        private static string GenerateMD5SignatureForGMO(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PaymentResponse(string uniqueId)
        {
            try
            {
                GMOPaymentResponse paymentResponse = new GMOPaymentResponse();
                BookedPassInformation bookedPassInformations = _context.BookedPassInformations.FirstOrDefault(t => t.UniqueReferrenceNumber.ToLower() == uniqueId.ToLower());
                if (bookedPassInformations == null)
                    throw new Exception("No record found.");

                //Get the Care4uPgConfig with the groupEntityId available in the IntermediarOrderDetail fetched.
                // Get configuaration by ApplicationName,ModuleName,GroupEntityID,PaymentGateWay
                GMOPGConfiguration pgConfig = _context.GMOPGConfigurations.FirstOrDefault(t => t.PaymentModule.ToUpper().Equals(paymentModule.ToLower())
                && t.PaymentGateway.ToLower().Equals(paymentGateway.ToLower())
                && t.IsActive);

                if (pgConfig == null)
                    throw new Exception("Payment for this module has not been enabled.");

                String merchantKey = pgConfig.ShopID;// "jBIWVeuQ0AKxUU%R"; 

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                foreach (string key in Request.Form.Keys)
                {
                    parameters.Add(key.Trim(), Request.Form[key].Trim());
                }

                paymentResponse.JobCd = parameters["JobCd"];
                paymentResponse.ShopID = parameters["ShopID"];
                paymentResponse.Amount = parameters["Amount"];
                paymentResponse.Tax = parameters["Tax"];
                paymentResponse.Currency = parameters["Currency"];
                paymentResponse.AccessID = parameters["AccessID"];
                paymentResponse.AccessPass = parameters["AccessPass"];
                paymentResponse.OrderID = parameters["OrderID"];
                paymentResponse.Forwarded = parameters["Forwarded"];
                paymentResponse.Method = parameters["Method"];
                paymentResponse.PayTimes = parameters["PayTimes"];
                paymentResponse.Approve = parameters["Approve"];
                paymentResponse.TranID = parameters["TranID"];
                paymentResponse.TranDate = parameters["TranDate"];
                paymentResponse.CheckString = parameters["CheckString"];
                paymentResponse.PayType = parameters["PayType"];
                paymentResponse.CardNo = parameters["CardNo"];
                paymentResponse.ErrorCode = parameters["ErrCode"];
                paymentResponse.ErrorDetails = parameters["ErrInfo"];
                paymentResponse.UniqueReference = uniqueId;
                paymentResponse.PGUrl = pgConfig.GMOPGPaymentUrl;

                string redirectAction = "";
                if (String.IsNullOrEmpty(paymentResponse.ErrorCode))
                    redirectAction = "PaymentSuccessResponse";
                else if (!String.IsNullOrEmpty(paymentResponse.ErrorCode))
                    redirectAction = "PaymentFailureResponse";
                else
                    redirectAction = "PaymentFailureResponse";
                var result = PerformPostPaymentJobs(paymentResponse);
                return RedirectToAction(redirectAction);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        //[HttpPost]
        //public ActionResult AfterCheckResponse(GMOPaymentResponse paymentResponse)
        //{
        //    try
        //    {
        //        BookedPassInformation bookedPassInformations = _context.BookedPassInformations.FirstOrDefault(t => t.UniqueReferrenceNumber.ToLower() == paymentResponse.UniqueReference.ToLower());
        //        if (bookedPassInformations == null)
        //            throw new Exception("No record found.");

        //        //Get the Care4uPgConfig with the groupEntityId available in the IntermediarOrderDetail fetched.
        //        // Get configuaration by ApplicationName,ModuleName,GroupEntityID,PaymentGateWay
        //        GMOPGConfiguration pgConfig = _context.GMOPGConfigurations.FirstOrDefault(t => t.PaymentModule.ToUpper().Equals(paymentModule.ToLower())
        //        && t.PaymentGateway.ToLower().Equals(paymentGateway.ToLower())
        //        && t.IsActive);

        //        if (pgConfig == null)
        //            throw new Exception("Payment for this module has not been enabled.");

        //        Dictionary<string, string> parameters = new Dictionary<string, string>();
        //        string paytmChecksum = "";
        //        foreach (string key in Request.Form.Keys)
        //        {
        //            parameters.Add(key.Trim(), Request.Form[key].Trim());
        //        }


        //        //string redirectAction = "";

        //        //if (paytmResponse.STATUS == "TXN_SUCCESS")
        //        //{
        //        //    //Update the IntermediarOrderDetails payment status, response and other records.
        //        //    interimOrderDetail.PaymentStatus = true;
        //        //    interimOrderDetail.PaymentResponse = "Success";

        //        //    //  Response.Write("Checksum Matched");
        //        //    redirectAction = "PaymentSuccessResponse";
        //        //    //return RedirectToAction("PaymentSuccessResponse", PaytmResponse);
        //        //}
        //        //else if (paytmResponse.STATUS == "TXN_FAILURE")
        //        //{
        //        //    //Update the IntermediarOrderDetails payment status, response and other records.
        //        //    interimOrderDetail.PaymentStatus = false;
        //        //    interimOrderDetail.PaymentResponse = "Failure";

        //        //    redirectAction = "PaymentFailureResponse";
        //        //    //return RedirectToAction("PaymentFailureResponse", PaytmResponse);
        //        //}
        //        //else
        //        //{
        //        //    //Update the IntermediarOrderDetails payment status, response and other records.
        //        //    interimOrderDetail.PaymentStatus = false;
        //        //    interimOrderDetail.PaymentResponse = "Pending";

        //        //    redirectAction = "PaymentPendingResponse";
        //        //    //return RedirectToAction("PaymentPendingResponse", PaytmResponse);
        //        //}

        //        //_context.InterimOrderDeatils.Attach(interimOrderDetail);
        //        //_context.Entry(interimOrderDetail).Property(p => p.PaymentStatus).IsModified = true;
        //        ////_context.Entry(interimOrderDetail).Property(p => p.PaymentResponse).IsModified = true;
        //        //_context.SaveChanges();
        //        //var result = PerformPostPaymentJobs(paytmResponse);
        //        //if (paytmResponse.STATUS == "TXN_SUCCESS")
        //        //{
        //        //    if (agentType == "MobileApp")
        //        //        return RedirectToAction("PaymentSuccessResponse");
        //        //    else if (agentType == "WebSite")
        //        //        return View("../Payment/HealthProWebAfterPgResponse", interimOrderDetail);
        //        //    else
        //        //        return RedirectToAction("PaymentSuccessResponse");
        //        //}
        //        //else if (paytmResponse.STATUS == "TXN_FAILURE")
        //        //{
        //        //    if (agentType == "MobileApp")
        //        //        return RedirectToAction("PaymentFailureResponse");
        //        //    else if (agentType == "WebSite")
        //        //        return View("../Payment/HealthProWebAfterPgResponse", interimOrderDetail);
        //        //    else
        //        //        return RedirectToAction("PaymentSuccessResponse");
        //        //}
        //        //else
        //        //{
        //        //    if (agentType == "MobileApp")
        //        //        return RedirectToAction("PaymentPendingResponse");
        //        //    else if (agentType == "WebSite")
        //        //        return View("../Payment/HealthProWebAfterPgResponse", interimOrderDetail);
        //        //    else
        //        //        return RedirectToAction("PaymentSuccessResponse");
        //        //}
        //        return null;

        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }

        //    //return View();
        //}

        public ActionResult PaymentSuccessResponse()
        {
            return View();
        }
        public ActionResult PaymentFailureResponse()
        {
            return View();
        }
        public ActionResult PaymentPendingResponse()
        {
            return View();
        }

        private HttpResponseMessage PerformPostPaymentJobs(GMOPaymentResponse response)
        {
            HttpClient httpClient = new HttpClient();
            string baseUrl = ConfigurationManager.AppSettings["PaymentResponseJobsUrl"];
            var jsonString = JsonConvert.SerializeObject(response);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(baseUrl, content).Result;
            return result;
        }




        ///This was for dynamic PG integration
        //Will be integrated later

        //private readonly IPaymentGatewayService _paymentGatewayService;
        ////private  HiroshimaMaasDBContext _context = new HiroshimaMaasDBContext();
        ////Constant value
        //private string paymentModule = String.Empty;
        //private string PGType = String.Empty;
        //private const string taxAmount = "0";
        //// GET: PaymentGatway
        //public PaymentGatewayController(IPaymentGatewayService PaymentGatewayService)
        //{
        //    _paymentGatewayService = PaymentGatewayService;
        //}
        //public ActionResult GMOPamentGateway(string uniqueId, PGModuleEnum paymentType, PGTypeEnum paymentGateway)
        //{
        //    try
        //    {
        //        GMOPaymentResponse paymentResponse = new GMOPaymentResponse();
        //        //Get enum description based on payment module
        //        paymentModule = GetPGModuleDescription(paymentType);
        //        //Get enum description based on payment type
        //        PGType = GetPGTypeDescription(paymentGateway);

        //        if (String.IsNullOrEmpty(uniqueId))
        //            throw new Exception("Invalid unique referrence id.");
        //        if (String.IsNullOrEmpty(paymentModule))
        //            throw new Exception("Invalid payment method.");

        //        paymentResponse = _paymentGatewayService.makePayment(uniqueId, paymentModule, PGType);
        //        if (!paymentResponse.Status)
        //            throw new Exception(paymentResponse.Message);
        //        ViewBag.PGPaymentUrl = paymentResponse.PGUrl;
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}


    }
}
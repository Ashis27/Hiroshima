using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMOPaymentGatway.Models.PaymentGatwayModels
{
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
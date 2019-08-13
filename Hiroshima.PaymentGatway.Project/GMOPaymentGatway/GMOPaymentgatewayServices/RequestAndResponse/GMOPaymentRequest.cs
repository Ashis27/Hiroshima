using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMOPaymentgatewayServices.RequestAndResponse
{
    public class GMOPaymentRequest
    {
        public string SiteID { get; set; }
        public string SitePassword { get; set; }
        public string MemberID { get; set; }
        public string ShopID { get; set; }
        public string ShopPassword { get; set; }
        public string OrderID { get; set; }
        public string DateTime { get; set; }
        public string MemberPassString { get; set; }
        public string ShopPassString { get; set; }
        public string Amount { get; set; }
        public string Tax { get; set; }
        public string Currency { get; set; }
        public string RetURL { get; set; }
        public string PaymentUrl { get; set; }
        public string PGForm { get; set; }  
    }
}
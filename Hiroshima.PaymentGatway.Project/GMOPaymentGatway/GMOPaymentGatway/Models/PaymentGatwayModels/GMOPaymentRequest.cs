using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMOPaymentGatway.Models.PaymentGatwayModels
{
    public class GMOPaymentRequest
    {
        public string SiteID { get; set; }
        public string SitePassword { get; set; }
        public string MemberID { get; set; }
        public string ShopID { get; set; }
        public string ShopPassword { get; set; }
        public string OrderID { get; set; }
        public string MemberPassString { get; set; }
        public string ShopPassString { get; set; }
        public string DateTime { get; set; }
        public string Amount { get; set; }
        public string Tax { get; set; }
        public string Currency { get; set; }
        public string RetURL { get; set; }
        public string PaymentUrl { get; set; }
        public string PGForm { get; set; }
        //Credit Card PG module
        public string JobCd { get; set; }
        public string UseCredit { get; set; }
        //Docomo Mobile PG module
        public string UseDocomo { get; set; }
        public string DocomoDisp1 { get; set; }
        public string DocomoDisp2 { get; set; }
        public string PaymentTermSec { get; set; }
        public string DispShopName { get; set; }
        public string DispPhoneNumber { get; set; }
        public string DispMailAddress { get; set; }
        public string DispShopUrl { get; set; }
        //Kantan Kessai PG module
        public string UseAu { get; set; }
        public byte[] Commodity { get; set; }
        public byte[] ServiceName { get; set; }
        public byte[] ServiceTel { get; set; }
        //Softbank Matomete Shiharai  PG module
        public string UseSb { get; set; }
    }
}
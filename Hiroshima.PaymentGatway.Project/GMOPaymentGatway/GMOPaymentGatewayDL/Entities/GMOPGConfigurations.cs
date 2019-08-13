using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GMOPaymentGatewayDL.Entities
{
    public class GMOPGConfiguration
    {
        [Key]
        public int Id { get; set; }
        public string PaymentModule { get; set; }
        public string PaymentGateway { get; set; }
        public string SiteID { get; set; }
        public string SitePassword { get; set; }
        public string MemberID { get; set; }
        public string ShopID { get; set; }
        public string ShopPassword { get; set; }
        public string JobCd { get; set; }
        public string UseCredit { get; set; }
        public string CallbackURL { get; set; }
        public string GMOPGPaymentUrl { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
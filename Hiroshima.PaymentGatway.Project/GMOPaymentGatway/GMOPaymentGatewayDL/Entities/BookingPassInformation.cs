using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GMOPaymentGatewayDL.Entities
{

    public class BookedPassInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PassInformationId { get; set; }
        public int TravellerId { get; set; }
        public string UniqueReferrenceNumber { get; set; }
        public long TransactionNumber { get; set; }
        public string RiderInformation { get; set; }
        public DateTime BookingDate { get; set; }
        public bool PaymentStatus { get; set; }
        public string PaymentResponse { get; set; }
        public double TotalAmout { get; set; }
        public int Child { get; set; }
        public int Adult { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
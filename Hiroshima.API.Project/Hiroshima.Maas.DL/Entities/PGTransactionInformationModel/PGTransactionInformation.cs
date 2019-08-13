using Hiroshima.Maas.DL.Entities.PassInformationModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hiroshima.Maas.DL.Entities.PGTransactionInformationModel
{
    public class PGTransactionInformation:BaseEntity
    {
        public int BookedPassInformationId { get; set; }
        public int TravellerId { get; set; }
        public string UniqueReferrenceNumber { get; set; }
        public string TransactionNumber { get; set; }
        public DateTime BookingDate { get; set; }
        [Column(TypeName = "TINYINT(1)")]
        public bool PaymentStatus { get; set; }
        public string PaymentResponse { get; set; }
        public string PaymentMode { get; set; }
        public double TotalAmout { get; set; }
        [Column(TypeName = "TINYINT(1)")]
        public bool IsRetry { get; set; }
        public string Remark { get; set; }
        public BookedPassInformation BookedPassInformation { get; set; }
    }
}

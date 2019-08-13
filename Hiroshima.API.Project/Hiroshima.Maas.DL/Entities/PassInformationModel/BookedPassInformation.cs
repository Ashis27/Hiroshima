using Hiroshima.Maas.DL.Entities.PGTransactionInformationModel;
using Hiroshima.Maas.DL.Entities.QRCodeModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hiroshima.Maas.DL.Entities.PassInformationModel
{
    public class BookedPassInformation:BaseEntity
    {
        public int PassInformationId { get; set; }
        public int TravellerId { get; set; }
        public string UniqueReferrenceNumber { get; set; }
        public string TransactionNumber { get; set; }
        public string RiderInformation { get; set; }
        public DateTime BookingDate { get; set; }
        [Column(TypeName = "TINYINT(1)")]
        public bool PaymentStatus { get; set; }
        public string PaymentResponse { get; set; }
        public double TotalAmout { get; set; }
        public int Child { get; set; }
        public int Adult { get; set; }
        public string Remark { get; set; }
        public PassInformation PassInformation { get; set; }
        public PGTransactionInformation PGTransactionInformation { get; set; }
        public QRCode QRCode { get; set; }
    }
}

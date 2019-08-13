using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.Services.ViewModels
{
    public class PGTransactionInformationViewModel : BaseResponseModel
    {
        public int BookedPassInformationId { get; set; }
        public int TravellerId { get; set; }
        public string UniqueReferrenceNumber { get; set; }
        public string TransactionNumber { get; set; }
        public DateTime BookingDate { get; set; }
        public bool PaymentStatus { get; set; }
        public string PaymentResponse { get; set; }
        public string PaymentMode { get; set; }
        public double TotalAmout { get; set; }
        public bool IsRetry { get; set; }
        public string Remark { get; set; }
        public BookedPassInformationViewModel BookingInfo { get; set; }
    }
}

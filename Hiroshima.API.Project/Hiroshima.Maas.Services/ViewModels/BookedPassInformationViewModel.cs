using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hiroshima.Maas.Services.ViewModels
{
    public class BookedPassInformationViewModel : BaseResponseModel
    {
        [Required(ErrorMessage = "Invalid pass in formation")]
        public int PassInformationId { get; set; }
        [Required(ErrorMessage = "Device id required")]
        public string TravellerDeviceId { get; set; }
        public string TravellerDeviceType { get; set; }
        public int TravellerId { get; set; }
        public string UniqueReferrenceNumber { get; set; }
        public string TransactionNumber { get; set; }
        public string RiderInformation { get; set; }
        public DateTime BookingDate { get; set; }
        public bool PaymentStatus { get; set; }
        public string PaymentResponse { get; set; }
        [Range(1, Double.MaxValue, ErrorMessage = "Total amount must be gretter than 0")]
        public double TotalAmout { get; set; }
        public int Child { get; set; }
        public int Adult { get; set; }
        public string Remark { get; set; }
        public PassInformationViewModel PassInformation { get; set; }
        public PGTransactionInformationViewModel PGTransactionInfo { get; set; }
        public QRCodeViewModel QRCode { get; set; }
    }
}

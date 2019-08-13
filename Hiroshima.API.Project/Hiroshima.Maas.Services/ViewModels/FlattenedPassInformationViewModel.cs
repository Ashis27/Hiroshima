using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.Services.ViewModels
{
    public class FlattenedPassInformationViewModel: BaseResponseModel
    {
        public int TravellerId { get; set; }
        public string PassName { get; set; }
        public string PassDesc { get; set; }
        public string PassAreaDescription { get; set; }
        public bool IsPerkAvailable { get; set; }
        public string PerkDescription { get; set; }
        public int DefaultCurrency { get; set; }
        public DateTime PassExpiredDate { get; set; }
        public DateTime PassActivatedDate { get; set; }
        public DateTime ActivatedPassExpiredDate { get; set; }
        public DateTime PassExpiredDuraionDate { get; set; }
        public byte[] QRCodeImage { get; set; }
        public string UniqueReferrenceNumber { get; set; }
        public long TransactionNumber { get; set; }
        public string RiderInformation { get; set; }
        public DateTime BookingDate { get; set; }
        public bool PaymentStatus { get; set; }
        public string PaymentResponse { get; set; }
        public double Price { get; set; }
        public int Child { get; set; }
        public int Adult { get; set; }
        public string ImageURL { get; set; }
        public string Remark { get; set; }
    }
}

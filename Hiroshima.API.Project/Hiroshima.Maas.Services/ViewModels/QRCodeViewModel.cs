using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.Services.ViewModels
{
    public class QRCodeViewModel: BaseResponseModel
    {
        public int BookedPassInformationId { get; set; }
        public DateTime PassActivatedDate { get; set; }
        public DateTime ActivatedPassExpiredDate { get; set; }
        public DateTime PassExpiredDuraionDate { get; set; }
        public byte[] QRCodeImage { get; set; }
        public BookedPassInformationViewModel BookedPassInformation { get; set; }
    }
}

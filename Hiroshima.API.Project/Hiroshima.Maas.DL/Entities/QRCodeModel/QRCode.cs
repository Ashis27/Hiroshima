using Hiroshima.Maas.DL.Entities.PassInformationModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.DL.Entities.QRCodeModel
{
    public class QRCode : BaseEntity
    {
        public int BookedPassInformationId { get; set; }
        public DateTime PassActivatedDate { get; set; }
        public DateTime ActivatedPassExpiredDate { get; set; }
        public DateTime PassExpiredDuraionDate { get; set; }
        public byte[] QRCodeImage { get; set; }
        public BookedPassInformation BookedPassInformation { get; set; }
    }
}

using Hiroshima.Maas.DL.Entities.PTOInformationModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.DL.Entities.PassInformationModel
{
    public class PassActivePTO:BaseEntity
    {
        public int PassInformationId { get; set; }
        //public PassInformation PassInformation { get; set; }
        public int PTOInformationId { get; set; }
        public PTOInformation PTOInformation { get; set; }
    }
}

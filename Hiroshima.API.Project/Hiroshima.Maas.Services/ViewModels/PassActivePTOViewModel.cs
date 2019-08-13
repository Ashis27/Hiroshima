using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.Services.ViewModels
{
    public class PassActivePTOViewModel:BaseResponseModel
    {
        public int PassInformationId { get; set; }
        public PassInformationViewModel PassInformation { get; set; }
        public int PTOInformationId { get; set; }
        public PTOInformationViewModel PTOInformation { get; set; }
    }
}

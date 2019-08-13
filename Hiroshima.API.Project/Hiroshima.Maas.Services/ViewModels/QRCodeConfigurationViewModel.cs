using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.Services.ViewModels
{
    public class QRCodeConfigurationViewModel:BaseResponseModel
    {
        public int RegenerationTimeInMin { get; set; }
    }
}

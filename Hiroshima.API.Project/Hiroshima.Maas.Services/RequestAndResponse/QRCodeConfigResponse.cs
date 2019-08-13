using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.Services.RequestAndResponse
{
    public class QRCodeConfigResponse: BaseResponse
    {
        public QRCodeConfigResponse(bool status, string message) : base(status, message)
        {

        }
        public string Result { get; set; }
    }
}

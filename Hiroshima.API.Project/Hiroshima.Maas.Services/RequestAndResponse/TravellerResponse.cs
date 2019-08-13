using Hiroshima.Maas.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.Services.RequestAndResponse
{
    public class TravellerResponse: BaseResponse
    {
        public TravellerResponse(bool status, string message) : base(status, message)
        {

        }
        public string Result { get; set; }
    }
}

using Hiroshima.Maas.Services.Utility.Helper;
using Hiroshima.Maas.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.Services.RequestAndResponse
{
    public class AdminResponse : BaseResponse
    {
        public AdminResponse(bool status,string message):base(status,message)
        {

        }
        public Token Token { get; set; }
        public AdminUserViewModel AdminUser { get; set; }
        public PassInformationViewModel PassInformation { get; set; }
        public PTOInformationViewModel PTOInformation { get; set; }
    }
}

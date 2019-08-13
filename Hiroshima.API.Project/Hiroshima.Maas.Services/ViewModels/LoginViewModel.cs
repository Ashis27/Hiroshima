using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.Services.ViewModels
{
    public class LoginViewModel : BaseResponseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

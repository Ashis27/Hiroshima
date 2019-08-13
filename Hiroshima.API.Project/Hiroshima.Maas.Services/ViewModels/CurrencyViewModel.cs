using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.Services.ViewModels
{
    public class CurrencyViewModel : BaseResponseModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public bool IsDefault { get; set; }
    }
}

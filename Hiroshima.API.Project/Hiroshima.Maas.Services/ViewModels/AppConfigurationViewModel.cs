using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.Services.ViewModels
{
    public class AppConfigurationViewModel : BaseResponseModel
    {
        public string ConfigurationType { get; set; }
        public string CurrentVersion { get; set; }
        public string UpdatedVersion { get; set; }
        public bool IsForceUpdate { get; set; }
        public string AppStoreURL { get; set; }
        public string AppContent { get; set; }
        public string Remark { get; set; }
    }
}

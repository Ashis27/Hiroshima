using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hiroshima.Maas.DL.Entities.AppConfigurationModel
{
    public class AppConfiguration:BaseEntity
    {
        public string ConfigurationType { get; set; }
        public string CurrentVersion { get; set; }
        public string UpdatedVersion { get; set; }
        [Column(TypeName = "TINYINT(1)")]
        public bool IsForceUpdate { get; set; }
        public string AppStoreURL { get; set; }
        public string AppContent { get; set; }
        public string Remark { get; set; }
    }
}

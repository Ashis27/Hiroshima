using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hiroshima.Maas.DL.Entities.CurrencyConfigurationModel
{
    public class CurrencyConfiguration : BaseEntity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        [Column(TypeName = "TINYINT(1)")]
        public bool IsDefault { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hiroshima.Maas.DL.Entities.PassInformationModel
{
    public class PassDescription:BaseEntity
    {
        public int PassInformationId { get; set; }
        public string PassName { get; set; }
        public string PassDesc { get; set; }
        public int SelectedLanguage { get; set; }
        public string PassAreaDescription { get; set; }
        [Column(TypeName = "TINYINT(1)")]
        public bool IsPerkAvailable { get; set; }
        public string PerkDescription { get; set; }
        public string PassAreaImageURL { get; set; }
    }
}

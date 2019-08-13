using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hiroshima.Maas.Services.ViewModels
{
   public class PassDescriptionViewModel:BaseResponseModel
    {
        public int PassInformationId { get; set; }
        [Required]
        public string PassName { get; set; }
        public string PassDesc { get; set; }
        [Required]
        public int SelectedLanguage { get; set; }
        public string PassAreaDescription { get; set; }
        public string PassAreaImageURL { get; set; }
        public bool IsPerkAvailable { get; set; }
        public string PerkDescription { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hiroshima.Maas.Services.ViewModels
{
    public class PTODescriptionViewModel : BaseResponseModel
    {
        public int PTOInformationId { get; set; }
        [Required]
        public string PTOName { get; set; }
        public string Description { get; set; }
        [Required]
        public int SelectedLanguage { get; set; }
    }
}

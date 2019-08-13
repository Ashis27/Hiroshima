using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hiroshima.Maas.Services.ViewModels
{
    public class PassInformationViewModel : BaseResponseModel
    {
        [Required]
        public int DefaultCurrency { get; set; }
        public int PassValidityInDays { get; set; }
        public int PassValidityInHours { get; set; }
        public int PassExpiredDurationInDays { get; set; }
        public int PassExpiredDurationInHours { get; set; }
        public DateTime PassExpiredDate { get; set; }
        [Required]
        public double AdultPrice { get; set; }
        public double ChildPrice { get; set; }
        public string ImageURL { get; set; }
        public string Remark { get; set; }
        public IEnumerable<PassDescriptionViewModel> PassDescription { get; set; }
        public IEnumerable<PassActivePTOViewModel> PassActivePTOs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.Services.ViewModels
{
   public class PTOInformationViewModel:BaseResponseModel
    {
        public string ImageURL { get; set; }
        public string Remark { get; set; }
        public IEnumerable<PTODescriptionViewModel> PTODescription { get; set; }
        public IEnumerable<PassActivePTOViewModel> PassActivePTOs { get; set; }
    }
}

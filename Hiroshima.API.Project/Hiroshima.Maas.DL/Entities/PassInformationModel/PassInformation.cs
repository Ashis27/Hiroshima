using Hiroshima.Maas.DL.Entities.CurrencyConfigurationModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.DL.Entities.PassInformationModel
{
   public class PassInformation:BaseEntity
    {
        public int PassValidityInDays{ get; set; }
        public int PassValidityInHours { get; set; }
        public int PassExpiredDurationInDays { get; set; }
        public int PassExpiredDurationInHours { get; set; }
        public DateTime PassExpiredDate { get; set; }
        public int DefaultCurrency { get; set; }
        public double AdultPrice { get; set; }
        public double ChildPrice { get; set; }
        public string ImageURL { get; set; }
        public string Remark { get; set; }
        public IEnumerable<PassDescription> PassDescription { get; set; }
        public IEnumerable<PassActivePTO> PassActivePTOs { get; set; }
    }
}

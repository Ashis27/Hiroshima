using Hiroshima.Maas.DL.Entities.PassInformationModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.DL.Entities.PTOInformationModel
{
    public class PTOInformation : BaseEntity
    {
        public string ImageURL { get; set; }
        public string Remark { get; set; }
        public IEnumerable<PTODescription> PTODescription { get; set; }
        //public IEnumerable<PassActivePTO> PassActivePTOs { get; set; }
    }
}

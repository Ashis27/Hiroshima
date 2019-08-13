using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.DL.Entities.PTOInformationModel
{
    public class PTODescription:BaseEntity
    {
        public int PTOInformationId { get; set; }
        public string PTOName { get; set; }
        public string Description { get; set; }
        public int SelectedLanguage { get; set; }
    }
}

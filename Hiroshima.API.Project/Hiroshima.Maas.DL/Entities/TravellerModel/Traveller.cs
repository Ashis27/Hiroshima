using Hiroshima.Maas.DL.Entities.PassInformationModel;
using Hiroshima.Maas.DL.Entities.TravellerFeedbackModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.DL.Entities.TravellerModel
{
    public class Traveller:BaseEntity
    {
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string DefaultLanguage { get; set; }
        public IEnumerable<BookedPassInformation> BookedPassInfo { get; set; }
        public IEnumerable<TravellerFeedback> TravellerFeedback { get; set; }
    }
}

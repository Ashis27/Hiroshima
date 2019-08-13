using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.DL.Entities.TravellerFeedbackModel
{
    public class TravellerFeedback:BaseEntity
    {
        public int TravellerId { get; set; }
        public string FullName { get; set; }
        public string ContactNumber { get; set; }
        public string FeedbackDescription { get; set; }
        public string FeedbackType { get; set; }
        public string CommentedByAdmin { get; set; }
    }
}

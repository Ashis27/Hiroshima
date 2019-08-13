using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hiroshima.Maas.Services.ViewModels
{
    public class TravellerFeedbackViewModel : BaseResponseModel
    {
        [Required(ErrorMessage = "Device id required")]
        public string DeviceId { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Contact number required")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid contact number")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Description canbot be empty")]
        public string FeedbackDescription { get; set; }
        public string FeedbackType { get; set; }
        public string CommentedByAdmin { get; set; }
    }
}

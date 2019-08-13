using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hiroshima.Maas.Services.ViewModels
{
    public class AdminUserViewModel : BaseResponseModel
    {
       // [Required(ErrorMessage = "UserName required")]
        public string UserName { get; set; }
       // [Required(ErrorMessage = "Password required")]
       //[StringLength(16, ErrorMessage = "Password must be between 6 and 16 characters", MinimumLength = 6)]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Hiroshima.Maas.Services.ViewModels
{
    public class BaseResponseModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        //public int CreatedBy { get; set; }
        //public int ModifiedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public DateTime ModifiedDate { get; set; }
    }
}

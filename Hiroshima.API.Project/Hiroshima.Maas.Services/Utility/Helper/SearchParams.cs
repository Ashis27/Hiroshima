using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.Services.Utility.Helper
{
    public class SearchParams
    {
        public string DeviceId { get; set; }
        public int TravellerId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Lang { get; set; }
        public DateTime? StartDateAndTime { get; set; }
        public DateTime? EndDateAndTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMOPaymentgatewayServices.RequestAndResponse
{
    public abstract class PGResponseMessage
    {
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}

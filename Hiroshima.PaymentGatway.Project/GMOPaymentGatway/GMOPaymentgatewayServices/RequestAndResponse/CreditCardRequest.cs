using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMOPaymentgatewayServices.RequestAndResponse
{
    public class CreditCardRequest:GMOPaymentRequest
    {
        public string JobCd { get; set; }
        public string UseCredit { get; set; }
    }
}

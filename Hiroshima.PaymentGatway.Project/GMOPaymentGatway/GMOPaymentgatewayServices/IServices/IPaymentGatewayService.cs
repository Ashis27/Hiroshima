using GMOPaymentgatewayServices.RequestAndResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMOPaymentgatewayServices.IServices
{
    public interface IPaymentGatewayService
    {
        GMOPaymentResponse makePayment(string uniqueId, string paymentType,string PGType);
    }
}

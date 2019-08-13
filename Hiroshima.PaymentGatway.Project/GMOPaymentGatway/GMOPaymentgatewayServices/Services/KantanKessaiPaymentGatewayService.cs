using GMOPaymentgatewayServices.IServices;
using GMOPaymentgatewayServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMOPaymentgatewayServices.Services
{
    public class KantanKessaiPaymentGatewayService: IPaymentGatewayService
    {
        public KantanKessaiPaymentGatewayService()
        {

        }
        public GMOPaymentResponse makePayment(string uniqueId, string paymentType)
        {
            return null;
        }
    }
}

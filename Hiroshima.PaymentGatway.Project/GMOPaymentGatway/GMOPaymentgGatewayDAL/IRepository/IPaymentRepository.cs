using GMOPaymentGatewayDL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMOPaymentgGatewayDAL.IRepository
{
    public interface IPaymentRepository
    {
        BookedPassInformation GetBookedPassInformations(string uniqueId);
        GMOPGConfiguration GetPGConfiguration(string paymentModule, string PGType);
    }
}

using GMOPaymentGatewayDL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMOPaymentgatewayServices.IServices
{
    public interface ICommonService
    {
        string GeneratePGForm(string paymentUrl, Dictionary<string, string> parameters);
        string GenerateMD5SignatureForGMO(string input);
        BookedPassInformation GetBookedPassInformations(string uniqueId);
        GMOPGConfiguration GetPGConfiguration(string paymentModule, string PGType);
    }
}

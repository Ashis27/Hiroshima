using Hiroshima.Maas.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.Services.Interfaces
{
    public interface IPaymentGatewayTransactionService
    {
        bool AddTransactionInformation(BookedPassInformationViewModel pgTransactionInfo, bool status, string transId);
    }
}

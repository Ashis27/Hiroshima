using Hiroshima.Maas.DL.Entities.PGTransactionInformationModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.DAL.Interfaces
{
    public interface IPaymentGatewayTransactionRepository
    {
        void AddTransactionInformation(PGTransactionInformation pgTransactionInfo);
    }
}

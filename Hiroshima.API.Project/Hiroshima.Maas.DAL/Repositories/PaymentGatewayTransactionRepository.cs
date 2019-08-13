using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.DAL.Contexts;
using Hiroshima.Maas.DAL.Interfaces;
using Hiroshima.Maas.DL.Entities.PGTransactionInformationModel;
using Hiroshima.Maas.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.DAL.Repositories
{
    public class PaymentGatewayTransactionRepository : BaseRepository<PGTransactionInformation>, IPaymentGatewayTransactionRepository
    {
        public PaymentGatewayTransactionRepository(HiroshimaMaaSDBContext context, IMessageHandler messageHandler, IUnitOfWork unitOfWork) : base(context, messageHandler, unitOfWork) { }

        #region Add_PG_Transation_information
        public async void AddTransactionInformation(PGTransactionInformation pgTransactionInfo)
        {
            this.Add(pgTransactionInfo);
            await this._unitOfWork.CompleteAsync();
        }
        #endregion
    }
}

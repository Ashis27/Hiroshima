using GMOPaymentGatewayDL.Entities;
using GMOPaymentgGatewayDAL.GMOPGContext;
using GMOPaymentgGatewayDAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMOPaymentgGatewayDAL.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private HiroshimaMaasDBContext _context;
        public PaymentRepository(HiroshimaMaasDBContext Context)
        {
            _context = Context;
        }
        public BookedPassInformation GetBookedPassInformations(string uniqueId)
        {
            return this._context.BookedPassInformations.FirstOrDefault(t => t.UniqueReferrenceNumber.ToLower() == uniqueId.ToLower());
        }
        public GMOPGConfiguration GetPGConfiguration(string paymentModule, string PGType)
        {
            return this._context.GMOPGConfigurations.FirstOrDefault(p => p.PaymentGateway.ToLower() == PGType.ToLower()
            && p.PaymentModule.ToLower() == paymentModule.ToLower() && p.IsActive);
        }
    }
}

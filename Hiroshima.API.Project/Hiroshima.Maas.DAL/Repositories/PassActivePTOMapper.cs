using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.DAL.Contexts;
using Hiroshima.Maas.DAL.Interfaces;
using Hiroshima.Maas.DL.Entities.PassInformationModel;
using Hiroshima.Maas.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiroshima.Maas.DAL.Repositories
{
    public class PassActivePTOMapper: BaseRepository<PassActivePTO>, IPassActivePTOMapper
    {
        public PassActivePTOMapper(HiroshimaMaaSDBContext context, IMessageHandler messageHandler, IUnitOfWork unitOfWork) : base(context, messageHandler, unitOfWork) { }

        #region Add_Pass_PTO_In_Mapper
        public async void BulkAddPTO(IEnumerable<PassActivePTO> activePTOs)
        {
            await _context.PassActivePTOs.AddRangeAsync(activePTOs);
            await this._unitOfWork.CompleteAsync();
        }
        #endregion

        #region Delete_PTO_From_Mapper
        public async void BulkDeletePTO(int id)
        {
            IEnumerable<PassActivePTO> activePTOs = this.GetDbSet<PassActivePTO>().Where(p => p.PTOInformationId == id).ToArray(); 
            this.DeleteRange(activePTOs);
            await this._unitOfWork.CompleteAsync();
        }
        #endregion

        #region Delete_Pass_From_Mapperass
        public async void BulkDeletePass(int id)
        {
            IEnumerable<PassActivePTO> activePasses = this.GetDbSet<PassActivePTO>().Where(p => p.PassInformationId == id).ToArray();
            this.DeleteRange(activePasses);
            await this._unitOfWork.CompleteAsync();
        }
        #endregion
    }
}

using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.DAL.Contexts;
using Hiroshima.Maas.DAL.Interfaces;
using Hiroshima.Maas.DL.Entities.PassInformationModel;
using Hiroshima.Maas.DL.Utility;
using Hiroshima.Maas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.DAL.Repositories
{
    public class PassRepository : BaseRepository<PassInformation>, IPassRepository
    {
        public PassRepository(HiroshimaMaaSDBContext context, IMessageHandler messageHandler, IUnitOfWork unitOfWork) : base(context, messageHandler, unitOfWork){ }

        #region Create_New_Pass
        public async void CreatePass(PassInformation passInformation)
        {
            this.Add(passInformation);
            await _unitOfWork.CompleteAsync();
        }
        #endregion

        #region Get_Active_Passes
        public async Task<PaginatedList<PassInformation>> GetActivePasses(PassInfoSearchParams searchParams)
        {
            IQueryable<PassInformation> passInformation = this._context.PassInformations
                                                              .Include(pass => pass.PassActivePTOs)
                                                              .ThenInclude(info => info.PTOInformation)
                                                              .ThenInclude(i => i.PTODescription)
                                                              .Include(desc => desc.PassDescription)
                                                              .Where(p => p.IsActive && p.PassExpiredDate >= DateTime.Today && p.PassDescription != null && p.PassActivePTOs != null && p.PassDescription.Any(o=>o.SelectedLanguage == searchParams.Lang));
            //This is used to create a PaginatedList class that uses Skip and Take statements to filter data on the server instead of always retrieving all rows of the table
            var filteredResult = await PaginatedList<PassInformation>.CreateAsync(passInformation.AsNoTracking(), searchParams.PageIndex, searchParams.PageSize);
            return filteredResult;
        }
        #endregion

        #region Get_Active_Pass
        public async Task<PassInformation> GetActivePass(int id)
        {
            var passInformation = await this._context.PassInformations
                                            .Include(pass => pass.PassActivePTOs)
                                            .ThenInclude(info => info.PTOInformation)
                                            .Include(desc => desc.PassDescription)
                                            .Where(p => p.IsActive && p.Id == id).FirstOrDefaultAsync();
            return passInformation;
        }
        #endregion

        #region Update_Pass
        public async void UpdatePass(PassInformation passInformation)
        {
            this.Update(passInformation);
            await this._unitOfWork.CompleteAsync();
        }
        #endregion
    }
}

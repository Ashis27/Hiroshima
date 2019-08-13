using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.DAL.Contexts;
using Hiroshima.Maas.DAL.Interfaces;
using Hiroshima.Maas.DL.Entities.PTOInformationModel;
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
    public class PTORepository : BaseRepository<PTOInformation>, IPTORepository
    {
        public PTORepository(HiroshimaMaaSDBContext context, IMessageHandler messageHandler, IUnitOfWork unitOfWork) : base(context, messageHandler, unitOfWork) { }

        #region Create_New_PTO
        public async void CreatePTO(PTOInformation ptoInformation)
        {
            //Added new PTO information with PTO description
            this.Add(ptoInformation);
            await _unitOfWork.CompleteAsync();
        }
        #endregion

        #region Get_All_Active_PTOS
        public async Task<PaginatedList<PTOInformation>> GetActivePTOs(PassInfoSearchParams searchParams)
        {
            //Get Existing PTO info based on search params
            IQueryable<PTOInformation> ptoInformation = this._context.PTOInformations
                                                             .Include(desc => desc.PTODescription)
                                                             .Where(p => p.IsActive && p.PTODescription != null && p.PTODescription.Any(o=>o.SelectedLanguage == searchParams.Lang));

            //This is used to create a PaginatedList class that uses Skip and Take statements to filter data on the server instead of always retrieving all rows of the table
            var filteredResult = await PaginatedList<PTOInformation>.CreateAsync(ptoInformation.AsNoTracking(), searchParams.PageIndex, searchParams.PageSize);
            return filteredResult;
        }
        #endregion

        #region Get_Active_PTO
        public async Task<PTOInformation> GetActivePTO(int id)
        {
            //Get Existing PTO info based on PTO id and selected lang
            PTOInformation ptoInformation = await this._context.PTOInformations
                                           .Include(desc => desc.PTODescription)
                                           .Where(p => p.IsActive && p.Id == id).FirstOrDefaultAsync();

            return ptoInformation;
        }
        #endregion

        #region Update_PTO
        public async void UpdatePTO(PTOInformation ptoInformation)
        {
            this.Update(ptoInformation);
            await this._unitOfWork.CompleteAsync();
        }
        #endregion

    }

}

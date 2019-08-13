using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.DAL.Contexts;
using Hiroshima.Maas.DAL.Interfaces;
using Hiroshima.Maas.DL.Entities.LanguageConfigurationModel;
using Hiroshima.Maas.DL.Entities.PTOInformationModel;
using Hiroshima.Maas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.DAL.Repositories
{
    public class PTODescriptionRepository : BaseRepository<PTODescription>, IPTODescriptionRepository
    {
        public PTODescriptionRepository(HiroshimaMaaSDBContext context, IMessageHandler messageHandler, IUnitOfWork unitOfWork) : base(context, messageHandler, unitOfWork) { }

        #region Update_PTO_Desc
        public async void UpdatePTODesc(PTODescription ptoDesc)
        {
            this.Update(ptoDesc);
            await this._unitOfWork.CompleteAsync();
        }
        #endregion

        #region Add_PTO_Desc
        public async void AddPTODesc(PTODescription ptoDesc)
        {
            this.Add(ptoDesc);
            await this._unitOfWork.CompleteAsync();
        }
        #endregion

        #region Get_PTO_Desc
        public async Task<IEnumerable<PTODescription>> GetActivePTODescription(int id)
        {
            var ptoDescriptions = await this._context.PTOInformations
                                                         .Include(desc => desc.PTODescription)
                                                         .Where(p => p.Id == id && p.IsActive)
                                                         .SelectMany(p => p.PTODescription).ToListAsync();
            return ptoDescriptions;
        }
        #endregion

        #region Get_Active_Languages 
        //Get all remaining language which is not been added yet with PTO description
        public async Task<IEnumerable<Language>> GetAvailableLanguages(int[] langIds)
        {
            //Get all active languages from selected PTO description ids
            IEnumerable<Language> availableLangs = await this._context.Languages.Where(q => !langIds.Contains(q.Id) && q.IsActive).ToListAsync();
            return availableLangs;
        }
        #endregion

    }
}

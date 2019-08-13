using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.DAL.Contexts;
using Hiroshima.Maas.DAL.Interfaces;
using Hiroshima.Maas.DL.Entities.LanguageConfigurationModel;
using Hiroshima.Maas.DL.Entities.PassInformationModel;
using Hiroshima.Maas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.DAL.Repositories
{
    public class PassDescriptionRepository: BaseRepository<PassDescription>, IPassDescriptionRepository
    {
        public PassDescriptionRepository(HiroshimaMaaSDBContext context, IMessageHandler messageHandler, IUnitOfWork unitOfWork) : base(context, messageHandler, unitOfWork) { }

        #region Update_Pass_Desc
        public async void UpdatePassDesc(PassDescription passDesc)
        {
            this.Update(passDesc);
            await this._unitOfWork.CompleteAsync();
        }
        #endregion

        #region Add_Pass_Desc
        public async void AddPassDesc(PassDescription passDesc)
        {
            this.Add(passDesc);
            await this._unitOfWork.CompleteAsync();
        }
        #endregion

        #region Get_Pass_Desc
        public async Task<IEnumerable<PassDescription>> GetActivePassDescription(int id)
        {
            var passDescriptions = await this._context.PassInformations
                                                         .Include(desc => desc.PassDescription)
                                                         .Where(p => p.Id == id && p.IsActive)
                                                         .SelectMany(p => p.PassDescription).ToListAsync();
            return passDescriptions;
        }
        #endregion

        #region Get_Active_Languages 
        //Get all remaining language which is not been added yet with Pass description
        public async Task<IEnumerable<Language>> GetAvailableLanguages(int[] langIds)
        {
            //Get all active languages from selected Pass description ids
            IEnumerable<Language> availableLangs = await this._context.Languages.Where(q => !langIds.Contains(q.Id) && q.IsActive).ToListAsync();
            return availableLangs;
        }
        #endregion
    }
}

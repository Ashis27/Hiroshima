using Hiroshima.Maas.DL.Entities.LanguageConfigurationModel;
using Hiroshima.Maas.DL.Entities.PassInformationModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.DAL.Interfaces
{
    public interface IPassDescriptionRepository
    {
        void UpdatePassDesc(PassDescription passDesc);
        void AddPassDesc(PassDescription passDesc);
        Task<IEnumerable<PassDescription>> GetActivePassDescription(int id);
        Task<IEnumerable<Language>> GetAvailableLanguages(int[] langIds);
    }
}

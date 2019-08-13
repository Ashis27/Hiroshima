using Hiroshima.Maas.DL.Entities.LanguageConfigurationModel;
using Hiroshima.Maas.DL.Entities.PTOInformationModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.DAL.Interfaces
{
    public interface IPTODescriptionRepository
    {
        void UpdatePTODesc(PTODescription ptoDesc);
        void AddPTODesc(PTODescription ptoDesc);
        Task<IEnumerable<PTODescription>> GetActivePTODescription(int id);
        Task<IEnumerable<Language>> GetAvailableLanguages(int[] langIds);
    }
}

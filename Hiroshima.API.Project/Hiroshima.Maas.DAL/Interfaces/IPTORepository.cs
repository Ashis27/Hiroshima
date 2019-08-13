using Hiroshima.Maas.DL.Entities.LanguageConfigurationModel;
using Hiroshima.Maas.DL.Entities.PTOInformationModel;
using Hiroshima.Maas.DL.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.DAL.Interfaces
{
    public interface IPTORepository
    {
        void CreatePTO(PTOInformation ptoInformation);
        Task<PaginatedList<PTOInformation>> GetActivePTOs(PassInfoSearchParams searchParams);
        Task<PTOInformation> GetActivePTO( int id);
        void UpdatePTO(PTOInformation ptoInformation);
        //void DeletePTO(PTOInformation ptoInfo);
    }
}

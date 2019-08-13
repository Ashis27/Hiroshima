using Hiroshima.Maas.DL.Entities.PassInformationModel;
using Hiroshima.Maas.DL.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.DAL.Interfaces
{
    public interface IPassRepository
    {
        void CreatePass(PassInformation passInformation);
        Task<PaginatedList<PassInformation>> GetActivePasses(PassInfoSearchParams searchParams);
        Task<PassInformation> GetActivePass(int id);
        void UpdatePass(PassInformation passInformation);
        //void DeletePass(int id);
    }
}

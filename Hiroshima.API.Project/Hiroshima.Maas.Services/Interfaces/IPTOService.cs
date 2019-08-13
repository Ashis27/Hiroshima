using Hiroshima.Maas.Services.RequestAndResponse;
using Hiroshima.Maas.Services.Utility.Helper;
using Hiroshima.Maas.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.Services.Interfaces
{
    public interface IPTOService
    {
        AdminResponse CreatePTO(PTOInformationViewModel ptoInformation);
        Task<PagedViewModelResult<PTOInformationViewModel>> GetAllActivePTOs(SearchParams searchParams);
        Task<PTOInformationViewModel> GetActivePTO(int lang, int id);
        Task<AdminResponse> UpdatePTO(PTOInformationViewModel ptoInformation);
        Task<IEnumerable<LanguageViewModel>> GetAvailableLanguages(int id);
        Task<AdminResponse> DeletePTO(int id);
    }
}

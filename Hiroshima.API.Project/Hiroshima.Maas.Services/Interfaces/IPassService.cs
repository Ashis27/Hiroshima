using Hiroshima.Maas.Services.RequestAndResponse;
using Hiroshima.Maas.Services.Utility.Helper;
using Hiroshima.Maas.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.Services.Interfaces
{
    public interface IPassService
    {
        AdminResponse CreatePass(PassInformationViewModel passInformation);
        Task<PagedViewModelResult<PassInformationViewModel>> GetActivePasses(SearchParams searchParams);
        Task<PassInformationViewModel> GetActivePass(int lang, int id);
        Task<AdminResponse> UpdatePass(PassInformationViewModel passInformation);
        Task<IEnumerable<LanguageViewModel>> GetAvailableLanguages(int id);
        Task<AdminResponse> DeletePass(int id);
    }
}

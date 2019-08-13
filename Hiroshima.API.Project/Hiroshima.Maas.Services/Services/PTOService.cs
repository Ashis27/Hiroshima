using AutoMapper;
using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.Common.Infrastructure.Logger;
using Hiroshima.Maas.DAL.Interfaces;
using Hiroshima.Maas.DL.Entities.LanguageConfigurationModel;
using Hiroshima.Maas.DL.Entities.PTOInformationModel;
using Hiroshima.Maas.DL.Entities.PTOInformationModel;
using Hiroshima.Maas.DL.Utility;
using Hiroshima.Maas.Services.Interfaces;
using Hiroshima.Maas.Services.RequestAndResponse;
using Hiroshima.Maas.Services.Utility.Helper;
using Hiroshima.Maas.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.Services.Services
{
    public class PTOService : BaseService, IPTOService
    {
        private readonly IPTORepository _ptoRepository;
        private readonly IPTODescriptionRepository _ptoDescriptionRepository;
        private readonly IPassActivePTOMapper _passActivePTOMapper;
        public PTOService(IPTORepository ptoRepository, IPTODescriptionRepository ptoDescriptionRepository, IPassActivePTOMapper passActivePTOMapper, IJwtFactory jwtFactory, IMessageHandler messageHandler, ILoggerManager logger, IMapper mapper) : base(messageHandler, mapper, logger, jwtFactory)
        {
            _ptoRepository = ptoRepository;
            _ptoDescriptionRepository = ptoDescriptionRepository;
            _passActivePTOMapper = passActivePTOMapper;
        }

        #region Create_New_PTO
        public AdminResponse CreatePTO(PTOInformationViewModel ptoInformation)
        {
            _logger.LogInfo("Trying to add a new PTO");
            try
            {
                if (ptoInformation.PTODescription.Count() == 0)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidPTODescription)));
                PTOInformation ptoInfo = _mapper.Map<PTOInformation>(ptoInformation);
                _ptoRepository.CreatePTO(ptoInfo);
                _logger.LogInfo("Successfully created a new PTO");
                AdminResponse response = new AdminResponse(true, string.Format(_messageHandler.GetSuccessMessage(SuccessMessagesEnum.SuccessfullySaved)));
                response.PTOInformation = ptoInformation;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new AdminResponse(false, ex.Message);
            }
        }
        #endregion

        #region Get_All_Active_PTOS
        public async Task<PagedViewModelResult<PTOInformationViewModel>> GetAllActivePTOs(SearchParams searchParams)
        {
            _logger.LogInfo("Trying to get all active PTOs on language " + searchParams.Lang);
            try
            {
                PagedViewModelResult<PTOInformationViewModel> ptoInformation = _mapper.Map<PaginatedList<PTOInformation>, PagedViewModelResult<PTOInformationViewModel>>(await _ptoRepository.GetActivePTOs(_mapper.Map<PassInfoSearchParams>(searchParams)));

                //Get all PTOs which PTO descriptions are available
                var activePTOs = new List<PTOInformationViewModel>();
                foreach (var item in ptoInformation.Items)
                {
                    //Filter based on language from the paginated result.
                    item.PTODescription = item?.PTODescription?.Where(p => p.SelectedLanguage == searchParams.Lang);
                    if (item.PTODescription.Count() > 0)
                        activePTOs.Add(item);
                }
                ptoInformation.Items = activePTOs;
                _logger.LogInfo("Retrieved all active PTOs on language " + searchParams.Lang);
                return ptoInformation;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return null;
            }
        }
        #endregion

        #region Get_Active_PTO
        public async Task<PTOInformationViewModel> GetActivePTO(int lang, int id)
        {
            _logger.LogInfo("Trying to get active PTOs with id " + id + " on language " + lang);
            try
            {
                //Get PTO information based on id and filter PTO description by selected language
                PTOInformationViewModel ptoInformation = _mapper.Map<PTOInformation, PTOInformationViewModel>(await _ptoRepository.GetActivePTO(id));

                //Filter based on language from the paginated result.
                ptoInformation?.PTODescription?.FirstOrDefault(p => p.SelectedLanguage == lang);
                _logger.LogInfo("Retrieved active PTO with id " + id + " on language " + lang);
                return ptoInformation;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return null;
            }
        }
        #endregion

        #region Update_PTO
        public async Task<AdminResponse> UpdatePTO(PTOInformationViewModel ptoInformation)
        {
            _logger.LogInfo("Trying to update existing PTO information");
            try
            {
                //Get Existing PTO info based on PTO
                PTOInformation ptoInfo = await this._ptoRepository.GetActivePTO(ptoInformation.Id);

                //If null then throw exception with invalid information
                if (ptoInfo == null)
                    return new AdminResponse(false, string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidPTOInformation)));

                PTODescription ptoDesc = new PTODescription();
                //if null then update only PTO information otherwise update PTO description
                if (ptoInformation.PTODescription != null && ptoInformation.PTODescription.Count() > 0)
                {
                    //Get Existing PTO desc based on PTO desc id
                    ptoDesc = ptoInfo.PTODescription?.Where(p => p.Id == ptoInformation.PTODescription.FirstOrDefault().Id && p.SelectedLanguage == ptoInformation.PTODescription.FirstOrDefault().SelectedLanguage && p.IsActive).FirstOrDefault();

                    ////mapped view model to entity
                    ptoInfo = _mapper.Map<PTOInformation>(ptoInformation);

                    //If null then add new PTO desc with selected language otherwise update existing PTO desc.
                    if (ptoDesc != null)
                    {
                        //update existing entity from edited by user
                        ptoDesc = ptoInfo.PTODescription.FirstOrDefault();

                        //Updated PTO information
                        _ptoRepository.UpdatePTO(ptoInfo);
                        _logger.LogInfo("Successfully updated PTO information");

                        //Updated PTO description
                        _ptoDescriptionRepository.UpdatePTODesc(ptoDesc);
                        _logger.LogInfo("Successfully updated PTO Description information");
                    }
                    else
                    {
                        //Added new PTO description if new language selected by user
                        _ptoDescriptionRepository.AddPTODesc(ptoInfo.PTODescription.FirstOrDefault());
                        _logger.LogInfo("Successfully added PTO description information");
                    }
                }
                else
                    return new AdminResponse(false, string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidPTODescription)));

                AdminResponse response = new AdminResponse(true, string.Format(_messageHandler.GetSuccessMessage(SuccessMessagesEnum.SuccessfullySaved)));
                response.PTOInformation = ptoInformation;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new AdminResponse(false, ex.Message);
            }
        }
        #endregion

        #region Delete_PTO
        public async Task<AdminResponse> DeletePTO(int id = 0)
        {
            _logger.LogInfo("Trying to delete PTO with id " + id);
            try
            {
                //Get Existing PTO info based on PTO
                PTOInformation ptoInfo = await this._ptoRepository.GetActivePTO(id);

                //If null then throw exception with invalid information
                if (ptoInfo == null)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidPTOInformation)));

                //Soft deleted
                ptoInfo.IsActive = false;

                //Updated PTO information
                _ptoRepository.UpdatePTO(ptoInfo);
                _logger.LogInfo("PTO soft deleted with id " + id);

                //If selected PTO has already been assigned then remove all assigned PTOs with Pass information from mapper table
                _passActivePTOMapper.BulkDeletePTO(ptoInfo.Id);

                _logger.LogInfo("Removed all assigned PTOs with Pass information from mapper table with id " + id);
                return new AdminResponse(true, string.Format(_messageHandler.GetSuccessMessage(SuccessMessagesEnum.SuccessfullyDeleted)));
            }
            catch (Exception ex)
            {
                _logger.LogError("PTO deleted with id " + id);
                return new AdminResponse(false, ex.Message);
            }
        }
        #endregion

        #region Get_Remaing_Lanaguages
        public async Task<IEnumerable<LanguageViewModel>> GetAvailableLanguages(int id)
        {
            _logger.LogInfo("Trying to get all available languages which is not yet added in PTO description with id " + id);
            try
            {
                //Get all active PTO description by PTO infomation id
                IEnumerable<PTODescription> ptoDesc = await this._ptoDescriptionRepository.GetActivePTODescription(id);

                //If null then throw exception with invalid information
                if (ptoDesc == null || ptoDesc.Count() == 0)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidPTODescription)));

                //Get all active added language ids in PTO description
                var langIds = ptoDesc?.Select(p => p.SelectedLanguage)?.ToArray();
                if (langIds.Count() == 0)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidLanguageInformation)));

                IEnumerable<LanguageViewModel> languages = _mapper.Map<IEnumerable<Language>, IEnumerable<LanguageViewModel>>(await _ptoDescriptionRepository.GetAvailableLanguages(langIds));
                _logger.LogInfo("Retrieved all available languages which is not yet added in PTO description with id " + id);
                return languages;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return null;
            }
        }
        #endregion
    }
}

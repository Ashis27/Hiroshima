using AutoMapper;
using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.Common.Infrastructure.Logger;
using Hiroshima.Maas.DAL.Interfaces;
using Hiroshima.Maas.DL.Entities.LanguageConfigurationModel;
using Hiroshima.Maas.DL.Entities.PassInformationModel;
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
    public class PassService : BaseService, IPassService
    {
        private readonly IPassRepository _passRepository;
        private readonly IPassDescriptionRepository _passDescriptionRepository;
        private readonly IPassActivePTOMapper _passActivePTOMapper;
        public PassService(IPassRepository passRepository, IPassDescriptionRepository passDescriptionRepository, IPassActivePTOMapper passActivePTOMapper, IJwtFactory jwtFactory, IMessageHandler messageHandler, ILoggerManager logger, IMapper mapper) : base(messageHandler, mapper, logger, jwtFactory)
        {
            _passRepository = passRepository;
            _passDescriptionRepository = passDescriptionRepository;
            _passActivePTOMapper = passActivePTOMapper;
        }

        #region Create_New_Pass
        public AdminResponse CreatePass(PassInformationViewModel passInformation)
        {
            _logger.LogInfo("Trying to add a new pass");
            try
            {
                //Pass remain active whole day(ex: 10/29/2019 23:59:59)
                passInformation.PassExpiredDate = passInformation.PassExpiredDate.AddDays(1).AddSeconds(-1);
                PassInformation passInfo = _mapper.Map<PassInformationViewModel, PassInformation>(passInformation);
                _passRepository.CreatePass(passInfo);
                _logger.LogInfo("Successfully created a new pass");
                AdminResponse response = new AdminResponse(true, string.Format(_messageHandler.GetSuccessMessage(SuccessMessagesEnum.SuccessfullySaved)));
                response.PassInformation = passInformation;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new AdminResponse(false, ex.Message);
            }
        }
        #endregion

        #region Get_Active_Passes
        public async Task<PagedViewModelResult<PassInformationViewModel>> GetActivePasses(SearchParams searchParams)
        {
            _logger.LogInfo("Trying to get all active passes on language " + searchParams.Lang);
            try
            {
                PagedViewModelResult<PassInformationViewModel> passInformation = _mapper.Map<PaginatedList<PassInformation>, PagedViewModelResult<PassInformationViewModel>>(await _passRepository.GetActivePasses(_mapper.Map<PassInfoSearchParams>(searchParams)));

                //Get all passes which PTOs and pass descriptions are available
                var activePass = new List<PassInformationViewModel>();

                
                foreach (var item in passInformation.Items)
                {
                    //Filter based on language from the paginated result.
                    item.PassDescription = item?.PassDescription?.Where(p => p.SelectedLanguage == searchParams.Lang);
                    
                    //Filter based on language from the list of active pass ptos mapper.
                    //Get all which PTOs are available
                    var activePTOs = new List<PassActivePTOViewModel>();
                    foreach (var element in item.PassActivePTOs)
                    {
                        element.PTOInformation.PTODescription = element?.PTOInformation?.PTODescription?.Where(p => p.SelectedLanguage == searchParams.Lang);
                        if (element.PTOInformation.PTODescription.Count() > 0)
                            activePTOs.Add(element);
                    }
                    item.PassActivePTOs = activePTOs;
                    if (item.PassActivePTOs.Count() > 0 && item.PassDescription.Count() > 0)
                        activePass.Add(item);
                }
                passInformation.Items = activePass;
                _logger.LogInfo("Retrieved all active passes on language " + searchParams.Lang);
                return passInformation;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return null;
            }
        }
        #endregion

        #region Get_Active_Pass
        public async Task<PassInformationViewModel> GetActivePass(int lang, int id)
        {
            _logger.LogInfo("Trying to get active passes with id " + id + " on language " + lang);
            try
            {
                PassInformationViewModel passInformation = _mapper.Map<PassInformation, PassInformationViewModel>(await _passRepository.GetActivePass(id));
                if (passInformation == null)
                {
                    _logger.LogInfo("Pass information not available with id " + id + " on language " + lang);
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidPassInformation)));

                }//Filter based on language from the paginated result.
                passInformation?.PassDescription?.FirstOrDefault(p => p.SelectedLanguage == lang);
                _logger.LogInfo("Retrieved active pass with id " + id + " on language " + lang);
                return passInformation;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return null;
            }
        }
        #endregion

        #region Update_Pass
        public async Task<AdminResponse> UpdatePass(PassInformationViewModel passInformation)
        {
            _logger.LogInfo("Trying to update existing pass information");
            try
            {
                _logger.LogInfo("Trying to update existing pass information");
                //Get Existing Pass info based on Pass
                PassInformation passInfo = await this._passRepository.GetActivePass(passInformation.Id);

                //If null then throw exception with invalid information
                if (passInfo == null)
                    return new AdminResponse(false, string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidPassDescription)));

                PassDescription passDesc = new PassDescription();
                //if null then update only Pass information otherwise update Pass description
                if (passInformation.PassDescription != null && passInformation.PassDescription.Count() > 0)
                {
                    //Get Existing Pass desc based on Pass desc id
                    passDesc = passInfo.PassDescription?.Where(p => p.Id == passInformation.PassDescription.FirstOrDefault().Id && p.SelectedLanguage == passInformation.PassDescription.FirstOrDefault().SelectedLanguage && p.IsActive).FirstOrDefault();

                    //Pass remain active whole day(ex: 10/29/2019 23:59:59)
                    passInformation.PassExpiredDate = passInformation.PassExpiredDate.AddDays(1).AddSeconds(-1);

                    ////mapped view model to entity
                    passInfo = _mapper.Map<PassInformation>(passInformation);

                    //If null then add new Pass desc with selected language otherwise update existing Pass desc.
                    if (passDesc != null)
                    {
                        //update existing entity from edited by user
                        passDesc = passInfo.PassDescription.FirstOrDefault();

                        //Updated Pass information
                        _passRepository.UpdatePass(passInfo);
                        _logger.LogInfo("Successfully updated Pass information");

                        //Updated pass description
                        _passDescriptionRepository.UpdatePassDesc(passDesc);
                        _logger.LogInfo("Successfully updated Pass Description information");

                        //If selected Pass has already been added then remove all assigned pass along with PTO information from mapper table
                        _passActivePTOMapper.BulkDeletePass(passInfo.Id);
                        _logger.LogInfo("Removed all assigned PTOs with Pass information from mapper table");

                        //Add new updated PTOs for a pass information in Mapper table
                        _passActivePTOMapper.BulkAddPTO(passInfo.PassActivePTOs.ToArray());
                        _logger.LogInfo("Successfully added PTOs information in mapper table");
                    }
                    else
                    {
                        //Added new Pass description if new language selected by user
                        _passDescriptionRepository.AddPassDesc(passInfo.PassDescription.FirstOrDefault());
                        _logger.LogInfo("Successfully added Pass description information");
                    }
                }
                else
                    return new AdminResponse(false, string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidPassDescription)));

                AdminResponse response = new AdminResponse(true, "Successfully saved");
                response.PassInformation = passInformation;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new AdminResponse(false, ex.Message);
            }
        }
        #endregion

        #region Delete_Pass
        public async Task<AdminResponse> DeletePass(int id = 0)
        {
            _logger.LogInfo("Trying to delete pass with id " + id);
            try
            {
                //Get Existing Pass info based on Pass
                PassInformation passInfo = await this._passRepository.GetActivePass(id);
                //If null then throw exception with invalid information
                if (passInfo == null)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidPassInformation)));
                //Soft deleted
                passInfo.IsActive = false;
                //Updated Pass information
                _passRepository.UpdatePass(passInfo);
                _logger.LogInfo("Pass soft deleted with id " + id);
                return new AdminResponse(true, "Successfully deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError("Pass deleted with id " + id);
                return new AdminResponse(false, ex.Message);
            }
        }
        #endregion

        #region Get_Remaing_Lanaguages
        public async Task<IEnumerable<LanguageViewModel>> GetAvailableLanguages(int id)
        {
            _logger.LogInfo("Trying to get all available languages which is not yet added in Pass description with id " + id);
            try
            {
                //Get all active Pass description by Pass infomation id
                IEnumerable<PassDescription> passDesc = await this._passDescriptionRepository.GetActivePassDescription(id);

                //If null then throw exception with invalid information
                if (passDesc == null || passDesc.Count() == 0)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidPassDescription)));

                //Get all active added language ids in Pass description
                var langIds = passDesc?.Select(p => p.SelectedLanguage)?.ToArray();
                if (langIds.Count() == 0)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidLanguageInformation)));

                IEnumerable<LanguageViewModel> languages = _mapper.Map<IEnumerable<Language>, IEnumerable<LanguageViewModel>>(await _passDescriptionRepository.GetAvailableLanguages(langIds));
                _logger.LogInfo("Retrieved all available languages which is not yet added in Pass description with id " + id);
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

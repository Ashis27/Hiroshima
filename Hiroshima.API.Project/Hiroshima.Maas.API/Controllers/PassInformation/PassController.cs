using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Hiroshima.Maas.Common.Infrastructure.Logger;
using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.Services.Interfaces;
using Hiroshima.Maas.Services.RequestAndResponse;
using Hiroshima.Maas.Services.Utility.Helper;
using Hiroshima.Maas.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hiroshima.Maas.API.Controllers.PassInformation
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PassController : BaseAPIController
    {
        private readonly IPassService _passService;
        public PassController(IPassService passService, IMessageHandler messageHandler, IHttpContextAccessorExtension _httpContextAccessorExtension, ILoggerManager logger) : base(_httpContextAccessorExtension, messageHandler, logger)
        {
            _passService = passService;
        }


        #region Create Pass
        /// <summary>
        /// To create a new pass
        /// </summary>
        /// <param name="passInformation"></param>
        /// <returns></returns>
        [HttpPost("CreatePass/{passInformation}")]
        public ActionResult<AdminResponse> CreateNewPass([FromBody] PassInformationViewModel passInformation)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), "", ModelState.Values.First().Errors.First().ErrorMessage));
            var response = _passService.CreatePass(passInformation);
            return Ok(response);
        }
        #endregion

        #region Update Pass
        /// <summary>
        /// To get updated pass with additional information
        /// </summary>
        /// <param name="passInformation"></param>
        /// <returns></returns>
        [HttpPut("UpdatePass/{passInformation}")]
        public async Task<ActionResult<AdminResponse>> UpdatePassInformation([FromBody] PassInformationViewModel passInformation)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), "", ModelState.Values.First().Errors.First().ErrorMessage));
            if (passInformation.Id <= 0)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.NotValidInformations)));
            var response = await _passService.UpdatePass(passInformation);
            return Ok(response);
        }
        #endregion

        #region Get Active Passes
        /// <summary>
        /// This is used to get all active passes along with additional information based on selected language
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpPost("GetAllPass/{searchParams}")]
        public async Task<ActionResult<PagedViewModelResult<PassInformationViewModel>>> GetAllActivePasses([FromBody] SearchParams searchParams)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), "", ModelState.Values.First().Errors.First().ErrorMessage));
            var response = await _passService.GetActivePasses(searchParams);
            return Ok(response);
        }
        #endregion

        #region Get Active Pass
        /// <summary>
        /// This is used to get active pass by selected id along with additional information based on selected language
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetPass/{id}/Onlanguage/{lang}")]
        public async Task<ActionResult<PassInformationViewModel>> GetActivePass(int lang = 0, int id = 0)
        {
            if (lang <= 0 && id <= 0)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), " Please try again"));
            var response = await _passService.GetActivePass(lang, id);
            return Ok(response);
        }
        #endregion

        #region Delete Pass
        /// <summary>
        /// To get deleted pass by selected id
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeletePass/{id}")]
        public async Task<ActionResult<AdminResponse>> DeletePass(int id = 0)
        {
            if (id <= 0)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), " No such pass available"));
            var response = await _passService.DeletePass(id);
            return Ok(response);
        }
        #endregion

        #region Get_Remaing_Lanaguages
        /// <summary>
        /// To get all available languages which is not yet added in PTO description
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.SuperAdmin + "," + Role.Admin)]
        [HttpGet("PassInformation/{id}/AvailableLanguage")]
        public async Task<ActionResult<IEnumerable<LanguageViewModel>>> GetAvailableLanguages(int id = 0)
        {
            if (id <= 0)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), " Please try again"));
            var response = await _passService.GetAvailableLanguages(id);
            return Ok(response);
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hiroshima.Maas.Common.Infrastructure.Logger;
using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.Services.Interfaces;
using Hiroshima.Maas.Services.RequestAndResponse;
using Hiroshima.Maas.Services.Utility.Helper;
using Hiroshima.Maas.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hiroshima.Maas.API.Controllers.PTOInformation
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PTOController : BaseAPIController
    {
        private readonly IPTOService _ptoService;
        public PTOController(IPTOService ptoService, IMessageHandler messageHandler, IHttpContextAccessorExtension _httpContextAccessorExtension, ILoggerManager logger) : base(_httpContextAccessorExtension, messageHandler, logger)
        {
            _ptoService = ptoService;
        }

        #region Create PTO
        /// <summary>
        /// To create a new PTO
        /// </summary>
        /// <param name="ptoInformation"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.SuperAdmin + "," + Role.Admin)]
        [HttpPost("CreatePTO/{ptoInformation}")]
        public ActionResult<AdminResponse> CreateNewPTO([FromBody] PTOInformationViewModel ptoInformation)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), "", ModelState.Values.First().Errors.First().ErrorMessage));
            var response = _ptoService.CreatePTO(ptoInformation);
            return Ok(response);
        }
        #endregion

        #region Update PTO
        /// <summary>
        /// To get updated PTO with additional information
        /// </summary>
        /// <param name="ptoInformation"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.SuperAdmin + "," + Role.Admin)]
        [HttpPut("UpdatePTO/{ptoInformation}")]
        public async Task<ActionResult<AdminResponse>> UpdatePTOInformation([FromBody] PTOInformationViewModel ptoInformation)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), "", ModelState.Values.First().Errors.First().ErrorMessage));
            if (ptoInformation.Id <= 0)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.NotValidInformations)));
            var response = await _ptoService.UpdatePTO(ptoInformation);
            return Ok(response);
        }
        #endregion

        #region Get Active PTOs
        /// <summary>
        /// This is used to get all active PTOs along with additional information based on selected language
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpPost("GetAllPTO/{searchParams}")]
        public async Task<ActionResult<PagedViewModelResult<PTOInformationViewModel>>> GetAllActivePTOs([FromBody] SearchParams searchParams)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), "", ModelState.Values.First().Errors.First().ErrorMessage));
            var response = await _ptoService.GetAllActivePTOs(searchParams);
            return Ok(response);
        }
        #endregion

        #region Get Active PTO
        /// <summary>
        /// This is used to get active PTO by selected id along with additional information based on selected language
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetPTO/{id}/Onlanguage/{lang}")]
        public async Task<ActionResult<PTOInformationViewModel>> GetActivePTO(int lang = 0, int id = 0)
        {
            if (lang <= 0 && id <= 0)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), " Please try again"));
            var response = await _ptoService.GetActivePTO(lang, id);
            return Ok(response);
        }
        #endregion

        #region Delete PTO
        /// <summary>
        /// To get deleted PTO by selected id on selected language
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.SuperAdmin + "," + Role.Admin)]
        [HttpDelete("DeletePTO/{id}")]
        public async Task<ActionResult<AdminResponse>> DeletePTO(int id = 0)
        {
            if (id <= 0)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), " Please try again"));
            var response = await _ptoService.DeletePTO(id);
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
        [HttpGet("PTOInformation/{id}/AvailableLanguage")]
        public async Task<ActionResult<IEnumerable<LanguageViewModel>>> GetAvailableLanguages(int id = 0)
        {
            if (id <= 0)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), " Please try again"));
            var response = await _ptoService.GetAvailableLanguages(id);
            return Ok(response);
        }
        #endregion
    }
}
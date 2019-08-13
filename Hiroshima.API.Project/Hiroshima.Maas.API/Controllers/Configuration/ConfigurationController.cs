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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hiroshima.Maas.API.Controllers.Configuration
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ConfigurationController : BaseAPIController
    {
        private readonly IConfigurationService _configurationService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ConfigurationController(IHostingEnvironment hostingEnvironment, IConfigurationService configurationService, IMessageHandler messageHandler, IHttpContextAccessorExtension _httpContextAccessorExtension, ILoggerManager logger) : base(_httpContextAccessorExtension, messageHandler, logger)
        {
            _configurationService = configurationService;
            _hostingEnvironment = hostingEnvironment;
        }

        #region Get_App_Configuration
        /// <summary>
        /// To get app configuration along with additional information by device type ex: android || iOS
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        [HttpGet("AppConfiguration/{deviceType}")]
        public async Task<ActionResult<AppConfigurationViewModel>> GetAppConfiguration(string deviceType)
        {
            if (String.IsNullOrEmpty(deviceType))
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), " Please try again"));
            var response = await _configurationService.GetAppConfiguration(deviceType);
            return Ok(response);
        }
        #endregion

        #region Get_Active_Languages
        /// <summary>
        /// To get active languages
        /// </summary>
        /// <returns></returns>
        [HttpPost("ActiveLanguages/{searchParams}")]
        public async Task<ActionResult<PagedViewModelResult<LanguageViewModel>>> GetActiveLanguages([FromBody] SearchParams searchParams)
        {
            var response = await _configurationService.GetActiveLanguages(searchParams);
            return Ok(response);
        }
        #endregion

        #region Get_Active_Curriencies
        /// <summary>
        /// To get active curriencies
        /// </summary>
        /// <returns></returns>
        [HttpPost("ActiveCurriencies/{searchParams}")]
        public async Task<ActionResult<PagedViewModelResult<CurrencyViewModel>>> GetActiveCurriencies([FromBody] SearchParams searchParams)
        {
            var response = await _configurationService.GetActiveCurriencies(searchParams);
            return Ok(response);
        }
        #endregion

        #region Get_Booking_Information
        /// <summary>
        /// To get all booking information based on search params
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        [HttpPost("BookingInformation/{searchParams}")]
        public async Task<ActionResult<PagedViewModelResult<BookedPassInformationViewModel>>> GetBookingInformation([FromBody] SearchParams searchParams)
        {
            if (searchParams.StartDateAndTime == null)
                searchParams.StartDateAndTime = DateTime.Today;
            var response = await _configurationService.GetBookingInformation(searchParams);
            return Ok(response);
        }
        #endregion

        #region Get_Language_Contents
        /// <summary>
        /// To get all the static label content based on selected language
        /// </summary>
        /// <returns></returns>
        [HttpGet("LanguageContent/OnLanguage/{lang}")]
        public ActionResult<JsonResult> GetLanguageContent(string lang)
        {
            try
            {
                string contentRootPath = _hostingEnvironment.ContentRootPath;
                var jsonlanguageInfoParseData = System.IO.File.ReadAllText(contentRootPath + "/LanguageConfig/Hiroshima-" + lang + ".json");
                var languageInfo = JsonConvert.DeserializeObject(jsonlanguageInfoParseData.ToString());
                if (languageInfo != null)
                    return Ok(languageInfo);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        #endregion

        #region Get_Traveller_feedback
        /// <summary>
        /// To get all active traveeler feedback information with pagination
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        [HttpPost("GetFeedback/{searchParams}")]
        public async Task<ActionResult<PagedViewModelResult<TravellerFeedbackViewModel>>> GetTravellerFeedback([FromBody] SearchParams searchParams)
        {
            var response = await _configurationService.GetTravellerFeedback(searchParams);
            return Ok(response);
        }
        #endregion

        #region Update_QR_Code_configuration
        /// <summary>
        /// To save QRCode regerate time configuration
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        [HttpPut("UpdateQRConfiguration/{configInfo}")]
        public ActionResult<QRCodeConfigResponse> UpdateQRCodeRegerateTime([FromBody] QRCodeConfigurationViewModel configInfo)
        {
            if (configInfo.Id <= 0)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), " Invalid configuration id"));
            var response = _configurationService.UpdateQRCodeRegerateTime(configInfo);
            return Ok(response);
        }
        #endregion

        #region Get_QR_Code_configuration
        /// <summary>
        /// To get QRCode regerate time configuration information
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetQRConfigurationInfo")]
        public ActionResult<QRCodeConfigurationViewModel> GetQRConfiguration()
        {
            var response = _configurationService.GetQRConfiguration();
            return Ok(response);
        }
        #endregion
    }
}
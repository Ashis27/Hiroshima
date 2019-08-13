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
using Microsoft.AspNetCore.Mvc;

namespace Hiroshima.Maas.API.Controllers.Traveller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TravellerController : BaseAPIController
    {
        private readonly ITravellerService _travellerService;
        public TravellerController(ITravellerService travellerService, IMessageHandler messageHandler, IHttpContextAccessorExtension _httpContextAccessorExtension, ILoggerManager logger) : base(_httpContextAccessorExtension, messageHandler, logger)
        {
            _travellerService = travellerService;
        }

        #region Get_Available_Passes
        /// <summary>
        /// To get all in available passes for selected decive id with pagination
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        [HttpPost("GetAvailablePass/{searchParams}")]
        public async Task<ActionResult<PagedViewModelResult<BookedPassInformationViewModel>>> GetAvailablePasses([FromBody] SearchParams searchParams)
        {
            if (String.IsNullOrEmpty(searchParams.DeviceId) || searchParams.Lang <= 0)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), " Please try again"));
            var response = await _travellerService.GetAvailablePasses(searchParams);
            return Ok(response);
        }
        #endregion

        #region Get_In_Use_Passes
        /// <summary>
        /// To get all in used passes for selected decive id with pagination
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        [HttpPost("GetInUsePass/{searchParams}")]
        public async Task<ActionResult<PagedViewModelResult<BookedPassInformationViewModel>>> GetInUsePasses([FromBody] SearchParams searchParams)
        {
            if (String.IsNullOrEmpty(searchParams.DeviceId) || searchParams.Lang <= 0)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), " Please try again"));
            var response = await _travellerService.GetInUsePasses(searchParams);
            return Ok(response);
        }
        #endregion

        #region Get_Expired_Passes
        /// <summary>
        /// To get all in expired passes for selected decive id with pagination
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        [HttpPost("GetExpiredPass/{searchParams}")]
        public async Task<ActionResult<PagedViewModelResult<BookedPassInformationViewModel>>> GetExpiredPasses([FromBody] SearchParams searchParams)
        {
            if (String.IsNullOrEmpty(searchParams.DeviceId) || searchParams.Lang <= 0)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), " Please try again"));
            var response = await _travellerService.GetExpiredPasses(searchParams);
            return Ok(response);
        }
        #endregion

        #region Get_Booking_information
        /// <summary>
        /// To get selected booking information with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("BookingInfo/{uId}")]
        public async Task<ActionResult<BookedPassInformationViewModel>> GetBookingInformation(string uId)
        {
            if (String.IsNullOrEmpty(uId))
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), " Invalid booking id"));
            var response = await _travellerService.GetBookingInformation(uId);
            return Ok(response);
        }
        #endregion

        #region Submit_Traveller_Feedback
        /// <summary>
        /// To get submitted traveller feedback.
        /// </summary>
        /// <param name="feedbackData"></param>
        /// <returns></returns>
        [HttpPost("TravellerFeedback/{feedbackData}")]
        public ActionResult<TravellerResponse> SubmitFeedback([FromBody] TravellerFeedbackViewModel feedbackData)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), "", ModelState.Values.First().Errors.First().ErrorMessage));
            var response = _travellerService.SubmitFeedback(feedbackData);
            return Ok(response);
        }
        #endregion

        #region Activate_Pass
        /// <summary>
        /// To get activated selected pass
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        [HttpGet("ActivatePass/{uId}")]
        public async Task<ActionResult<TravellerResponse>> ActivatePass(string uId)
        {
            if (String.IsNullOrEmpty(uId))
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), " Invalid booking id"));
            var response = await _travellerService.ActivatePass(uId);
            return Ok(response);
        }
        #endregion

        #region Book_Pass
        /// <summary>
        /// Book Pass with selected pass information
        /// </summary>
        /// <param name="passInfo"></param>
        /// <returns></returns>
        [HttpPost("BookingInformation/{passInfo}")]
        public async Task<ActionResult<TravellerResponse>> BookPass([FromBody] BookedPassInformationViewModel bookingInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), "", ModelState.Values.First().Errors.First().ErrorMessage));
            var response = await _travellerService.BookPass(bookingInfo);
            return Ok(response);
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hiroshima.Maas.Common;
using Hiroshima.Maas.Common.Infrastructure.Logger;
using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.Services.Interfaces;
using Hiroshima.Maas.Services.RequestAndResponse;
using Hiroshima.Maas.Services.Utility.Helper;
using Hiroshima.Maas.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Hiroshima.Maas.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class AdminController : BaseAPIController
    {
        private readonly IAdminUserService _adminUserService;
        public AdminController(IAdminUserService adminUserService, IMessageHandler messageHandler, IHttpContextAccessorExtension _httpContextAccessorExtension, ILoggerManager logger) : base(_httpContextAccessorExtension, messageHandler, logger)
        {
            _adminUserService = adminUserService;
        }

        #region Authenticate
        /// <summary>
        /// Admin Authentication and JWT Token creation
        /// </summary>
        /// <param name="adminUserData"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<ActionResult<AdminResponse>> Authenticate([FromBody] AdminUserViewModel adminUserData)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), "", ModelState.Values.First().Errors.First().ErrorMessage));
            var response = await _adminUserService.Authenticate(adminUserData.UserName, adminUserData.Password);
            return Ok(response);
        }
        #endregion

        #region Register_NEC_Admin
        /// <summary>
        /// NEC admin registration
        /// </summary>
        /// <param name="adminUserData"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult<AdminUserViewModel> RegisterNECAdmin([FromBody] AdminUserViewModel adminUserData)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), "", ModelState.Values.First().Errors.First().ErrorMessage));
            if (adminUserData.Role != Role.NECAdmin)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.AuthNotValidInformations)));
            var users = _adminUserService.Register(adminUserData);
            return Ok(users);
        }
        #endregion

        #region Register_Super_Admin

        /// <summary>
        /// Super Admin registration by NEC Admin
        /// </summary>
        /// <param name="adminUserData"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.NECAdmin)]
        [HttpPost("NECAdmin/{necAdminId}/CreateSuperAdmin/{adminUserData}")]
        public ActionResult<AdminResponse> RegisterSuperAdmin([FromBody] AdminUserViewModel adminUserData, int necAdminId)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), "", ModelState.Values.First().Errors.First().ErrorMessage));
            int currentUserId = this.CurrentUserID();
            if (currentUserId == 0 || currentUserId != necAdminId)
                return Unauthorized();
            adminUserData.Role = Role.SuperAdmin;
            var adminUsers = _adminUserService.Register(adminUserData);
            return Ok(adminUsers);
        }
        #endregion

        #region Register_Admin
        /// <summary>
        /// Admin registration by Super Admin
        /// </summary>
        /// <param name="adminUserData"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.SuperAdmin)]
        [HttpPost("SuperAdmin/{superAdminId}/CreateAdmin/{adminUserData}")]
        public ActionResult<AdminResponse> RegisterAdmin([FromBody] AdminUserViewModel adminUserData, int superAdminId)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), "", ModelState.Values.First().Errors.First().ErrorMessage));
            int currentUserId = this.CurrentUserID();
            if (currentUserId == 0 || currentUserId != superAdminId)
                return Unauthorized();
            adminUserData.Role = Role.Admin;
            var users = _adminUserService.Register(adminUserData);
            return Ok(users);
        }
        #endregion

        #region Get_All_Super_Admins
        /// <summary>
        /// Get All active super admins which is registered by NEC admin
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.NECAdmin)]
        [HttpGet("NECAdmin/{adminId}/SuperAdmins")]
        public async Task<ActionResult<IEnumerable<AdminUserViewModel>>> GetAllSuperAdmins(int adminId)
        {
            int currentUserId = this.CurrentUserID();
            if (currentUserId == 0 || currentUserId != adminId)
                return Unauthorized();
            var adminUsers = await _adminUserService.GetActiveAdminUsers(currentUserId, Role.SuperAdmin.ToString());
            return Ok(adminUsers);
        }
        #endregion

        #region Get_All_Admins
        /// <summary>
        /// Get All active admins which is registered by super admin
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.SuperAdmin)]
        [HttpGet("SuperAdmin/{superAdminId}/Admins")]
        public async Task<ActionResult<IEnumerable<AdminUserViewModel>>> GetAllAdmins(int superAdminId)
        {
            int currentUserId = this.CurrentUserID();
            if (currentUserId == 0 || currentUserId != superAdminId)
                return Unauthorized();
            var adminUsers = await _adminUserService.GetActiveAdminUsers(currentUserId, Role.Admin.ToString());
            return Ok(adminUsers);
        }
        #endregion

        #region Delete_Admin
        /// <summary>
        /// To get soft deleted an admin by super admin
        /// </summary>
        /// <param name="superAdminId"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.SuperAdmin)]
        [HttpDelete("SuperAdmin/{superAdminId}/Admin/{adminId}")]
        public async Task<ActionResult<AdminResponse>> DeleteAdmin(int superAdminId, int adminId)
        {
            int currentUserId = this.CurrentUserID();
            if (currentUserId == 0 || currentUserId != superAdminId)
                return Unauthorized();
            var response = await _adminUserService.DeleteAdmin(adminId);
            return Ok(response);
        }
        #endregion

        #region Update_Admin_User_By_NEC_Admin
        /// <summary>
        /// To update admin user information by NEC admin
        /// </summary>
        /// <param name="adminUserData"></param>
        /// <param name="necAdminId"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.NECAdmin)]
        [HttpPost("NECAdmin/{necAdminId}/UpdateSuperAdminInfo/{adminUserData}")]
        public ActionResult<AdminResponse> UpdateSuperAdmin([FromBody] AdminUserViewModel adminUserData, int necAdminId)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), "", ModelState.Values.First().Errors.First().ErrorMessage));
            int currentUserId = this.CurrentUserID();
            if (currentUserId == 0 || currentUserId != necAdminId)
                return Unauthorized();
            if (adminUserData.Id <= 0)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), "", "Invalid user"));

            adminUserData.Role = Role.SuperAdmin;
            var adminUsers = _adminUserService.UpdateAdmin(adminUserData);
            return Ok(adminUsers);
        }
        #endregion
        #region Update_Admin_User_By_Super_Admin
        /// <summary>
        /// To update admin user information by super admin
        /// </summary>
        /// <param name="adminUserData"></param>
        /// <param name="necAdminId"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.SuperAdmin)]
        [HttpPost("SuperAdmin/{adminId}/UpdateAdminInfo/{adminUserData}")]
        public async Task<ActionResult<AdminResponse>> UpdateAdmin([FromBody] AdminUserViewModel adminUserData, int adminId)
        {
            if (!ModelState.IsValid)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), "", ModelState.Values.First().Errors.First().ErrorMessage));
            int currentUserId = this.CurrentUserID();
            if (currentUserId == 0 || currentUserId != adminId)
                return Unauthorized();
            if (adminUserData.Id <= 0)
                return BadRequest(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.ModelValidation), "", "Invalid user"));

            adminUserData.Role = Role.Admin;
            var adminUsers = await _adminUserService.UpdateAdmin(adminUserData);
            return Ok(adminUsers);
        }
        #endregion

        #region Logout
        /// <summary>
        /// To logout current user
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Role.SuperAdmin + "," + Role.Admin)]
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
        #endregion
    }
}

using Hiroshima.Maas.Common.Infrastructure.Logger;
using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.DL.Entities;
using Hiroshima.Maas.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Hiroshima.Maas.API.Controllers
{
 
    public abstract class BaseAPIController : ControllerBase
    {
        protected readonly IMessageHandler _messageHandler;
        private readonly IHttpContextAccessorExtension _httpContextAccessorExtension;
        protected ILoggerManager _logger;
        public BaseAPIController(IHttpContextAccessorExtension httpContextAccessorExtension, IMessageHandler messageHandler, ILoggerManager logger)
        {
            _httpContextAccessorExtension = httpContextAccessorExtension;
            _messageHandler = messageHandler;
            _logger = logger;
        }
        /// <summary>
        /// Get current logged in user Id
        /// </summary>
        /// <returns></returns>

        [ApiExplorerSettings(IgnoreApi = true)]
        public int CurrentUserID()
        {
            return _httpContextAccessorExtension.CurrentUserId();
        }

        /// <summary>
        /// Get current logged in user role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool CurrentUserRole(string role)
        {
            return _httpContextAccessorExtension.CurrentUserRole(role);
        }
    }
}

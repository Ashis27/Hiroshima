using Hiroshima.Maas.Services.Interfaces;
using Hiroshima.Maas.Services.Utility.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hiroshima.Maas.API.Infrastructure.Auth
{
    public class HttpContextAccessorExtension : IHttpContextAccessorExtension
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpContextAccessorExtension(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get current logged in user Id
        /// </summary>
        /// <returns></returns>

        [ApiExplorerSettings(IgnoreApi = true)]
        public int CurrentUserId()
        {
            if (_httpContextAccessor.HttpContext != null)
                return int.Parse(_httpContextAccessor?.HttpContext?.User?.FindFirst(Constants.Strings.JwtClaimIdentifiers.Id)?.Value);
            return 0;
        }

        /// <summary>
        /// Get current logged in user role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool CurrentUserRole(string role)
        {
            if (_httpContextAccessor.HttpContext != null)
                return _httpContextAccessor.HttpContext.User.IsInRole(role);
            return false;
        }
    }
}

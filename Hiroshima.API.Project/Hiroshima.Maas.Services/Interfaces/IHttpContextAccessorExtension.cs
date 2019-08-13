using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.Services.Interfaces
{
    public interface IHttpContextAccessorExtension
    {
        int CurrentUserId();
        bool CurrentUserRole(string role);
    }
}

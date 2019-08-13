using Hiroshima.Maas.Services.RequestAndResponse;
using Hiroshima.Maas.Services.Utility.Helper;
using Hiroshima.Maas.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.Services.Interfaces
{
    public interface IAdminUserService
    {
        Task<AdminResponse> Authenticate(string username, string password);
        AdminResponse Register(AdminUserViewModel adminUserData);
        Task<AdminResponse> UpdateAdmin(AdminUserViewModel adminUserData);
        Task<IEnumerable<AdminUserViewModel>> GetActiveAdminUsers(int currentUserId, string role);
        Task<AdminResponse> DeleteAdmin(int adminId);
        //Task<AdminResponse> Logout();
    }
}

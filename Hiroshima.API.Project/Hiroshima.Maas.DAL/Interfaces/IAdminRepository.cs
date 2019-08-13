
using Hiroshima.Maas.DL.Entities.AdminUserModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.DAL.Interfaces
{
    public interface IAdminUserRepository
    {
        AdminUser GetAdminUser(string username);
        void Register(AdminUser adminUser);
        void UpdateAdmin(AdminUser adminUser);
        Task<AdminUser> GetAdminUserById(int adminId);
        Task<IEnumerable<AdminUser>> GetActiveAdminUsers(int currentUserId, string role);
        //void DeleteAdmin(int currentUserId, int adminId,string role);
    }
}

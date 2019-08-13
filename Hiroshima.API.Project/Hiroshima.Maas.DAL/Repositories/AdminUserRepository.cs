using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.DAL.Contexts;
using Hiroshima.Maas.DAL.Interfaces;
using Hiroshima.Maas.DL.Entities.AdminUserModel;
using Hiroshima.Maas.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.DAL.Repositories
{
    public class AdminUserRepository : BaseRepository<AdminUser>, IAdminUserRepository
    {
        public AdminUserRepository(HiroshimaMaaSDBContext context, IMessageHandler messageHandler, IUnitOfWork unitOfWork) : base(context, messageHandler, unitOfWork) { }

        #region Authenticate
        public AdminUser GetAdminUser(string username)
        {
            return this.Where(p => p.UserName == username && p.IsActive).FirstOrDefault();
        }
        #endregion

        #region Register
        public async void Register(AdminUser adminUser)
        {
            this.Add(adminUser);
            await _unitOfWork.CompleteAsync();
        }
        #endregion

        #region Get_Active_Admins
        public async Task<IEnumerable<AdminUser>> GetActiveAdminUsers(int currentUserId, string role)
        {
            return await this.SearchBy(p => Convert.ToInt32(p.CreatedBy) == currentUserId && p.Role == role);
        }
        #endregion

        #region Get_User_By_Id
        public async Task<AdminUser> GetAdminUserById(int adminId)
        {
            return await this.GetById(adminId);
        }
        #endregion

        #region Update_Admin
        public async void UpdateAdmin(AdminUser adminUser)
        {
            this.Update(adminUser);
            await _unitOfWork.CompleteAsync();
        }
        #endregion
    }
}

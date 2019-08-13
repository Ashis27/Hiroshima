using AutoMapper;
using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.Common.Infrastructure.Logger;
using Hiroshima.Maas.DAL.Interfaces;
using Hiroshima.Maas.DL.Entities.AdminUserModel;
using Hiroshima.Maas.Services.Interfaces;
using Hiroshima.Maas.Services.RequestAndResponse;
using Hiroshima.Maas.Services.Utility.Helper;
using Hiroshima.Maas.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.Services.Services
{

    public class AdminUserService : BaseService, IAdminUserService
    {
        private readonly IAdminUserRepository _adminUserRepo;

        public AdminUserService(IAdminUserRepository adminUserRepo, IJwtFactory jwtFactory, IMessageHandler messageHandler, ILoggerManager logger, IMapper mapper) : base(messageHandler, mapper, logger, jwtFactory)
        {
            _adminUserRepo = adminUserRepo;
        }

        #region Authenticate
        public async Task<AdminResponse> Authenticate(string userName, string password)
        {
            _logger.LogInfo("Authentication method called");
            try
            {
                AdminUser adminUser = _mapper.Map<AdminUser>(_adminUserRepo.GetAdminUser(userName));
                if (adminUser == null)
                {
                    _logger.LogError("User doesn't exist");
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.AuthUserDoesNotExists)));
                }
                if (!VerifyPasswordHash(password, adminUser.PasswordHash, adminUser.PasswordSalt))
                {
                    _logger.LogError("Invalid credential");
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.AuthWrongCredentials)));
                }
                _logger.LogInfo("JWT Token creation initiatted");
                var token = await _jwtFactory.GenerateEncodedToken(adminUser.Id.ToString(), adminUser.UserName, adminUser.Role);
                _logger.LogInfo("Successfully generate JWT Token");
                AdminResponse response = new AdminResponse(true, string.Format(_messageHandler.GetSuccessMessage(SuccessMessagesEnum.SuccessfullyLoggedIn)));
                response.Token = token;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new AdminResponse(false, ex.Message);
            }
        }
        #endregion

        #region Register
        public AdminResponse Register(AdminUserViewModel adminUserData)
        {
            _logger.LogInfo("Registration method called");
            byte[] passwordHash, passwordSalt;
            try
            {
                AdminUser adminUser = _mapper.Map<AdminUser>(_adminUserRepo.GetAdminUser(adminUserData.UserName));
                if (adminUser != null)
                {
                    _logger.LogInfo("The user doesn't not exists");
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.UserAlreadyExist)));
                }
                else
                    adminUser = _mapper.Map<AdminUser>(adminUserData);
                CreatePasswordHash(adminUserData.Password, out passwordHash, out passwordSalt);
                _logger.LogInfo("HMACSHA512 password created");
                adminUser.PasswordHash = passwordHash;
                adminUser.PasswordSalt = passwordSalt;
                _adminUserRepo.Register(adminUser);
                _logger.LogInfo("Successfully registered new admin user by role = " + adminUserData.Role);
                adminUserData.Password = null;
                adminUserData.ConfirmPassword = null;
                AdminResponse response = new AdminResponse(true, string.Format(_messageHandler.GetSuccessMessage(SuccessMessagesEnum.SuccessfullRegister)));
                response.AdminUser = adminUserData;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new AdminResponse(false, ex.Message);
            }
        }
        #endregion

        #region Get_Active_Admins
        public async Task<IEnumerable<AdminUserViewModel>> GetActiveAdminUsers(int currentUserId, string role)
        {
            _logger.LogInfo("Role " + role + " is trying to get all active admins");
            try
            {
                IEnumerable<AdminUserViewModel> adminUser = _mapper.Map<IEnumerable<AdminUser>, IEnumerable<AdminUserViewModel>>(await _adminUserRepo.GetActiveAdminUsers(currentUserId, role));
                _logger.LogInfo("Fetched active admins for role " + role);
                return adminUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        #endregion

        #region Delete_Admin
        public async Task<AdminResponse> DeleteAdmin(int adminId)
        {
            _logger.LogInfo("Trying to delete admin");
            try
            {
                //Get active admin user by id
                AdminUser user = await _adminUserRepo.GetAdminUserById(adminId);
                if (user == null)
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.AuthUserDoesNotExists)));

                //Soft deleted
                user.IsActive = false;

                //Updated admin user
                _adminUserRepo.UpdateAdmin(user);
                _logger.LogInfo("Admin deleted");
                return new AdminResponse(true, string.Format(_messageHandler.GetSuccessMessage(SuccessMessagesEnum.SuccessfullyDeleted)));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete admin");
                return new AdminResponse(false, ex.Message);
            }
        }
        #endregion

        #region Update Admin
        public async Task<AdminResponse> UpdateAdmin(AdminUserViewModel adminUserData)
        {
            _logger.LogInfo("Update Admin method called by role " + adminUserData.Role);
            byte[] passwordHash, passwordSalt;
            try
            {
                //Get active admin user by id
                AdminUser adminUser = await _adminUserRepo.GetAdminUserById(adminUserData.Id);
                if (adminUser == null)
                {
                    _logger.LogInfo("The user doesn't not exists");
                    throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.AuthUserDoesNotExists)));
                }
                else
                {
                    passwordHash = adminUser.PasswordHash;
                    passwordSalt = adminUser.PasswordSalt;
                    adminUser = _mapper.Map<AdminUser>(adminUserData);
                }

                if (!String.IsNullOrEmpty(adminUserData.Password) && !String.IsNullOrEmpty(adminUserData.ConfirmPassword) && adminUserData.Password == adminUserData.ConfirmPassword)
                {
                    //if(!VerifyPasswordHash(adminUserData.Password, adminUser.PasswordHash, adminUser.PasswordSalt))
                    //{
                    //    _logger.LogInfo(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidPassword)," Password doesn't match"));
                    //    return new AdminResponse(false, string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.InValidPassword), " Password doesn't match"));
                    //}
                    CreatePasswordHash(adminUserData.Password, out passwordHash, out passwordSalt);
                    _logger.LogInfo("HMACSHA512 password created for password update");
                }
                adminUser.PasswordHash = passwordHash;
                adminUser.PasswordSalt = passwordSalt;
                _adminUserRepo.UpdateAdmin(adminUser);
                _logger.LogInfo("Successfully updated by role " + adminUserData.Role);
                adminUserData.Password = null;
                adminUserData.ConfirmPassword = null;
                AdminResponse response = new AdminResponse(true, string.Format(_messageHandler.GetSuccessMessage(SuccessMessagesEnum.SuccessfullyUpdated)));
                response.AdminUser = adminUserData;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new AdminResponse(false, ex.Message);
            }
        }
        #endregion

        #region Create_Hash_Password
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
            {
                throw new Exception("Password cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password cannot be empty or contain whitespace");
            }
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        #endregion

        #region Verify_Hash_Password
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null)
            {
                throw new Exception("Password cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password cannot be empty or contain whitespace");
            }
            if (storedHash.Length != 64)
            {
                throw new Exception("Invalid length of password hash (64 bytes expected)");
            }
            if (storedSalt.Length != 128)
            {
                throw new Exception("Invalid length of password salt (128 bytes expected)");
            }
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
        #endregion

        #region Logout
        //public async Task<AdminResponse> Logout()
        //{
        //    _logger.LogInfo("Logout method called");
        //    try
        //    {
        //       _logger.LogInfo("Successfully logout");
        //        AdminResponse response = new AdminResponse(true, "");
        //        response.Token = null;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return new AdminResponse(false, ex.Message);
        //    }
        //}
        #endregion

    }
}

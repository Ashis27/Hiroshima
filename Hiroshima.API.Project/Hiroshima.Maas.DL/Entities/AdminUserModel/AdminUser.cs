using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.DL.Entities.AdminUserModel
{
    public class AdminUser:BaseEntity
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int DefaultLanguage { get; set; }
    }
}

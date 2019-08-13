using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hiroshima.Maas.DL.Enums
{
    public enum UserRole
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Super Admin")]
        SuperAdmin = 1,
        [Description("NEC Admin")]
        NECAdmin = 1,
        [Description("User")]
        User = 1,
    }
}

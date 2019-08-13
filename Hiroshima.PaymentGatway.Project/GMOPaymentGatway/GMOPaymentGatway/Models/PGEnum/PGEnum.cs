using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GMOPaymentGatway.Models.PGEnum
{
    public enum PGModuleEnum
    {
        [Description("Credit Card")]
        UseCredit = 0,
        [Description("Softbank Matomete Shiharai")]
        UseSb = 1,
        [Description("Docomo Mobile")]
        UseDocomo = 2,
        [Description("Kantan Kessai")]
        UseAu = 3,
    }
    public enum PGTypeEnum
    {
        [Description("GMO")]
        GMO = 0,
    }
}
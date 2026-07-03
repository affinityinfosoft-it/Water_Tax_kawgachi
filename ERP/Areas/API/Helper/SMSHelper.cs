using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Areas.API.Helper
{
    public class SMSHelper
    {
        public static bool SendOTP(string mobileNo, string otp)
        {
            // Integrate your SMS Gateway here

            // Example:
            // MSG91
            // Fast2SMS
            // TextLocal

            return true;
        }
    }
}
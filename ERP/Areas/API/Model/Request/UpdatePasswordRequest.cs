using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Areas.API.Model.Request
{
    public class UpdatePasswordRequest
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Areas.API.Model.Request
{
    public class VerifyPartyRequest
    {
        [Required]
        public string PartyCode { get; set; }

        [Required]
        public string MobileNo { get; set; }
    }
}
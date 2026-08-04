using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Areas.API.Model.Request
{
    public class NoticeRequest
    {
        // PartyCode will be taken from JWT Token
        public string PartyCode { get; set; }
    }
}
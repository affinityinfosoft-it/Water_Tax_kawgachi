using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    public class StatusResponse
    {
        public Int64 Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ExMessage { get; set; }
        public bool CanRedirect { get; set; }
        public string Param { get; set; }
        public string ActionName { get; set; }
        public string Controller { get; set; }
        public StatusResponse()
        {
            CanRedirect = false;
        }

    }
}
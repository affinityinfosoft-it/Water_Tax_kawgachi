using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Areas.API.Model.Response
{
    public class NoticeResponse
    {
        public long NoticeId { get; set; }

        public string NoticeCode { get; set; }

        public string NoticeType { get; set; }

        public string Title { get; set; }

        public string Notice { get; set; }

        public string NoticeFor { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public bool IsPublish { get; set; }

        public string Attachment { get; set; }
    }
}
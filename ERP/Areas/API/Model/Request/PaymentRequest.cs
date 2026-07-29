using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Areas.API.Model.Request
{
    public class PaymentRequest
    {
        public string PartyCode { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? NoOfMonth { get; set; }
        public string PaymentType { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? PaymentFrom { get; set; }
        public DateTime? PaymentTo { get; set; }
        public decimal? PaymentAmount { get; set; }

        public long? CM_ID { get; set; }
        public int? FyId { get; set; }
        public int? UserId { get; set; }
    }
}
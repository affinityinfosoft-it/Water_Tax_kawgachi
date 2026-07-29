using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Areas.API.Model.Request
{
    public class ComplaintRequest
    {
        public long ComplaintId { get; set; }

        public string PartyCode { get; set; }

        public int ComplaintTypeId { get; set; }

        public string Description { get; set; }

        public string Priority { get; set; }

        public string Status { get; set; }

        public string Remarks { get; set; }

        public int AssignTo { get; set; }

        public long CM_ID { get; set; }

        public int FyId { get; set; }

        public int UserId { get; set; }
    }
}
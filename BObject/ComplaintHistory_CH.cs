using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class ComplaintHistory_CH
    {
        public long CH_Id { get; set; }

        public long CH_ComplaintId { get; set; }

        public string CH_Status { get; set; }

        public string CH_AssignTo { get; set; }

        public string CH_Remarks { get; set; }

        public string CH_UpdatedBy { get; set; }
        public DateTime? CMPL_ResolvedDate { get; set; }
        public DateTime CH_UpdatedDate { get; set; }
    }
}

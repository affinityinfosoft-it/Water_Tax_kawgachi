using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class ComplaintMaster_CM : Common
    {
        public long CMPL_Id { get; set; }

        public string CMPL_ComplaintNo { get; set; }

        public DateTime? CMPL_ComplaintDate { get; set; }

        public string CMPL_PartyCode { get; set; }

        public string PM_PartyName { get; set; }
        public String PM_FHName { get; set; }

        public string PM_MobNo { get; set; }

        public string AM_AreaName { get; set; }

        public long? CMPL_ComplaintTypeId { get; set; }

        public string ComplaintType { get; set; }

        public string CMPL_Description { get; set; }

        public string CMPL_Priority { get; set; }

        public string CMPL_Status { get; set; }

        public string CMPL_AssignTo { get; set; }

        public string CMPL_Remarks { get; set; }
        public DateTime? CMPL_ResolvedDate { get; set; }
      //  public DateTime? CMPL_ResolvedDate { get; set; }

        //public DateTime? CreatedDate { get; set; }

        public List<ComplaintMaster_CM> ComplaintList { get; set; }
        public List<ComplaintHistory_CH> HistoryList { get; set; }
    }
}

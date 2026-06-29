using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class ferrulMaster:Common
    {
        //bill_No, bill_date, custId, fAmt, sAmt, Auto_No, puser
        public Int64 FerulId { get; set; }
        public string fbill_No { get; set; }
        public string cbill_No { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime bill_date { get; set; } = DateTime.Now;
        public string custId { get; set; }
        public double fAmt { get; set; }
        public double cAmt { get; set; }
        public string fReceptCode { get; set; }
        public string cReceptCode { get; set; }
        public decimal Auto_No { get; set; }
        public string PL_PartyCode { get; set; }
        public string PM_PartyCode { get; set; }
        public string PM_PhoneNo { get; set; }
        public string PM_MobNo { get; set; }
        public string PM_FHName { get; set; }
        public string AM_AreaName { get; set; }
        public string PM_ParaName { get; set; }
        public string PM_PartyName { get; set; }
        public string PL_RcptType { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }

        public List<ferrulMaster> ferrulMasterList { get; set; }

    }
}

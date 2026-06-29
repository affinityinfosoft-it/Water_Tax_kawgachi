using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class PartyLedger_PL: Common
    {
        //PL_Id, PL_BillNo, PL_BillDate, PL_PartyCode, PL_Type, PL_Amount, PL_RcptType, PL_RcptCode, PL_RcptDate, PL_RcptNo, PL_Bank, PL_ChqNo,
        //PL_ChqDate, FyId, CompanyCode, Puser, PL_BType
        public string PM_PartyCode { get; set; }
   
        public string PM_MobNo { get; set; }
        public string PM_PhoneNo { get; set; }
        public string PM_FHName { get; set; }
        public decimal PL_Id { get; set; }
        public string PL_BillNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PL_BillDate { get; set; }
        //public string PL_BillDate { get; set; }
        public string PL_BillDates { get; set; }
        public string PL_PartyCode { get; set; }
        public string PL_Type { get; set; }
        public double PL_BillAmount { get; set; }
        public double PL_PaidAmount { get; set; }
        public string PL_RcptType { get; set; }
        public string PL_RcptCode { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime PL_RcptDate { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PL_RcptDate { get; set; } = DateTime.Now;

        public decimal PL_RcptNo { get; set; }
        public string PL_Bank { get; set; }
        public string PL_ChqNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PL_ChqDate { get; set; } = DateTime.Now;
        //public int FyId { get; set; }
        public int CompanyCode { get; set; }
        public string Puser { get; set; }
        public string PL_BType { get; set; }
        public string AM_AreaName { get; set; }
        public string PM_ParaName { get; set; }
        public string PM_PartyName { get; set; }
        public decimal PM_PartyId { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }

        public List<PartyLedger_PL> PartyLadgerList { get; set; }
    }
}

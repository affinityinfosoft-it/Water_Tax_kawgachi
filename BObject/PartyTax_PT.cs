using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class PartyTax_PT: Common
    {
        //PT_RcptNo, PT_PtyCode, PT_PmtDate, PT_DtFrom, PT_DtTo, PT_Mth, PT_Rate, PT_Amount, auto_no, companycode, FyId, puser
        public Int64 PT_ID { get; set; }
        public string PT_RcptNo { get; set; }
        public string PT_PtyCode { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PT_PmtDate { get; set; } = DateTime.Now;
        public DateTime PT_DtFrom { get; set; }
        public string PT_DtFroms { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PT_DtTo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PT_CurDate { get; set; } = DateTime.Now;

        public string PT_DtTos { get; set; }
        //public int DueMonth { get; set; }
        public int PT_Mth { get; set; }
        public double PT_Rate { get; set; }
        public double PT_Amount { get; set; }
        public string PL_PartyCode { get; set; }
        public string PM_PartyCode { get; set; }
        public string PM_PhoneNo { get; set; }
        public string PM_MobNo { get; set; }
        public string PM_FHName { get; set; }
        public string AM_AreaName { get; set; }
        public string PM_ParaName { get; set; }
        public string PM_PartyName { get; set; }
        public int f_date { get; set; }
        public int s_date { get; set; }
        public int t_date { get; set; }
        public int f_amt { get; set; }
        public int s_amt { get; set; }
        public int t_amt { get; set; }
        public string PL_RcptType { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public List<PartyTax_PT> PartyTax_PTList { get; set; }

    }
}

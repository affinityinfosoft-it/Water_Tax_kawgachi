using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class RepReConnection:Common
    {
        public Int64 GS_SIID { get; set; }
        public string GS_BillNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime GS_BillDate { get; set; } = DateTime.Now;
        public string PM_PartyCode { get; set; }
        public string PL_PartyCode { get; set; }
        public string PM_PhoneNo { get; set; }
        public string PM_MobNo { get; set; }
        public string PM_FHName { get; set; }
        public string AM_AreaName { get; set; }
        public string PM_ParaName { get; set; }
        public string PM_PartyName { get; set; }
        public string GS_PartyCode { get; set; }
        public string ins_code { get; set; }
        public string ItemName { get; set; }
        public string GS_ItemCode { get; set; }
        public double GS_Qty { get; set; }
        public string Unit { get; set; }
        public double GS_Rate { get; set; }
        public double GS_Vat { get; set; }
        public double TaxAmt { get; set; }
        public double Amount { get; set; }
        public double GS_VatAmt { get; set; }
        public double GS_Amount { get; set; }
        public double GS_GrossAmt { get; set; }
        public double GS_AdjAmt { get; set; }
        public double GS_NetAmt { get; set; }
        public double GS_Paid { get; set; }
        public double GS_Due { get; set; }
        public string IM_ItemName { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public List<ItemMaster_IM> RepReItemList { get; set; }
        public List<RepReConnection> RepInsItemList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class ItemMaster_IM:Common
    {
        //IM_ItemCode, IM_ItemDescription, IM_Unit, Auto_No, IM_Type, IM_Rate
        public Int64 IM_ID { get; set; }
        //public Int64 CM_ID { get; set; }
        public string IM_ItemCode { get; set; }
        public string IM_ItemName { get; set; }
        public string IM_ItemDescription { get; set; }
        public string IM_Unit { get; set; }
        public decimal Auto_No { get; set; }
        public string IM_Type { get; set; }
        public double IM_Rate { get; set; }
        public string GS_BillNo { get; set; }
        public Int64 GS_SIID { get; set; }
        public double GS_Qty { get; set; }
        public double GS_Vat { get; set; }
        public double GS_VatAmt { get; set; }
        public double GS_Amount { get; set; }

        public decimal? ItemGroupID { get; set; }
        public decimal? ItemSubGroupID { get; set; }
        public decimal? IM_GST { get; set; }
        public string IM_StockInHand { get; set; }
        //public DateTime CreatedDate { get; set; }

        public IEnumerable<ItemMaster_IM> ItemList { get; set; }

    }
}

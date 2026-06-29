using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
  public class VanBooking_VB : Common
    {
        public Int64 AP_ID { get; set; }
        public string AP_Code { get; set; }
        public string AP_Name { get; set; }
        public string AP_FathersName { get; set; }
        [Display(Name = "RegDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RegDate { get; set; } = DateTime.Now;
        public string AP_Mobile { get; set; }
        public string AM_AreaID { get; set; }
        public string AM_AreaName { get; set; }
        public string PM_ParaId { get; set; }
        public string PM_ParaName { get; set; }
        public string AP_Address { get; set; }
        public string DeliveryLandmark { get; set; }
        public string Bk_Purpose { get; set; }
        public string VM_ID { get; set; }
        public DateTime FromDate { get; set; }
        public string FromTime { get; set; } 
        public DateTime ToDate { get; set; }
        public string ToTime { get; set; }
        public decimal Amount { get; set; }
        public string Qty { get; set; }
        public string FormNo { get; set; }
        public decimal FormAmount { get; set; }
        public Double fr_rate { get; set; }
        public string VM_Type { get; set; }
        public string VM_VanName { get; set; }
        public Int64 CompanyID { get; set; }
        public string VB_RcptNo { get; set; }
        public string PL_RcptType { get; set; }
        public decimal academicyear { get; set; }
        public DateTime CreatedDate { get; set; }
        public string fromdates { get; set; }
        public string todates { get; set; }

        public List<VanBooking_VB> ListBooking { get; set; }
        public IEnumerable<VanBooking_VB>VanList { get; set; }
    }
}

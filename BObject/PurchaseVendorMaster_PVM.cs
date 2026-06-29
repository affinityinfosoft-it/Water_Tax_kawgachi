using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class PurchaseVendorMaster_PVM : Common
    {
        //PVM_VendorID, PVM_VendorCode, PVM_VendorName, PVM_Address1, PVM_Address2, PVM_City, PVM_PIN, PVM_Phone, PVM_FaxNo, PVM_Email,
        //PVM_SubAccID, companycode
        public Int64 PVM_VendorID { get; set; }
        public string PVM_VendorCode { get; set; }
        public string PVM_VendorName { get; set; }
        public string PVM_Address1 { get; set; }
        public string PVM_Address2 { get; set; }
        public string PVM_City { get; set; }
        public string PVM_PIN { get; set; }
        public string PVM_Phone { get; set; }
        public string PVM_FaxNo { get; set; }
        public string PVM_Email { get; set; }
        public int PVM_SubAccID { get; set; }
        public Int64 CompanyID { get; set; }

        public List<PurchaseVendorMaster_PVM> ListVendor { get; set; }

    }
}

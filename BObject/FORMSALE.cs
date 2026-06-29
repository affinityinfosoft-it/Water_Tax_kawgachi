using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class FORMSALE:Common
    {
        //ID, BILLNO, BILLDATE, AMOUNT, FORMNO, PAYMODE, CHQNO, CASHIER
        public decimal ID { get; set; }
        public string BILLNO { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BILLDATE { get; set; } = DateTime.Now;
        public decimal AMOUNT { get; set; }
        public decimal FORMNO { get; set; }
        public string PAYMODE { get; set; }
        public string CHQNO { get; set; }
        public string CASHIER { get; set; }
        public string PL_PartyCode { get; set; }
        public string PM_PartyCode { get; set; }
        public string PM_PhoneNo { get; set; }
        public string PM_MobNo { get; set; }
        public string PM_FHName { get; set; }
        public string AM_AreaName { get; set; }
        public string PM_ParaName { get; set; }
        public string PM_PartyName { get; set; }
        public Double fr_rate { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }

        public List<FORMSALE> FORMSALEList { get; set; }

    }
}

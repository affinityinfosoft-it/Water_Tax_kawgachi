using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class company:Common
    {
        //CM_ID ,code, name, address, city, pin, phoneno, puser, dt_of_entry, invbill_prefix, ho, ipd_pref, opd_pref, opdinv_pref, logo
        //public Int64 CM_ID { get; set; }
        public int code { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string pin { get; set; }
        public string phoneno { get; set; }
        public string puser { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime dt_of_entry { get; set; }=DateTime.Now;
        public string invbill_prefix { get; set; }
        public string ho { get; set; }
        public string ipd_pref { get; set; }
        public string opd_pref { get; set; }
        public string opdinv_pref { get; set; }
        public string logo { get; set; }
        public IEnumerable<company> CompanyList { get; set; }

    }
}

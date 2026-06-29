using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class academicyear : Common
    {
        //code, description, fromdate, todate, dt_of_entry, puser, companycode
        public int code { get; set; }
        public string description { get; set; }
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
        public DateTime dt_of_entry { get; set; }
        public string puser { get; set; }
        public string companycode { get; set; }
    }
}

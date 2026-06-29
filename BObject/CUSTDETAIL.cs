using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class CUSTDETAIL:Common
    {
        //ID, SPLID, PARA, AREA, WATERTAX, WATERTAXSTARTDATE, FORMSUBMITDATE, FORMNO, PERMISSIONDATE, PERMISSION, CONNECTIONDATE,
        //CONNECTION, WORKORDERDATE, WORKORDER
        public int ID { get; set; }
        public char SPLID { get; set; }
        public int PARA { get; set; }
        public int AREA { get; set; }
        public char WATERTAX { get; set; }
        public DateTime WATERTAXSTARTDATE { get; set; }
        public DateTime FORMSUBMITDATE { get; set; }
        public char FORMNO { get; set; }
        public DateTime PERMISSIONDATE { get; set; }
        public char PERMISSION { get; set; }
        public DateTime CONNECTIONDATE { get; set; }
        public char CONNECTION { get; set; }
        public DateTime WORKORDERDATE { get; set; }
        public char WORKORDER { get; set; }
    }
}

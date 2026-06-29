using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class TankMaster_TM : Common
    {
        public Int64 TM_ID { get; set; }
        //public Int64 CM_ID { get; set; }
        public string TM_TankCode { get; set; }
        public string TM_TankName { get; set; }
        public string TM_Lt { get; set; }
        public decimal Auto_No { get; set; }
        public double TM_Rate { get; set; }
        public IEnumerable<TankMaster_TM> TankList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class VanMaster_VM : Common
    {
        public Int64 VM_ID { get; set; }

        public string VM_VanCode { get; set; }
        public string VM_VanName { get; set; }
        public string VM_Type { get; set; }
        public decimal VM_Rate { get; set; }
        public decimal Auto_No { get; set; }
        //public Int64 CM_ID { get; set; }
        public IEnumerable<VanMaster_VM> VanList { get; set; }
    }
}

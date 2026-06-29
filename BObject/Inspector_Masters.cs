using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class Inspector_Masters
    {
        //ins_code, ins_name

        public Boolean ins_code { get; set; }
        public string ins_name { get; set; }

        //public Int64 CM_ID { get; set; }
        public List<Inspector_Masters> InspectorsList { get; set; }
        //public List<Inspector_Master> InspectorMaster{ get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class Inspector_Master
    {
        //Ins_Id,ins_code, ins_name
        public Int64 Ins_Id { get; set; }
       
        public decimal ins_code { get; set; }

        public string ins_name { get; set; }
        public List<Inspector_Master> InspectorList { get; set; }
    }
}

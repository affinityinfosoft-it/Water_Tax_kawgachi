using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class Master_Rate
    {
        //bf_rate, fr_rate
        public Int64 CM_ID { get; set; }
        public Double bf_rate { get; set; }
        public Double fr_rate { get; set; }
        public Double fAmt { get; set; }
        public Double cAmt { get; set; }
    }
}

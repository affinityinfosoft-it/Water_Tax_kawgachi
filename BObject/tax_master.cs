using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class tax_master:Common
    {
        //f_date, s_date, t_date, f_amt, s_amt, t_amt

        public int Tax_ID { get; set; }
        public int f_date { get; set; }
        public int s_date { get; set; }
        public int t_date { get; set; }
        public int f_amt { get; set; }
        public int s_amt { get; set; }
        public int t_amt { get; set; }

    }
}

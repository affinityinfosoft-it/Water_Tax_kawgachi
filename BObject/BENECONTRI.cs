using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class BENECONTRI
    {
        //ID, BILLNO, BILLDATE, AMOUNT, PAYMODE, CHQNO, CASHIER
        public int ID { get; set; }
        public int BILLNO { get; set; }
        public DateTime BILLDATE { get; set; }
        public int AMOUNT { get; set; }
        public char PAYMODE { get; set; }
        public char CHQNO { get; set; }
        public char CASHIER { get; set; }
    }
}

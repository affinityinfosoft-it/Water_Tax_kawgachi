using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class MATERIAL
    {
        //ID, BILLDATE, BILLNO, PIPE_AMT, LAYING_AMT, SOCKET_AMT, ELBOW_AMT, NIPPLE_AMT, REDUCER_AMT, BIBCOCK_AMT, BIBCOCKFITTING_AMT,
        //TEE_AMT, STOPPLUG_AMT, ROADCUTTING_AMT, OTHER_AMT, BORING_AMT, DISCOUNTAMOUNT, PAYABLEAMOUNT, AMOUNT, PAYMODE, CHQNO, CASHIER
        public int ID { get; set; }
        public DateTime BILLDATE { get; set; }
        public int BILLNO { get; set; }
        public decimal PIPE_AMT { get; set; }
        public decimal LAYING_AMT { get; set; }
        public decimal SOCKET_AMT { get; set; }
        public decimal ELBOW_AMT { get; set; }
        public decimal NIPPLE_AMT { get; set; }
        public decimal REDUCER_AMT { get; set; }
        public decimal BIBCOCK_AMT { get; set; }
        public decimal BIBCOCKFITTING_AMT { get; set; }
        public decimal TEE_AMT { get; set; }
        public decimal STOPPLUG_AMT { get; set; }
        public decimal ROADCUTTING_AMT { get; set; }
        public decimal OTHER_AMT { get; set; }
        public decimal BORING_AMT { get; set; }
        public decimal DISCOUNTAMOUNT { get; set; }
        public decimal PAYABLEAMOUNT { get; set; }
        public decimal AMOUNT { get; set; }
        public char PAYMODE { get; set; }
        public char CHQNO { get; set; }
        public char CASHIER { get; set; }
    }
}

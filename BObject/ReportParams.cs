using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
   public class ReportParams<T>
    {
        public String RptFileName { get; set; }
        public String ReportTitle { get; set; }
        public List<T> DataSource { get; set; }
        public bool IsPassParamToCr { get; set; }



    }
}

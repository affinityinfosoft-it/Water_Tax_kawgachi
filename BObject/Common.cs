using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class Common
    {
        //CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, IsActive
        public Int64 CreatedBy { get; set; }
        public Int64 CM_ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Boolean IsActive { get; set; }
        public string OppType { get; set; }
        public int UserId { get; set; }
        public int FyId { get; set; }
        
    }
}

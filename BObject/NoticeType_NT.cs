using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class NoticeType_NT
    {
        public long NT_Id { get; set; }

        public string NT_Name { get; set; }

        public bool NT_IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }
    }
}

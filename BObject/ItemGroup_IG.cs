using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class ItemGroup_IG : Common
    {
        public long ItemGroupID { get; set; }
        [Display(Name = "ItemGroup Code")]
        public decimal ItemCode { get; set; }
        [Display(Name = "ItemGroup Name")]
        public string ItemGroupName { get; set; }
        //public int CM_ID { get; set; }
        //public int UserId { get; set; }
        public List<ItemGroup_IG> ItemGroupList { get; set; }

    }
}

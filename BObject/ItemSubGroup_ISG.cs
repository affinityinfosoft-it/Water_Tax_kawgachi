using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
   public class ItemSubGroup_ISG : Common
    {
            
            public long ItemSubGroupID { get; set; }
            //public long CM_ID { get; set; }
            [Display(Name = "ItemSubGroup Code")]
            public decimal ItemSubCode { get; set; }
            public long ItemGroupID { get; set; }
            [Display(Name = "ItemGroupName")]
            public decimal ItemCode { get; set; }
            public string ItemGroupName { get; set; }
            [Display(Name = "Item Subgroup Name")]
            public string ItemSubGroupName { get; set; }
             //public int FyId { get; set; }
             //public int UserId { get; set; }
            public List<ItemSubGroup_ISG> ItemSubGroupList { get; set; }
            public List<ItemGroup_IG> ItemGroupList { get; set; }
        }
}

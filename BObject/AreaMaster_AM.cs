using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class AreaMaster_AM : Common
    {
        //AM_AreaID,AM_AreaCode, AM_AreaName
        public Int64 AM_AreaID { get; set; }
        //public Int64 CM_ID { get; set; }
        [Display(Name="Area Code")]
        public decimal AM_AreaCode { get; set; }
        [Display(Name = "Area Name")]
        public string AM_AreaName { get; set; }
        public List<AreaMaster_AM> AreaList { get; set; }
        //public List<company> companyList { get; set; }

    }
}

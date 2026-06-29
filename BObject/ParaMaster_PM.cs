using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class ParaMaster_PM
    {
        //PM_ParaId, CM_ID, PM_ParaCode, PM_AreaCode, PM_ParaName
        public Int64 PM_ParaId { get; set; }
        public Int64 CM_ID { get; set; }
        [Display(Name = "Para Code")]
        public decimal PM_ParaCode { get; set; }
        public Int64 AM_AreaID { get; set; }
        [Display(Name = "Area Name")]
        public decimal AM_AreaCode { get; set; }
        public string AM_AreaName { get; set; }
        [Display(Name = "Para Name")]
        public string PM_ParaName { get; set; }
        public List<ParaMaster_PM> ParaList { get; set; }
        public List<AreaMaster_AM> AreaList { get; set; }

    }
}

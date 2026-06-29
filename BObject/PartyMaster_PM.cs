using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class PartyMaster_PM : Common
    {
        //PM_PartyId, PM_PartyCode, PM_RegDate, PM_PartyName, PM_FHName, PM_AreaCode, PM_ParaCode, 
        //PM_Address, PM_City, PM_PhoneNo, PM_MobNo, PM_Email, PM_PIN, companycode, PM_BFAmount, 
        //PM_PaidAmt, PM_BFlag, PM_FFlag, PM_CFlag, PM_TaxDate, PM_SFlag, PM_FormNo, PM_RcptNo, 
        //formno, puser, PM_InvNo, academicyear
        public decimal PM_PartyId { get; set; }
        [Display(Name = "Consumer ID")]
        public string PM_PartyCode { get; set; }
        [Display(Name = "Reg Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PM_RegDate { get; set; } = DateTime.Now;
        [Display(Name = "Consumer Name")]
        public string PM_PartyName { get; set; }
        [Display(Name = "Father/Husband Name")]
        public string PM_FHName { get; set; }
        [Display(Name = "Area")]
        public Int64 AM_AreaID { get; set; }
        [Display(Name = "Para/Village")]
        public Int64 PM_ParaId { get; set; }
        [Display(Name = "Address")]
        public string PM_Address { get; set; }
        [Display(Name = "City")]
        public string PM_City { get; set; }
        [Display(Name = "Aadhaar Number")]
        public string PM_PhoneNo { get; set; }
        [Display(Name = "Mob No")]
        public string PM_MobNo { get; set; }
        [Display(Name = "Email")]
        public string PM_Email { get; set; }
        [Display(Name = "Pin")]
        public string PM_PIN { get; set; }
        //[Display(Name = "Pin")]
        //public Int64 CM_ID { get; set; }
        [Display(Name = "Benificary Amount")]
        public double PM_BFAmount { get; set; }
        [Display(Name = "Paid Amount")]
        public double PM_PaidAmt { get; set; }
        [Display(Name = "Due Amount")]
        public double PM_DueAmt { get; set; }
        public Boolean PM_BFlagF { get; set; }
        public Boolean PM_FFlagF { get; set; }
        public Boolean PM_CFlagF { get; set; }
        public string PM_BFlag { get; set; }
        public string PM_FFlag { get; set; }
        public string PM_CFlag { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PM_TaxDate { get; set; } = DateTime.Now;
        public Boolean PM_SFlagF { get; set; }
        public string PM_SFlag { get; set; }
        public string PM_FormNo { get; set; }
        public string PM_RcptNo { get; set; }
        public decimal formno { get; set; }
        public string puser { get; set; }
        public string PM_InvNo { get; set; }
        public decimal academicyear { get; set; }
        public string AM_AreaName { get; set; }
        public string PM_ParaName { get; set; }
        public string PL_RcptType { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public List<PartyMaster_PM> BeneficiryList { get; set; }

        public List<PartyMaster_PM> PartyList { get; set; }
        public static object ToList()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject.AccountModel
{

    public class AccountGroup
    {

        public int? GM_CompanyID { get; set; }
        public int? GM_FYId { get; set; }
        public string GM_GroupCode { get; set; }
        public string GM_GroupDescription { get; set; }
        public long? GM_GroupId { get; set; }

    }


    public class AccountMaster
    {

        public AccountMaster()
        {

           // SessionList = new List<SessionMaster>();
            AccountGroupList = new List<AccountGroup>();
            AccountMasterList = new List<AccountMaster>();

        }
        //public List<SessionMaster> SessionList { get; set; }
        public List<AccountGroup> AccountGroupList { get; set; }
        public List<AccountMaster> AccountMasterList { get; set; }

        public int? AM_CompanyID { get; set; }
        public int? AM_FYId { get; set; }
        public int? AM_AccountId { get; set; }
        public int? AM_GroupId { get; set; }

        public string AM_AccountCode { get; set; }
        public string AM_AccDescription { get; set; }
        public string AM_LongName { get; set; }
        public string AM_SubOption { get; set; }
        public int? AM_Intrate { get; set; }
        public int? AM_ParamId { get; set; }
        public Decimal? AM_OpeningBalance { get; set; }
        public string AM_OPeningType { get; set; }
        public string ISSubAc { get; set; }

        public Decimal? AM_TotalDebit { get; set; }
        public Decimal? AM_TotalCredit { get; set; }
        public Decimal? AM_ClosingBalance { get; set; }
        public string AM_ClosingType { get; set; }
        public string AM_SuppressPayee { get; set; }
        public string AM_GroupCode { get; set; }
        public string SessionName { get; set; }
        public string GM_GroupDescription { get; set; }
        public string GM_GroupCode { get; set; }
        public int? AM_AccountOpId { get; set; }

        public Boolean AM_IsFund { get; set; }

    }

    public class SubAccountMaster
    {

        public SubAccountMaster()
        {

            //SessionList = new List<SessionMaster>();
            AccountGroupList = new List<AccountGroup>();
            SubAccountMasterList = new List<SubAccountMaster>();
            AccountMasterList = new List<AccountMaster>();

        }
       // public List<SessionMaster> SessionList { get; set; }
        public List<AccountGroup> AccountGroupList { get; set; }
        public List<SubAccountMaster> SubAccountMasterList { get; set; }
        public List<AccountMaster> AccountMasterList { get; set; }
        public int? SAM_CompanyID { get; set; }
        public int? SAM_FYID { get; set; }
        public int? SAM_SubId { get; set; }
        public int? SAM_AccountId { get; set; }

        public string SAM_SubCode { get; set; }
        public string AM_AccountCode { get; set; }

        public string SAM_SubDescription { get; set; }
        public string SAM_SubLongDesc { get; set; }
        public string SAM_Address1 { get; set; }
        public string SAM_Address2 { get; set; }
        public string SAM_Address3 { get; set; }
        public string SAM_Address4 { get; set; }
        public string SAM_OPhone { get; set; }
        public string SAM_RPhone { get; set; }
        public string SAM_FAX { get; set; }
        public string SAM_CellNo { get; set; }
        public string SAM_Email { get; set; }
        public string SAM_Website { get; set; }
        public string SAM_PAN { get; set; }
        public string SAM_CST { get; set; }
        public string SAM_SST { get; set; }
        public int? SAM_AccountOpId { get; set; }
        public int? AM_AccountId { get; set; }

        public string AM_AccDescription { get; set; }

        public Decimal? SAM_OpeningBalance { get; set; }
        public string SAM_OPeningType { get; set; }


        public Decimal? SAM_TotalDebit { get; set; }
        public Decimal? SAM_TotalCredit { get; set; }
        public Decimal? SAM_ClosingBalance { get; set; }
        public string SAM_ClosingType { get; set; }
        public Boolean SAM_IsFund { get; set; }


    }




    public class AccountLedger
    {

        public AccountLedger()
        {

           // SessionList = new List<SessionMaster>();
            AccountGroupList = new List<AccountGroup>();
            SubAccountMasterList = new List<SubAccountMaster>();
            AccountMasterList = new List<AccountMaster>();
            AccountLedgerList = new List<AccountLedger>();
            AccountVoucherTypeMasterList = new List<AccountVoucherTypeMaster>();
            AccountList = new List<AccountLedger>();
            ParticularsList = new List<AccountLedger>();
        }
        public List<AccountLedger> ParticularsList { get; set; }

        public List<AccountLedger> AccountList { get; set; }
        public List<AccountVoucherTypeMaster> AccountVoucherTypeMasterList { get; set; }
       // public List<SessionMaster> SessionList { get; set; }
        public List<AccountGroup> AccountGroupList { get; set; }
        public List<SubAccountMaster> SubAccountMasterList { get; set; }
        public List<AccountMaster> AccountMasterList { get; set; }
        public List<AccountLedger> AccountLedgerList { get; set; }
        public int? LD_CompanyId { get; set; }
        public int? LD_FYId { get; set; }
        public int? LD_LedgerID { get; set; }
        public int? LD_HdrId { get; set; }
        public string LD_Transactiontype { get; set; }
        public string LD_VoucherNo { get; set; }
        public string LD_ReferenceNo { get; set; }
        public DateTime? LD_Date { get; set; }
        public string LD_DateS { get; set; }
        public int? LD_AccountID { get; set; }
        public int? LD_SubID { get; set; }
        public string LD_AccountCode { get; set; }
        public string LD_SubCode { get; set; }
        public int? LD_CostCenterID { get; set; }
        public string LD_CostCenterCode { get; set; }
        public string LD_DrCr { get; set; }
        public Decimal? LD_DrAmount { get; set; }
        public Decimal? LD_CrAmount { get; set; }

        public string LD_Narration { get; set; }
        public string LD_ChequeNo { get; set; }
        public DateTime? LD_ChequeDate { get; set; }
        public string LD_Payee { get; set; }
        public int? LD_Userid { get; set; }
        public string LD_Remarks { get; set; }
        public string LD_DOID { get; set; }
        public string LD_Automatic { get; set; }
        public string LD_AdjustmentAmt { get; set; }
        public string LD_DOCSrlNo { get; set; }
        public int? LD_InvoiceID { get; set; }
        //public string MOD_FLA { get; set; }
        public int? VoucherTypeId { get; set; }
        public string VoucherType { get; set; }
        public string Prefix { get; set; }
        public string FYear { get; set; }
        public int? StartNo { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public bool IsCancel { get; set; }
        public int? LD_VoucherTypeId { get; set; }
        public string AccountValue { get; set; }
        public string ParticularsValue { get; set; }
        public string ParticularsText { get; set; }
        public int? LD_FundAccountID { get; set; }
        public int? LD_FundSubID { get; set; }
        public Boolean LD_IsFund { get; set; }
    }


    public class AccountVoucherTypeMaster
    {
        public AccountVoucherTypeMaster()
        {
            AccountVoucherTypeMasterList = new List<AccountVoucherTypeMaster>();
        }
        public List<AccountVoucherTypeMaster> AccountVoucherTypeMasterList { get; set; }
        public int? VoucherTypeId { get; set; }
        public string VoucherType { get; set; }
        public string Prefix { get; set; }
        public string FYear { get; set; }
        public int? StartNo { get; set; }

    }

    public class vw_AccountOpeningBalance
    {
        public int AccountId { get; set; }
        public int SubAccountId { get; set; }
        public int FYId { get; set; }
        public decimal OpeningBalance { get; set; }
        public string OPeningType { get; set; }
        public string LongName { get; set; }
    }
    public class vw_AccountLedger
    {
        public vw_AccountLedger()
        {
            vw_AccountLedgerList = new List<vw_AccountLedger>();
            vw_AccountOpeningBalanceList = new List<vw_AccountOpeningBalance>();
        }
        public List<vw_AccountLedger> vw_AccountLedgerList { get; set; }
        public List<vw_AccountOpeningBalance> vw_AccountOpeningBalanceList { get; set; }
        public int LD_CompanyId { get; set; }
        public int LD_FYId { get; set; }
        public int LD_HdrId { get; set; }
        public int LD_VoucherTypeId { get; set; }
        public string LD_VoucherNo { get; set; }
        public string LD_ReferenceNo { get; set; }
        public System.DateTime LD_Date { get; set; }
        public int LD_BankCode { get; set; }
        public int LD_AccountID { get; set; }
        public int LD_SubID { get; set; }
        public int LD_FundAccountID { get; set; }
        public int LD_FundSubID { get; set; }
        public string LD_AccountCode { get; set; }
        public string LD_SubCode { get; set; }
        public int LD_CostCenterID { get; set; }
        public string LD_CostCenterCode { get; set; }
        public string LD_DrCr { get; set; }
        public decimal LD_CrAmount { get; set; }
        public decimal LD_DrAmount { get; set; }
        public string LD_Narration { get; set; }
        public string LD_ChequeNo { get; set; }
        public System.DateTime LD_ChequeDate { get; set; }
        public string LD_Payee { get; set; }
        public int LD_Userid { get; set; }
        public string LD_Remarks { get; set; }
        public decimal LD_DOID { get; set; }
        public bool LD_Automatic { get; set; }
        public decimal LD_AdjustmentAmt { get; set; }
        public decimal LD_DOCSrlNo { get; set; }
        public decimal LD_InvoiceID { get; set; }
        public string MOD_FLAG { get; set; }
        public int UniqueMaxId { get; set; }
        public bool IsCancel { get; set; }
        public int CompanyID { get; set; }
        public int TrustID { get; set; }
        public string CollegeShortName { get; set; }
        public string CollegeName { get; set; }
        public string CollegeAddress { get; set; }
        public string CollegePinCode { get; set; }
        public string CollegePhone { get; set; }
        public string CollegeEmail { get; set; }
        public string CollegeLogoUrl { get; set; }
        public string PrincipleSignature { get; set; }
        public string CollegeCode { get; set; }
        public byte[] CollegeLogoBinary { get; set; }
        public string SessionName { get; set; }
        public System.DateTime SessionStartDate { get; set; }
        public System.DateTime SessionEndDate { get; set; }
        public int AM_CompanyID { get; set; }
        public int AM_FYId { get; set; }
        public int AM_GroupId { get; set; }
        public string AM_AccountCode { get; set; }
        public string AM_AccDescription { get; set; }
        public string AM_LongName { get; set; }
        public string AM_SuppressPayee { get; set; }
        public string AM_GroupCode { get; set; }
        public string ISSubAc { get; set; }
        public bool AM_IsFund { get; set; }
        public bool IsFeesHead { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public int SAM_CompanyID { get; set; }
        public int SAM_FYID { get; set; }
        public int SAM_AccountId { get; set; }
        public string SAM_SubCode { get; set; }
        public string SAM_SubDescription { get; set; }
        public string SAM_SubLongDesc { get; set; }
        public string SAM_Address1 { get; set; }
        public string SAM_Address2 { get; set; }
        public string SAM_Address3 { get; set; }
        public string SAM_Address4 { get; set; }
        public string SAM_OPhone { get; set; }
        public string SAM_RPhone { get; set; }
        public string SAM_FAX { get; set; }
        public string SAM_CellNo { get; set; }
        public string SAM_Email { get; set; }
        public string SAM_Website { get; set; }
        public string SAM_PAN { get; set; }
        public string SAM_CST { get; set; }
        public string SAM_SST { get; set; }
        public bool SAM_IsFund { get; set; }
        public bool IsActive1 { get; set; }
        public System.DateTime CreatedOn1 { get; set; }
        public System.DateTime ModifiedOn1 { get; set; }
        public string VoucherType { get; set; }
        public string Prefix { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal CloseingBalance { get; set; }
        public decimal SubBalance { get; set; }
        public decimal FinalBalance { get; set; }
    }

}

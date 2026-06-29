using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLogic;
using DAccess;
using BObject;
using BObject.AccountModel;

namespace BLogic
{
    public class AccountService
    {
        ICommonRepository common;
        //ILoginRepository login;
        IMasterRepository master;
        ITransactionRepository transaction;
        IAccountRepository accountRepository;
        public AccountService()
        {
            this.common = new CommonRepository();
            //this.login = new LoginRepository();
            this.master = new MasterRepository();
            this.transaction = new TransactionRepository();
            this.accountRepository = new AccountRepository();

        }

        public List<AccountGroup> getAccountGroupList()
        {
            List<AccountGroup> AccountGroupList = accountRepository.getAccountGroupList("AccountGroupMaster", null, null, null);
            return AccountGroupList;
        }

        public List<AccountMaster> GetAccountMasterDropdownList()
        {
            List<AccountMaster> AccountGroupList = accountRepository.GetAccountMasterDropdownList("AccountMaster", null, null, null);
            return AccountGroupList;
        }

        public int? InsertAccountMasterDetails(AccountMaster AccountMasterDetails)
        {
            string TransType = "INSERT";
            if (AccountMasterDetails.AM_AccountId != null && AccountMasterDetails.AM_AccountId != 0)
            {
                TransType = "UPDATE";
            }
            //if (StuAdminDetails.SemesterId > 1)
            //{
            //    TransType = "PROMOTE";
            //}


            return accountRepository.InsertUpdateAccountMaster(TransType, AccountMasterDetails);

        }

        public int? InsertAccountMasterOpeningDetails(AccountMaster AccountMasterDetails)
        {
            string TransType = "INSERT";
            if (AccountMasterDetails.AM_AccountOpId != 0)
            {
                TransType = "UPDATE";
            }
            //if (StuAdminDetails.SemesterId > 1)
            //{
            //    TransType = "PROMOTE";
            //}
            AccountMasterDetails.AM_AccountOpId = accountRepository.InsertAccountMasterOpeningDetails(TransType, AccountMasterDetails);


            return AccountMasterDetails.AM_AccountOpId;
        }


        public int? InsertSubAccountMasterOpeningDetails(SubAccountMaster SubAccountMasterDetails)
        {
            int? val = 0;
            string TransType = "INSERT";

            val = accountRepository.InsertSubAccountMasterOpeningDetails(TransType, SubAccountMasterDetails);


            return val;
        }

        public int? InsertVoucherEntryDetails(AccountLedger AccountLedgerS)
        {
            int? val = 0;
            string TransType = "INSERT";

            val = accountRepository.InsertVoucherEntryDetails(TransType, AccountLedgerS);


            return val;
        }

        //public List<AccountLedger> GetVoucherID(int? SessionId, string Type)
        //{
        //    return accountRepository.GetProvisionalPayment(SessionId, Type);
        //}
        public string InsertUpdateAccountMasterOpening(int? AccId)
        {

            return accountRepository.InsertUpdateAccountMasterOpening(AccId);
        }

        public int? InsertSubAccountMasterDetails(SubAccountMaster AccountMasterDetails)
        {
            string TransType = "INSERT";
            if (AccountMasterDetails.SAM_SubId != null && AccountMasterDetails.SAM_SubId != 0)
            {
                TransType = "UPDATE";
            }

            AccountMasterDetails.SAM_SubId = accountRepository.InsertUpdateSubAccountMaster(TransType, AccountMasterDetails);


            return AccountMasterDetails.SAM_SubId;
        }
        public List<AccountMaster> AccountMasterList(string TransType, AccountMaster AccountMaster)
        {
            List<AccountMaster> CFRecords = accountRepository.AccountMasterList(TransType, AccountMaster);
            return CFRecords;
        }
        public List<SubAccountMaster> SubAccountMasterOpeningbalanceList(string TransType, int? Id)
        {
            List<SubAccountMaster> CFRecords = accountRepository.SubAccountMasterOpeningbalanceList(TransType, Id);
            return CFRecords;
        }
        public List<SubAccountMaster> SubAccountMasterList(string TransType, SubAccountMaster SubAccountMaster)
        {
            List<SubAccountMaster> CFRecords = accountRepository.SubAccountMasterList(TransType, SubAccountMaster);
            return CFRecords;
        }

        public AccountMaster GetAccountMasterDetails(string TransType, int? Id)
        {

            AccountMaster AccountMasterDetails = accountRepository.GetAccountMasterDetails(TransType, Id);

            return AccountMasterDetails;
        }

        public SubAccountMaster GetSubAccountMasterDetails(string TransType, int? Id)
        {

            SubAccountMaster AccountMasterDetails = accountRepository.GetSubAccountMasterDetails(TransType, Id);
            AccountMasterDetails.AccountMasterList = accountRepository.GetAccountMasterDropdownList("AccountMaster", null, null, null);

            return AccountMasterDetails;
        }
        public List<AccountVoucherTypeMaster> AccountVoucherTypeList(AccountVoucherTypeMaster PerObj)
        {
            return accountRepository.AccountVoucherTypeList(PerObj);
        }

        public List<AccountLedger> VoucherEntryList(AccountLedger PerObj)
        {
            return accountRepository.VoucherEntryList(PerObj);

        }

        public vw_AccountLedger GetReportPaymentReceiptAccountLedger(int FYId, string FDate, string TDate)
        {
            return accountRepository.GetReportPaymentReceiptAccountLedger(FYId, FDate, TDate);
        }

        public vw_AccountLedger GetReportIncomeExpenceAccountLedger(int FYId, string FDate, string TDate)
        {
            return accountRepository.GetReportIncomeExpenceAccountLedger(FYId, FDate, TDate);
        }

        public string GetVoucherID(int? SessionId, int? VoucherTypeId)
        {
            return accountRepository.GetVoucherID(SessionId, VoucherTypeId);
        }

        //public List<TallyDataGet> TallyDataGet(string FDate, string TDate)
        //{
        //    return accountRepository.TallyDataGet(FDate, TDate);
        //}
        //public int? TallyDataPost(List<TallyDataPost> datas)
        //{
        //    return accountRepository.TallyDataPost(datas);
        //}


    }
   
}

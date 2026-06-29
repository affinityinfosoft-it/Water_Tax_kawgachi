using BObject.AccountModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLogic
{
  public  interface IAccountRepository
    {
        List<AccountGroup> getAccountGroupList(string TransType, long? PId, long? SId, long? XId);
        int? InsertUpdateAccountMaster(string TransType, AccountMaster AccountMasterDetails);
        int? InsertAccountMasterOpeningDetails(string TransType, AccountMaster AccountMasterDetails);
        int? InsertSubAccountMasterOpeningDetails(string TransType, SubAccountMaster SubAccountMasterDetails);
        int? InsertVoucherEntryDetails(string TransType, AccountLedger AccountLedgerS);
        string GetVoucherID(int? SessionId, int? VoucherTypeId);
        int? InsertUpdateSubAccountMaster(string TransType, SubAccountMaster AccountMasterDetails);
        string InsertUpdateAccountMasterOpening(int? AccId);
        List<AccountMaster> AccountMasterList(string TransType, AccountMaster AccountMaster);
        List<SubAccountMaster> SubAccountMasterOpeningbalanceList(string TransType, int? Id);
        List<AccountMaster> GetAccountMasterDropdownList(string TransType, long? PId, long? SId, long? XId);
        List<SubAccountMaster> SubAccountMasterList(string TransType, SubAccountMaster SubAccountMaster);
        AccountMaster GetAccountMasterDetails(string TransType, int? Id);
        SubAccountMaster GetSubAccountMasterDetails(string TransType, int? Id);
        List<AccountVoucherTypeMaster> AccountVoucherTypeList(AccountVoucherTypeMaster PerObj);
        List<AccountLedger> VoucherEntryList(AccountLedger PerObj);
        vw_AccountLedger GetReportPaymentReceiptAccountLedger(int FYId, string FDate, string TDate);
        vw_AccountLedger GetReportIncomeExpenceAccountLedger(int FYId, string FDate, string TDate);

    }
}

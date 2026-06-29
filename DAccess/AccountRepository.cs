using BObject;
using BObject.AccountModel;
using ERP.DAccess;
using IBLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;


namespace DAccess
{
  public  class AccountRepository: IAccountRepository
    {
        #region GetConnectionString
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ERP_DB_Conn"].ConnectionString;
        }
        #endregion


        #region getAccountGroupList
        public List<AccountGroup> getAccountGroupList(string TransType, long? PId, long? SId, long? XId)
        {
            List<AccountGroup> DDLObjList = new List<AccountGroup>();
            AccountGroup DDLObj = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", TransType));
            arrParams.Add(new SqlParameter("@PId", PId));
            arrParams.Add(new SqlParameter("@SId", SId));
            arrParams.Add(new SqlParameter("@XId", XId));
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GlobalSelect", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DDLObj = new AccountGroup();
                    DDLObj.GM_GroupId = Convert.ToInt32(rdr["Value"]);
                    DDLObj.GM_GroupDescription = Convert.ToString(rdr["Text"]);
                    DDLObjList.Add(DDLObj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return DDLObjList;
        }
        #endregion

        #region AccountMaster
        public int? InsertUpdateAccountMaster(string TransType, AccountMaster AccountMasterDetails)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@TransType", TransType));
            arrParams.Add(new SqlParameter("@AM_AccountId", AccountMasterDetails.AM_AccountId));
            arrParams.Add(new SqlParameter("@AM_AccountCode", AccountMasterDetails.AM_AccountCode));
            arrParams.Add(new SqlParameter("@AM_LongName", AccountMasterDetails.AM_LongName));
            arrParams.Add(new SqlParameter("@AM_AccDescription", AccountMasterDetails.AM_AccDescription));
            arrParams.Add(new SqlParameter("@AM_SuppressPayee", AccountMasterDetails.AM_SuppressPayee));
            arrParams.Add(new SqlParameter("@ISSubAc", AccountMasterDetails.ISSubAc));
            arrParams.Add(new SqlParameter("@AM_GroupId", AccountMasterDetails.AM_GroupId));
            arrParams.Add(new SqlParameter("@AM_CompanyID", AccountMasterDetails.AM_CompanyID));
            arrParams.Add(new SqlParameter("@AM_FYId", AccountMasterDetails.AM_FYId));
            arrParams.Add(new SqlParameter("@AM_IsFund", AccountMasterDetails.AM_IsFund));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_AccountMaster", arrParams.ToArray());
            int? val = Convert.ToInt32(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<AccountMaster> AccountMasterList(string TransType, AccountMaster AccountMaster)
        {
            List<AccountMaster> CFSjList = new List<AccountMaster>();
            AccountMaster CFSObj = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", TransType));

            arrParams.Add(new SqlParameter("@AM_GroupId", AccountMaster.AM_GroupId));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_AccountMaster", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    CFSObj = new AccountMaster();

                    CFSObj.AM_FYId = Convert.ToInt32(rdr["AM_FYId"]);
                    CFSObj.AM_GroupId = Convert.ToInt32(rdr["AM_GroupId"]);
                    CFSObj.AM_CompanyID = Convert.ToInt32(rdr["AM_CompanyID"]);
                    //CFSObj.SessionName = rdr["SessionName"].ToString();
                    CFSObj.AM_AccountId = Convert.ToInt32(rdr["AM_AccountId"]);
                    CFSObj.GM_GroupDescription = rdr["GM_GroupDescription"].ToString();

                    CFSObj.GM_GroupCode = rdr["GM_GroupCode"].ToString();

                    CFSObj.AM_AccountCode = rdr["AM_AccountCode"].ToString();
                    CFSObj.AM_LongName = rdr["AM_LongName"].ToString();
                    CFSObj.AM_OPeningType = rdr["AM_OPeningType"].ToString();
                    CFSObj.AM_ClosingType = rdr["AM_ClosingType"].ToString();
                    CFSObj.ISSubAc = rdr["ISSubAc"].ToString();
                    CFSObj.AM_AccDescription = rdr["AM_AccDescription"].ToString();

                    CFSObj.AM_TotalDebit = Convert.ToDecimal(rdr["AM_TotalDebit"]);
                    CFSObj.AM_TotalCredit = Convert.ToDecimal(rdr["AM_TotalCredit"]);
                    CFSObj.AM_ClosingBalance = Convert.ToDecimal(rdr["AM_ClosingBalance"]);
                    CFSObj.AM_OpeningBalance = Convert.ToDecimal(rdr["AM_OpeningBalance"]);
                    CFSObj.AM_IsFund = Convert.ToBoolean(rdr["AM_IsFund"]);

                    CFSjList.Add(CFSObj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return CFSjList;
        }
        public AccountMaster GetAccountMasterDetails(string TransType, int? Id)
        {
            AccountMaster AccountMaster = new AccountMaster();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", TransType));

            arrParams.Add(new SqlParameter("@AM_AccountId", Id));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_AccountMaster", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    AccountMaster.AM_FYId = Convert.ToInt32(rdr["AM_FYId"]);
                    AccountMaster.AM_GroupId = Convert.ToInt32(rdr["AM_GroupId"]);
                    AccountMaster.AM_CompanyID = Convert.ToInt32(rdr["AM_CompanyID"]);
                    //AccountMaster.SessionName = rdr["SessionName"].ToString();
                    AccountMaster.AM_AccountId = Convert.ToInt32(rdr["AM_AccountId"]);


                    AccountMaster.AM_AccountOpId = Convert.ToInt32(rdr["AM_AccountOpId"]);



                    AccountMaster.GM_GroupDescription = rdr["GM_GroupDescription"].ToString();

                    AccountMaster.GM_GroupCode = rdr["GM_GroupCode"].ToString();

                    AccountMaster.AM_AccDescription = rdr["AM_AccDescription"].ToString();

                    AccountMaster.AM_AccountCode = rdr["AM_AccountCode"].ToString();
                    AccountMaster.AM_LongName = rdr["AM_LongName"].ToString();
                    AccountMaster.AM_OPeningType = rdr["AM_OPeningType"].ToString();
                    AccountMaster.AM_ClosingType = rdr["AM_ClosingType"].ToString();
                    AccountMaster.AM_SuppressPayee = rdr["AM_SuppressPayee"].ToString();
                    AccountMaster.ISSubAc = rdr["ISSubAc"].ToString();


                    AccountMaster.AM_TotalDebit = Convert.ToDecimal(rdr["AM_TotalDebit"]);
                    AccountMaster.AM_TotalCredit = Convert.ToDecimal(rdr["AM_TotalCredit"]);
                    AccountMaster.AM_ClosingBalance = Convert.ToDecimal(rdr["AM_ClosingBalance"]);
                    AccountMaster.AM_OpeningBalance = Convert.ToDecimal(rdr["AM_OpeningBalance"]);
                    AccountMaster.AM_IsFund = Convert.ToBoolean(rdr["AM_IsFund"]);

                }
                rdr.Close();
            }
            rdr.Dispose();
            return AccountMaster;
        }
        public List<AccountMaster> GetAccountMasterDropdownList(string TransType, long? PId, long? SId, long? XId)
        {
            List<AccountMaster> DDLObjList = new List<AccountMaster>();
            AccountMaster DDLObj = null;

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", TransType));
            arrParams.Add(new SqlParameter("@PId", PId));
            arrParams.Add(new SqlParameter("@SId", SId));
            arrParams.Add(new SqlParameter("@XId", XId));
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GlobalSelect", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DDLObj = new AccountMaster();
                    DDLObj.AM_AccountId = Convert.ToInt32(rdr["Value"]);
                    DDLObj.AM_AccDescription = Convert.ToString(rdr["Text"]);
                    DDLObj.AM_AccountCode = Convert.ToString(rdr["Code"]);
                    DDLObjList.Add(DDLObj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return DDLObjList;
        }
        #endregion

        #region SubAccountMaster
        public int? InsertUpdateSubAccountMaster(string TransType, SubAccountMaster AccountMasterDetails)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", TransType));
            arrParams.Add(new SqlParameter("@SAM_CompanyID", AccountMasterDetails.SAM_CompanyID));
            arrParams.Add(new SqlParameter("@SAM_FYID", AccountMasterDetails.SAM_FYID));
            arrParams.Add(new SqlParameter("@SAM_SubId", AccountMasterDetails.SAM_SubId));
            arrParams.Add(new SqlParameter("@SAM_AccountId", AccountMasterDetails.SAM_AccountId));
            arrParams.Add(new SqlParameter("@SAM_SubCode", AccountMasterDetails.SAM_SubCode));
            arrParams.Add(new SqlParameter("@SAM_SubLongDesc", AccountMasterDetails.SAM_SubLongDesc));
            arrParams.Add(new SqlParameter("@SAM_SubDescription", AccountMasterDetails.SAM_SubDescription));
            arrParams.Add(new SqlParameter("@SAM_Address1", AccountMasterDetails.SAM_Address1));
            arrParams.Add(new SqlParameter("@SAM_Address2", AccountMasterDetails.SAM_Address2));
            arrParams.Add(new SqlParameter("@SAM_Address3", AccountMasterDetails.SAM_Address3));
            arrParams.Add(new SqlParameter("@SAM_Address4", AccountMasterDetails.SAM_Address4));
            arrParams.Add(new SqlParameter("@SAM_OPhone", AccountMasterDetails.SAM_OPhone));
            arrParams.Add(new SqlParameter("@SAM_FAX", AccountMasterDetails.SAM_FAX));
            arrParams.Add(new SqlParameter("@SAM_Email", AccountMasterDetails.SAM_Email));
            arrParams.Add(new SqlParameter("@SAM_Website", AccountMasterDetails.SAM_Website));
            arrParams.Add(new SqlParameter("@SAM_PAN", AccountMasterDetails.SAM_PAN));
            arrParams.Add(new SqlParameter("@SAM_CST", AccountMasterDetails.SAM_CST));
            arrParams.Add(new SqlParameter("@SAM_SST", AccountMasterDetails.SAM_SST));
            arrParams.Add(new SqlParameter("@SAM_IsFund", AccountMasterDetails.SAM_IsFund));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_SubAccountMaster", arrParams.ToArray());
            int? val = Convert.ToInt32(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<SubAccountMaster> SubAccountMasterList(string TransType, SubAccountMaster SubAccountMaster)
        {
            List<SubAccountMaster> CFSjList = new List<SubAccountMaster>();
            SubAccountMaster AccountMaster = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", TransType));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_SubAccountMaster", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    AccountMaster = new SubAccountMaster();
                    AccountMaster.SAM_AccountId = Convert.ToInt32(rdr["SAM_AccountId"]);
                    AccountMaster.SAM_SubId = Convert.ToInt32(rdr["SAM_SubId"]);
                    AccountMaster.SAM_CompanyID = Convert.ToInt32(rdr["SAM_CompanyID"]);
                    AccountMaster.SAM_FYID = Convert.ToInt32(rdr["SAM_FYID"]);
                    AccountMaster.SAM_SubCode = rdr["SAM_SubCode"].ToString();
                    AccountMaster.SAM_SubDescription = rdr["SAM_SubDescription"].ToString();
                    AccountMaster.SAM_SubLongDesc = rdr["SAM_SubLongDesc"].ToString();
                    AccountMaster.SAM_Address1 = rdr["SAM_Address1"].ToString();
                    AccountMaster.SAM_Address2 = rdr["SAM_Address2"].ToString();
                    AccountMaster.SAM_Address3 = rdr["SAM_Address3"].ToString();
                    AccountMaster.SAM_Address4 = rdr["SAM_Address4"].ToString();
                    AccountMaster.SAM_OPhone = rdr["SAM_OPhone"].ToString();
                    AccountMaster.SAM_RPhone = rdr["SAM_RPhone"].ToString();
                    AccountMaster.SAM_FAX = rdr["SAM_FAX"].ToString();
                    AccountMaster.SAM_CellNo = rdr["SAM_CellNo"].ToString();
                    AccountMaster.SAM_Email = rdr["SAM_Email"].ToString();
                    AccountMaster.SAM_Website = rdr["SAM_Website"].ToString();
                    AccountMaster.SAM_PAN = rdr["SAM_PAN"].ToString();
                    AccountMaster.SAM_CST = rdr["SAM_CST"].ToString();
                    AccountMaster.SAM_SST = rdr["SAM_SST"].ToString();
                    AccountMaster.SAM_IsFund = Convert.ToBoolean(rdr["SAM_IsFund"]);
                    CFSjList.Add(AccountMaster);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return CFSjList;
        }
        public SubAccountMaster GetSubAccountMasterDetails(string TransType, int? Id)
        {
            SubAccountMaster AccountMaster = new SubAccountMaster();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", TransType));

            arrParams.Add(new SqlParameter("@SAM_SubId", Id));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_SubAccountMaster", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    AccountMaster.SAM_SubId = Convert.ToInt32(rdr["SAM_SubId"]);
                    AccountMaster.SAM_AccountId = Convert.ToInt32(rdr["SAM_AccountId"]);

                    AccountMaster.SAM_CompanyID = Convert.ToInt32(rdr["SAM_CompanyID"]);
                    AccountMaster.SAM_FYID = Convert.ToInt32(rdr["SAM_FYID"]);
                    AccountMaster.SAM_SubCode = rdr["SAM_SubCode"].ToString();
                    AccountMaster.SAM_SubDescription = rdr["SAM_SubDescription"].ToString();
                    AccountMaster.SAM_SubLongDesc = rdr["SAM_SubLongDesc"].ToString();
                    AccountMaster.SAM_Address1 = rdr["SAM_Address1"].ToString();
                    AccountMaster.SAM_Address2 = rdr["SAM_Address2"].ToString();
                    AccountMaster.SAM_Address3 = rdr["SAM_Address3"].ToString();
                    AccountMaster.SAM_Address4 = rdr["SAM_Address4"].ToString();
                    AccountMaster.SAM_OPhone = rdr["SAM_OPhone"].ToString();
                    AccountMaster.SAM_RPhone = rdr["SAM_RPhone"].ToString();
                    AccountMaster.SAM_FAX = rdr["SAM_FAX"].ToString();
                    AccountMaster.SAM_CellNo = rdr["SAM_CellNo"].ToString();
                    AccountMaster.SAM_Email = rdr["SAM_Email"].ToString();
                    AccountMaster.SAM_Website = rdr["SAM_Website"].ToString();
                    AccountMaster.SAM_PAN = rdr["SAM_PAN"].ToString();
                    AccountMaster.SAM_CST = rdr["SAM_CST"].ToString();
                    AccountMaster.SAM_SST = rdr["SAM_SST"].ToString();
                    AccountMaster.SAM_IsFund = Convert.ToBoolean(rdr["SAM_IsFund"]);

                }
                rdr.Close();
            }
            rdr.Dispose();
            return AccountMaster;
        }
        #endregion

        #region OpeningBalanceAccountMaster
        public int? InsertAccountMasterOpeningDetails(string TransType, AccountMaster AccountMasterDetails)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@TransType", TransType));

            arrParams.Add(new SqlParameter("@AM_AccountId", AccountMasterDetails.AM_AccountId));
            arrParams.Add(new SqlParameter("@AM_AccountOpId", AccountMasterDetails.AM_AccountOpId));
            arrParams.Add(new SqlParameter("@AM_AccountCode", AccountMasterDetails.AM_AccountCode));
            arrParams.Add(new SqlParameter("@AM_OPeningType", AccountMasterDetails.AM_OPeningType));
            arrParams.Add(new SqlParameter("@AM_OpeningBalance", AccountMasterDetails.AM_OpeningBalance));
            arrParams.Add(new SqlParameter("@AM_FYId", AccountMasterDetails.AM_FYId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_AccountMasterOpening", arrParams.ToArray());
            int? val = Convert.ToInt32(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public int? InsertSubAccountMasterOpeningDetails(string TransType, SubAccountMaster SubAccountMasterDetails)
        {
            int? val = 0;

            foreach (var EME in SubAccountMasterDetails.SubAccountMasterList)
            {
                List<SqlParameter> arrParams = new List<SqlParameter>();

                arrParams.Add(new SqlParameter("@TransType", TransType));
                arrParams.Add(new SqlParameter("@SAM_AccountId", EME.SAM_AccountId));
                arrParams.Add(new SqlParameter("@SAM_AccountCode", EME.SAM_SubCode));
                arrParams.Add(new SqlParameter("@SAM_OPeningType", EME.SAM_OPeningType));
                arrParams.Add(new SqlParameter("@SAM_OpeningBalance", EME.SAM_OpeningBalance));
                arrParams.Add(new SqlParameter("@SAM_FYId", SubAccountMasterDetails.SAM_FYID));
                arrParams.Add(new SqlParameter("@AM_AccountId", SubAccountMasterDetails.AM_AccountId));
                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
                OutPutId.Direction = ParameterDirection.Output;
                arrParams.Add(OutPutId);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_SubAccountMasterOpening", arrParams.ToArray());
                val = Convert.ToInt32(arrParams[arrParams.Count - 1].Value);

            }
            return val;
        }
        public string InsertUpdateAccountMasterOpening(int? AccId)
        {
            long val = 0;
            int EnrollmentId = 0;
            string EnqId = "";
            try
            {
                using (var cn = new SqlConnection(GetConnectionString()))
                {

                    string _sql = @"DELETE FROM [dbo].AccountMaster_AMBalance WHERE AM_AccountId = @AM_AccountId ";//AND SessionId = @SessionId
                    var cmd = new SqlCommand(_sql, cn);
                    cmd.Parameters.Add(new SqlParameter("@AM_AccountId", SqlDbType.VarChar, 50)).Value = AccId;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }



                List<SqlParameter> arrParamsN = new List<SqlParameter>();
                arrParamsN.Add(new SqlParameter("@TransType", "UPDATEDETAILS"));
                arrParamsN.Add(new SqlParameter("@AM_AccountId", AccId));
                SqlParameter OutPutIdN = new SqlParameter("@OutPutId", SqlDbType.BigInt);
                OutPutIdN.Direction = ParameterDirection.Output;
                arrParamsN.Add(OutPutIdN);

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_AccountMasterOpening", arrParamsN.ToArray());
                EnrollmentId = Convert.ToInt32(arrParamsN[arrParamsN.Count - 1].Value);

            }

            catch (Exception Ex)
            {
                throw Ex;
            }
            return EnqId;
        }
        public List<SubAccountMaster> SubAccountMasterOpeningbalanceList(string TransType, int? Id)
        {
            List<SubAccountMaster> CFSjList = new List<SubAccountMaster>();
            SubAccountMaster CFSObj = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", TransType));

            arrParams.Add(new SqlParameter("@AM_AccountId", Id));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_SubAccountMasterOpeningBalance", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    CFSObj = new SubAccountMaster();


                    CFSObj.AM_AccountId = Convert.ToInt32(rdr["AM_AccountId"]);
                    CFSObj.SAM_AccountId = Convert.ToInt32(rdr["SAM_AccountId"]);

                    CFSObj.AM_AccountCode = rdr["AM_AccountCode"].ToString();
                    CFSObj.AM_AccDescription = rdr["AM_AccDescription"].ToString();
                    CFSObj.SAM_SubCode = rdr["SAM_SubCode"].ToString();
                    CFSObj.SAM_SubDescription = rdr["SAM_SubDescription"].ToString();


                    CFSObj.SAM_OPeningType = rdr["SAM_OPeningType"].ToString();
                    CFSObj.SAM_ClosingType = rdr["SAM_ClosingType"].ToString();



                    CFSObj.SAM_TotalDebit = Convert.ToDecimal(rdr["SAM_TotalDebit"]);
                    CFSObj.SAM_TotalCredit = Convert.ToDecimal(rdr["SAM_TotalCredit"]);
                    CFSObj.SAM_ClosingBalance = Convert.ToDecimal(rdr["SAM_ClosingBalance"]);
                    CFSObj.SAM_OpeningBalance = Convert.ToDecimal(rdr["SAM_OpeningBalance"]);

                    CFSjList.Add(CFSObj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return CFSjList;
        }

        #endregion

        #region voucherEntry
        public int? InsertVoucherEntryDetails(string TransType, AccountLedger AccountLedgerS)
        {
            int? val = 0;
            int? LD_LedgerID = 0;

            foreach (var EME in AccountLedgerS.AccountLedgerList)
            {
                List<SqlParameter> arrParams = new List<SqlParameter>();

                arrParams.Add(new SqlParameter("@TransType", TransType));
                arrParams.Add(new SqlParameter("@LD_AccountID", EME.LD_AccountID));
                arrParams.Add(new SqlParameter("@LD_AccountCode", EME.LD_AccountCode ?? (object)DBNull.Value));
                arrParams.Add(new SqlParameter("@LD_SubID", EME.LD_SubID));
                arrParams.Add(new SqlParameter("@LD_SubCode", EME.LD_SubCode ?? (object)DBNull.Value));
                arrParams.Add(new SqlParameter("@LD_FundAccountID", EME.LD_FundAccountID));
                arrParams.Add(new SqlParameter("@LD_FundSubID", EME.LD_FundSubID));
                arrParams.Add(new SqlParameter("@LD_DrAmount", EME.LD_DrAmount));
                arrParams.Add(new SqlParameter("@LD_CrAmount", EME.LD_CrAmount));
                arrParams.Add(new SqlParameter("@LD_DrCr", EME.LD_DrCr ?? (object)DBNull.Value));
                arrParams.Add(new SqlParameter("@LD_Remarks", EME.LD_Remarks ?? (object)DBNull.Value));
                arrParams.Add(new SqlParameter("@LD_VoucherTypeId", EME.LD_VoucherTypeId));
                arrParams.Add(new SqlParameter("@LD_FYId", AccountLedgerS.LD_FYId));
                arrParams.Add(new SqlParameter("@LD_CompanyId", AccountLedgerS.LD_CompanyId));
                arrParams.Add(new SqlParameter("@LD_ChequeDate",
                AccountLedgerS.LD_ChequeDate == default(DateTime) ? (object)DBNull.Value : AccountLedgerS.LD_ChequeDate));
                arrParams.Add(new SqlParameter("@LD_ChequeNo", AccountLedgerS.LD_ChequeNo ?? (object)DBNull.Value));
                arrParams.Add(new SqlParameter("@LD_ReferenceNo", AccountLedgerS.LD_ReferenceNo ?? (object)DBNull.Value));
                arrParams.Add(new SqlParameter("@LD_Payee", AccountLedgerS.LD_Payee ?? (object)DBNull.Value));
                arrParams.Add(new SqlParameter("@LD_Narration", AccountLedgerS.LD_Narration ?? (object)DBNull.Value));
                arrParams.Add(new SqlParameter("@LD_VoucherNo", AccountLedgerS.LD_VoucherNo ?? (object)DBNull.Value));
                arrParams.Add(new SqlParameter("@LD_LedgerID", LD_LedgerID));
                DateTime parsedDate;
                if (DateTime.TryParseExact(AccountLedgerS.LD_DateS, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    arrParams.Add(new SqlParameter("@LD_Date", parsedDate));
                }
                else
                {
                    arrParams.Add(new SqlParameter("@LD_Date", DateTime.Now));
                }

                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                arrParams.Add(OutPutId);

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_LedgerVoucherEntry", arrParams.ToArray());

                val = Convert.ToInt32(OutPutId.Value);

                if (val > 0 && LD_LedgerID == 0)
                {
                    LD_LedgerID = val;
                }
            }

            return val;
        }
        public string GetVoucherID(int? SessionId, int? VoucherTypeId)
        {
            string Text = "";
            List<SqlParameter> arrParams = new List<SqlParameter>
    {
                new SqlParameter("@SessionId", SessionId), // Only this is needed
        new SqlParameter("@VoucherTypeId", VoucherTypeId) // Only this is needed
    };

            SqlDataReader rdr = SqlHelper.ExecuteReader(
                GetConnectionString(),
                CommandType.StoredProcedure,
                "SP_GetAccountVoucherNO",
                arrParams.ToArray()
            );

            if (rdr != null)
            {
                while (rdr.Read())
                {
                    Text = Convert.ToString(rdr["VoucherNO"]);
                }
                rdr.Close();
            }

            rdr.Dispose();
            return Text;
        }
        public List<AccountVoucherTypeMaster> AccountVoucherTypeList(AccountVoucherTypeMaster PerObj)
        {
            List<AccountVoucherTypeMaster> List = new List<AccountVoucherTypeMaster>();
            AccountVoucherTypeMaster Obj = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@VoucherTypeId", PerObj.VoucherTypeId));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_AccountVoucherTypeMaster", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    Obj = new AccountVoucherTypeMaster();

                    Obj.VoucherTypeId = Convert.ToInt32(rdr["VoucherTypeId"]);
                    Obj.VoucherType = rdr["VoucherType"].ToString();

                    List.Add(Obj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return List;
        }
        public List<AccountLedger> VoucherEntryList(AccountLedger PerObj)
        {
            List<AccountLedger> List = new List<AccountLedger>();
            AccountLedger Obj = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));
            arrParams.Add(new SqlParameter("@LD_VoucherTypeId", PerObj.LD_VoucherTypeId));
            arrParams.Add(new SqlParameter("@LD_FYId", PerObj.LD_FYId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_LedgerVoucherEntry", arrParams.ToArray());
            if (rdr != null) 
            {
                while (rdr.Read())
                {
                    Obj = new AccountLedger();

                    Obj.LD_VoucherNo = Convert.ToString(rdr["LD_VoucherNo"]);
                    Obj.LD_ReferenceNo = Convert.ToString(rdr["LD_ReferenceNo"]);
                    Obj.LD_DateS = Convert.ToString(rdr["LD_DateS"]);
                    Obj.LD_Narration = Convert.ToString(rdr["LD_Narration"]);
                    Obj.LD_CrAmount = Convert.ToDecimal(rdr["LD_CrAmount"]);
                    Obj.LD_DrAmount = Convert.ToDecimal(rdr["LD_DrAmount"]);
                    Obj.LD_LedgerID = Convert.ToInt32(rdr["LD_LedgerID"]);
                    List.Add(Obj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return List;
        }
        #endregion


        #region GetReportPaymentReceiptAccountLedger
        public vw_AccountLedger GetReportPaymentReceiptAccountLedger(int FYId, string FDate, string TDate)
        {

            vw_AccountLedger AccountLedgerS = new vw_AccountLedger();
            DataSet ds = new DataSet();
            GenericList Gen = new GenericList();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@FYId", FYId));
            if (FDate != null && FDate != "" && TDate != null && TDate != "")
            {
                arrParams.Add(new SqlParameter("@FDate", FDate));
                arrParams.Add(new SqlParameter("@TDate", TDate));
            }

            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "SP_ReportPaymentReceiptAccountLedger", arrParams.ToArray());
            if (ds != null && ds.Tables.Count > 0)
            {

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    AccountLedgerS.vw_AccountLedgerList = Gen.ConvertDataTable<vw_AccountLedger>(dt);
                }
            }
            return AccountLedgerS;
        }
        #endregion

        #region GetReportIncomeExpenceAccountLedger
        public vw_AccountLedger GetReportIncomeExpenceAccountLedger(int FYId, string FDate, string TDate)
        {

            vw_AccountLedger AccountLedgerS = new vw_AccountLedger();
            DataSet ds = new DataSet();
            GenericList Gen = new GenericList();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@FYId", FYId));
            if (FDate != null && FDate != "" && TDate != null && TDate != "")
            {
                arrParams.Add(new SqlParameter("@FDate", FDate));
                arrParams.Add(new SqlParameter("@TDate", TDate));
            }

            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "SP_ReportIncomeExpenceAccountLedger", arrParams.ToArray());
            if (ds != null && ds.Tables.Count > 0)
            {

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    AccountLedgerS.vw_AccountLedgerList = Gen.ConvertDataTable<vw_AccountLedger>(dt);
                }
            }
            return AccountLedgerS;
        }
        #endregion


    }
}

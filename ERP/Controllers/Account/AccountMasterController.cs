using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BObject.AccountModel;
namespace ERP.Controllers.Account
{
    public class AccountMasterController : BaseController
    {
        // GET: AccountMaster
        public ActionResult Index()
        {
            return View();
        }

        #region Accountmaster
        public ActionResult AccountMaster(int? Id)
        {
            if (UserModel == null) return returnLogin("~/AccountMaster/AccountMasterList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            AccountMaster AccountMaster = new AccountMaster();

            if (Id != null && Id != 0)
            {
                AccountMaster = accountService.GetAccountMasterDetails("SELECT-ONE", Id);
            }
            AccountMaster.AccountGroupList = accountService.getAccountGroupList();
            return View(AccountMaster);
        }
        [HttpPost]
        public ActionResult AccountMaster(FormCollection FC, int? Id)
        {
            if (UserModel == null) return returnLogin("~/AccountMaster/AccountMasterList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            AccountMaster AccountMaster = new AccountMaster();
            AccountMaster.AM_AccountId = Convert.ToInt32(FC["hdnAccountId"] == "" ? "0" : FC["hdnAccountId"]);
            AccountMaster.AM_AccountCode = Convert.ToString(FC["txtAccountCode"]);
            AccountMaster.AM_CompanyID = Convert.ToInt32(UserModel.CM_ID);
            AccountMaster.AM_FYId = Convert.ToInt32(UserModel.FyId);
            AccountMaster.AM_LongName = Convert.ToString(FC["txtAccountName"]);
            AccountMaster.AM_AccDescription = Convert.ToString(FC["txtAccountDesc"]);
            AccountMaster.AM_GroupId = Convert.ToInt32(FC["ddlAcgroup"] == "" ? "0" : FC["ddlAcgroup"]);
            AccountMaster.ISSubAc = Convert.ToString(FC["ISSubAc"]);
            Boolean chk = Convert.ToBoolean(FC["AM_SuppressPayee"].Split(',')[0]);
            if (chk == true)
            {
                AccountMaster.AM_SuppressPayee = "Y";
            }
            else
            {
                AccountMaster.AM_SuppressPayee = "N";
            }

            AccountMaster.AM_IsFund = Convert.ToBoolean(FC["AM_IsFund"].Split(',')[0]);


            try
            {

                int? AccId = accountService.InsertAccountMasterDetails(AccountMaster);
                if (AccountMaster.AM_AccountId > 0)
                {
                    AccountMaster = accountService.GetAccountMasterDetails("SELECT-ONE", AccId);
                }

                if (AccId > 0)
                {
                    TempData["SuccessMessage"] = "Record saved successfully!";
                    return RedirectToAction("AccountMaster"); // Redirects to GET
                }
                else if (AccId == -100)
                {
                    ViewBag.Message = "Account code already exists";
                    ViewBag.MessageId = "-100";
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Message = "Record not saved successfully...";
                ViewBag.MessageId = "0";
            }
            AccountMaster.AccountGroupList = accountService.getAccountGroupList();
            return View(AccountMaster);
        }

        [HttpGet]
        public ActionResult AccountMasterList()
        {
            if (UserModel == null) return returnLogin("~/AccountMaster/AccountMasterList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            AccountMaster FModel = new AccountMaster();
            FModel.AM_FYId = Convert.ToInt32(UserModel.FyId);
            FModel.AM_CompanyID = Convert.ToInt32(UserModel.CM_ID);
            FModel.AccountGroupList = accountService.getAccountGroupList();
            if (FModel.AccountGroupList != null && FModel.AccountGroupList.Count > 0)
                FModel.AM_GroupId = null;

            var FSRecords = accountService.AccountMasterList("SELECT-ONE", FModel);

            if (FSRecords.Count > 0)
            {
                FModel.AccountMasterList = FSRecords;
            }

            return View(FModel);
        }
        [HttpPost]
        public ActionResult AccountMasterList(FormCollection FC)
        {
            if (UserModel == null) return returnLogin("~/AccountMaster/AccountMasterList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            AccountMaster FModel = new AccountMaster();

            FModel.AM_FYId = Convert.ToInt32(UserModel.FyId);
            FModel.AM_CompanyID = Convert.ToInt32(UserModel.CM_ID);
            FModel.AccountGroupList = accountService.getAccountGroupList();
            if (FModel.AccountGroupList != null && FModel.AccountGroupList.Count > 0)
                FModel.AM_GroupId = null;
            FModel.AM_GroupId = Convert.ToInt32(FC["ddlAcgroup"] == "" ? "0" : FC["ddlAcgroup"]);


            var FSRecords = accountService.AccountMasterList("SELECT-ALL", FModel);
            if (FSRecords.Count>0)
            {
                FModel.AccountMasterList = FSRecords;
            }
            
            return View(FModel);
        }

        #endregion

        #region SubAccountMaster
        public ActionResult SubAccountMaster(int? Id)
        {
            if (UserModel == null) return returnLogin("~/AccountMaster/SubAccountMasterList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            //GetRights(url);
            SubAccountMaster AccountMaster = new SubAccountMaster();
            //ViewBag.CollegeName = UserModel.UserCollege.CollegeName;
            //ViewBag.AcademicYear = UserModel.AcademicYear;
            //ViewBag.Role = UserModel.RoleName;
            ViewBag.TUserId = UserModel.UserId;
            ViewBag.RoleId = UserModel.RoleId;
            //ViewBag.CollegeId = UserModel.UserCollege.CollegeId;

            AccountMaster.AccountMasterList = accountService.GetAccountMasterDropdownList();
            if (Id != null && Id != 0)
            {
                AccountMaster = accountService.GetSubAccountMasterDetails("SELECT-ONE", Id);
            }

            return View(AccountMaster);
        }
        [HttpPost]
        public ActionResult SubAccountMaster(FormCollection FC, int? Id)
        {
            if (UserModel == null) return returnLogin("~/AccountMaster/SubAccountMasterList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            SubAccountMaster AccountMaster = new SubAccountMaster();
            AccountMaster.SAM_SubId = Convert.ToInt32(FC["hdnAccountId"] == "" ? "0" : FC["hdnAccountId"]);
            AccountMaster.SAM_AccountId = Convert.ToInt32(FC["ddlAccount"] == "" ? "0" : FC["ddlAccount"]);
            AccountMaster.SAM_SubCode = Convert.ToString(FC["txtAccountCode"]);
            AccountMaster.SAM_CompanyID = Convert.ToInt32(UserModel.CM_ID);
            AccountMaster.SAM_FYID = Convert.ToInt32(UserModel.FyId);

            AccountMaster.SAM_SubLongDesc = Convert.ToString(FC["txtAccountName"]);
            AccountMaster.SAM_SubDescription = Convert.ToString(FC["txtAccountDesc"]);
            AccountMaster.SAM_Address1 = Convert.ToString(FC["txtAddress1"]);
            AccountMaster.SAM_Address2 = Convert.ToString(FC["txtAddress2"]);
            AccountMaster.SAM_Address3 = Convert.ToString(FC["txtAddress3"]);
            AccountMaster.SAM_Address4 = Convert.ToString(FC["txtAddress4"]);
            AccountMaster.SAM_OPhone = Convert.ToString(FC["txtOPhone"]);
            AccountMaster.SAM_FAX = Convert.ToString(FC["txtFAX"]);
            AccountMaster.SAM_Email = Convert.ToString(FC["txtEmail"]);
            AccountMaster.SAM_Website = Convert.ToString(FC["txtWebsite"]);
            AccountMaster.SAM_PAN = Convert.ToString(FC["txtPAN"]);
            AccountMaster.SAM_CST = Convert.ToString(FC["txtCST"]);
            AccountMaster.SAM_SST = Convert.ToString(FC["txtSST"]);
            // AccountMaster.SAM_IsFund = Convert.ToBoolean(FC["SAM_IsFund"].Split(',')[0]);
            AccountMaster.SAM_IsFund = !string.IsNullOrEmpty(FC["SAM_IsFund"]) && FC["SAM_IsFund"].Split(',')[0] == "true";
            try
            {
                int? AccId = accountService.InsertSubAccountMasterDetails(AccountMaster);
                TempData["Success"] = "Record saved successfully!";
                return RedirectToAction("SubAccountMaster", new { id = AccId });  // Redirect to GET with TempData
            }
            catch (Exception Ex)
            {
                ViewBag.Message = "Record not saved successfully...";
                ViewBag.MessageId = "0";
            }

            return View(AccountMaster);
        }
        [HttpGet]
        public ActionResult SubAccountMasterList()
        {
            if (UserModel == null) return returnLogin("~/AccountMaster/SubAccountMasterList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            SubAccountMaster FModel = new SubAccountMaster();
            FModel.SAM_FYID = UserModel.FyId;
            FModel.SAM_CompanyID = 1;
            var FSRecords = accountService.SubAccountMasterList("SELECT-ONE", FModel);
            if (FSRecords.Count > 0)
            {
                FModel.SubAccountMasterList = FSRecords;
            }
            return View(FModel);
        }
        [HttpPost]
        public ActionResult SubAccountMasterList(FormCollection FC)
        {
            if (UserModel == null) return returnLogin("~/AccountMaster/SubAccountMasterList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            SubAccountMaster FModel = new SubAccountMaster();

            FModel.SAM_FYID = UserModel.FyId; 
            FModel.SAM_CompanyID = 1;    
            var FSRecords = accountService.SubAccountMasterList("SELECT-ALL", FModel);
            if (FSRecords.Count > 0)
            {
                FModel.SubAccountMasterList = FSRecords;
            }

            return View(FModel);
        }
        [HttpGet]
        #endregion

        #region OpeningBalanceAccountMaster
        public ActionResult OpeningBalanceAccountMaster(int? Id)
        {
            if (UserModel == null) return returnLogin("~/AccountMaster/OpeningBalanceMasterList");
            var url = "/AccountMaster/OpeningBalanceMasterList";//Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            AccountMaster AccountMaster = new AccountMaster();
            //ViewBag.CollegeName = UserModel.UserCollege.CollegeName;
            //ViewBag.AcademicYear = UserModel.AcademicYear;
            //ViewBag.Role = UserModel.RoleName;
            ViewBag.TUserId = UserModel.UserId;
            ViewBag.RoleId = UserModel.RoleId;
            //ViewBag.CollegeId = UserModel.UserCollege.CollegeId;

            if (Id != null && Id != 0)
            {
                AccountMaster = accountService.GetAccountMasterDetails("SELECT-ONE", Id);
            }
            return View(AccountMaster);
        }
        [HttpPost]
        public ActionResult OpeningBalanceAccountMaster(FormCollection FC, int? Id)
        {
            if (UserModel == null) return returnLogin("~/AccountMaster/OpeningBalanceMasterList");
            var url = "/AccountMaster/OpeningBalanceMasterList";//Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            //ViewBag.CollegeName = UserModel.UserCollege.CollegeName;
            //ViewBag.AcademicYear = UserModel.AcademicYear;
            //ViewBag.Role = UserModel.RoleName;
            ViewBag.TUserId = UserModel.UserId;
            ViewBag.RoleId = UserModel.RoleId;
            //ViewBag.CollegeId = UserModel.UserCollege.CollegeId;
            AccountMaster AccountMaster = new AccountMaster();
            AccountMaster.AM_AccountId = Convert.ToInt32(FC["hdnAccountId"] == "" ? "0" : FC["hdnAccountId"]);
            AccountMaster.AM_AccountOpId = Convert.ToInt32(FC["hdnAccountOpId"] == "" ? "0" : FC["hdnAccountOpId"]);
            AccountMaster.AM_AccountCode = Convert.ToString(FC["hdnAccountCode"]);
            AccountMaster.AM_CompanyID = 1;    /*UserModel.UserCollege.CollegeId;*/
            AccountMaster.AM_FYId = UserModel.FyId;
            AccountMaster.AM_OpeningBalance = Convert.ToDecimal(FC["txtOpeningBalance"] == "" ? "0" : FC["txtOpeningBalance"]);
            AccountMaster.AM_OPeningType = Convert.ToString(FC["ddlOpeningType"]);

            try
            {

                int? AccId = accountService.InsertAccountMasterOpeningDetails(AccountMaster);
                AccountMaster = accountService.GetAccountMasterDetails("SELECT-ONE", AccountMaster.AM_AccountId);



                ViewBag.Message = "Record saved successfully...";
                ViewBag.MessageId = "1";


            }
            catch (Exception Ex)
            {
                ViewBag.Message = "Record not saved successfully...";
                ViewBag.MessageId = "0";
            }
            return View(AccountMaster);
        }

        [HttpGet]
        public ActionResult OpeningBalanceSubAccountMaster(int? Id)
        {
            if (UserModel == null) return returnLogin("~/AccountMaster/OpeningBalanceMasterList");
            var url = "/AccountMaster/OpeningBalanceMasterList";//Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            SubAccountMaster SubAccountMaster = new SubAccountMaster();
            //ViewBag.CollegeName = UserModel.UserCollege.CollegeName;
            //ViewBag.AcademicYear = UserModel.AcademicYear;
            //ViewBag.Role = UserModel.RoleName;
            ViewBag.TUserId = UserModel.UserId;
            ViewBag.RoleId = UserModel.RoleId;
            //ViewBag.CollegeId = UserModel.UserCollege.CollegeId;
            if (Id != null && Id != 0)
            {
                SubAccountMaster.SubAccountMasterList = accountService.SubAccountMasterOpeningbalanceList("SELECT-ONE", Id);
            }
            return View(SubAccountMaster);
        }
        [HttpPost]
        public ActionResult OpeningBalanceSubAccountMaster(FormCollection FC, int? Id)
        {
            if (UserModel == null) return returnLogin("~/AccountMaster/OpeningBalanceMasterList");
            var url = "/AccountMaster/OpeningBalanceMasterList";//Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            //ViewBag.CollegeName = UserModel.UserCollege.CollegeName;
            //ViewBag.AcademicYear = UserModel.AcademicYear;
            //ViewBag.Role = UserModel.RoleName;
            ViewBag.TUserId = UserModel.UserId;
            ViewBag.RoleId = UserModel.RoleId;
            //ViewBag.CollegeId = UserModel.UserCollege.CollegeId;
            SubAccountMaster SubAccountMaster = new SubAccountMaster();
            int? AccId = Convert.ToInt32(FC["hdnAccountId"] == "" ? "0" : FC["hdnAccountId"]);
            SubAccountMaster.AM_AccountId = AccId;
            SubAccountMaster.SAM_CompanyID = 1;/*UserModel.UserCollege.CollegeId;*/
            SubAccountMaster.SAM_FYID = UserModel.FyId;  /*UserModel.SessionId;*/

            for (int i = 1; i < 1000; ++i)
            {
                var obj = new SubAccountMaster();

                if (FC["hdnSubAccountId_" + i.ToString()] != null)
                {

                    obj.SAM_AccountId = Convert.ToInt32(FC["hdnSubAccountId_" + i.ToString()]);
                    obj.SAM_SubCode = Convert.ToString(FC["hdnSubAccountCode_" + i.ToString()]);
                    obj.SAM_OpeningBalance = Convert.ToDecimal(FC["txtOpeningBalance_" + i.ToString()] == "" ? "0" : FC["txtOpeningBalance_" + i.ToString()]);
                    obj.SAM_OPeningType = Convert.ToString(FC["OpeningType_" + i.ToString()]);

                    SubAccountMaster.SubAccountMasterList.Add(obj);
                }
                else
                {
                    break;
                }
            }

            try
            {

                int? val = accountService.InsertSubAccountMasterOpeningDetails(SubAccountMaster);
                SubAccountMaster.SubAccountMasterList = accountService.SubAccountMasterOpeningbalanceList("SELECT-ONE", AccId);
                if (val > 0)
                {
                    accountService.InsertUpdateAccountMasterOpening(AccId);
                }


                ViewBag.Message = "Record saved successfully...";
                ViewBag.MessageId = "1";


            }
            catch (Exception Ex)
            {
                ViewBag.Message = "Record not saved successfully...";
                ViewBag.MessageId = "0";
            }
            return View(SubAccountMaster);
        }

        [HttpGet]
        public ActionResult OpeningBalanceMasterList()
        {
            if (UserModel == null) return returnLogin("~/AccountMaster/OpeningBalanceMasterList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            ViewBag.TUserId = UserModel.UserId;
            ViewBag.RoleId = UserModel.RoleId;
            AccountMaster FModel = new AccountMaster();
            FModel.AM_FYId = UserModel.FyId; 
            FModel.AM_CompanyID = 1;
            FModel.AccountMasterList = accountService.AccountMasterList("SELECT-ALL", FModel);
            return View(FModel);
        }
        #endregion

        #region VoucherEntry
        [HttpGet]
        public ActionResult VoucherEntry(int? VoucherTypeId, int? Id)
        {
            if (UserModel == null) return returnLogin("~/AccountMaster/VoucherEntryList?VoucherTypeId=" + VoucherTypeId + ""); 
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            AccountLedger AccountLedgerS = new AccountLedger();

            if (VoucherTypeId == 0 || VoucherTypeId == null)
            {

                return returnLogin("~/AccountMaster/VoucherEntryList?VoucherTypeId=" + VoucherTypeId + "");

            }
            else
            {
                AccountVoucherTypeMaster AccountVoucherTypeMasterS = new AccountVoucherTypeMaster();
                AccountVoucherTypeMasterS.VoucherTypeId = VoucherTypeId;
                AccountVoucherTypeMasterS.AccountVoucherTypeMasterList = accountService.AccountVoucherTypeList(AccountVoucherTypeMasterS);
                if (AccountVoucherTypeMasterS.AccountVoucherTypeMasterList.Count() > 0)
                {
                    AccountLedgerS.VoucherType = AccountVoucherTypeMasterS.AccountVoucherTypeMasterList.FirstOrDefault().VoucherType;
                }
                else
                {
                    return returnLogin("~/AccountMaster/VoucherEntryList?VoucherTypeId=" + VoucherTypeId + "");


                }
            }

            ViewBag.TUserId = UserModel.UserId;
            ViewBag.RoleId = UserModel.RoleId;
            AccountLedgerS.LD_VoucherTypeId = VoucherTypeId;
            AccountLedgerS.LD_VoucherNo = accountService.GetVoucherID(0, VoucherTypeId);
            AccountMaster AModel = new AccountMaster();
            AModel.AM_FYId = UserModel.FyId;
            AModel.AM_CompanyID = Convert.ToInt32(UserModel.CM_ID);
            AccountLedgerS.AccountMasterList = accountService.AccountMasterList("SELECT-ALL", AModel);
            SubAccountMaster SAModel = new SubAccountMaster();
            SAModel.SAM_FYID = UserModel.FyId;
            SAModel.SAM_CompanyID =Convert.ToInt32(UserModel.CM_ID);
            AccountLedgerS.SubAccountMasterList = accountService.SubAccountMasterList("SELECT-ALL", SAModel);

            if (VoucherTypeId == 1 || VoucherTypeId == 2)
            {
                if (AccountLedgerS.AccountMasterList.Count() > 0)
                {
                    foreach (var item in AccountLedgerS.AccountMasterList.Where(a => a.AM_AccountCode.ToUpper() == "CA"))
                    {
                        AccountLedger Account = new AccountLedger();

                        Account.Value = item.AM_AccountId + "-" + "0";
                        Account.Text = item.AM_LongName;
                        Account.LD_IsFund = item.AM_IsFund;
                        AccountLedgerS.AccountList.Add(Account);
                    }
                }
                if (AccountLedgerS.SubAccountMasterList.Count() > 0)
                {
                    foreach (var Acc in AccountLedgerS.AccountMasterList.Where(a => a.AM_AccountCode.ToUpper() == "BK"))
                    {
                        foreach (var item in AccountLedgerS.SubAccountMasterList)
                        {
                            if (Acc.AM_AccountId == item.SAM_AccountId)
                            {
                                AccountLedger Account = new AccountLedger();

                                Account.Value = item.SAM_AccountId + "-" + item.SAM_SubId;
                                Account.Text = item.SAM_SubLongDesc;
                                Account.LD_IsFund = item.SAM_IsFund;
                                AccountLedgerS.AccountList.Add(Account);
                            }

                        }
                    }

                }


                if (AccountLedgerS.AccountMasterList.Count() > 0)
                {
                    foreach (var item in AccountLedgerS.AccountMasterList.Where(a => a.ISSubAc.ToUpper() == "NO"))
                    {
                        AccountLedger Account = new AccountLedger();

                        Account.Value = item.AM_AccountId + "-" + "0";
                        Account.Text = item.AM_LongName;
                        Account.LD_IsFund = item.AM_IsFund;
                        AccountLedgerS.ParticularsList.Add(Account);
                    }
                }

                if (AccountLedgerS.SubAccountMasterList.Count() > 0)
                {
                    foreach (var item in AccountLedgerS.SubAccountMasterList)
                    {
                        AccountLedger Account = new AccountLedger();

                        Account.Value = item.SAM_AccountId + "-" + item.SAM_SubId;
                        Account.Text = item.SAM_SubLongDesc;
                        Account.LD_IsFund = item.SAM_IsFund;
                        AccountLedgerS.ParticularsList.Add(Account);
                    }
                }

            }
            else
            {
                if (AccountLedgerS.AccountMasterList.Count() > 0)
                {
                    foreach (var item in AccountLedgerS.AccountMasterList.Where(a => a.ISSubAc.ToUpper() == "NO"))
                    {
                        AccountLedger Account = new AccountLedger();

                        Account.Value = item.AM_AccountId + "-" + "0";
                        Account.Text = item.AM_LongName;
                        Account.LD_IsFund = item.AM_IsFund;
                        AccountLedgerS.ParticularsList.Add(Account);
                    }
                }

                if (AccountLedgerS.SubAccountMasterList.Count() > 0)
                {
                    foreach (var item in AccountLedgerS.SubAccountMasterList)
                    {
                        AccountLedger Account = new AccountLedger();

                        Account.Value = item.SAM_AccountId + "-" + item.SAM_SubId;
                        Account.Text = item.SAM_SubLongDesc;
                        Account.LD_IsFund = item.SAM_IsFund;
                        AccountLedgerS.ParticularsList.Add(Account);
                    }
                }
            }

            return View(AccountLedgerS);
        }
        [HttpPost]
        public ActionResult VoucherEntry(FormCollection FC)
        {
            if (UserModel == null)
                return returnLogin("~/AccountMaster/VoucherEntryList?VoucherTypeId=" + Convert.ToInt32(FC["LD_VoucherTypeId"]));

            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);

            ViewBag.TUserId = UserModel.UserId;
            ViewBag.RoleId = UserModel.RoleId;

            AccountLedger AccountLedgerS = new AccountLedger
            {
                LD_CompanyId = Convert.ToInt32(UserModel.CM_ID),
                LD_FYId = Convert.ToInt32(UserModel.FyId),
                LD_DateS = Convert.ToString(FC["txtDateS"]),
                LD_VoucherTypeId = Convert.ToInt32(FC["LD_VoucherTypeId"])
            };

            if (AccountLedgerS.LD_VoucherTypeId != 3)
            {
                string chequeDateInput = FC["LD_ChequeDate"];
                DateTime parsedDate;

                if (string.IsNullOrWhiteSpace(chequeDateInput) ||
                    !DateTime.TryParseExact(chequeDateInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    parsedDate = DateTime.Now;
                }

                AccountLedgerS.LD_ChequeDate = parsedDate;
                AccountLedgerS.LD_VoucherNo = Convert.ToString(FC["txtVoucherNo"]);
                AccountLedgerS.LD_ChequeNo = Convert.ToString(FC["txtChequeNo"]);
                AccountLedgerS.AccountValue = Convert.ToString(FC["ddlAccount"]);
            }

            AccountLedgerS.LD_ReferenceNo = Convert.ToString(FC["txtReferenceNo"]);
            AccountLedgerS.LD_Payee = Convert.ToString(FC["txtPayee"]);
            AccountLedgerS.LD_Narration = Convert.ToString(FC["txtNarration"]);

            int RowCount = Convert.ToInt32(string.IsNullOrWhiteSpace(FC["RowCount"]) ? "0" : FC["RowCount"]);
            decimal Amount = 0;

            for (int i = 0; i < RowCount; ++i)
            {
                if (FC["Particulars_" + i] != null)
                {
                    var obj = new AccountLedger();
                    var ParticularsValue = FC["Particulars_" + i].Split('-');
                    obj.LD_AccountID = Convert.ToInt32(ParticularsValue[0]);
                    obj.LD_SubID = Convert.ToInt32(ParticularsValue[1]);

                    var FundValue = FC["Fund_" + i].Split('-');
                    obj.LD_FundAccountID = Convert.ToInt32(FundValue[0]);
                    obj.LD_FundSubID = Convert.ToInt32(FundValue[1]);

                    obj.LD_DrCr = Convert.ToString(FC["DrCr_" + i]);
                    decimal lineAmount = Convert.ToDecimal(FC["Amount_" + i]);

                    if (obj.LD_DrCr == "C")
                        obj.LD_CrAmount = lineAmount;
                    else
                        obj.LD_DrAmount = lineAmount;

                    obj.LD_Remarks = Convert.ToString(FC["Remarks_" + i]);
                    obj.LD_VoucherTypeId = AccountLedgerS.LD_VoucherTypeId;

                    AccountLedgerS.AccountLedgerList.Add(obj);
                    Amount += lineAmount;
                }
                else break;
            }

            if (AccountLedgerS.LD_VoucherTypeId == 1 || AccountLedgerS.LD_VoucherTypeId == 2)
            {
                var AccountValue = AccountLedgerS.AccountValue.Split('-');
                var obj = new AccountLedger
                {
                    LD_AccountID = Convert.ToInt32(AccountValue[0]),
                    LD_SubID = Convert.ToInt32(AccountValue[1]),
                    LD_VoucherTypeId = AccountLedgerS.LD_VoucherTypeId
                };

                if (AccountLedgerS.LD_VoucherTypeId == 1)
                {
                    obj.LD_CrAmount = Amount;
                    obj.LD_DrCr = "C";
                }
                else
                {
                    obj.LD_DrAmount = Amount;
                    obj.LD_DrCr = "D";
                }

                AccountLedgerS.AccountLedgerList.Add(obj);
            }

            try
            {
                int? val = accountService.InsertVoucherEntryDetails(AccountLedgerS);
                ViewBag.LD_LedgerID = val;

                int? VoucherTypeId = AccountLedgerS.LD_VoucherTypeId;
                AccountLedgerS = new AccountLedger();

                if (VoucherTypeId == null || VoucherTypeId == 0)
                    return returnLogin("~/AccountMaster/VoucherEntryList?VoucherTypeId=" + Convert.ToInt32(FC["LD_VoucherTypeId"]));

                var AccountVoucherTypeMasterS = new AccountVoucherTypeMaster
                {
                    VoucherTypeId = VoucherTypeId
                };
                AccountVoucherTypeMasterS.AccountVoucherTypeMasterList = accountService.AccountVoucherTypeList(AccountVoucherTypeMasterS);
                AccountLedgerS.VoucherType = AccountVoucherTypeMasterS.AccountVoucherTypeMasterList.FirstOrDefault()?.VoucherType ?? "";

                AccountLedgerS.LD_VoucherTypeId = VoucherTypeId;
                AccountMaster AModel = new AccountMaster
                {
                    AM_FYId = Convert.ToInt32(UserModel.FyId),
                    AM_CompanyID = Convert.ToInt32(UserModel.CM_ID)
                };
                AccountLedgerS.AccountMasterList = accountService.AccountMasterList("SELECT-ALL", AModel);

                SubAccountMaster SAModel = new SubAccountMaster
                {
                    SAM_FYID = Convert.ToInt32(UserModel.FyId),
                    SAM_CompanyID = Convert.ToInt32(UserModel.CM_ID)
                };
                AccountLedgerS.SubAccountMasterList = accountService.SubAccountMasterList("SELECT-ALL", SAModel);
                var accList = AccountLedgerS.AccountMasterList;
                var subList = AccountLedgerS.SubAccountMasterList;

                if (VoucherTypeId == 1 || VoucherTypeId == 2)
                {
                    foreach (var item in accList.Where(a => a.AM_AccountCode.ToUpper() == "CA"))
                        AccountLedgerS.AccountList.Add(new AccountLedger
                        {
                            Value = item.AM_AccountId + "-0",
                            Text = item.AM_LongName,
                            LD_IsFund = item.AM_IsFund
                        });

                    foreach (var acc in accList.Where(a => a.AM_AccountCode.ToUpper() == "BK"))
                        foreach (var item in subList.Where(s => acc.AM_AccountId == s.SAM_AccountId))
                            AccountLedgerS.AccountList.Add(new AccountLedger
                            {
                                Value = item.SAM_AccountId + "-" + item.SAM_SubId,
                                Text = item.SAM_SubLongDesc,
                                LD_IsFund = item.SAM_IsFund
                            });

                    foreach (var item in accList.Where(a => a.ISSubAc.ToUpper() == "NO"))
                        AccountLedgerS.ParticularsList.Add(new AccountLedger
                        {
                            Value = item.AM_AccountId + "-0",
                            Text = item.AM_LongName,
                            LD_IsFund = item.AM_IsFund
                        });

                    foreach (var item in subList)
                        AccountLedgerS.ParticularsList.Add(new AccountLedger
                        {
                            Value = item.SAM_AccountId + "-" + item.SAM_SubId,
                            Text = item.SAM_SubLongDesc,
                            LD_IsFund = item.SAM_IsFund
                        });
                }
                else
                {
                    foreach (var item in accList.Where(a => a.ISSubAc.ToUpper() == "NO"))
                        AccountLedgerS.ParticularsList.Add(new AccountLedger
                        {
                            Value = item.AM_AccountId + "-0",
                            Text = item.AM_LongName,
                            LD_IsFund = item.AM_IsFund
                        });

                    foreach (var item in subList)
                        AccountLedgerS.ParticularsList.Add(new AccountLedger
                        {
                            Value = item.SAM_AccountId + "-" + item.SAM_SubId,
                            Text = item.SAM_SubLongDesc,
                            LD_IsFund = item.SAM_IsFund
                        });
                }

                ViewBag.Message = "Record saved successfully...";
                ViewBag.MessageId = "1";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Record not saved successfully. Error: " + ex.Message;
                ViewBag.MessageId = "0";
            }

            return View(AccountLedgerS);
        }
        [HttpGet]
        public ActionResult VoucherEntryList(int? VoucherTypeId)
        {

            if (UserModel == null) return returnLogin("~/AccountMaster/VoucherEntryList?VoucherTypeId="+ VoucherTypeId + "");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            AccountLedger AccountLedgerS = new AccountLedger();

            if (VoucherTypeId == 0 || VoucherTypeId == null)
            {

               return returnLogin("~/AccountMaster/VoucherEntryList?VoucherTypeId=" + VoucherTypeId + "");

            }
            else
            {
                AccountVoucherTypeMaster AccountVoucherTypeMasterS = new AccountVoucherTypeMaster();
                AccountVoucherTypeMasterS.VoucherTypeId = VoucherTypeId;
                AccountVoucherTypeMasterS.AccountVoucherTypeMasterList = accountService.AccountVoucherTypeList(AccountVoucherTypeMasterS);
                if (AccountVoucherTypeMasterS.AccountVoucherTypeMasterList.Count() > 0)
                {
                    AccountLedgerS.VoucherType = AccountVoucherTypeMasterS.AccountVoucherTypeMasterList.FirstOrDefault().VoucherType;
                }
                else
                {
                   return returnLogin("~/AccountMaster/VoucherEntryList?VoucherTypeId=" + VoucherTypeId + "");

                }
                AccountLedgerS.LD_VoucherTypeId = VoucherTypeId;
                AccountLedgerS.LD_FYId = Convert.ToInt32(UserModel.FyId);  /* UserModel.SessionId;*/
                AccountLedgerS.AccountLedgerList = accountService.VoucherEntryList(AccountLedgerS);
                var FSRecords = AccountLedgerS.AccountLedgerList;

                if (FSRecords.Count > 0)
                {
                    AccountLedgerS.AccountLedgerList = FSRecords;
                }
            }
            return View(AccountLedgerS);
        }
        #endregion


    }
}
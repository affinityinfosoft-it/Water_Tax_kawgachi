using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BObject.AccountModel;

namespace ERP.Controllers.Account
{
    public class AccountReportController:BaseController
    {
        public ActionResult ReceiptPaymentReport(int? FYId, string FDate, string TDate)
        {

            if (UserModel == null) return returnLogin("~/AccountReport/ReceiptPaymentReport");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            vw_AccountLedger vw_AccountLedgerS = new vw_AccountLedger();
          
            if (FYId > 0 && FDate != null && FDate != "" && TDate != null && TDate != "")
            {
                ViewBag.FDate = FDate;
                ViewBag.TDate = TDate;
                vw_AccountLedgerS = accountService.GetReportPaymentReceiptAccountLedger(Convert.ToInt16(FYId), FDate, TDate);
            }
            return View(vw_AccountLedgerS);
        }

        public ActionResult ReportIncomeExpenceReport(int? FYId, string FDate, string TDate)
        {
            if (UserModel == null) return returnLogin("~/AccountReport/ReportIncomeExpenceReport");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            vw_AccountLedger vw_AccountLedgerS = new vw_AccountLedger();
         
            if (FYId > 0 && FDate != null && FDate != "" && TDate != null && TDate != "")
            {
                ViewBag.FDate = FDate;
                ViewBag.TDate = TDate;
                vw_AccountLedgerS = accountService.GetReportIncomeExpenceAccountLedger(Convert.ToInt16(FYId), FDate, TDate);
            }
            return View(vw_AccountLedgerS);
        }
        #region  
        public ActionResult VoucherEntryReport()
        {
            if (UserModel == null) return returnLogin("~/AccountReport/ReportIncomeExpenceReport");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            vw_AccountLedger vw_AccountLedgerS = new vw_AccountLedger();
           
            return View();
        }

        #endregion

    }
}
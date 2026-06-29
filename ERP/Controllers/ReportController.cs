using BObject;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers
{
    public class ReportController : BaseController
    {
        // GET: Report
        #region Receipt Report
        public ActionResult BeneficaryConnection(int?id)
        {
            if (UserModel == null) return returnLogin("~/Report/BeneficaryConnection");
           // var PM_RcptNo =Request.QueryString["PM_RcptNo"].ToString();
            PartyMaster_PM TEntity = new PartyMaster_PM();
            if (id != null)
            {
                TEntity.PM_PartyId = (decimal)id;
                TEntity = service.GetBeneficaryReport<PartyMaster_PM>(TEntity, "usp_Report");
            }
                        return View(TEntity);
        }
        public ActionResult FormSaleReport(int? id)
        {
            if (UserModel == null) return returnLogin("~/Report/FormSaleReport");
            //var BILLNO = Request.QueryString["BILLNO"].ToString();
            FORMSALE TEntity = new FORMSALE();
            if (id != null)
            {
                TEntity.ID = (decimal)id;
                TEntity = service.GetFormSaleReport<FORMSALE>(TEntity, "usp_Report");
            }
          
            return View(TEntity);
        }
        public ActionResult FerrulChargesReport(int? id)
        {
            if (UserModel == null) return returnLogin("~/Report/FerrulChargesReport");
            //var BILLNO = Request.QueryString["BILLNO"].ToString();
            ferrulMaster TEntity = new ferrulMaster();
            if (id != null)
            {
                TEntity.FerulId = (Int64)id;
                TEntity = service.GetFerrulChargesReport<ferrulMaster>(TEntity, "usp_Report");
            }
           
            return View(TEntity);
        }
        public ActionResult WaterTaxCollectionReport(int? id)
        {
            if (UserModel == null) return returnLogin("~/Report/WaterTaxCollectionReport");
            //var BILLNO = Request.QueryString["BILLNO"].ToString();
            PartyTax_PT TEntity = new PartyTax_PT();
            TEntity.PT_ID = (Int64)id;
            TEntity = service.WaterTaxCollectionReport<PartyTax_PT>(TEntity, "usp_Report");
            return View(TEntity);
        }


        public ActionResult ReConnection(String GS_BillNo)
        {
            if (UserModel == null) return returnLogin("~/Report/BeneficaryConnection");
            RepReConnection rcb = new RepReConnection();
            //rc.GS_SIID = (Int64)id;
            rcb.GS_BillNo = GS_BillNo;
            rcb = service.GetReConnectionReport<RepReConnection>(rcb, "usp_Report").FirstOrDefault();

            rcb.RepInsItemList = service.GetReConnectionReport<RepReConnection>(rcb, "usp_Report");
            // <RepReConnection>(rc, "usp_Report");
            return View(rcb);

        }
        public ActionResult ConsumerInvoice(string GS_BillNo)
        {
            if (UserModel == null) return returnLogin("~/Report/ConsumerInvoice");
            RepReConnection rcb = new RepReConnection();
            //rc.GS_SIID = (Int64)id;
            rcb.GS_BillNo = GS_BillNo;
            rcb = service.GetConsumerinvoiceReport<RepReConnection>(rcb, "usp_Report").FirstOrDefault();

            rcb.RepInsItemList = service.GetConsumerinvoiceReport<RepReConnection>(rcb, "usp_Report");
            // <RepReConnection>(rc, "usp_Report");
            return View(rcb);
        }
        public ActionResult ConsumerPayment(string PL_BillNo)
        {
            if (UserModel == null) return returnLogin("~/Report/ConsumerInvoice");
            PartyLedger_PL PLL = new PartyLedger_PL();
            //rc.GS_SIID = (Int64)id;
            PLL.PL_BillNo = PL_BillNo;
            PLL = service.GetConsumerPaymentReport<PartyLedger_PL>(PLL, "usp_Report").FirstOrDefault();

            PLL.PartyLadgerList = service.GetConsumerPaymentReport<PartyLedger_PL>(PLL, "usp_Report");
            return View(PLL);
        }

        public ActionResult TankBooking(int? id)
        {
            if (UserModel == null) return returnLogin("~/Report/TankBooking");
            // var PM_RcptNo =Request.QueryString["PM_RcptNo"].ToString();
            TankBooking_TB TEntity = new TankBooking_TB();
            if (id != null)
            {
                TEntity.AP_ID = (Int32)id;
                TEntity = service.GetTankBookingReport<TankBooking_TB>(TEntity, "usp_Report");
            }
            return View(TEntity);
        }
        public ActionResult VanBooking(int? id)
        {
            if (UserModel == null) return returnLogin("~/Report/VanBooking");
            // var PM_RcptNo =Request.QueryString["PM_RcptNo"].ToString();
            VanBooking_VB TEntity = new VanBooking_VB();
            if (id != null)
            {
                TEntity.AP_ID = (Int64)id;
                TEntity = service.GetVanBookingReport<VanBooking_VB>(TEntity, "usp_Report");
            }
            return View(TEntity);
        }
        #endregion

        #region Main Report 
        //Add by Uttaran 2023-12-13 Reports////
        #region BeneficiaryConnectionRegister Report
        public ActionResult BeneficiaryConnectionRegister()
        {
            if (UserModel == null) return returnLogin("~/Report/BeneficiaryConnectionRegister");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            PartyMaster_PM PartyMaster = new PartyMaster_PM();
            PartyMaster.BeneficiryList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "PM_PartyId", null);
            var AreaList = service.GetGlobalSelect<AreaMaster_AM>("AreaMaster_AM", "AM_AreaID", null);
            ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaID", "AM_AreaName");
            var ParaList = service.GetGlobalSelect<ParaMaster_PM>("ParaMaster_PM", "PM_ParaId", null);
            ViewBag.PM_ParaId = new SelectList(ParaList, "PM_ParaId", "PM_ParaName");
            return View(PartyMaster);
            
        }
        #endregion
        #region FromSales Report
        public ActionResult FormSalesReport()
        {
            if (UserModel == null) return returnLogin("~/Transaction/FormSalesList");
            GetRights("/Transaction/FormSalesList");
            FORMSALE FORMSALE = new FORMSALE();
            FORMSALE.FyId = UserModel.FyId;
            FORMSALE.CM_ID = UserModel.CM_ID;
            FORMSALE.FORMSALEList = service.GetFORMSALE<FORMSALE>(FORMSALE, "usp_FORMSALE");
            var AreaList = service.GetGlobalSelect<AreaMaster_AM>("AreaMaster_AM", "AM_AreaID", null);
            ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaID", "AM_AreaName");
            var ParaList = service.GetGlobalSelect<ParaMaster_PM>("ParaMaster_PM", "PM_ParaId", null);
            ViewBag.PM_ParaId = new SelectList(ParaList, "PM_ParaId", "PM_ParaName");
            return View(FORMSALE);
        }

      

        #endregion
        #region FerrulCharges Report 
        public ActionResult FerrulChargeReport()
        {
            if (UserModel == null) return returnLogin("~/Transaction/FerrulChargeList");
            GetRights("/Transaction/FerrulChargeList");
            ferrulMaster fm = new ferrulMaster();
            fm.FyId = UserModel.FyId;
            fm.CM_ID = UserModel.CM_ID;
            fm.ferrulMasterList = service.GetFerrul<ferrulMaster>(fm, "usp_FerrulCharge");
            var AreaList = service.GetGlobalSelect<AreaMaster_AM>("AreaMaster_AM", "AM_AreaID", null);
            ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaID", "AM_AreaName");
            var ParaList = service.GetGlobalSelect<ParaMaster_PM>("ParaMaster_PM", "PM_ParaId", null);
            ViewBag.PM_ParaId = new SelectList(ParaList, "PM_ParaId", "PM_ParaName");
            return View(fm);
        }

        ///Direct Download pdf///
        //public ActionResult FerrulChargesReports()
        //{
        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/Views/Report/RptFiles/FerrulChargeReport.rpt")));
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    try
        //    {
        //        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        return File(stream, "application/pdf", "Ferrul Charges Report.pdf");
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        #endregion
        #region WaterTax Collection Report
        public ActionResult WaterTaxCollectionReports()
        {
            if (UserModel == null) return returnLogin("~/Transaction/WaterTaxCollectionList");
            GetRights("/Transaction/WaterTaxCollectionList");
            PartyTax_PT pt = new PartyTax_PT();
            pt.FyId = UserModel.FyId;
            pt.CM_ID = UserModel.CM_ID;
            pt.PartyTax_PTList = service.GetWaterTaxCollection<PartyTax_PT>(pt, "usp_PartyTax_PT");
            var AreaList = service.GetGlobalSelect<AreaMaster_AM>("AreaMaster_AM", "AM_AreaID", null);
            ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaID", "AM_AreaName");
            var ParaList = service.GetGlobalSelect<ParaMaster_PM>("ParaMaster_PM", "PM_ParaId", null);
            ViewBag.PM_ParaId = new SelectList(ParaList, "PM_ParaId", "PM_ParaName");
            return View(pt);
        }

        ///Direct Download pdf///
        //public ActionResult WaterTaxCollectionReportss()
        //{
        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/Views/Report/RptFiles/WaterTaxCollectionReport.rpt")));
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    try
        //    {
        //        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        return File(stream, "application/pdf", "WaterTaxCollectionReport.pdf");
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        #endregion
        #region Reconnection Report
        public ActionResult ReConnectionReport()
        {
            if (UserModel == null) return returnLogin("~/Transaction/ReConnectionList");
            GetRights("/Transaction/ReConnectionList");
            RepReConnection rc = new RepReConnection();
            rc.FyId = UserModel.FyId;
            rc.CM_ID = UserModel.CM_ID;
            rc.RepInsItemList = service.GetReconnection<RepReConnection>(rc, "usp_ReConnection");
            var AreaList = service.GetGlobalSelect<AreaMaster_AM>("AreaMaster_AM", "AM_AreaID", null);
            ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaID", "AM_AreaName");
            var ParaList = service.GetGlobalSelect<ParaMaster_PM>("ParaMaster_PM", "PM_ParaId", null);
            ViewBag.PM_ParaId = new SelectList(ParaList, "PM_ParaId", "PM_ParaName");
            return View(rc);
        }
        #endregion
        #region Defaulter Report
        public ActionResult DefaulterReport()
        {
            if (UserModel == null) return returnLogin("~/Report/BeneficiaryConnectionRegister");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            PartyMaster_PM PartyMaster = new PartyMaster_PM();
            PartyMaster.BeneficiryList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "PM_PartyId", null);
            var AreaList = service.GetGlobalSelect<AreaMaster_AM>("AreaMaster_AM", "AM_AreaID", null);
            ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaID", "AM_AreaName");
            var ParaList = service.GetGlobalSelect<ParaMaster_PM>("ParaMaster_PM", "PM_ParaId", null);
            ViewBag.PM_ParaId = new SelectList(ParaList, "PM_ParaId", "PM_ParaName");
            //var PartyList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "PM_PartyName", null);
            return View(PartyMaster);

        }
        #endregion
        #region Collection Register Report
        public ActionResult CollectionRegister()
        {
            if (UserModel == null) return returnLogin("~/Report/BeneficiaryConnectionRegister");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            PartyMaster_PM PartyMaster = new PartyMaster_PM();
            PartyMaster.BeneficiryList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "PM_PartyId", null);
            var AreaList = service.GetGlobalSelect<AreaMaster_AM>("AreaMaster_AM", "AM_AreaID", null);
            ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaID", "AM_AreaName");
            var ParaList = service.GetGlobalSelect<ParaMaster_PM>("ParaMaster_PM", "PM_ParaId", null);
            ViewBag.PM_ParaId = new SelectList(ParaList, "PM_ParaId", "PM_ParaName");
            return View(PartyMaster);

        }
        #endregion
        #region ConsumerInvoiceReport
        public ActionResult ConsumerInvoiceReport()
        {
            if (UserModel == null) return returnLogin("~/Report/ConsumerInvoiceReport");
            RepReConnection rc = new RepReConnection();
            rc.FyId = UserModel.FyId;
            rc.CM_ID = UserModel.CM_ID;
            rc.RepInsItemList = service.GetConInv<RepReConnection>(rc, "usp_ConsumerInv");
            var AreaList = service.GetGlobalSelect<AreaMaster_AM>("AreaMaster_AM", "AM_AreaID", null);
            ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaID", "AM_AreaName");
            var ParaList = service.GetGlobalSelect<ParaMaster_PM>("ParaMaster_PM", "PM_ParaId", null);
            ViewBag.PM_ParaId = new SelectList(ParaList, "PM_ParaId", "PM_ParaName");
            return View(rc);
        }
        #endregion
        #region Defaulter Letter
        public ActionResult DefaulterLetter()
        {
            if (UserModel == null) return returnLogin("~/Report/BeneficiaryConnectionRegister");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            PartyMaster_PM PartyMaster = new PartyMaster_PM();
            PartyMaster.BeneficiryList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "PM_PartyId", null);
            var AreaList = service.GetGlobalSelect<AreaMaster_AM>("AreaMaster_AM", "AM_AreaID", null);
            ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaID", "AM_AreaName");
            var ParaList = service.GetGlobalSelect<ParaMaster_PM>("ParaMaster_PM", "PM_ParaId", null);
            ViewBag.PM_ParaId = new SelectList(ParaList, "PM_ParaId", "PM_ParaName");
            //var PartyList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "PM_PartyName", null);
            return View(PartyMaster);
            //if (UserModel == null) return returnLogin("~/Report/BeneficiaryConnectionRegister");
            //var url = Request.RequestContext.HttpContext.Request.RawUrl;
            //GetRights(url);
            //PartyMaster_PM PartyMaster = new PartyMaster_PM();
            ////PartyMaster.PartyList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "PM_PartyCode", null);
            //var PartyList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "PM_PartyCode", null);
            //return View(PartyMaster);

        }
        #endregion
        #region TankBooking Report
        public ActionResult TankBookingReport()
        {
            if (UserModel == null) return returnLogin("~/Report/TankBookingReport");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            TankBooking_TB Tankbooking = new TankBooking_TB();
            Tankbooking.ListBooking = service.GetGlobalSelect<TankBooking_TB>("TankBooking_TB", "AP_ID", null);
            var AreaList = service.GetGlobalSelect<AreaMaster_AM>("AreaMaster_AM", "AM_AreaID", null);
            ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaID", "AM_AreaName");
            var ParaList = service.GetGlobalSelect<ParaMaster_PM>("ParaMaster_PM", "PM_ParaId", null);
            ViewBag.PM_ParaId = new SelectList(ParaList, "PM_ParaId", "PM_ParaName");
            return View(Tankbooking);

        }
        #endregion
        #region Van Booking Report
        public ActionResult VanBookingReport()
        {
            if (UserModel == null) return returnLogin("~/Report/VanBookingReport");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            VanBooking_VB Vanbooking = new VanBooking_VB();
            Vanbooking.ListBooking = service.GetGlobalSelect<VanBooking_VB>("VanBooking_VB", "AP_ID", null);
            var AreaList = service.GetGlobalSelect<AreaMaster_AM>("AreaMaster_AM", "AM_AreaID", null);
            ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaID", "AM_AreaName");
            var ParaList = service.GetGlobalSelect<ParaMaster_PM>("ParaMaster_PM", "PM_ParaId", null);
            ViewBag.PM_ParaId = new SelectList(ParaList, "PM_ParaId", "PM_ParaName");
            return View(Vanbooking);

        }
        #endregion
        #endregion
    }
}



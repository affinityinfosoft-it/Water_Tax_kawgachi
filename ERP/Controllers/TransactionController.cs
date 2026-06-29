using BObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers
{
    public class TransactionController : BaseController
    {
        // GET: Transaction
        #region ConsumerPayment
        public ActionResult ConsumerPayment(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Transaction/ConsumerPayment");
            GetRights("/Transaction/ConsumerPaymentList");
            var partyList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "PM_PartyId", null);
            PartyLedger_PL pl = new PartyLedger_PL();
            PartyLedger_PL partyL = new PartyLedger_PL();
            if (Id != null)
            {
                pl.PL_Id = (decimal)Id;
                pl.CM_ID = UserModel.CM_ID;
                partyL.PL_Id = (decimal)Id;
                partyL.CM_ID = UserModel.CM_ID;
                partyL.FyId = UserModel.FyId;
                pl = service.GetConsumerDetailsById<PartyLedger_PL>(pl, "usp_ConsumerPayment");
                ViewBag.PM_PartyCode = new SelectList(partyList, "PM_PartyCode", "PM_PartyName", pl.PL_PartyCode);
                pl.PartyLadgerList = service.GetConsumerPaymentEditList<PartyLedger_PL>(partyL, "usp_ConsumerPayment");
                pl.PM_PartyCode = pl.PL_PartyCode;

                // Null check for Due
                var Due = service.GetConsumerDueList<PartyLedger_PL>(pl, "usp_ConsumerPayment").FirstOrDefault();
                if (Due != null)
                {
                    ViewBag.DueAmt = Due.PL_PaidAmount;
                }
                else
                {
                    ViewBag.DueAmt = 0;  // Set a default value if Due is null
                }

                ViewBag.Button = "UPDATE";
            }
            else
            {
                ViewBag.PM_PartyCode = new SelectList(partyList, "PM_PartyCode", "PM_PartyName");
                ViewBag.Button = "SUBMIT";
            }
            return View(pl);
        }
        //public ActionResult ConsumerPayment(int? Id)
        //{
        //    if (UserModel == null) return returnLogin("~/Transaction/ConsumerPayment");
        //    GetRights("/Transaction/ConsumerPaymentList");
        //    var partyList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "PM_PartyId", null);
        //    PartyLedger_PL pl = new PartyLedger_PL();
        //    PartyLedger_PL partyL = new PartyLedger_PL();
        //    if (Id != null)
        //    {
        //        pl.PL_Id = (decimal)Id;
        //        pl.CM_ID = UserModel.CM_ID;
        //        partyL.PL_Id = (decimal)Id;
        //        partyL.CM_ID = UserModel.CM_ID;
        //        partyL.FyId = UserModel.FyId;
        //        pl = service.GetConsumerDetailsById<PartyLedger_PL>(pl, "usp_ConsumerPayment");
        //        ViewBag.PM_PartyCode = new SelectList(partyList, "PM_PartyCode", "PM_PartyName", pl.PL_PartyCode);
        //        pl.PartyLadgerList = service.GetConsumerPaymentEditList<PartyLedger_PL>(partyL, "usp_ConsumerPayment");
        //        pl.PM_PartyCode = pl.PL_PartyCode;
        //        var Due = service.GetConsumerDueList<PartyLedger_PL>(pl, "usp_ConsumerPayment").FirstOrDefault();
        //        ViewBag.DueAmt = Due.PL_PaidAmount;
        //        ViewBag.Button = "UPDATE";
        //    }
        //    else
        //    {
        //        ViewBag.PM_PartyCode = new SelectList(partyList, "PM_PartyCode", "PM_PartyName");
        //        ViewBag.Button = "SUBMIT";
        //    }
        //    return View(pl);
        //}
        public ActionResult ConsumerPaymentList(string fromdate, string todate, string PL_PartyCode)
        {
            if (UserModel == null) return returnLogin("~/Transaction/ConsumerPaymentList");
            GetRights("/Transaction/ConsumerPaymentList");
            PartyLedger_PL pl = new PartyLedger_PL();
            pl.FyId = UserModel.FyId;
            pl.CM_ID = UserModel.CM_ID;
            pl.PL_PartyCode = PL_PartyCode;
            if (!string.IsNullOrWhiteSpace(fromdate) && !string.IsNullOrWhiteSpace(todate))
            {
                pl.fromdate = fromdate;
                pl.todate = todate;
            }
            pl.PartyLadgerList = service.GetConsumerPayment<PartyLedger_PL>(pl, "usp_ConsumerPayment").OrderByDescending(a => a.PL_ChqDate).ToList(); ;
            pl.PL_PartyCode = PL_PartyCode;
            if (!string.IsNullOrWhiteSpace(fromdate) && !string.IsNullOrWhiteSpace(todate))
            {
                pl.fromdate = fromdate;
                pl.todate = todate;
            }
            return View(pl);
        }
        #endregion
        #region FormSales
        public ActionResult FormSales(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Transaction/FormSales");
            GetRights("/Transaction/FormSalesList");
            var partyList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "PM_PartyId", null);
            FORMSALE FORMSALE = new FORMSALE();
            var BenRate = service.GetGlobalSelectOne<Master_Rate>("Master_Rate", "CM_ID", UserModel.CM_ID);
            FORMSALE.fr_rate = BenRate.fr_rate;
            if (Id != null)
            {
                ViewBag.Button = "UPDATE";
                FORMSALE.CM_ID = UserModel.CM_ID;
                FORMSALE.FyId = UserModel.FyId;
                FORMSALE.ID = (decimal)Id;
                FORMSALE = service.GetFORMSALEById<FORMSALE>(FORMSALE, "usp_FORMSALE");
                FORMSALE.fr_rate = (double)FORMSALE.AMOUNT;
                FORMSALE.PL_PartyCode = FORMSALE.PM_PartyCode;
                ViewBag.PM_PartyCode = new SelectList(partyList, "PM_PartyCode", "PM_PartyName", FORMSALE.PM_PartyCode);
            }
            else
            {
                ViewBag.PM_PartyCode = new SelectList(partyList, "PM_PartyCode", "PM_PartyName");
                ViewBag.Button = "SUBMIT";
            }
            return View(FORMSALE);
        }
        public ActionResult FormSalesList(string fromdate, string todate, string PM_PartyCode)
        {
            if (UserModel == null) return returnLogin("~/Transaction/FormSalesList");
            GetRights("/Transaction/FormSalesList");
            FORMSALE FORMSALE = new FORMSALE();
            FORMSALE.FyId = UserModel.FyId;
            FORMSALE.CM_ID = UserModel.CM_ID;
            FORMSALE.PM_PartyCode = PM_PartyCode;
            if (!string.IsNullOrWhiteSpace(fromdate) && !string.IsNullOrWhiteSpace(todate))
            {
                FORMSALE.fromdate = fromdate;
                FORMSALE.todate = todate;
            }
            FORMSALE.FORMSALEList = service.GetFORMSALE<FORMSALE>(FORMSALE, "usp_FORMSALE");
            FORMSALE.PM_PartyCode = PM_PartyCode;
            if (!string.IsNullOrWhiteSpace(fromdate) && !string.IsNullOrWhiteSpace(todate))
            {
                FORMSALE.fromdate = fromdate;
                FORMSALE.todate = todate;
            }
            return View(FORMSALE);
        }
        #endregion
        #region FerrulCharge
        public ActionResult FerrulCharge(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Transaction/FerrulCharge");
            GetRights("/Transaction/FerrulChargeList");
            ferrulMaster fm = new ferrulMaster();
            var partyList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "CM_ID", UserModel.CM_ID);
            var BenRate = service.GetGlobalSelectOne<Master_Rate>("Master_Rate", "CM_ID", UserModel.CM_ID);
            fm.fAmt = BenRate.fAmt;
            fm.cAmt = BenRate.cAmt;
            if (Id != null)
            {
                ViewBag.Button = "UPDATE";
                fm.CM_ID = UserModel.CM_ID;
                fm.FyId = UserModel.FyId;
                fm.FerulId = (long)Id;
                fm = service.GetFerrulById<ferrulMaster>(fm, "usp_FerrulCharge");
                //fm.fr_rate = (double)FORMSALE.AMOUNT;
                fm.PL_PartyCode = fm.PM_PartyCode;
                ViewBag.PM_PartyCode = new SelectList(partyList, "PM_PartyCode", "PM_PartyName", fm.PM_PartyCode);
            }
            else
            {
                ViewBag.PM_PartyCode = new SelectList(partyList, "PM_PartyCode", "PM_PartyName");
                ViewBag.Button = "SUBMIT";
            }
            return View(fm);
        }
        public ActionResult FerrulChargeList(string fromdate, string todate, string custId)
        {
            if (UserModel == null) return returnLogin("~/Transaction/FerrulChargeList");
            GetRights("/Transaction/FerrulChargeList");
            ferrulMaster fm = new ferrulMaster();
            fm.FyId = UserModel.FyId;
            fm.CM_ID = UserModel.CM_ID;
            fm.custId = custId;
            if (!string.IsNullOrWhiteSpace(fromdate) && !string.IsNullOrWhiteSpace(todate))
            {
                fm.fromdate = fromdate;
                fm.todate = todate;
            }
          
            fm.ferrulMasterList = service.GetFerrul<ferrulMaster>(fm, "usp_FerrulCharge");
            fm.custId = custId;
            if (!string.IsNullOrWhiteSpace(fromdate) && !string.IsNullOrWhiteSpace(todate))
            {
                fm.fromdate = fromdate;
                fm.todate = todate;
            }
            return View(fm);
        }

        
        #endregion
        #region ReConnection
        public ActionResult ReConnection(string Id)
        {
            if (UserModel == null) return returnLogin("~/Transaction/ReConnection");
            GetRights("/Transaction/ReConnectionList");
            RepReConnection rc = new RepReConnection();
            var partyList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "CM_ID", UserModel.CM_ID);
            //var InspectorList = service.GetGlobalSelect<Inspector_Master>("Inspector_Master", "Ins_Id", null );
            if (Id != null)
            {
                ViewBag.Button = "UPDATE";
                rc.GS_BillNo = Id;
                rc= service.GetRepReConDetailsEdit<RepReConnection>(rc, "usp_ReConnection");
                rc.PL_PartyCode = rc.GS_PartyCode;
                rc.RepReItemList = service.SELECTRepReEdit<ItemMaster_IM>(rc, "usp_ReConnection");
                ViewBag.PM_PartyCode = new SelectList(partyList, "PM_PartyCode", "PM_PartyName",rc.GS_PartyCode);
               // ViewBag.Ins_Id = new SelectList(InspectorList, "ins_code", "ins_name");
            }
            else
            {
                ViewBag.PM_PartyCode = new SelectList(partyList, "PM_PartyCode", "PM_PartyName");
               // ViewBag.Ins_Id = new SelectList(InspectorList, "ins_code", "ins_name");
                rc.RepReItemList = service.GetGlobalSelect<ItemMaster_IM>("ItemMaster_IM", "CM_ID", UserModel.CM_ID);
                ViewBag.Button = "SUBMIT";
            }
            return View(rc);
        }
        public ActionResult ReConnectionList(string fromdate, string todate, string GS_PartyCode)
        {
            if (UserModel == null) return returnLogin("~/Transaction/ReConnectionList");
            GetRights("/Transaction/ReConnectionList");
            RepReConnection rc = new RepReConnection();
            rc.FyId = UserModel.FyId;
            rc.CM_ID = UserModel.CM_ID;
            rc.GS_PartyCode = GS_PartyCode;
            if (!string.IsNullOrWhiteSpace(fromdate) && !string.IsNullOrWhiteSpace(todate))
            {
                rc.fromdate = fromdate;
                rc.todate = todate;
            }
            rc.RepInsItemList = service.GetReconnection<RepReConnection>(rc, "usp_ReConnection");
            rc.GS_PartyCode = GS_PartyCode;
            if (!string.IsNullOrWhiteSpace(fromdate) && !string.IsNullOrWhiteSpace(todate))
            {
                rc.fromdate = fromdate;
                rc.todate = todate;
            }
            return View(rc);
        }
        #endregion
        #region ConsumerInvoice
        public ActionResult ConsumerInvoice(string Id)
        {

            if (UserModel == null) return returnLogin("~/Transaction/ConsumerInvoice");
            GetRights("/Transaction/ConsumerInvoiceList");
            RepReConnection rc = new RepReConnection();
            var partyList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "CM_ID", UserModel.CM_ID);
            ///var InspectorList = service.GetGlobalSelect<Inspector_Master>("Inspector_Master", "Ins_Id", null);
            if (Id != null)
            {
                ViewBag.Button = "UPDATE";
                rc.GS_BillNo = Id;
                rc = service.GetConInvDetailsEdit<RepReConnection>(rc, "usp_ConsumerInv");
                rc.PL_PartyCode = rc.GS_PartyCode;
                rc.RepReItemList = service.SELECTConInvEdit<ItemMaster_IM>(rc, "usp_ConsumerInv");
                ViewBag.PM_PartyCode = new SelectList(partyList, "PM_PartyCode", "PM_PartyName", rc.GS_PartyCode);
                ///ViewBag.Ins_Id = new SelectList(InspectorList, "ins_code", "ins_name");
            }
            else
            {
                ViewBag.PM_PartyCode = new SelectList(partyList, "PM_PartyCode", "PM_PartyName");
               /// ViewBag.Ins_Id = new SelectList(InspectorList, "ins_code", "ins_name");
                rc.RepReItemList = service.GetGlobalSelect<ItemMaster_IM>("ItemMaster_IM", "CM_ID", UserModel.CM_ID);
                ViewBag.Button = "SUBMIT";
            }
            return View(rc);
        }
        public ActionResult ConsumerInvoiceList(string fromdate, string todate, string GS_PartyCode)
        {
            if (UserModel == null) return returnLogin("~/Transaction/ConsumerInvoiceList");
            GetRights("/Transaction/ConsumerInvoiceList");
            RepReConnection rc = new RepReConnection();
            rc.FyId = UserModel.FyId;
            rc.CM_ID = UserModel.CM_ID;
            rc.GS_PartyCode = GS_PartyCode;
            if (!string.IsNullOrWhiteSpace(fromdate) && !string.IsNullOrWhiteSpace(todate))
            {
                rc.fromdate = fromdate;
                rc.todate = todate;
            }
            rc.RepInsItemList = service.GetConInv<RepReConnection>(rc, "usp_ConsumerInv");
            rc.GS_PartyCode = GS_PartyCode;
            if (!string.IsNullOrWhiteSpace(fromdate) && !string.IsNullOrWhiteSpace(todate))
            {
                rc.fromdate = fromdate;
                rc.todate = todate;
            }
            return View(rc);
        }
        #endregion
        #region WaterTaxCollection
        public ActionResult WaterTaxCollection(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Transaction/WaterTaxCollection");
            GetRights("/Transaction/WaterTaxCollectionList");
            var partyList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "PM_PartyId", null);
            PartyTax_PT pt = new PartyTax_PT();
            var tax_master = service.GetGlobalSelectOne<tax_master>("tax_master", "CM_ID", UserModel.CM_ID);
            if (Id != null)
            {
                ViewBag.Button = "UPDATE";
                pt.CM_ID = UserModel.CM_ID;
                pt.FyId = UserModel.FyId;
                pt.f_date = tax_master.f_date;
                pt.s_date = tax_master.s_date;
                pt.t_date = tax_master.t_date;
                pt.f_amt = tax_master.f_amt;
                pt.s_amt = tax_master.s_amt;
                pt.t_amt = tax_master.t_amt;

                pt.PT_ID = (int)Id;                
                pt = service.GetTaxByIds<PartyTax_PT>(pt, "usp_PartyTax_PT");
                pt.CM_ID = UserModel.CM_ID;
                pt.FyId = UserModel.FyId;
                pt.f_date = tax_master.f_date;
                pt.s_date = tax_master.s_date;
                pt.t_date = tax_master.t_date;
                pt.f_amt = tax_master.f_amt;
                pt.s_amt = tax_master.s_amt;
                pt.t_amt = tax_master.t_amt;
                pt.PL_PartyCode = pt.PT_PtyCode;
                ViewBag.PM_PartyCode = new SelectList(partyList, "PM_PartyCode", "PM_PartyName");
                
            }
            else
            {
                pt.f_date = tax_master.f_date;
                pt.s_date = tax_master.s_date;
                pt.t_date = tax_master.t_date;
                pt.f_amt = tax_master.f_amt;
                pt.s_amt = tax_master.s_amt;
                pt.t_amt = tax_master.t_amt;
                DateTime date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                pt.PT_DtTo = lastDayOfMonth;
                ViewBag.PM_PartyCode = new SelectList(partyList, "PM_PartyCode", "PM_PartyName");
                ViewBag.Button = "SUBMIT";
            }
            return View(pt);
        }


        //public ActionResult WaterTaxCollection()
        //{
        //    if (UserModel == null) return returnLogin("~/Transaction/WaterTaxCollectionList");
        //    GetRights("/Transaction/WaterTaxCollectionList");
        //    PartyTax_PT pt = new PartyTax_PT();
        //    var partyList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "CM_ID", UserModel.CM_ID);
        //    ViewBag.PM_PartyCode = new SelectList(partyList, "PM_PartyCode", "PM_PartyName");
        //    var tax_master = service.GetGlobalSelectOne<tax_master>("tax_master", "CM_ID", UserModel.CM_ID);
        //    pt.f_date = tax_master.f_date;
        //    pt.s_date = tax_master.s_date;
        //    pt.t_date = tax_master.t_date;
        //    pt.f_amt = tax_master.f_amt;
        //    pt.s_amt = tax_master.s_amt;
        //    pt.t_amt = tax_master.t_amt;
        //    ViewBag.Button = "UPDATE";
        //    DateTime date = DateTime.Now;
        //    var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
        //    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
        //    pt.PT_DtTo = lastDayOfMonth;
        //    return View(pt);
        //}
        public ActionResult WaterTaxCollectionList(string fromdate, string todate, string PT_PtyCode)
        {
            if (UserModel == null) return returnLogin("~/Transaction/WaterTaxCollectionList");
            GetRights("/Transaction/WaterTaxCollectionList");
            PartyTax_PT pt = new PartyTax_PT();
            pt.FyId  = UserModel.FyId;
            pt.CM_ID = UserModel.CM_ID;
            pt.PT_PtyCode = PT_PtyCode;
            if (!string.IsNullOrWhiteSpace(fromdate) && !string.IsNullOrWhiteSpace(todate))
            {
                pt.fromdate = fromdate;
                pt.todate   = todate;
            }
            pt.PartyTax_PTList = service.GetWaterTaxCollection<PartyTax_PT>(pt, "usp_PartyTax_PT");
            pt.PT_PtyCode = PT_PtyCode;
            if (!string.IsNullOrWhiteSpace(fromdate) && !string.IsNullOrWhiteSpace(todate))
            {
                pt.fromdate = fromdate;
                pt.todate   = todate;
            }
            return View(pt);
        }
        #endregion
        #region UpdateForConsumerTaxPayment
        public ActionResult UpdateForConsumerTaxPayment()
        {
            if (UserModel == null) return returnLogin("~/Transaction/UpdateForConsumerTaxPayment");
            GetRights("/Transaction/UpdateForConsumerTaxPayment");
            var partyList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "CM_ID", UserModel.CM_ID);
            ViewBag.PM_PartyCode = new SelectList(partyList, "PM_PartyCode", "PM_PartyName");
            ViewBag.Button = "UPDATE";
            return View();
        }
        #endregion
        //add by uttaran 01-05-2024
        #region Water Tank booking
        public ActionResult Tankbooking(int? Id)
        {
            if (UserModel == null)
                return returnLogin("~/Transaction/Tankbooking");

            GetRights("/Transaction/TankbookingList");

            TankBooking_TB tankBooking = new TankBooking_TB();

            var tankList = service.GetGlobalSelect<TankMaster_TM>("TankMaster_TM", "TM_Lt", null)
                                  .Select(t => new
                                  {
                                      t.TM_ID,
                                      DisplayText = t.TM_Lt + " (" + t.TM_TankName + ")"
                                  })
                                  .ToList();

            var areaList = service.GetGlobalSelect<AreaMaster_AM>("AreaMaster_AM", "AM_AreaName", null);
            var paraList = service.GetGlobalSelect<ParaMaster_PM>("ParaMaster_PM", "PM_ParaName", null);

            if (Id.HasValue)
            {
                tankBooking = service.GetGlobalSelectOne<TankBooking_TB>("TankBooking_TB", "AP_ID", Id.Value);
                ViewBag.Button = "UPDATE";
            }
            else
            {
                ViewBag.Button = "SUBMIT";
            }

            ViewBag.TM_ID = new SelectList(tankList, "TM_ID", "DisplayText", tankBooking.TM_ID);
            ViewBag.AM_AreaID = new SelectList(areaList, "AM_AreaID", "AM_AreaName", tankBooking.AM_AreaID);
            ViewBag.PM_ParaId = new SelectList(paraList, "PM_ParaId", "PM_ParaName", tankBooking.PM_ParaId);

            return View(tankBooking);
        }

        public ActionResult TankbookingList(string fromdates, string todates, string AP_Code)
        {
            if (UserModel == null) return returnLogin("~/Transaction/TankbookingList");
            GetRights("/Transaction/TankbookingList");
            TankBooking_TB TankBooking_TB = new TankBooking_TB();
            TankBooking_TB.FyId = UserModel.FyId;
            TankBooking_TB.CM_ID = UserModel.CM_ID;
            TankBooking_TB.AP_Code = AP_Code;
            if (!string.IsNullOrWhiteSpace(fromdates) && !string.IsNullOrWhiteSpace(todates))
            {
                TankBooking_TB.fromdates = fromdates;
                TankBooking_TB.todates = todates;
            }
            TankBooking_TB.ListBooking = service.GetTankBooking<TankBooking_TB>(TankBooking_TB, "usp_TankBooking");
            TankBooking_TB.AP_Code = AP_Code;
            if (!string.IsNullOrWhiteSpace(fromdates) && !string.IsNullOrWhiteSpace(todates))
            {
                TankBooking_TB.fromdates = fromdates;
                TankBooking_TB.todates = todates;
            }
            return View(TankBooking_TB);
        }
        #endregion
        #region Van booking
        public ActionResult Vanbooking(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Transaction/Vanbooking");
            GetRights("/Transaction/VanbookingList");
            VanBooking_VB Vanbooking = new VanBooking_VB();
            var VanList = service.GetGlobalSelect<VanMaster_VM>("VanMaster_VM", "VM_Type", null);
            var AreaList = service.GetGlobalSelect<AreaMaster_AM>("AreaMaster_AM", "AM_AreaName", null);
            var ParaList = service.GetGlobalSelect<ParaMaster_PM>("ParaMaster_PM", "PM_ParaName", null);
            Int64 editId = 0;
            if (Id != null)
            {
                editId = (Int64)Id;
                Vanbooking = service.GetGlobalSelectOne<VanBooking_VB>("VanBooking_VB", "AP_ID", editId);
                ViewBag.VM_ID = new SelectList(VanList, "VM_ID", "VM_Type");
                ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaID", "AM_AreaName");
                ViewBag.PM_ParaId = new SelectList(ParaList, "PM_ParaId", "PM_ParaName");
                ViewBag.Button = "UPDATE";
            }
            else
            {
                ViewBag.Button = "SUBMIT";
            }
            ViewBag.VM_ID = new SelectList(VanList, "VM_ID", "VM_Type");
            ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaID", "AM_AreaName");
            ViewBag.PM_ParaId = new SelectList(ParaList, "PM_ParaId", "PM_ParaName");
            return View(Vanbooking);
        }
        public ActionResult VanbookingList(string fromdates, string todates, string AP_Code)
        {
            if (UserModel == null) return returnLogin("~/Transaction/VanbookingList");
            GetRights("/Transaction/VanbookingList");
            VanBooking_VB VanBooking_VB = new VanBooking_VB();
            VanBooking_VB.FyId = UserModel.FyId;
            VanBooking_VB.CM_ID = UserModel.CM_ID;
            VanBooking_VB.AP_Code = AP_Code;
            if (!string.IsNullOrWhiteSpace(fromdates) && !string.IsNullOrWhiteSpace(todates))
            {
                VanBooking_VB.fromdates = fromdates;
                VanBooking_VB.todates = todates;
            }
            VanBooking_VB.ListBooking = service.GetVanBooking<VanBooking_VB>(VanBooking_VB, "usp_VanBooking");
            VanBooking_VB.AP_Code = AP_Code;
            if (!string.IsNullOrWhiteSpace(fromdates) && !string.IsNullOrWhiteSpace(todates))
            {
                VanBooking_VB.fromdates = fromdates;
                VanBooking_VB.todates = todates;
            }
            return View(VanBooking_VB);
        }
        #endregion

    }
}
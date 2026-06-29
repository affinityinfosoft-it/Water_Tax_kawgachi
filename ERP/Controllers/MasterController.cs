using BObject;
using ERP.DataAccess;
using ERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers
{
    public class MasterController : BaseController
    {
        // GET: Master
        #region Company Master
        public ActionResult Company(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Master/Company");
            GetRights("/Master/CompanyList");
            company company = new company();
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            Int64 editId = 0;
            if (Id != null)
            {
                editId = (Int64)Id;
                company = service.GetGlobalSelectOne<company>("company", "CM_ID", editId);
                //company.dt_of_entry=company.dt_of_entry.ToShortDateString();
                ViewBag.Button = "UPDATE";
            }
            else
            {
                ViewBag.Button = "SUBMIT";
            }

            return View(company);
        }
        public ActionResult CompanyList()
        {
            if (UserModel == null) return returnLogin("~/Master/CompanyList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            company company = new company();
            company.CompanyList = service.GetGlobalSelect<company>("company", "CM_ID", null);
            return View(company);
        }
        #endregion
        #region Item Master
       public ActionResult Item(int? Id)
        {
            if (UserModel == null)
                return returnLogin("~/Master/Item");

            GetRights("/Master/ItemList");

            ItemMaster_IM item = new ItemMaster_IM();
            Int64 editId = 0;
            var itemGroupList = service.GetGlobalSelect<ItemGroup_IG>("ItemGroup_IG", "ItemCode", null);
            ViewBag.ItemGroupList = new SelectList(itemGroupList, "ItemCode", "ItemGroupName");

            if (Id != null)
            {
                editId = (Int64)Id;
                item = service.GetGlobalSelectOne<ItemMaster_IM>("ItemMaster_IM", "IM_ID", editId);
                ViewBag.Button = "UPDATE";

                //var itemSubGroupList = service.GetGlobalSelect<ItemSubGroup_ISG>("ItemSubGroup_ISG", "ItemSubGroupID", item.ItemGroupID);
                var itemSubGroupList = service.GetGlobalSelect<ItemSubGroup_ISG>("ItemSubGroup_ISG", "ItemSubGroupID", item.ItemGroupID.HasValue ? (long?)Convert.ToInt64(item.ItemGroupID.Value) : null);
                ViewBag.ItemSubGroupList = new SelectList(itemSubGroupList, "ItemSubGroupID", "ItemSubGroupName", item.ItemSubGroupID);
            }
            else
            {
                ViewBag.ItemSubGroupList = new SelectList(Enumerable.Empty<SelectListItem>());
                ViewBag.Button = "SUBMIT";
            }
            return View(item);
        }

        public ActionResult ItemList()
        {
            if (UserModel == null) return returnLogin("~/Master/ItemList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            ItemMaster_IM item = new ItemMaster_IM();
            item.ItemList = service.GetGlobalSelect<ItemMaster_IM>("ItemMaster_IM", "CM_ID", UserModel.CM_ID);
            return View(item);
        }
        #endregion
        //ADD BY UTTARAN FOR INVENTORY MANAGEMENT 11-06-2025
        #region ItemGroup Master
        public ActionResult ItemGroup(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Master/ItemGroup");
            GetRights("/Master/ItemGroupList");
            ItemGroup_IG igroup = new ItemGroup_IG();
            var BenRate = service.GetGlobalSelectOne<Master_Rate>("Master_Rate", "CM_ID", UserModel.CM_ID);
            Int64 editId = 0;
            if (Id != null)
            {
                editId = (Int64)Id;
                igroup = service.GetGlobalSelectOne<ItemGroup_IG>("ItemGroup_IG", "ItemGroupID", editId);
                ViewBag.Button = "UPDATE";
            }
            else
            {
                ViewBag.Button = "SUBMIT";
            }

            return View(igroup);
        }
        public ActionResult ItemGroupList()
        {
            if (UserModel == null) return returnLogin("~/Master/ItemGroupList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            ItemGroup_IG igroup = new ItemGroup_IG();
            igroup.ItemGroupList = service.GetGlobalSelect<ItemGroup_IG>("ItemGroup_IG", "ItemGroupID", null);
            return View(igroup);
        }
        #endregion
        #region ItemSubGroup Master
        public ActionResult ItemSubGroup(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Master/ItemSubGroup");
            GetRights("/Master/ItemSubGroupList");
            ItemSubGroup_ISG Itemsubgroupmaster = new ItemSubGroup_ISG();
            var ItemGroupList = service.GetGlobalSelect<ItemGroup_IG>("ItemGroup_IG", "ItemGroupID", null);
            Int64 editId = 0;
            if (Id != null)
            {
                editId = (Int64)Id;
                Itemsubgroupmaster = service.GetGlobalSelectOne<ItemSubGroup_ISG>("ItemSubGroup_ISG", "ItemSubGroupID", editId);
                ViewBag.ItemGroupID = new SelectList(ItemGroupList, "ItemCode", "ItemGroupName", Itemsubgroupmaster.ItemCode);
                ViewBag.Button = "UPDATE";
            }
            else
            {
                ViewBag.ItemGroupID = new SelectList(ItemGroupList, "ItemCode", "ItemGroupName");
                ViewBag.Button = "SUBMIT";
            }

            return View(Itemsubgroupmaster);
        }
        public ActionResult ItemSubGroupList()
        {
            if (UserModel == null) return returnLogin("~/Master/ItemSubGroupList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            ItemSubGroup_ISG ItemSubGroup = new ItemSubGroup_ISG();

            ItemSubGroup.ItemSubGroupList = service.GetItemSubGroupList<ItemSubGroup_ISG>(ItemSubGroup, "SP_ItemSubGroup_IUD");
            return View(ItemSubGroup);
        }
        #endregion
        #region Beneficiary Fixed Rate Master
        public ActionResult BeneficiaryFixedRate()
        {
            if (UserModel == null) return returnLogin("~/Master/Item");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            Master_Rate Master_Rate = new Master_Rate();
            Master_Rate = service.GetGlobalSelectOne<Master_Rate>("Master_Rate", "CM_ID", UserModel.CM_ID);
            ViewBag.Button = "UPDATE";
            return View(Master_Rate);
        }
        #endregion
        #region Water Tax Slab Master
        public ActionResult WaterTax()
        {
            if (UserModel == null) return returnLogin("~/Master/Item");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            tax_master tax_master = new tax_master();
            tax_master = service.GetGlobalSelectOne<tax_master>("tax_master", "CM_ID", UserModel.CM_ID);
            ViewBag.Button = "UPDATE";
            return View(tax_master);
        }
        #endregion
        #region Purchase Vendor Master
        public ActionResult PurchaseVendor(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Master/PurchaseVendor");
            GetRights("/Master/PurchaseVendorList");
            PurchaseVendorMaster_PVM PurchaseVendor = new PurchaseVendorMaster_PVM();
            Int64 editId = 0;
            if (Id != null)
            {
                editId = (Int64)Id;
                PurchaseVendor = service.GetGlobalSelectOne<PurchaseVendorMaster_PVM>("PurchaseVendorMaster_PVM", "PVM_VendorID", editId);
                ViewBag.Button = "UPDATE";
            }
            else
            {
                ViewBag.Button = "SUBMIT";
            }

            return View(PurchaseVendor);
        }
        public ActionResult PurchaseVendorList()
        {
            if (UserModel == null) return returnLogin("~/Master/PurchaseVendorList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            PurchaseVendorMaster_PVM PurchaseVendor = new PurchaseVendorMaster_PVM();
            PurchaseVendor.ListVendor = service.GetGlobalSelect<PurchaseVendorMaster_PVM>("PurchaseVendorMaster_PVM", "PVM_VendorID", null);
            return View(PurchaseVendor);
        }
        #endregion
        #region Beneficiary Connection Master
        public ActionResult BeneficiaryConnection(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Master/BeneficiaryConnection");
            GetRights("/Master/BeneficiaryConnectionList");
            PartyMaster_PM PartyMaster = new PartyMaster_PM();
            var AreaList = service.GetGlobalSelect<AreaMaster_AM>("AreaMaster_AM", "AM_AreaID", null);
            var ParaList = service.GetGlobalSelect<ParaMaster_PM>("ParaMaster_PM", "PM_ParaId", null);
            var BenRate = service.GetGlobalSelectOne<Master_Rate>("Master_Rate", "CM_ID", UserModel.CM_ID);
            PartyMaster.PM_BFAmount = BenRate.bf_rate;
            Int64 editId = 0;
            if (Id != null)
            {
                editId = (Int64)Id;
                PartyMaster = service.GetGlobalSelectOne<PartyMaster_PM>("PartyMaster_PM", "PM_PartyId", editId);
                //PartyMaster.PM_BFlagF = PartyMaster.PM_BFlag == "A" ? true : false;
                //PartyMaster.PM_CFlagF = PartyMaster.PM_CFlag == "C" ? true : false;
                //PartyMaster.PM_FFlagF = PartyMaster.PM_FFlag == "E" ? true : false;
                //PartyMaster.PM_SFlagF = PartyMaster.PM_SFlag == "S" ? true : false;
                PartyMaster.PM_DueAmt = PartyMaster.PM_BFAmount - PartyMaster.PM_PaidAmt;
                ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaID", "AM_AreaName", PartyMaster.AM_AreaID);
                ViewBag.PM_ParaId = new SelectList(ParaList, "PM_ParaId", "PM_ParaName", PartyMaster.PM_ParaId);
                ViewBag.Button = "UPDATE";
            }
            else
            {
                ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaID", "AM_AreaName");
                ViewBag.PM_ParaId = new SelectList(ParaList, "PM_ParaId", "PM_ParaName");
                ViewBag.Button = "SUBMIT";
            }

            return View(PartyMaster);
        }
        public ActionResult BeneficiaryConnectionList(string fromdate, string todate, string PM_PartyCode)
        {
            if (UserModel == null) return returnLogin("~/Master/BeneficiaryConnectionList");
            GetRights("/Master/BeneficiaryConnectionList");
            PartyMaster_PM pms = new PartyMaster_PM();
            pms.FyId = UserModel.FyId;
            pms.CM_ID = UserModel.CM_ID;
            pms.PM_PartyCode = PM_PartyCode;
            if (!string.IsNullOrWhiteSpace(fromdate) && !string.IsNullOrWhiteSpace(todate) )
            {
                pms.fromdate = fromdate;
                pms.todate = todate;
               
            }
            pms.BeneficiryList = service.GetBeneficeryConnection<PartyMaster_PM>(pms, "usp_beneficeryconnection");
            pms.PM_PartyCode = PM_PartyCode;
            if (!string.IsNullOrWhiteSpace(fromdate) && !string.IsNullOrWhiteSpace(todate) )
            {
                pms.fromdate = fromdate;
                pms.todate = todate;
               
            }
            return View(pms);
        }
       //public ActionResult BeneficiaryConnectionList()
        //{
        //    if (UserModel == null) return returnLogin("~/Master/BeneficiaryConnectionList");
        //    var url = Request.RequestContext.HttpContext.Request.RawUrl;
        //    GetRights(url);
        //    PartyMaster_PM PartyMaster = new PartyMaster_PM();
        //    PartyMaster.BeneficiryList = service.GetGlobalSelect<PartyMaster_PM>("PartyMaster_PM", "PM_PartyId", null).OrderByDescending(a=>a.PM_RegDate).ToList();
        //    return View(PartyMaster);
        //}
        #endregion
        #region Area Master
        public ActionResult Area(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Master/Area");
            GetRights("/Master/AreaList");
            AreaMaster_AM AREA = new AreaMaster_AM();
            Int64 editId = 0;
            if (Id != null)
            {
                editId = (Int64)Id;
                AREA = service.GetGlobalSelectOne<AreaMaster_AM>("AreaMaster_AM", "AM_AreaID", editId);
                ViewBag.Button = "UPDATE";
            }
            else
            {
                ViewBag.Button = "SUBMIT";
            }

            return View(AREA);
        }
        public ActionResult AreaList()
        {
            if (UserModel == null) return returnLogin("~/Master/AreaList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            AreaMaster_AM AREA = new AreaMaster_AM();
            AREA.AreaList = service.GetGlobalSelect<AreaMaster_AM>("AreaMaster_AM", "AM_AreaID", null);
            return View(AREA);
        }
        #endregion
        #region Para Master
        public ActionResult Para(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Master/Para");
            GetRights("/Master/ParaList");
            ParaMaster_PM Paramaster = new ParaMaster_PM();
            var AreaList = service.GetGlobalSelect<AreaMaster_AM>("AreaMaster_AM", "AM_AreaID", null);
            Int64 editId = 0;
            if (Id != null)
            {
                editId = (Int64)Id;
                Paramaster = service.GetGlobalSelectOne<ParaMaster_PM>("ParaMaster_PM", "PM_ParaId", editId);
                ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaCode", "AM_AreaName", Paramaster.AM_AreaCode);
                ViewBag.Button = "UPDATE";
            }
            else
            {
                ViewBag.AM_AreaID = new SelectList(AreaList, "AM_AreaCode", "AM_AreaName");
                ViewBag.Button = "SUBMIT";
            }

            return View(Paramaster);
        }
        public ActionResult ParaList()
        {
            if (UserModel == null) return returnLogin("~/Master/ParaList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            ParaMaster_PM Paramaster = new ParaMaster_PM();

            Paramaster.ParaList = service.GetParaList<ParaMaster_PM>(Paramaster, "usp_Para");
            return View(Paramaster);
        }
        #endregion
        #region Inspector Master
        public ActionResult InspectorMaster(Int64? Id)
        {
            if (UserModel == null) return returnLogin("~/Master/InspectorMaster");
            GetRights("/Master/InspectorList");
            Inspector_Master ins = new Inspector_Master();
            Int64 editId = 0;
            if (Id != null)
            {
                editId = (Int64)Id;
                ins = service.GetGlobalSelectOne<Inspector_Master>("Inspector_Master", "Ins_Id", editId);
                ViewBag.Button = "UPDATE";
            }
            else
            {
                ViewBag.Button = "SUBMIT";
            }

            return View(ins);
        }
        public ActionResult InspectorList()
        {
            if (UserModel == null) return returnLogin("~/Master/InspectorList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            Inspector_Master ins = new Inspector_Master();
            ins.InspectorList = service.GetGlobalSelect<Inspector_Master>("Inspector_Master", "Ins_Id", null);
            return View(ins);
        }
        #endregion
        #region Tank Master
        public ActionResult Tank(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Master/Tank");
            GetRights("/Master/TankList");
            TankMaster_TM item = new TankMaster_TM();
            Int64 editId = 0;
            if (Id != null)
            {
                editId = (Int64)Id;
                item = service.GetGlobalSelectOne<TankMaster_TM>("TankMaster_TM", "TM_ID", editId);
                ViewBag.Button = "UPDATE";
            }
            else
            {
                ViewBag.Button = "SUBMIT";
            }
            return View(item);
        }
        public ActionResult TankList()
        {
            if (UserModel == null) return returnLogin("~/Master/TankList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            TankMaster_TM item = new TankMaster_TM();
            item.TankList = service.GetGlobalSelect<TankMaster_TM>("TankMaster_TM", "CM_ID", UserModel.CM_ID);
            return View(item);
        }
        #endregion
        #region Van Master
        public ActionResult Van(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Master/Van");
            GetRights("/Master/VanList");
            VanMaster_VM van = new VanMaster_VM();
            Int64 editId = 0;
            if (Id != null)
            {
                editId = (Int64)Id;
                van = service.GetGlobalSelectOne<VanMaster_VM>("VanMaster_VM", "VM_ID", editId);
                ViewBag.Button = "UPDATE";
            }
            else
            {
                ViewBag.Button = "SUBMIT";
            }
            return View(van);
        }
        public ActionResult VanList()
        {
            if (UserModel == null) return returnLogin("~/Master/VanList");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            VanMaster_VM van = new VanMaster_VM();
            van.VanList = service.GetGlobalSelect<VanMaster_VM>("VanMaster_VM", "CM_ID", UserModel.CM_ID);
            return View(van);
        }
        #endregion
        public JsonResult GetItemSubGroups(long itemGroupId)
        {
            var subGroups = service.GetGlobalSelect<ItemSubGroup_ISG>("ItemSubGroup_ISG", "ItemCode", itemGroupId);

            var list = subGroups.Select(s => new SelectListItem
            {
                Value = s.ItemSubGroupID.ToString(),
                Text = s.ItemSubGroupName
            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}

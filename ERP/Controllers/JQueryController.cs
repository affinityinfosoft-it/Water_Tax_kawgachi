using BObject;
using ERP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ERP.Controllers
{
    public class JQueryController : BaseController
    {
        // GET: JQuery
        #region Master
        #region Company Master
        public JsonResult InsUpCompany(company TEntity)
        {
            if (UserModel == null)
            {
                returnLogin("~/Home/Index");
            }
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.code != 0)
                {
                    var result = service.InsUpCompany(TEntity, "usp_Company");
                    if (result == 100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteCompany(company TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.CM_ID != 0)
                {
                    var result = service.GlobalDelete("company", "CM_ID", TEntity.CM_ID, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Item Master
        public JsonResult InsUpItem(ItemMaster_IM TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.IM_ItemCode != null)
                {
                    TEntity.CM_ID = UserModel.CM_ID;
                    var result = service.InsUpItem(TEntity, "usp_Item");
                    if (result == 100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteItem(ItemMaster_IM TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.IM_ID != 0)
                {
                    var result = service.GlobalDelete("ItemMaster_IM", "IM_ID", TEntity.IM_ID, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        //ADD BY UTTARAN FOR INVENTORY MANAGEMENT 11-06-2025
        #region ItemGroup Master
        public JsonResult InsUpItemGroup(ItemGroup_IG TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.ItemCode != 0)
                {
                    TEntity.CM_ID = UserModel.CM_ID;
                    TEntity.UserId = UserModel.UserId;
                    TEntity.FyId = 2022;
                    var result = service.InsUpItemGroup(TEntity, "SP_ItemGroup_IUD");
                    if (result == 100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteItemGroup(ItemGroup_IG TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.ItemGroupID != 0)
                {
                    var result = service.GlobalDelete("ItemGroup_IG", "ItemGroupID", TEntity.ItemGroupID, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ItemSubGroup Master
        public JsonResult InsUpItemSubGroup(ItemSubGroup_ISG TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.ItemCode != 0)
                {
                    TEntity.CM_ID = UserModel.CM_ID;
                    TEntity.UserId = UserModel.UserId;
                    TEntity.FyId = 2022;
                    var result = service.InsUpItemSubGroup(TEntity, "SP_ItemSubGroup_IUD");
                    if (result == 100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteItemSubGroup(ItemSubGroup_ISG TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.ItemSubGroupID != 0)
                {
                    var result = service.GlobalDelete("ItemSubGroup_ISG", "ItemSubGroupID", TEntity.ItemSubGroupID, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Beneficary Fixed Rate Master
        public JsonResult InsUpBeneficaryFixedRate(Master_Rate TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.bf_rate != 0 && TEntity.fr_rate != 0)
                {
                    long CmId = UserModel.CM_ID;
                    var result = service.InsUpBeneficaryFixedRate(TEntity, CmId, "usp_BeneficaryFixedRate");
                    if (result == 100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfully Data already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Water Tax Slab Master
        public JsonResult InsUpWaterTaxSlab(tax_master TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.f_amt != 0 && TEntity.f_date != 0 && TEntity.s_amt != 0 && TEntity.s_date != 0 && TEntity.t_amt != 0)
                {
                    long CmId = UserModel.CM_ID;
                    TEntity.UserId = UserModel.UserId;
                    var result = service.InsUpWaterTaxSlab(TEntity, CmId, "usp_WaterTaxSlab");
                    if (result == 100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfully Data already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region PuecheseVendor
        public JsonResult InsUpPurcheseVendor(PurchaseVendorMaster_PVM TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.PVM_VendorCode != "")
                {
                    TEntity.CM_ID = UserModel.CM_ID;
                    var result = service.InsUpPurcheseVendor(TEntity, "USP_PurchaseVendor");
                    if (result == 100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteVendor(PurchaseVendorMaster_PVM TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.PVM_VendorID != 0)
                {
                    //long deleteid = (long)TEntity.AM_AreaID;
                    var result = service.GlobalDelete("PurchaseVendorMaster_PVM", "PVM_VendorID", TEntity.PVM_VendorID, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region BeneficeryConnection
        public JsonResult InsUpBeneficeryConnection(PartyMaster_PM TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.PM_PartyCode != "")
                {
                    TEntity.CM_ID = UserModel.CM_ID;
                    TEntity.UserId = UserModel.UserId;
                    TEntity.FyId = 2022;
                    var result = service.InsUpBeneficeryConnection(TEntity, "usp_beneficeryconnection");
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    else if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    else if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                    else
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteBeneficery(PartyMaster_PM TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.PM_PartyId != 0)
                {
                    long deleteid = (long)TEntity.PM_PartyId;
                    var result = service.GlobalDelete("PartyMaster_PM", "PM_PartyId", deleteid, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Area
        public JsonResult InsUpArea(AreaMaster_AM TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.AM_AreaCode != 0)
                {
                    var result = service.InsUpArea(TEntity, "usp_Area");
                    if (result == 100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteArea(AreaMaster_AM TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.AM_AreaID != 0)
                {
                    //long deleteid = (long)TEntity.AM_AreaID;
                    var result = service.GlobalDelete("AreaMaster_AM", "AM_AreaID", TEntity.AM_AreaID, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Para
        public JsonResult InsUpPara(ParaMaster_PM TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.AM_AreaCode != 0)
                {
                    var result = service.InsUpPara(TEntity, "usp_Para");
                    if (result == 100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeletePara(ParaMaster_PM TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.PM_ParaId != 0)
                {
                    //long deleteid = (long)TEntity.AM_AreaID;
                    var result = service.GlobalDelete("ParaMaster_PM", "PM_ParaId", TEntity.PM_ParaId, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Inspector Master 
        public JsonResult InsUpInspector(Inspector_Master TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.ins_code != 0)
                {
                    var result = service.InsUpInspector(TEntity, "usp_Inspector");
                    if (result == 100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteInspector(Inspector_Master TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.Ins_Id != 0)
                {
                    //long deleteid = (long)TEntity.ins_code;
                    var result = service.GlobalDelete("Inspector_Master", "Ins_Id", TEntity.Ins_Id, null, null);
                    if ((long)result > 0)
                    {
                        status.Result = (long)result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Tank Master
        public JsonResult InsUpTank(TankMaster_TM TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.TM_TankCode != null)
                {
                    TEntity.CM_ID = UserModel.CM_ID;
                    var result = service.InsUpTank(TEntity, "usp_Tank");
                    if (result == 100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteTank(TankMaster_TM TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.TM_ID != 0)
                {
                    var result = service.GlobalDelete("TankMaster_TM", "TM_ID", TEntity.TM_ID, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Van Master
        public JsonResult InsUpVan(VanMaster_VM TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.VM_VanCode != null)
                {
                    TEntity.CM_ID = UserModel.CM_ID;
                    var result = service.InsUpVan(TEntity, "usp_Van");
                    if (result == 100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteVan(VanMaster_VM TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.VM_ID != 0)
                {
                    var result = service.GlobalDelete("VanMaster_VM", "VM_ID", TEntity.VM_ID, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region Transaction
        #region ConsumerPayment
        public JsonResult GetConsumerDetails(string PM_PartyCode)
        {
            PartyLedger_PL TEntity = new PartyLedger_PL();
            TEntity.PM_PartyCode = PM_PartyCode;
            StatusResponse status = new StatusResponse();
            var ConsumerDetails = service.GetConsumerDetails<PartyLedger_PL>(TEntity, "usp_ConsumerPayment");
            var DueList = service.GetConsumerDueList<PartyLedger_PL>(TEntity, "usp_ConsumerPayment");
            var ReceptNo = service.GetReceptNo<PartyLedger_PL>(TEntity, "usp_ConsumerPayment");
            try
            {
                if (ConsumerDetails != null)
                {
                    status.IsSuccess = true;
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(new { ConsumerDetails= ConsumerDetails, DueList= DueList, ReceptNo= ReceptNo }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InsUpConPayment(PartyLedger_PL TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.PM_PartyCode != "")
                {
                    TEntity.CM_ID = UserModel.CM_ID;
                    TEntity.UserId = UserModel.UserId;
                    TEntity.FyId = UserModel.FyId;
                    var result = service.InsUpConsumerPayment(TEntity, "usp_ConsumerPayment");
                    if (result == -101)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                    else
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                  status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteConsumerPayment(PartyLedger_PL TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.PL_Id != 0)
                {
                    var result = service.GlobalDelete("PartyLedger_PL", "PL_Id", (long)TEntity.PL_Id, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Form Sale
        public JsonResult GetConDelForFormSale(string PM_PartyCode)
        {
            PartyLedger_PL TEntity = new PartyLedger_PL();
            TEntity.PM_PartyCode = PM_PartyCode;
            StatusResponse status = new StatusResponse();
            var ConsumerDetails = service.GetConsumerDetails<PartyLedger_PL>(TEntity, "usp_ConsumerPayment");
            var Party = service.GetGlobalSelectOne<PartyMaster_PM>("PartyMaster_PM", "PM_PartyId", (long)ConsumerDetails.PM_PartyId);
            try
            {
                if (ConsumerDetails != null)
                {
                    status.IsSuccess = true;
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(new { ConsumerDetails = ConsumerDetails, Party= Party }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InsUpFORMSALE(FORMSALE TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.PM_PartyCode != "")
                {
                    TEntity.CM_ID = UserModel.CM_ID;
                    TEntity.UserId = UserModel.UserId;
                    TEntity.FyId = UserModel.FyId;
                    var result = service.InsUpFORMSALE(TEntity, "usp_FORMSALE");
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    else if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    else if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                    else
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteFormSale(FORMSALE TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.ID != 0)
                {
                    TEntity = service.GetGlobalSelectOne<FORMSALE>("FORMSALE","ID",(long)TEntity.ID);
                    TEntity.CM_ID = UserModel.CM_ID;
                    TEntity.FyId = UserModel.FyId;
                    var LDelete= service.DeleteAnyMasters(TEntity, "usp_FORMSALE");
                    var result = service.GlobalDelete("FORMSALE", "ID", (long)TEntity.ID, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been delete successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Ferrul Charge
        public JsonResult GetConDtlForFerrul(string PM_PartyCode)
        {
            PartyLedger_PL TEntity = new PartyLedger_PL();
            ferrulMaster fm = new ferrulMaster();
            TEntity.PM_PartyCode = PM_PartyCode;
            StatusResponse status = new StatusResponse();
            var ConsumerDetails = service.GetConsumerDetails<PartyLedger_PL>(TEntity, "usp_ConsumerPayment");
            var FormNo = service.GetFerrulBillNo<ferrulMaster>(fm, "usp_FerrulCharge",UserModel.CM_ID);
            var fReceptCode = service.GetfReceptCode<ferrulMaster>(fm, "usp_FerrulCharge", UserModel.CM_ID);
            var cReceptCode = service.GetcReceptCode<ferrulMaster>(fm, "usp_FerrulCharge", UserModel.CM_ID);
            try
            {
                if (ConsumerDetails != null)
                {
                    status.IsSuccess = true;
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(new { ConsumerDetails = ConsumerDetails, FormNo = FormNo, fReceptCode= fReceptCode, cReceptCode= cReceptCode }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsUpFerrul(ferrulMaster TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.PM_PartyCode != "")
                {
                    TEntity.CM_ID = UserModel.CM_ID;
                    TEntity.UserId = UserModel.UserId;
                    TEntity.FyId = UserModel.FyId;
                    var result = service.InsUpFerrul(TEntity, "usp_FerrulCharge");
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    else if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    else if(result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                    else
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteFerrul(ferrulMaster TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.FerulId != 0)
                {
                    var result = service.GlobalDelete("ferrulMaster", "FerulId", (long)TEntity.FerulId, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been delete successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Update For Consumer Tax Payment
        public JsonResult GetConDelForConTaxPayment(string PM_PartyCode)
        {
            PartyLedger_PL TEntity = new PartyLedger_PL();
            ferrulMaster fm = new ferrulMaster();
            TEntity.PM_PartyCode = PM_PartyCode;
            StatusResponse status = new StatusResponse();
            var ConsumerDetails = service.GetConsumerDetails<PartyLedger_PL>(TEntity, "usp_ConsumerPayment");
            try
            {
                if (ConsumerDetails != null)
                {
                    status.IsSuccess = true;
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(new { ConsumerDetails = ConsumerDetails}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InsUpConTaxPayment(PartyMaster_PM TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.PM_PartyCode != "")
                {
                    TEntity.CM_ID = UserModel.CM_ID;
                    TEntity.UserId = UserModel.UserId;
                    TEntity.FyId = UserModel.FyId;
                    var result = service.InsUpConTaxPayment(TEntity, "usp_InsUpConTaxPayment");
                    if (result == 100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region WaterTax Collection
        public JsonResult GetConDelForWaterTaxCollection(string PM_PartyCode)
        {
            PartyLedger_PL TEntity = new PartyLedger_PL();
            PartyTax_PT pl = new PartyTax_PT();
            TEntity.PM_PartyCode = PM_PartyCode;
            pl.PT_PtyCode = PM_PartyCode;
            pl.CM_ID = UserModel.CM_ID;
            StatusResponse status = new StatusResponse();
            var ConsumerDetails = service.GetConsumerDetails<PartyLedger_PL>(TEntity, "usp_ConsumerPayment");
            var fReceptCode = service.GetReceptNoForTax<PartyTax_PT>(pl, "usp_PartyTax_PT");
            var LastTaxDate = service.GetLastTaxDate<PartyTax_PT>(pl, "usp_PartyTax_PT");

            try
            {
                if (ConsumerDetails != null)
                {
                    status.IsSuccess = true;
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(new { ConsumerDetails = ConsumerDetails, fReceptCode= fReceptCode, LastTaxDate= LastTaxDate }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InsUpWaterTaxCollection(PartyTax_PT TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.PM_PartyCode != "")
                {
                    TEntity.CM_ID = UserModel.CM_ID;
                    TEntity.UserId = UserModel.UserId;
                    TEntity.FyId = UserModel.FyId;
                    var result = service.InsUpWaterTaxCollection(TEntity, "usp_PartyTax_PT");
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    else if(result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    else if(result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                    else
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteWaterTaxCollection(PartyTax_PT TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.PT_ID != 0)
                {
                    var result = service.GlobalDelete("PartyTax_PT", "PT_ID", (long)TEntity.PT_ID, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been delete successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Re Connection
        public JsonResult GetConDelForReCon(string PM_PartyCode)
        {
            PartyLedger_PL TEntity = new PartyLedger_PL();
            RepReConnection rc = new RepReConnection();
            rc.CM_ID = UserModel.CM_ID;
            TEntity.PM_PartyCode = PM_PartyCode;
            StatusResponse status = new StatusResponse();
            var ConsumerDetails = service.GetConsumerDetails<PartyLedger_PL>(TEntity, "usp_ConsumerPayment");
            var BillNo = service.GetBillNoForRecon<RepReConnection>(rc, "usp_ReConnection");
            try
            {
                if (ConsumerDetails != null)
                {
                    status.IsSuccess = true;
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(new { ConsumerDetails = ConsumerDetails, BillNo = BillNo }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InsUpReConnection(RepReConnection TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.PM_PartyCode != "")
                {
                    TEntity.CM_ID = UserModel.CM_ID;
                    TEntity.UserId = UserModel.UserId;
                    TEntity.FyId = UserModel.FyId;
                    var result = service.InsUpReConnection(TEntity, "usp_ReConnection");
                    if (result == 100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteReCon(RepReConnection TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.GS_SIID != 0)
                {
                    //long deleteid = (long)TEntity.ins_code;
                    var result = service.GlobalDelete("SalesInvoice_GS", "GS_SIID", (long)TEntity.GS_SIID, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Consumer Invoice
        public JsonResult GetConDelForConInv(string PM_PartyCode)
        {
            PartyLedger_PL TEntity = new PartyLedger_PL();
            RepReConnection rc = new RepReConnection();
            rc.CM_ID = UserModel.CM_ID;
            TEntity.PM_PartyCode = PM_PartyCode;
            StatusResponse status = new StatusResponse();
            var ConsumerDetails = service.GetConsumerDetails<PartyLedger_PL>(TEntity, "usp_ConsumerPayment");
            var BillNo = service.GetBillNoForConInv<RepReConnection>(rc, "usp_ConsumerInv");
            try
            {
                if (ConsumerDetails != null)
                {
                    status.IsSuccess = true;
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(new { ConsumerDetails = ConsumerDetails, BillNo = BillNo }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InsUpConInv(RepReConnection TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.PM_PartyCode != "")
                {
                    TEntity.CM_ID = UserModel.CM_ID;
                    TEntity.UserId = UserModel.UserId;
                    TEntity.FyId = UserModel.FyId;
                    var result = service.InsUpConInv(TEntity, "usp_ConsumerInv");
                    if (result == 100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == 10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteConInv(RepReConnection TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.GS_SIID != 0)
                {
                    
                    var result = service.GlobalDelete("SalesInvoice_GS", "GS_SIID", (long)TEntity.GS_SIID, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }



        #endregion
        #region TankBooking

        public JsonResult GetConDelForTankBooking(string AP_Code)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                TankBooking_TB applicant = new TankBooking_TB();
                applicant.AP_Code = AP_Code;

                var ApplicantDetails = service.GetConsumerDetailss<TankBooking_TB>(
     new PartyLedger_PL { PM_PartyCode = AP_Code },
     "usp_ConsumerPayment"
 );

                if (ApplicantDetails != null)
                {
                    status.IsSuccess = true;
                    return Json(new { IsSuccess = true, ApplicantDetails = ApplicantDetails }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "Applicant not found!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = "Error: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult InsUpTankbooking(TankBooking_TB TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.AP_Code != "")
                {
                    TEntity.CM_ID = UserModel.CM_ID;
                    TEntity.FyId = 2022;
                    TEntity.CreatedDate = DateTime.Now;
                    var result = service.InsUpTankbooking(TEntity, "usp_TankBooking");
                    if (result >0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == -1)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteBooking(TankBooking_TB TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.AP_ID != 0)
                {
                    var result = service.GlobalDelete("TankBooking_TB", "AP_ID", TEntity.AP_ID, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region VanBooking

        public JsonResult GetConDelForVanBooking(string AP_Code)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                VanBooking_VB applicant = new VanBooking_VB();
                applicant.AP_Code = AP_Code;

                var ApplicantDetails = service.GetConsumerDetailsss<VanBooking_VB>(
     new PartyLedger_PL { PM_PartyCode = AP_Code },
     "usp_ConsumerPayment"
 );

                if (ApplicantDetails != null)
                {
                    status.IsSuccess = true;
                    return Json(new { IsSuccess = true, ApplicantDetails = ApplicantDetails }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "Applicant not found!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = "Error: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult InsUpVanbooking(VanBooking_VB TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.AP_Code != "")
                {
                    TEntity.CM_ID = UserModel.CM_ID;
                    TEntity.FyId = 2022;
                    TEntity.CreatedDate = DateTime.Now;
                    var result = service.InsUpVanbooking(TEntity, "usp_VanBooking");
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    if (result == -1)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation Update has been done successfully.......";
                    }
                    if (result == -10)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Operation Update has been done successfullyData already exixts........";
                    }
                    if (result == -100)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteVanBooking(VanBooking_VB TEntity)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (TEntity.AP_ID != 0)
                {
                    var result = service.GlobalDelete("VanBooking_VB", "AP_ID", TEntity.AP_ID, null, null);
                    if (result > 0)
                    {
                        status.Result = result;
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Something wrong happened. ";
                    }
                }
                else
                {
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Result = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region NoticeMaster
        public JsonResult GetConsumerForNotice(string PartyCode)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                NoticeMaster_NM notice = new NoticeMaster_NM();
                notice.NM_PartyCode = PartyCode;

                var ConsumerDetails = service.GetConsumerDetailsssGet<NoticeMaster_NM>(
      new PartyLedger_PL { PM_PartyCode = PartyCode },
      "SP_NoticeMaster_NM"
  );

              

                if (ConsumerDetails != null)
                {
                    status.IsSuccess = true;

                    return Json(new
                    {
                        IsSuccess = true,
                        ConsumerDetails = ConsumerDetails
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        IsSuccess = false,
                        Message = "Consumer not found!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    IsSuccess = false,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult InsUpNotice(NoticeMaster_NM TEntity, HttpPostedFileBase NoticeFile)
        {
            StatusResponse status = new StatusResponse();

            try
            {
                if (NoticeFile != null && NoticeFile.ContentLength > 0)
                {
                    string folder = Server.MapPath("~/Uploads/Notice/");

                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    string fileName = Guid.NewGuid().ToString()
                                    + Path.GetExtension(NoticeFile.FileName);

                    string fullPath = Path.Combine(folder, fileName);

                    NoticeFile.SaveAs(fullPath);

                    TEntity.NM_UploadFile = "/Uploads/Notice/" + fileName;
                }

                if (TEntity.NM_Id == 0)
                    TEntity.NM_CreatedBy = UserModel.UserId;
                else
                    TEntity.NM_EditedBy = UserModel.UserId;

                long result = service.InsUpNotice(TEntity, "SP_NoticeMaster_NM");

                if (result > 0)
                {
                    status.IsSuccess = true;
                    status.Message = TEntity.NM_Id == 0
                        ? "Notice Saved Successfully."
                        : "Notice Updated Successfully.";
                }
                else
                {
                    status.IsSuccess = false;
                    status.Message = "Operation Failed.";
                }
            }
            catch (Exception ex)
            {
                status.IsSuccess = false;
                status.Message = ex.Message;
            }

            return Json(status);
        }
        public JsonResult DeleteNotice(NoticeMaster_NM TEntity)
        {
            StatusResponse status = new StatusResponse();

            try
            {
                if (TEntity.NM_Id != 0)
                {
                    var result = service.GlobalDelete(
                        "NoticeMaster_NM",
                        "NM_Id",
                        TEntity.NM_Id,
                        null,
                        null);

                    if (result > 0)
                    {
                        status.IsSuccess = true;
                        status.Result = result;
                        status.Message = "Notice Deleted Successfully.";
                    }
                    else
                    {
                        status.IsSuccess = false;
                        status.Message = "Delete Failed.";
                    }
                }
                else
                {
                    status.IsSuccess = false;
                    status.Message = "Invalid Notice.";
                }
            }
            catch (Exception ex)
            {
                status.IsSuccess = false;
                status.Message = ex.Message;
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PublishNotice(long NM_Id, bool Publish)
        {
            StatusResponse status = new StatusResponse();

            try
            {
                NoticeMaster_NM obj = new NoticeMaster_NM();

                obj.NM_Id = NM_Id;
                obj.NM_IsPublish = Publish;

                var result = service.PublishNotice(obj, "SP_NoticeMaster_NM");

                if (result > 0)
                {
                    status.IsSuccess = true;
                    status.Message = Publish
                        ? "Notice Published Successfully."
                        : "Notice Unpublished Successfully.";
                }
                else
                {
                    status.IsSuccess = false;
                    status.Message = "Operation Failed.";
                }
            }
            catch (Exception ex)
            {
                status.IsSuccess = false;
                status.Message = ex.Message;
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetParaByArea(long AreaId)
        {
            try
            {
                var list = service.GetParaByArea<ParaMaster_PM>(
                    new ParaMaster_PM
                    {
                        AM_AreaCode = AreaId
                    },
                    "SP_NoticeMaster_NM");

                return Json(new
                {
                    IsSuccess = true,
                    Data = list
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    IsSuccess = false,
                    Message = ex.ToString()   // <-- use ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #endregion

    }
}
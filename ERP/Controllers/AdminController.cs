using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BObject;
using ERP.CustomAuthentication;
using System.Messaging;
using ERP.DataAccess;
using ERP.Models;
namespace ERP.Controllers
{
    //[CustomAuthorize(Roles = "User,Admin,SuperAdmin")]
    //[HandleError(View = "Error")]
    public class AdminController : BaseController
    {
        // GET: Admin
        #region Layout
        public ActionResult Layout()
        {
            if (UserModel == null) return returnLogin(null);

            return View();
        }
        public ActionResult LayoutList()
        {
            if (UserModel == null) return returnLogin(null);

            return View();
        }
        #endregion
        #region Menu
        public ActionResult Menu(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Admin/Menu");
            //var url = Request.RequestContext.HttpContext.Request.RawUrl;
            //GetRights(url);
            MenuMasterModel menu = new MenuMasterModel();
            var MenuList = service.GetGlobalSelect<MenuMasterModel>("Menu", "IsActive", 1).Where(p => p.IsBaseMenu == true);
            if (Id != null)
            {
                menu = service.GetGlobalSelectOne<MenuMasterModel>("Menu", "MenuId", Id);
                ViewBag.ParentMenuId = new SelectList(MenuList, "MenuId", "MenuName", menu.ParentMenuId);
                ViewBag.Button = "UPDATE";
            }
            else
            {
                ViewBag.ParentMenuId = new SelectList(MenuList, "MenuId", "MenuName");
                ViewBag.Button = "SUBMIT";
            }
            return View(menu);
        }
        [HttpPost]
        public ActionResult Menu(MenuMasterModel menu)
        {
            if (UserModel == null) return returnLogin("~/Admin/Menu");
            //var url = Request.RequestContext.HttpContext.Request.RawUrl;
            //GetRights(url);
            menu.CreatedBy = UserModel.UserId;
            var result = service.InsUpMenu(menu, "usp_Menu");
            if (result == 100)
            {
                TempData["Message"] = "Menu Created Successfull.";
                return RedirectToAction("Menu", "Admin");
            }
            else if (result == 10)
            {
                TempData["Message"] = "Menu Already Exist...!";
            }
            else
            {
                TempData["Message"] = "Somthing Went Wrong Please Contact Support team...!";
            }
            var MenuList = service.GetGlobalSelect<MenuMasterModel>("Menu", "IsActive", 1).Where(p => p.IsBaseMenu == true);
            ViewBag.ParentMenuId = new SelectList(MenuList, "MenuId", "MenuName");
            ViewBag.Button = "UPDATE";
            return View(menu);
        }
        public ActionResult MenuList()
        {
            if (UserModel == null) return returnLogin(null);

            return View();
        }
        #endregion
        #region AccessRight
        public ActionResult AccessRight(int? Id)
        {
            if (UserModel == null) return returnLogin("~/Admin/AccessRight");
            var url = Request.RequestContext.HttpContext.Request.RawUrl;
            GetRights(url);
            AccessRights mmm = new AccessRights();
            var RoleList = service.GetGlobalSelect<Role>("Roles", "RoleId", null);
            if (Id != 0 && Id != null)
            {
                ViewBag.RoleId = new SelectList(RoleList, "RoleId", "RoleName", Id);
                mmm.RoleId = Id;
            }
            else
            {
                ViewBag.RoleId = new SelectList(RoleList, "RoleId", "RoleName");
            }
            mmm.MenuList = service.GetAccessRightMenuList<AccessRights>(mmm, "usp_AccessRightMenu");
            return View(mmm);
        }
        public ActionResult AccessRightMenuList(AccessRights arm)
        {
            if (UserModel == null) return returnLogin(null);
            arm.MenuList = service.GetAccessRightMenuList<AccessRights>(arm, "usp_AccessRightMenu");
            var MenuList = arm.MenuList.ToList().Where(m => m.MenuId.Equals(arm.MenuId) || m.ParentMenuId.Equals(arm.MenuId));
            return Json(MenuList, JsonRequestBehavior.AllowGet);
        }
        [HandleError(View = "Error")]
        public ActionResult InsUpAccessRightMenu(AccessRights arm)
        {
            if (UserModel == null) return returnLogin(null);
            StatusResponse sr = new StatusResponse();
            try
            {
                arm.CreatedBy = UserModel.UserId;
                sr.Result = service.InsUpAccessRightMenuList(arm, "usp_AccessRightMenu");
                if (sr.Result == 100)
                {
                    sr.IsSuccess = true;
                    sr.Result = 100;
                    sr.Message = "Menu Access Right Change Updated Successfull";
                }
                else
                {
                    sr.IsSuccess = false;
                    sr.Result = -100;
                    sr.Message = "Something Went Wrong...!";
                }
            }
            catch (Exception ex)
            {
                sr.IsSuccess = false;
                sr.Result = -10;
                sr.Message = ex.Message;
                sr.ActionName = "AccessRightMenuList";
                sr.Controller = "Admin";
            }
            return Json(sr, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Company Change And Session Bind
        public ActionResult ChangeCompanyAndSessionBind(int CM_ID)
        {
            StatusResponse sr = new StatusResponse();
            try
            {
                UserModel.CM_ID = CM_ID;
                sr.IsSuccess = true;
                sr.Result = 100;
            }
            catch (Exception ex)
            {
                sr.IsSuccess = false;
                sr.Result = -10;
                sr.Message = ex.Message;
                sr.ActionName = "AccessRightMenuList";
                sr.Controller = "Admin";
            }
            return Json(sr, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
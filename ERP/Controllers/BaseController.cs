using BLogic;
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
    public class BaseController : Controller
    {
        public static Service service;
        public static AccountService  accountService;
        public StatusResponse response = new StatusResponse();
        public static List<MenuMasterModel> MenuModel { get; set; }
        public static UserRoleDetails UserModel { get; set; }
        public static void GetSession()
        {
            UserModel = (UserRoleDetails)System.Web.HttpContext.Current.Session["UserModel"];
        }
        public BaseController()
        {
            GetSession();
            service = new Service();
            accountService = new AccountService();
            this.response = new StatusResponse();
            if (UserModel != null)
            {
                UserModel.FyId = 2022;
                ViewBag.UserId = UserModel.UserId;
                ViewBag.RoleId = UserModel.RoleId;
                ViewBag.ProjectId = UserModel.ProjectId;
                ViewBag.FirstName = UserModel.FirstName;
                ViewBag.LastName = UserModel.LastName;
                ViewBag.CMID = UserModel.CM_ID;
                ViewBag.CompanyList = UserModel.LstCompany;
                MenuModel = BuildMenu(UserModel.UserId);
            }
        }
        public ActionResult home(string returnUrl)
        {
            return string.IsNullOrWhiteSpace(returnUrl) ? (ActionResult)RedirectToAction("Index", "Home", new { area = "" }) : Redirect(returnUrl);
        }
        public ActionResult returnLogin(string returnUrl)
        {
            return (ActionResult)RedirectToAction("Login", "Account", new { area = "", returnUrl = returnUrl });
        }
        #region BuildMenu
        public static List<MenuMasterModel> BuildMenu(int UserId)
        {
            List<MenuMasterModel> lstMenu = new List<MenuMasterModel>();
            MenuMasterModel TEntity = new MenuMasterModel();
            TEntity.UserId = UserId;
            if (UserId == 0)
                return lstMenu;
            lstMenu = service.GetMenuList<MenuMasterModel>(TEntity, "usp_MenuAccess");
            return lstMenu;
        }
        #endregion
        #region GetRights
        public static void GetRights(string url)
        {
            string[] urllink = url.Split('?');
            url = urllink[0];
            string lastonechar = url.Substring((url.Length - 1), 1);
            if (lastonechar == "/")
            {
                url = url.Remove(url.Length - 1, 1);
            }
            
            long MenuId = MenuModel.FirstOrDefault(x => x.ActionUrl.Contains(url)).MenuId;
            long RoleId = UserModel.RoleId;
            var AssignRightsList = service.GetGlobalSelect<AccessRights>("AccessRights", "AssignRightsId", null)
                .Single(x => x.MenuId == MenuId && x.RoleId == RoleId);

            System.Web.HttpContext.Current.Session["CanView"] = AssignRightsList.CanView == true ? true : false;
            System.Web.HttpContext.Current.Session["CanAdd"] = AssignRightsList.CanAdd == true ? true : false;
            System.Web.HttpContext.Current.Session["CanEdit"] = AssignRightsList.CanEdit == true ? true : false;
            System.Web.HttpContext.Current.Session["CanDelete"] = AssignRightsList.CanDelete == true ? true : false;
            System.Web.HttpContext.Current.Session["CanSubmit"] = AssignRightsList.CanSubmit == true ? true : false;
            System.Web.HttpContext.Current.Session["CanUpdate"] = AssignRightsList.CanUpdate == true ? true : false;
        }
        #endregion
    }
}
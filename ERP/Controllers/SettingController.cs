using ERP.CustomAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers
{
    //[CustomAuthorize(Roles = "User,Admin,SuperAdmin")]
    //[HandleError(View = "Error")]
    public class SettingController : BaseController
    {
        // GET: Setting
        #region Project
        public ActionResult Project()
        {
            if (UserModel == null) return returnLogin(null);
            ViewBag.Button = "UPDATE";
            return View();
        }
        #endregion
    }
}
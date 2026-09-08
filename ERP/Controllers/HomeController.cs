using BObject;
using ERP.CustomAuthentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLogic;

namespace ERP.Controllers
{
    public class HomeController : BaseController
    {
        //public ActionResult Index()
        //{
        //    if (UserModel == null) return returnLogin(null);
        //    return View();

        //}

        public ActionResult Index()
        {
            if (UserModel == null)
                return returnLogin(null);

            DashboardModel model = service.GetDashboardCard();

            ViewBag.MonthChart =
                JsonConvert.SerializeObject(service.GetMonthlyChart());

            ViewBag.YearChart =
                JsonConvert.SerializeObject(service.GetYearlyChart());

            return View(model);
        }
        [HttpPost]
        public JsonResult GetDashboardByDate(DateTime FromDate, DateTime ToDate)
        {
            DashboardModel model = service.GetDashboardCard(FromDate, ToDate);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}
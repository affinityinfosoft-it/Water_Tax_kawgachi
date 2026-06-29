using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Models;

namespace ERP.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Message(string message)
        {
            StatusResponse sr = new StatusResponse();
            sr.Message = message;
            return View(sr);
        }
    }
}
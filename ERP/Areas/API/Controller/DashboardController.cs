using ERP.Areas.API.DAL;
using ERP.Areas.API.Helper;
using ERP.Areas.API.Model.Request;
using ERP.Areas.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP.Areas.API.Controller
{
    [RoutePrefix("API/dashboard")]
    public class DashboardController : ApiController
    {
        [HttpPost]
        [Route("dashboard-amount")]
        public IHttpActionResult Dashboard()
        {
            ApiResponse response = new ApiResponse();

            var auth = Request.Headers.Authorization;

            if (auth == null)
            {
                response.Success = false;
                response.Message = "Authorization Token Missing.";
                return Ok(response);
            }

            if (auth.Scheme != "Bearer")
            {
                response.Success = false;
                response.Message = "Invalid Authorization Type.";
                return Ok(response);
            }

            TokenManager tokenManager = new TokenManager();

            ApiResponse token = tokenManager.ValidateToken(auth.Parameter);

            if (!token.Success)
            {
                return Ok(token);
            }

            DashboardRequest request = new DashboardRequest();

            request.PartyCode = token.Data.ToString();

            DashboardDAL dal = new DashboardDAL();

            return Ok(dal.GetDashboard(request));
        }
    }
}

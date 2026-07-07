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
    [RoutePrefix("API/receipt")]
    public class ReceiptController : ApiController
    {
        [HttpPost]
        [Route("receipt-list")]
        public IHttpActionResult ReceiptList()
        {
            ApiResponse response = new ApiResponse();

            // Check Authorization Header
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

            // Validate Token
            TokenManager tokenManager = new TokenManager();

            ApiResponse token = tokenManager.ValidateToken(auth.Parameter);

            if (!token.Success)
            {
                return Ok(token);
            }

            // Get PartyCode from Token
            ReceiptRequest request = new ReceiptRequest();

            request.PartyCode = token.Data.ToString();

            // Call DAL
            ReceiptDAL dal  = new ReceiptDAL();

            return Ok(dal.GetReceiptList(request));
        }

        [HttpPost]
        [Route("receipt-details")]
        public IHttpActionResult ReceiptDetails(ReceiptDetailsRequest request)
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

            // Optional security check:
            // Verify that the BillNo belongs to the authenticated PartyCode
            // before returning details.

            ReceiptDAL dal = new ReceiptDAL();

            return Ok(dal.GetReceiptDetails(request));
        }

    }
}

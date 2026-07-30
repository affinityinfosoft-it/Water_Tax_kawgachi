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
        [RoutePrefix("api/Complaint")]
        public class ComplaintController : ApiController
        {
            ComplaintDAL dal = new ComplaintDAL();

            //-------------------------------------------------------
            // Get Complaint Types
            //-------------------------------------------------------
            [HttpPost]
            [Route("complaint-types")]
            public IHttpActionResult GetComplaintTypes()
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
                    return Ok(token);

                return Ok(dal.GetComplaintTypes());
            }

            //-------------------------------------------------------
            // Save Complaint
            //-------------------------------------------------------
            [HttpPost]
            [Route("save-complaint")]
            public IHttpActionResult SaveComplaint(ComplaintRequest request)
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
                    return Ok(token);

                // PartyCode from Token
                request.PartyCode = token.Data.ToString();

                // Default Values
                request.CM_ID = 1;
                request.FyId = 2022;
                request.UserId = Convert.ToInt32(request.PartyCode);

                return Ok(dal.SaveComplaint(request));
            }

            //-------------------------------------------------------
            // My Complaints
            //-------------------------------------------------------
            [HttpPost]
            [Route("my-complaints")]
            public IHttpActionResult MyComplaints()
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
                    return Ok(token);

                ComplaintRequest request = new ComplaintRequest();

                request.PartyCode = token.Data.ToString();

                return Ok(dal.MyComplaints(request.PartyCode));
            }

            //-------------------------------------------------------
            // Complaint Details
            //-------------------------------------------------------
            [HttpPost]
            [Route("complaint-details")]
            public IHttpActionResult ComplaintDetails(ComplaintRequest request)
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
                    return Ok(token);

                request.PartyCode = token.Data.ToString();

                return Ok(dal.ComplaintDetails(request.ComplaintId));
            }

        //-------------------------------------------------------
        // Complaint History
        //-------------------------------------------------------
        [HttpPost]
        [Route("complaint-history")]
        public IHttpActionResult ComplaintHistory(ComplaintRequest request)
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
                return Ok(token);

            request.PartyCode = token.Data.ToString();

            return Ok(dal.ComplaintHistory(request.ComplaintId));
        }


    }
    }


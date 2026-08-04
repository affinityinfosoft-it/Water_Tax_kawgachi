using ERP.Areas.API.DAL;
using ERP.Areas.API.Helper;
using ERP.Areas.API.Model.Request;
using ERP.Areas.API.Model.Response;
using System.Web.Http;

namespace ERP.Areas.API.Controller
{
    [RoutePrefix("API/notice")]
    public class NoticeController : ApiController
    {
        [HttpPost]
        [Route("notice-list")]
        public IHttpActionResult NoticeList()
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

            // Get PartyCode from Token
            NoticeRequest request = new NoticeRequest();
            request.PartyCode = token.Data.ToString();

            NoticeDAL dal = new NoticeDAL();

            return Ok(dal.GetNoticeList(request));
        }
    }
}
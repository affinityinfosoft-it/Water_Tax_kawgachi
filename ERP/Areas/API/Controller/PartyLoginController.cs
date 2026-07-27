using ERP.Areas.API.DAL;
using ERP.Areas.API.Helper;
using ERP.Areas.API.Model.Request;
using ERP.Areas.API.Model.Response;
using System.Web.Http;

namespace ERP.Areas.API.Controllers
{
    [RoutePrefix("API/party")]
    public class PartyLoginController : ApiController
    {
        private readonly PartyLoginDAL dal = new PartyLoginDAL();

        [HttpPost]
        [Route("verify-party")]
        public IHttpActionResult VerifyParty(VerifyPartyRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = dal.VerifyParty(request);

            return Ok(result);
        }

        [HttpPost]
        [Route("send-otp")]
        public IHttpActionResult SendOTP(SendOTPRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = dal.SendOTP(request);

            return Ok(result);
        }


        [HttpPost]
        [Route("verify-otp")]
        public IHttpActionResult VerifyOTP(VerifyOTPRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = dal.VerifyOTP(request);

            return Ok(result);
        }

        [HttpPost]
        [Route("create-password")]
        public IHttpActionResult CreatePassword(CreatePasswordRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.Password != request.ConfirmPassword)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Password and Confirm Password do not match."
                });
            }

            var result = dal.CreatePassword(request);

            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = dal.Login(request);

            return Ok(result);
        }

        [HttpPost]
        [Route("update-password")]
        public IHttpActionResult UpdatePassword(UpdatePasswordRequest request)
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

            string partyCode = token.Data.ToString();

            return Ok(dal.UpdatePassword(partyCode, request));
        }
    }
}
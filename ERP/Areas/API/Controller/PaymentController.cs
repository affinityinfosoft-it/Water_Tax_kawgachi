using ERP.Areas.API.DAL;
using ERP.Areas.API.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP.Areas.API.Controller
{
    [RoutePrefix("api/payment")]
    public class PaymentController : ApiController
    {
        PaymentDAL dal = new PaymentDAL();

        //---------------------------------------------------
        // 1. Payment Details
        //---------------------------------------------------

        [HttpPost]
        [Route("details")]
        public IHttpActionResult PaymentDetails(PaymentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = dal.GetPaymentDetails(request.PartyCode);

            return Ok(result);
        }

        //---------------------------------------------------
        // 2. Calculate Month Wise Amount
        //---------------------------------------------------

        [HttpPost]
        [Route("calculate")]
        public IHttpActionResult CalculateAmount(PaymentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = dal.CalculateAmount(request);

            return Ok(result);
        }

        //---------------------------------------------------
        // 3. Calculate Full Due
        //---------------------------------------------------

        [HttpPost]
        [Route("fulldue")]
        public IHttpActionResult CalculateFullDue(PaymentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = dal.CalculateFullDue(request.PartyCode);

            return Ok(result);
        }

        //---------------------------------------------------
        // 4. Save Payment
        //---------------------------------------------------

        [HttpPost]
        [Route("save")]
        public IHttpActionResult SavePayment(PaymentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            request.UserId = Convert.ToInt32(request.PartyCode);
            var result = dal.SavePayment(request);

            return Ok(result);
        }
    }
}
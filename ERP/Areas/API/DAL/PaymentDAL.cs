using ERP.Areas.API.Model.Request;
using ERP.Areas.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ERP.Areas.API.DAL
{
    public class PaymentDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["ERP_DB_Conn"].ConnectionString;


        public ApiResponse GetPaymentDetails(string partyCode)
        {
            ApiResponse response = new ApiResponse();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SP_APIPayment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TransType", "GetPaymentDetails");
                cmd.Parameters.AddWithValue("@PartyCode", partyCode);
                cmd.Parameters.AddWithValue("@CM_ID", 1);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            dr.GetName(i) + " = " +
                            (dr.IsDBNull(i) ? "NULL" : dr.GetValue(i).ToString()));
                    }

                    response.Success = true;

                    response.Data = new
                    {
                        PartyCode = dr["PM_PartyCode"].ToString(),
                        PartyName = dr["PM_PartyName"].ToString(),
                        FathersName = dr["PM_FHName"].ToString(),
                        Address = dr["PM_Address"].ToString(),
                        Mobile = dr["PM_MobNo"].ToString(),

                        LastPaidDate = dr["LastPaidDate"].ToString(),
                        DueFrom = dr["DueFrom"].ToString(),

                        DueMonth = Convert.ToInt32(dr["DueMonth"]),

                        FirstSlabDate = Convert.ToInt32(dr["FirstSlabDate"]),
                        SecondSlabDate = Convert.ToInt32(dr["SecondSlabDate"]),
                        ThirdSlabDate = Convert.ToInt32(dr["ThirdSlabDate"]),

                        FirstSlabAmount = Convert.ToDecimal(dr["FirstSlabAmount"]),
                        SecondSlabAmount = Convert.ToDecimal(dr["SecondSlabAmount"]),
                        ThirdSlabAmount = Convert.ToDecimal(dr["ThirdSlabAmount"]),

                        PaymentDate = Convert.ToDateTime(dr["PaymentDate"]).ToString("yyyy-MM-dd")
                    };
                }
            }

            return response;
        }


        public ApiResponse CalculateAmount(PaymentRequest request)
        {
            ApiResponse response = new ApiResponse();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SP_APIPayment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TransType", "CalculateAmount");
                cmd.Parameters.AddWithValue("@PartyCode", request.PartyCode);
                cmd.Parameters.AddWithValue("@CM_ID", 1);
                cmd.Parameters.Add("@PaymentDate", SqlDbType.Date).Value =request.PaymentDate.HasValue? (object)request.PaymentDate.Value.Date: DBNull.Value;
                cmd.Parameters.AddWithValue("@NoOfMonth", request.NoOfMonth);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    response.Success = true;

                    response.Data = new
                    {
                        PaymentFrom = dr["PaymentFrom"].ToString(),
                        PaymentTo = dr["PaymentTo"].ToString(),
                        PaymentMonth = Convert.ToInt32(dr["PaymentMonth"]),
                        Amount = Convert.ToDecimal(dr["Amount"])
                    };
                }
            }

            return response;
        }
        public ApiResponse CalculateFullDue(string partyCode)
        {
            ApiResponse response = new ApiResponse();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SP_APIPayment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TransType", "CalculateFullDue");
                cmd.Parameters.AddWithValue("@PartyCode", partyCode);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    response.Success = true;

                    response.Data = new
                    {
                        DueMonth = Convert.ToInt32(dr["TotalDueMonth"]),
                        PaymentFrom = dr["PaymentFrom"].ToString(),
                        PaymentTo = dr["PaymentTo"].ToString(),
                        TotalDueAmount = Convert.ToDecimal(dr["TotalDueAmount"])
                    };
                }
            }

            return response;
        }
        public ApiResponse SavePayment(PaymentRequest request)
        {
            ApiResponse response = new ApiResponse();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SP_APIPayment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TransType", "SavePayment");
                cmd.Parameters.AddWithValue("@PartyCode", request.PartyCode);
                cmd.Parameters.AddWithValue("@PaymentDate", request.PaymentDate);
                cmd.Parameters.AddWithValue("@NoOfMonth", request.NoOfMonth);
                cmd.Parameters.AddWithValue("@PaymentFrom", request.PaymentFrom);
                cmd.Parameters.AddWithValue("@PaymentTo", request.PaymentTo);
                cmd.Parameters.AddWithValue("@Amount", request.Amount);
                cmd.Parameters.AddWithValue("@PaymentAmount", request.PaymentAmount);
                cmd.Parameters.AddWithValue("@CM_ID", 1);
                cmd.Parameters.AddWithValue("@FyId", 2022);
                request.UserId = Convert.ToInt32(request.PartyCode);
                cmd.Parameters.AddWithValue("@UserId", request.UserId);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    response.Success = Convert.ToInt32(dr["Status"]) == 1;
                    response.Message = dr["Message"].ToString();

                    if (response.Success)
                    {
                        response.Data = new
                        {
                            ReceiptNo = dr["ReceiptNo"].ToString(),
                            BillNo = dr["BillNo"].ToString(),
                            Amount = Convert.ToDecimal(dr["Amount"])
                        };
                    }
                }
            }

            return response;
        }
    }
}
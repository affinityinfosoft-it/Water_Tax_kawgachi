using ERP.Areas.API.Helper;
using ERP.Areas.API.Model.Request;
using ERP.Areas.API.Model.Response;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ERP.Areas.API.DAL
{
    public class PartyLoginDAL
    {
        private readonly string conString =
            ConfigurationManager.ConnectionStrings["ERP_DB_Conn"].ConnectionString;

        public ApiResponse VerifyParty(VerifyPartyRequest request)
        {
            ApiResponse response = new ApiResponse();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SP_APIVerifyParty", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PartyCode", request.PartyCode);
                cmd.Parameters.AddWithValue("@MobileNo", request.MobileNo);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    bool success = Convert.ToInt32(dr["Status"]) == 1;

                    response.Success = success;
                    response.Message = dr["Message"].ToString();

                    if (success)
                    {
                        Random random = new Random();

                        string otp = random.Next(100000, 999999).ToString();

                        dr.Close();

                        SaveOTP(request.PartyCode, otp);

                        // TODO
                        // SMSHelper.Send(request.MobileNo, otp);

                        // Remove this after SMS integration
                        response.Data = otp;
                    }
                }

                return response;
            }
        }
        public ApiResponse SendOTP(SendOTPRequest request)
        {
            ApiResponse response = new ApiResponse();

            Random random = new Random();
            string otp = random.Next(100000, 999999).ToString();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SP_APISaveOTP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PartyCode", request.PartyCode);
                cmd.Parameters.AddWithValue("@OTP", otp);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            

            // SMS sending is bypassed during development
            response.Success = true;
            response.Message = "OTP generated successfully.";

            ///disable for don't have sms gatway SMSHelper.SendOTP(request.MobileNo, otp); 
            //SMSHelper.SendOTP(request.MobileNo, otp);

            response.Data = otp;   // Return OTP only for testing
            return response;
        }

        //private void SaveOTP(string partyCode, string otp)
        //{
        //    using (SqlConnection con = new SqlConnection(conString))
        //    {
        //        SqlCommand cmd = new SqlCommand("SP_APISaveOTP", con);

        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@PartyCode", partyCode);

        //        cmd.Parameters.AddWithValue("@OTP", otp);

        //        con.Open();

        //        cmd.ExecuteNonQuery();
        //    }
        //}
        public ApiResponse VerifyOTP(VerifyOTPRequest request)
        {
            ApiResponse response = new ApiResponse();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SP_APIVerifyOTP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PartyCode", request.PartyCode);
                cmd.Parameters.AddWithValue("@OTP", request.OTP);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    response.Success = Convert.ToInt32(dr["Status"]) == 1;
                    response.Message = dr["Message"].ToString();
                }
            }

            return response;
        }

        public ApiResponse CreatePassword(CreatePasswordRequest request)
        {
            ApiResponse response = new ApiResponse();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SP_APICreatePassword", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PartyCode", request.PartyCode);
                cmd.Parameters.AddWithValue("@Password", request.Password);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    response.Success = Convert.ToInt32(dr["Status"]) == 1;
                    response.Message = dr["Message"].ToString();
                }
            }

            return response;
        }



    }
}
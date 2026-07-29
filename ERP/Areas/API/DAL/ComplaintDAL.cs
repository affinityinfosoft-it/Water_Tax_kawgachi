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
    public class ComplaintDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["ERP_DB_Conn"].ConnectionString;
        public ApiResponse GetComplaintTypes()
        {
            ApiResponse response = new ApiResponse();

            List<object> list = new List<object>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SP_APIComplaint", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TransType", "GetComplaintTypes");
                cmd.Parameters.AddWithValue("@CM_ID", 1);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new
                    {
                        ComplaintTypeId = Convert.ToInt32(dr["CTM_Id"]),
                        ComplaintType = dr["CTM_ComplaintType"].ToString()
                    });
                }

                response.Success = true;
                response.Data = list;
            }

            return response;
        }
        public ApiResponse SaveComplaint(ComplaintRequest request)
        {
            ApiResponse response = new ApiResponse();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SP_APIComplaint", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TransType", "SaveComplaint");
                cmd.Parameters.AddWithValue("@PartyCode", request.PartyCode);
                cmd.Parameters.AddWithValue("@ComplaintTypeId", request.ComplaintTypeId);
                cmd.Parameters.AddWithValue("@Description", request.Description);
                cmd.Parameters.AddWithValue("@Priority", request.Priority);

                // Default values
                cmd.Parameters.AddWithValue("@CM_ID", 1);
                cmd.Parameters.AddWithValue("@FyId", 2022);

                // UserId = PartyCode
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(request.PartyCode));

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    response.Success = Convert.ToInt32(dr["Status"]) == 1;
                    response.Message = dr["Message"].ToString();

                    response.Data = new
                    {
                        ComplaintNo = dr["ComplaintNo"].ToString()
                    };
                }
            }

            return response;
        }
        public ApiResponse MyComplaints(string partyCode)
        {
            ApiResponse response = new ApiResponse();

            List<object> list = new List<object>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SP_APIComplaint", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TransType", "MyComplaints");
                cmd.Parameters.AddWithValue("@PartyCode", partyCode);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new
                    {
                        ComplaintId = Convert.ToInt64(dr["CMPL_Id"]),
                        ComplaintNo = dr["CMPL_ComplaintNo"].ToString(),
                        ComplaintType = dr["CTM_ComplaintType"].ToString(),
                        Description = dr["CMPL_Description"].ToString(),
                        Priority = dr["CMPL_Priority"].ToString(),
                        Status = dr["CMPL_Status"].ToString(),
                        ComplaintDate = dr["ComplaintDate"].ToString()
                    });
                }

                response.Success = true;
                response.Data = list;
            }

            return response;
        }
        public ApiResponse ComplaintDetails(long complaintId)
        {
            ApiResponse response = new ApiResponse();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SP_APIComplaint", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TransType", "ComplaintDetails");
                cmd.Parameters.AddWithValue("@ComplaintId", complaintId);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    response.Success = true;

                    response.Data = new
                    {
                        ComplaintId = Convert.ToInt64(dr["CMPL_Id"]),
                        ComplaintNo = dr["CMPL_ComplaintNo"].ToString(),
                        PartyCode = dr["CMPL_PartyCode"].ToString(),
                        ComplaintType = dr["CTM_ComplaintType"].ToString(),
                        Description = dr["CMPL_Description"].ToString(),
                        Priority = dr["CMPL_Priority"].ToString(),
                        Status = dr["CMPL_Status"].ToString(),
                        Remarks = dr["CMPL_Remarks"].ToString(),
                        AssignTo = dr["CMPL_AssignTo"].ToString(),
                        ComplaintDate = dr["ComplaintDate"].ToString(),
                        ResolvedDate = dr["ResolvedDate"].ToString()
                    };
                }
            }

            return response;
        }
    }
}
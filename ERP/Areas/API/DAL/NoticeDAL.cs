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
    public class NoticeDAL
    {
        public readonly string conString =
            ConfigurationManager.ConnectionStrings["ERP_DB_Conn"].ConnectionString;

        public ApiResponse GetNoticeList(NoticeRequest request)
        {
            ApiResponse response = new ApiResponse();

            List<object> list = new List<object>();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand("SP_APINotice", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TransType", "NoticeList");
                    cmd.Parameters.AddWithValue("@PartyCode", request.PartyCode);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        list.Add(new
                        {
                            NoticeId = Convert.ToInt64(dr["NM_Id"]),
                            NoticeCode = dr["NM_Code"].ToString(),
                            NoticeType = Convert.ToString(dr["NoticeType"]).Trim(),
                            Title = dr["NM_Title"].ToString(),
                            Notice = dr["NM_Notice"].ToString(),
                            FromDate = dr["NM_FromDate"] != DBNull.Value? Convert.ToDateTime(dr["NM_FromDate"]).ToString("dd-MMM-yyyy"): "",
                            ToDate = dr["NM_ToDate"] != DBNull.Value? Convert.ToDateTime(dr["NM_ToDate"]).ToString("dd-MMM-yyyy"): "",
                            Attachment = Convert.ToString(dr["Attachment"])
                        });
                    }

                    response.Success = true;
                    response.Message = "Notice List";
                    response.Data = list;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
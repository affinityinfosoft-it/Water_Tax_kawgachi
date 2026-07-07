using ERP.Areas.API.Model.Request;
using ERP.Areas.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ERP.Areas.API.DAL
{
    public class ReceiptDAL
    {
        public readonly string conString =
            ConfigurationManager.ConnectionStrings["ERP_DB_Conn"].ConnectionString;

        public ApiResponse GetReceiptList(ReceiptRequest request)
        {
            ApiResponse response = new ApiResponse();

            List<object> list = new List<object>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SP_APIReceipt", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TransType", "SelectAllReceipt");
                cmd.Parameters.AddWithValue("@PartyCode", request.PartyCode);
                cmd.Parameters.AddWithValue("@BillNo", DBNull.Value);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new
                    {
                        ReceiptNo = dr["ReceiptNo"].ToString(),

                        ReceiptDate = dr["ReceiptDate"] != DBNull.Value
                            ? Convert.ToDateTime(dr["ReceiptDate"]).ToString("dd-MMM-yyyy")
                            : "",

                        BillNo = dr["BillNo"].ToString(),

                        BillType = dr["BillType"].ToString(),

                        FromDate = dr["FromDate"] != DBNull.Value
                            ? Convert.ToDateTime(dr["FromDate"]).ToString("dd-MMM-yyyy")
                            : "",

                        ToDate = dr["ToDate"] != DBNull.Value
                            ? Convert.ToDateTime(dr["ToDate"]).ToString("dd-MMM-yyyy")
                            : "",

                        PaidAmount = dr["PaidAmount"] != DBNull.Value
                            ? Convert.ToDecimal(dr["PaidAmount"])
                            : 0
                    });
                }

                response.Success = true;
                response.Message = "Receipt List";
                response.Data = list;
            }

            return response;
        }
        public ApiResponse GetReceiptDetails(ReceiptDetailsRequest request)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand("SP_APIReceipt", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TransType", "SelectReceiptDetails");
                    cmd.Parameters.AddWithValue("@PartyCode", DBNull.Value);
                    cmd.Parameters.AddWithValue("@BillNo", request.BillNo);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        response.Success = true;
                        response.Message = "Receipt Details";

                        response.Data = new
                        {
                            BillNo = dr["PL_BillNo"].ToString(),

                            BillDate = dr["PL_BillDate"] != DBNull.Value
                                ? Convert.ToDateTime(dr["PL_BillDate"]).ToString("dd-MMM-yyyy")
                                : "",

                            PartyCode = dr["PL_PartyCode"].ToString(),

                            PartyName = dr["PM_PartyName"].ToString(),

                            FatherName = dr["PM_FHName"].ToString(),

                            Area = dr["AM_AreaName"].ToString(),

                            Para = dr["PM_ParaName"].ToString(),

                            ReceiptNo = dr["PL_RcptCode"].ToString(),

                            ReceiptDate = dr["PL_RcptDate"] != DBNull.Value
                                ? Convert.ToDateTime(dr["PL_RcptDate"]).ToString("dd-MMM-yyyy")
                                : "",

                            ReceiptType = dr["PL_RcptType"].ToString(),

                            BillAmount = dr["PL_BillAmount"] != DBNull.Value
                                ? Convert.ToDecimal(dr["PL_BillAmount"])
                                : 0,

                            PaidAmount = dr["PL_PaidAmount"] != DBNull.Value
                                ? Convert.ToDecimal(dr["PL_PaidAmount"])
                                : 0
                        };
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Receipt Not Found.";
                    }
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
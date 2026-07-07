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
   
        public class DashboardDAL
        {
            public readonly string conString =
                ConfigurationManager.ConnectionStrings["ERP_DB_Conn"].ConnectionString;

            public ApiResponse GetDashboard(DashboardRequest request)
            {
                ApiResponse response = new ApiResponse();

                try
                {
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_APIDASHBOARD", con);

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@PartyCode", request.PartyCode);

                        con.Open();

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            response.Success = true;
                            response.Message = "Dashboard Data";

                            response.Data = new
                            {
                                PartyCode = dr["PM_PartyCode"].ToString(),

                                PartyName = dr["PM_PartyName"].ToString(),

                                FatherName = dr["PM_FHName"].ToString(),

                                MobileNo = dr["PM_MobNo"].ToString(),

                                Email = dr["PM_Email"].ToString(),

                                Address = dr["PM_Address"].ToString(),

                                City = dr["PM_City"].ToString(),

                                Area = dr["AM_AreaName"].ToString(),

                                Para = dr["PM_ParaName"].ToString(),

                                PIN = dr["PM_PIN"].ToString(),

                                TotalPaidAmount = dr["TotalPaidAmount"] != DBNull.Value
                                    ? Convert.ToDecimal(dr["TotalPaidAmount"])
                                    : 0,

                                TotalOutstandingAmt = dr["TotalOutstandingAmt"] != DBNull.Value
                                    ? Convert.ToDecimal(dr["TotalOutstandingAmt"])
                                    : 0,

                                DueAmount = dr["DueAmount"] != DBNull.Value
                                    ? Convert.ToDecimal(dr["DueAmount"])
                                    : 0,

                                DueMonth = dr["DueMonth"] != DBNull.Value
                                    ? Convert.ToInt32(dr["DueMonth"])
                                    : 0,

                                LastPaidMonth = dr["LastPaidMonth"].ToString(),

                                DueFrom = dr["PT_DtFroms"].ToString(),

                                DueTo = dr["PT_DtTo"].ToString()
                            };
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "No Record Found.";
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

using ERP.Areas.API.Model.Response;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ERP.Areas.API.Helper
{
    public class TokenManager
    {
        string conString = ConfigurationManager.ConnectionStrings["ERP_DB_Conn"].ConnectionString;

        public ApiResponse ValidateToken(string token)
        {
            ApiResponse response = new ApiResponse();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(@"
                SELECT PM_PartyCode
                FROM APIPartyLogin_APL
                WHERE AccessToken=@Token
                AND IsActive=1
                AND TokenExpiry>GETDATE()", con);

                cmd.Parameters.AddWithValue("@Token", token);

                con.Open();

                object result = cmd.ExecuteScalar();

                if (result == null)
                {
                    response.Success = false;
                    response.Message = "Invalid or Expired Token.";
                    response.Data = null;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Token Valid.";
                    response.Data = result.ToString();
                }
            }

            return response;
        }
    }
}
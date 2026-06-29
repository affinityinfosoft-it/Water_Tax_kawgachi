using BObject;
using IBLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAccess
{
    public class CommonRepository : ICommonRepository
    {
        public static string GetConnectionString()
        {
            try
            {
                return ConfigurationManager.ConnectionStrings["ERP_DB_Conn"].ConnectionString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region GetGlobalSelect
        public List<T> GetGlobalSelect<T>(string MainTableName, string MainFieldName, Int64? PId) where T : class, new()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@MainTableName", MainTableName));
            arrParams.Add(new SqlParameter("@MainFieldName", MainFieldName));
            arrParams.Add(new SqlParameter("@PId", PId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "GlobalSelect_SP", arrParams.ToArray());
            return Utility.DataTableToList<T>(ds.Tables[0]);
        }
        #endregion
        #region GetGlobalSelect One
        public T GetGlobalSelectOne<T>(string MainTableName, string MainFieldName, Int64? PId) where T : class, new()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@MainTableName", MainTableName));
            arrParams.Add(new SqlParameter("@MainFieldName", MainFieldName));
            arrParams.Add(new SqlParameter("@PId", PId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "GlobalSelect_SP", arrParams.ToArray());
            return Utility.DataTableToList<T>(ds.Tables[0]).FirstOrDefault();
        }
        #endregion
        #region GlobalDelete
        public long GlobalDelete(string MainTableName, string MainFieldName, long? PId, string TransTableName = null, string TransFieldName = null)
        {

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@PId", PId));
            arrParams.Add(new SqlParameter("@MainTableName", MainTableName));
            arrParams.Add(new SqlParameter("@MainFieldName", MainFieldName));
            if (TransTableName != null) arrParams.Add(new SqlParameter("@TransTableName", TransTableName));
            if (TransFieldName != null) arrParams.Add(new SqlParameter("@TransFieldName", TransFieldName));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_GlobalDelete", arrParams.ToArray());

            long ReturnValue = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return Convert.ToInt64(ReturnValue);

        }
        #endregion
        #region DuplicateDataCheck
        public string DuplicateDataCheck(string TableName, string DataField, string DataFieldValue, string OptionalField, long? OptionalFieldValue)
        {
            DataSet ds = new DataSet();
            string data = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@DataFieldValue", DataFieldValue));
            arrParams.Add(new SqlParameter("@TableName", TableName));
            arrParams.Add(new SqlParameter("@DataField", DataField));
            arrParams.Add(new SqlParameter("@OptionalField", OptionalField));
            arrParams.Add(new SqlParameter("@OptionalFieldValue", OptionalFieldValue));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "SP_DuplicateDataChecking", arrParams.ToArray());
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    data = ds.Tables[0].Rows[0]["GetData"].ToString();
                }
            }

            return data;

        }
        #endregion
        #region Insert Eny Master
        public Int64 InsertAnyMasters(List<SqlParameter> arrParams, string SP_Name, SqlParameter OutPutId)
        {
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, SP_Name, arrParams.ToArray());
            Int64 ID = Convert.ToInt64(OutPutId.Value);
            return Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
        }
        #region Delete Any Master
        public Int64 DeleteAnyMasters(List<SqlParameter> arrParams, string SP_Name, SqlParameter OutPutId)
        {
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, SP_Name, arrParams.ToArray());
            Int64 ID = Convert.ToInt64(OutPutId.Value);
            return Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
        }
        #endregion
        #endregion
        #region GetAnyList
        public List<T> GetAnyList<T>(List<SqlParameter> arrParams, string SP_Name) where T : class, new()
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, SP_Name, arrParams.ToArray());
            return Utility.DataTableToList<T>(ds.Tables[0]);
        }
        public T GetAnySelectOne<T>(List<SqlParameter> arrParams, string SP_Name) where T : class, new()
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, SP_Name, arrParams.ToArray());
            return Utility.DataTableToList<T>(ds.Tables[0]).FirstOrDefault();
        }
        #endregion
        public static DataSet GetGlobalMasterSet(GlobalDataList global)
        {
            DataSet dt = new DataSet();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", global.TransactionType));
            //arrParams.Add(new SqlParameter("@FDate", global.ParamFromDate));
            //arrParams.Add(new SqlParameter("@TDate", global.ParamToDate));

            if (global.Param != null)
            {

                arrParams.Add(new SqlParameter("@" + global.Param, global.paramString));
            }

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            //SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            dt = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, global.StoreProcedure, arrParams.ToArray());

            return dt;
        }
        public T GetGlobalMaster<T>(GlobalDataList global) where T : class, new()
        {
            DataSet ds = GetGlobalMasterSet(global);
            return Utility.DataTableToList<T>(ds.Tables[0]).FirstOrDefault();
        }
        public List<T> GetGlobalMasterList<T>(GlobalDataList global) where T : class, new()
        {
            List<T> List = new List<T>();
            DataSet ds = GetGlobalMasterSet(global);
            if (ds.Tables[0].Rows.Count > 0)
            {
                List = Utility.DataTableToList<T>(ds.Tables[0]);
            }
            return List;
            //return Utility.DataTableToList<T>(ds.Tables[0]);
        }
        public void SaveSmsTracker(List<SMSBO> smsBOs)
        {
            foreach (var item in smsBOs)
            {
                try
                {
                    List<SqlParameter> arrParams = new List<SqlParameter>();
                    arrParams.Add(new SqlParameter("@TransType", "Save"));
                    arrParams.Add(new SqlParameter("@trackingCode", item.trackingCode));
                    arrParams.Add(new SqlParameter("@mobileNo", item.mobileNo));
                    arrParams.Add(new SqlParameter("@trackingNo", item.trackingNo));
                    arrParams.Add(new SqlParameter("@msg", item.msg));
                    arrParams.Add(new SqlParameter("@remarks", item.remarks));
                    SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_smsTracker", arrParams.ToArray());
                }
                catch (SqlException ) { continue; }
            }
        }

        public Int64 InsertAnyGlobalMasters(List<SqlParameter> arrParams, string SP_Name, SqlParameter OutPutId)
        {
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, SP_Name, arrParams.ToArray());
            Int64 ID = Convert.ToInt64(OutPutId.Value);
            return Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLogic
{
    public interface ICommonRepository
    {
        List<T> GetGlobalSelect<T>(string MainTableName, string MainFieldName, Int64? PId) where T : class, new();
        long GlobalDelete(string MainTableName, string MainFieldName, long? PId, string TransTableName = null, string TransFieldName = null);
        T GetGlobalSelectOne<T>(string MainTableName, string MainFieldName, Int64? PId) where T : class, new();
        string DuplicateDataCheck(string TableName, string DataField, string DataFieldValue, string OptionalField, long? OptionalFieldValue);
        Int64 InsertAnyMasters(List<SqlParameter> arrParams, string SP_Name, SqlParameter OutPutId);
        List<T> GetAnyList<T>(List<SqlParameter> arrParams, string SP_Name) where T : class, new();
        T GetAnySelectOne<T>(List<SqlParameter> arrParams, string SP_Name) where T : class, new();
        //T GetGlobalMaster<T>(GlobalDataList global) where T : class, new();
        //List<T> GetGlobalMasterList<T>(GlobalDataList global) where T : class, new();
        //void SaveSmsTracker(List<SMSBO> smsBOs);

    }
    public interface ILoginRepository
    {
        //LoginViewModel GetLogin(LoginViewModel user);
    }
    public interface IMasterRepository
    {
        //long AddEditSession(SessionMasters_SM Ses);
        //List<ClsSubGrWiseSubSetting_CSGWS> GetGroupWiseSubject(ClsSubGrWiseSubSetting_CSGWS obj);

    }
    public interface ITransactionRepository
    {
        //LoginViewModel GetLogin(LoginViewModel user);
    }
}

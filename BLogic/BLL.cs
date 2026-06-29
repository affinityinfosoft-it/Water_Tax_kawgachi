using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLogic;
using DAccess;
using BObject;
namespace BLogic
{
    public class Service
    {
        ICommonRepository common;
        //ILoginRepository login;
        IMasterRepository master;
        ITransactionRepository transaction;
        public Service()
        {
            this.common = new CommonRepository();
            //this.login = new LoginRepository();
            this.master = new MasterRepository();
            this.transaction = new TransactionRepository();
        }
        #region Common
        #region GetGlobalSelect
        public List<T> GetGlobalSelect<T>(string MainTableName, string MainFieldName, Int64? PId) where T : class, new()
        {
            return common.GetGlobalSelect<T>(MainTableName, MainFieldName, PId);
        }
        #endregion
        #region GetGlobalSelectOne
        public T GetGlobalSelectOne<T>(string MainTableName, string MainFieldName, Int64? PId) where T : class, new()
        {
            return common.GetGlobalSelectOne<T>(MainTableName, MainFieldName, PId);
        }
        #endregion
        #region GetGlobalDelete
        public long GlobalDelete(string MainTableName, string MainFieldName, long? PId, string TransTableName = null, string TransFieldName = null)
        {
            return common.GlobalDelete(MainTableName, MainFieldName, PId, TransTableName, TransFieldName);
        }
        #endregion
        #region Get User Wise Company List
        public List<T> GetUserWiseCompanyList<T>(UserWiseCompany TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECTComUserWise"));
            arrParams.Add(new SqlParameter("@Email", TEntity.Email));
            arrParams.Add(new SqlParameter("@Password", TEntity.Password));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        #endregion
        #endregion
        #region Layout
        #region Menu Bind
        public List<T> GetMenuList<T>(MenuMasterModel TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@UserId", TEntity.UserId));
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        public List<T> GetAccessRightMenuList<T>(AccessRights TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECT"));
            arrParams.Add(new SqlParameter("@RoleId", TEntity.RoleId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        #endregion
        #region InsUp Menu
        public Int64 InsUpMenu(MenuMasterModel TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            //SqlParameter OutPutId=new SqlParameter();
            if (TEntity.MenuId == 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@MenuId", TEntity.MenuId));
            }
            arrParams.Add(new SqlParameter("@MenuName", TEntity.MenuName));
            arrParams.Add(new SqlParameter("@ActionUrl", TEntity.ActionUrl));
            arrParams.Add(new SqlParameter("@IsBaseMenu", TEntity.IsBaseMenu));
            arrParams.Add(new SqlParameter("@IsSubMenu", TEntity.IsSubMenu));
            arrParams.Add(new SqlParameter("@IsMenu", TEntity.IsMenu));
            arrParams.Add(new SqlParameter("@ParentMenuId", TEntity.ParentMenuId));
            arrParams.Add(new SqlParameter("@DisplayPosition", TEntity.DisplayPosition));
            arrParams.Add(new SqlParameter("@CreatedBy", TEntity.CreatedBy));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region InsUpAccessRightMenuList
        public Int64 InsUpAccessRightMenuList(AccessRights TEntity, string SP_Name)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MenuId", typeof(Int64));
            dt.Columns.Add("CanView", typeof(bool));
            dt.Columns.Add("CanAdd", typeof(bool));
            dt.Columns.Add("CanEdit", typeof(bool));
            dt.Columns.Add("CanDelete", typeof(bool));
            dt.Columns.Add("CanSubmit", typeof(bool));
            dt.Columns.Add("CanUpdate", typeof(bool));

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            arrParams.Add(new SqlParameter("@RoleId", TEntity.RoleId));
            arrParams.Add(new SqlParameter("@CreatedBy", TEntity.CreatedBy));
            foreach (var item in TEntity.lstAccessRights)
            {
                DataRow dr = dt.NewRow();
                dr["MenuId"] = item.MenuId;
                dr["CanView"] = item.CanView;
                dr["CanAdd"] = item.CanAdd;
                dr["CanEdit"] = item.CanEdit;
                dr["CanDelete"] = item.CanDelete;
                dr["CanSubmit"] = item.CanSubmit; ;
                dr["CanUpdate"] = item.CanUpdate; ;
                dt.Rows.Add(dr);
            }
            arrParams.Add(new SqlParameter("@AccessRightMenuList", dt));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region InsUp User Role
        public Int64 InsUpUserRole(UserRole TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.OppType == "Insert")
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
            }
            arrParams.Add(new SqlParameter("@UserId", TEntity.UserId));
            arrParams.Add(new SqlParameter("@RoleId", TEntity.RoleId));
            arrParams.Add(new SqlParameter("@ProjectId", TEntity.ProjectId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #endregion
        #region Master
        #region Company master
        public Int64 InsUpCompany(company TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.CM_ID != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            } 
            arrParams.Add(new SqlParameter("@code", TEntity.code));
            arrParams.Add(new SqlParameter("@name", TEntity.name));
            arrParams.Add(new SqlParameter("@address", TEntity.address));
            arrParams.Add(new SqlParameter("@city", TEntity.city));
            arrParams.Add(new SqlParameter("@pin", TEntity.pin));
            arrParams.Add(new SqlParameter("@phoneno", TEntity.phoneno));
            arrParams.Add(new SqlParameter("@puser", TEntity.puser));
            arrParams.Add(new SqlParameter("@dt_of_entry", TEntity.dt_of_entry));
            arrParams.Add(new SqlParameter("@invbill_prefix", TEntity.invbill_prefix));
            arrParams.Add(new SqlParameter("@ho", TEntity.ho));
            arrParams.Add(new SqlParameter("@ipd_pref", TEntity.ipd_pref));
            arrParams.Add(new SqlParameter("@opd_pref", TEntity.opd_pref));
            arrParams.Add(new SqlParameter("@opdinv_pref", TEntity.opdinv_pref));
            arrParams.Add(new SqlParameter("@logo", TEntity.logo));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region Item master
        public Int64 InsUpItem(ItemMaster_IM TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.IM_ID != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@IM_ID", TEntity.IM_ID));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@IM_ItemCode", TEntity.IM_ItemCode));
            arrParams.Add(new SqlParameter("@IM_ItemName", TEntity.IM_ItemName));
            arrParams.Add(new SqlParameter("@ItemGroupCode", TEntity.ItemGroupID));
            arrParams.Add(new SqlParameter("@ItemSubGroupID", TEntity.ItemSubGroupID));
            arrParams.Add(new SqlParameter("@IM_ItemDescription", TEntity.IM_ItemDescription));
            arrParams.Add(new SqlParameter("@IM_GST", TEntity.IM_GST));
            arrParams.Add(new SqlParameter("@IM_StockInHand", TEntity.IM_StockInHand));
            arrParams.Add(new SqlParameter("@IM_Unit", TEntity.IM_Unit));
            arrParams.Add(new SqlParameter("@IM_Rate", TEntity.IM_Rate));
            arrParams.Add(new SqlParameter("@IM_Type", TEntity.IM_Type));
            //arrParams.Add(new SqlParameter("@CreatedDate", TEntity.CreatedDate));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        //ADD BY UTTARAN FOR INVENTORY MANAGEMENT ItemGroup,Subgroup  11-06-2025
        #region ItemGroup Master
        public Int64 InsUpItemGroup(ItemGroup_IG TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.ItemGroupID != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@ItemGroupID", TEntity.ItemGroupID));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            arrParams.Add(new SqlParameter("@UserId", TEntity.UserId));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@ItemCode", TEntity.ItemCode));
            arrParams.Add(new SqlParameter("@ItemGroupName", TEntity.ItemGroupName));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region ItemSubGroup Master
        public List<T> GetItemSubGroupList<T>(ItemSubGroup_ISG TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECT"));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        public Int64 InsUpItemSubGroup(ItemSubGroup_ISG TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.ItemSubGroupID != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@ItemSubGroupID", TEntity.ItemSubGroupID));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            arrParams.Add(new SqlParameter("@UserId", TEntity.UserId));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@ItemCode", TEntity.ItemCode));
            arrParams.Add(new SqlParameter("@ItemSubCode", TEntity.ItemSubCode));
            arrParams.Add(new SqlParameter("@ItemSubGroupName", TEntity.ItemSubGroupName));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region Beneficary Fixed Rate Master
        public Int64 InsUpBeneficaryFixedRate(Master_Rate TEntity,long CmId, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.CM_ID != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            arrParams.Add(new SqlParameter("@CM_ID", CmId));
            arrParams.Add(new SqlParameter("@bf_rate", TEntity.bf_rate));
            arrParams.Add(new SqlParameter("@fr_rate", TEntity.fr_rate));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region Water Tax Slab Master
        public Int64 InsUpWaterTaxSlab(tax_master TEntity, long CmId, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.CM_ID != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            arrParams.Add(new SqlParameter("@CM_ID", CmId));
            arrParams.Add(new SqlParameter("@f_date", TEntity.f_date));
            arrParams.Add(new SqlParameter("@f_amt", TEntity.f_amt));
            arrParams.Add(new SqlParameter("@s_date", TEntity.s_date));
            arrParams.Add(new SqlParameter("@s_amt", TEntity.s_amt));
            arrParams.Add(new SqlParameter("@t_date", TEntity.t_date));
            arrParams.Add(new SqlParameter("@t_amt", TEntity.t_amt));
            arrParams.Add(new SqlParameter("@UserId", TEntity.UserId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region PurcheseVendor
        public Int64 InsUpPurcheseVendor(PurchaseVendorMaster_PVM TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.PVM_VendorID != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@PVM_VendorID", TEntity.PVM_VendorID));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@PVM_VendorCode", TEntity.PVM_VendorCode));
            arrParams.Add(new SqlParameter("@PVM_VendorName", TEntity.PVM_VendorName));
            arrParams.Add(new SqlParameter("@PVM_Address1", TEntity.PVM_Address1));
            arrParams.Add(new SqlParameter("@PVM_Address2", TEntity.PVM_Address2));
            arrParams.Add(new SqlParameter("@PVM_City", TEntity.PVM_City));
            arrParams.Add(new SqlParameter("@PVM_PIN", TEntity.PVM_PIN));
            arrParams.Add(new SqlParameter("@PVM_Phone", TEntity.PVM_Phone));
            arrParams.Add(new SqlParameter("@PVM_FaxNo", TEntity.PVM_FaxNo));
            arrParams.Add(new SqlParameter("@PVM_Email", TEntity.PVM_Email));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region BENEFICERY CONNECTION MASTER
        public Int64 InsUpBeneficeryConnection(PartyMaster_PM TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.PM_PartyId != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@PM_PartyId", TEntity.PM_PartyId));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            arrParams.Add(new SqlParameter("@PM_PartyCode", TEntity.PM_PartyCode));
            arrParams.Add(new SqlParameter("@PM_RegDate", TEntity.PM_RegDate));
            arrParams.Add(new SqlParameter("@PM_PartyName", TEntity.PM_PartyName));
            arrParams.Add(new SqlParameter("@PM_FHName", TEntity.PM_FHName));
            arrParams.Add(new SqlParameter("@AM_AreaID", TEntity.AM_AreaID));
            arrParams.Add(new SqlParameter("@PM_ParaId", TEntity.PM_ParaId));
            arrParams.Add(new SqlParameter("@PM_Address", TEntity.PM_Address));
            arrParams.Add(new SqlParameter("@PM_City", TEntity.PM_City));
            arrParams.Add(new SqlParameter("@PM_PIN", TEntity.PM_PIN));
            arrParams.Add(new SqlParameter("@PM_PhoneNo", TEntity.PM_PhoneNo));
            arrParams.Add(new SqlParameter("@PM_MobNo", TEntity.PM_MobNo));
            arrParams.Add(new SqlParameter("@PM_BFAmount", TEntity.PM_BFAmount));
            arrParams.Add(new SqlParameter("@PM_PaidAmt", TEntity.PM_PaidAmt));
            arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@UserId", TEntity.UserId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        public List<T> GetBeneficeryConnection<T>(PartyMaster_PM TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECT"));
            arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));

            arrParams.Add(new SqlParameter("@fromdate", TEntity.fromdate == "" ? null : TEntity.fromdate));
            arrParams.Add(new SqlParameter("@todate", TEntity.todate == "" ? null : TEntity.todate));
            arrParams.Add(new SqlParameter("@PM_PartyCode", string.IsNullOrWhiteSpace(TEntity.PM_PartyCode) ? null : TEntity.PM_PartyCode));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        #endregion
        #region AreaMaster
        public Int64 InsUpArea(AreaMaster_AM TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.AM_AreaID != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@AM_AreaID", TEntity.AM_AreaID));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            //arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@AM_AreaCode", TEntity.AM_AreaCode));
            arrParams.Add(new SqlParameter("@AM_AreaName", TEntity.AM_AreaName));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region ParaMaster
        public List<T> GetParaList<T>(ParaMaster_PM TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECT"));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        public Int64 InsUpPara(ParaMaster_PM TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.PM_ParaId != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@PM_ParaId", TEntity.PM_ParaId));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            //arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@AM_AreaCode", TEntity.AM_AreaCode));
            arrParams.Add(new SqlParameter("@PM_ParaCode", TEntity.PM_ParaCode));
            arrParams.Add(new SqlParameter("@PM_ParaName", TEntity.PM_ParaName));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region InspectorMaster
        public Int64 InsUpInspector(Inspector_Master TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.Ins_Id != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@Ins_Id", TEntity.Ins_Id));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            //arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@ins_code", TEntity.ins_code));
            arrParams.Add(new SqlParameter("@ins_name", TEntity.ins_name));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }

        #endregion
        #region Tank master
        public Int64 InsUpTank(TankMaster_TM TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.TM_ID != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@TM_ID", TEntity.TM_ID));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@TM_TankCode", TEntity.TM_TankCode));
            arrParams.Add(new SqlParameter("@TM_TankName", TEntity.TM_TankName));
            arrParams.Add(new SqlParameter("@TM_Lt", TEntity.TM_Lt));
            arrParams.Add(new SqlParameter("@TM_Rate", TEntity.TM_Rate));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region Van Master
        public Int64 InsUpVan(VanMaster_VM TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.VM_ID != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@VM_ID", TEntity.VM_ID));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@VM_VanCode", TEntity.VM_VanCode));
            arrParams.Add(new SqlParameter("@VM_VanName", TEntity.VM_VanName));
            arrParams.Add(new SqlParameter("@VM_Type", TEntity.VM_Type));
            arrParams.Add(new SqlParameter("@VM_Rate", TEntity.VM_Rate));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #endregion
        #region Transaction
        #region Consumer Payment
        public T GetConsumerDetails<T>(PartyLedger_PL TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SelectParty"));
            arrParams.Add(new SqlParameter("@PM_PartyCode", TEntity.PM_PartyCode));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        public List<T> GetConsumerDueList<T>(PartyLedger_PL TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SelectDue"));
            arrParams.Add(new SqlParameter("@PM_PartyCode", TEntity.PM_PartyCode));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        public T GetReceptNo<T>(PartyLedger_PL TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SelectReceptNo"));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        public Int64 InsUpConsumerPayment(PartyLedger_PL TEntity, string SP_Name)
        {

            Int64 r = 0;
            foreach (var item in TEntity.PartyLadgerList)
            {
                List<SqlParameter> arrParams = new List<SqlParameter>();

                if (TEntity.PL_Id != 0)
                {
                    arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                    arrParams.Add(new SqlParameter("@PL_Id", TEntity.PL_Id));
                }
                else
                {
                    arrParams.Add(new SqlParameter("@OppType", "INSERT"));
                }
                arrParams.Add(new SqlParameter("@PL_RcptNo", TEntity.PL_RcptNo));
                arrParams.Add(new SqlParameter("@PL_PartyCode", TEntity.PL_PartyCode));
                arrParams.Add(new SqlParameter("@PL_RcptDate", TEntity.PL_RcptDate));
                arrParams.Add(new SqlParameter("@PL_RcptType", TEntity.PL_RcptType));
                arrParams.Add(new SqlParameter("@PL_ChqNo", TEntity.PL_ChqNo));
                arrParams.Add(new SqlParameter("@PL_ChqDate", TEntity.PL_ChqDate));
                arrParams.Add(new SqlParameter("@PL_Bank", TEntity.PL_Bank));
                arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
                arrParams.Add(new SqlParameter("@UserId", TEntity.UserId));
                arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));

                arrParams.Add(new SqlParameter("@PL_BillNo", item.PL_BillNo));
                arrParams.Add(new SqlParameter("@PL_BillDate", item.PL_BillDate));
                arrParams.Add(new SqlParameter("@PL_BillAmount", item.PL_BillAmount));
                arrParams.Add(new SqlParameter("@PL_PaidAmount", item.PL_PaidAmount));
                arrParams.Add(new SqlParameter("@PL_BType", item.PL_BType));
                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
                OutPutId.Direction = ParameterDirection.Output;
                arrParams.Add(OutPutId);
                r = common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
            }
            return r;
        }
        public List<T> GetConsumerPayment<T>(PartyLedger_PL TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECT"));
            arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));

            arrParams.Add(new SqlParameter("@fromdate", TEntity.fromdate == "" ? null : TEntity.fromdate));
            arrParams.Add(new SqlParameter("@todate", TEntity.todate == "" ? null : TEntity.todate));
            arrParams.Add(new SqlParameter("@PL_PartyCode", string.IsNullOrWhiteSpace(TEntity.PL_PartyCode) ? null : TEntity.PL_PartyCode));


            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        public T GetConsumerDetailsById<T>(PartyLedger_PL TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SelectPartyEdit"));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@PL_Id", TEntity.PL_Id));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }

        public List<T> GetConsumerPaymentEditList<T>(PartyLedger_PL TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECTDueEditList"));
            arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@PL_Id", TEntity.PL_Id));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        #endregion
        #region Form Sale
        public Int64 InsUpFORMSALE(FORMSALE TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.ID != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@ID", TEntity.ID));
               // arrParams.Add(new SqlParameter("@PL_PartyCode", TEntity.PM_PartyCode));

            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            arrParams.Add(new SqlParameter("@BILLDATE", TEntity.BILLDATE));
            arrParams.Add(new SqlParameter("@PM_PartyCode", TEntity.PM_PartyCode));
            arrParams.Add(new SqlParameter("@fr_rate", TEntity.fr_rate));
            arrParams.Add(new SqlParameter("@FORMNO", TEntity.FORMNO));
            arrParams.Add(new SqlParameter("@AMOUNT", TEntity.AMOUNT));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@UserId", TEntity.UserId));
            arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        public List<T> GetFORMSALE<T>(FORMSALE TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECT"));
            arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));

            arrParams.Add(new SqlParameter("@fromdate", TEntity.fromdate == "" ? null : TEntity.fromdate));
            arrParams.Add(new SqlParameter("@todate", TEntity.todate == "" ? null : TEntity.todate));
            arrParams.Add(new SqlParameter("@PM_PartyCode", string.IsNullOrWhiteSpace(TEntity.PM_PartyCode) ? null : TEntity.PM_PartyCode));



            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }



        public T GetFORMSALEById<T>(FORMSALE TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SelectFormEdit"));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@ID", TEntity.ID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        public Int64 DeleteAnyMasters(FORMSALE TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "DELETE"));
            arrParams.Add(new SqlParameter("@BILLNO", TEntity.BILLNO));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region Ferrul Charge
        public T GetFerrulById<T>(ferrulMaster TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SelectFerrulEdit"));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@FerulId", TEntity.FerulId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        public T GetFerrulBillNo<T>(ferrulMaster TEntity, string SP_Name,long CM_ID) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECTFromNo"));
            arrParams.Add(new SqlParameter("@CM_ID", CM_ID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        public T GetfReceptCode<T>(ferrulMaster TEntity, string SP_Name, long CM_ID) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECTfReceptCode"));
            arrParams.Add(new SqlParameter("@CM_ID", CM_ID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }

        public object GlobalDelete(string v1, string v2, string ins_name, object p1, object p2)
        {
            throw new NotImplementedException();
        }

        public object GlobalDelete(string v1, string v2, decimal ins_code, object p1, object p2)
        {
            throw new NotImplementedException();
        }

        public T GetcReceptCode<T>(ferrulMaster TEntity, string SP_Name, long CM_ID) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECTcReceptCode"));
            arrParams.Add(new SqlParameter("@CM_ID", CM_ID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        public Int64 InsUpFerrul(ferrulMaster TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.FerulId != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@FerulId", TEntity.FerulId));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            arrParams.Add(new SqlParameter("@bill_date", TEntity.bill_date));
            //arrParams.Add(new SqlParameter("@bill_No", TEntity.bill_No));
            arrParams.Add(new SqlParameter("@custId", TEntity.PM_PartyCode));
            arrParams.Add(new SqlParameter("@fAmt", TEntity.fAmt));
            arrParams.Add(new SqlParameter("@cAmt", TEntity.cAmt));
            arrParams.Add(new SqlParameter("@fReceptCode", TEntity.fReceptCode));
            arrParams.Add(new SqlParameter("@cReceptCode", TEntity.cReceptCode));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@UserId", TEntity.UserId));
            arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        public List<T> GetFerrul<T>(ferrulMaster TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECT"));
            //arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));

            arrParams.Add(new SqlParameter("@fromdate", TEntity.fromdate==""?null: TEntity.fromdate));
            arrParams.Add(new SqlParameter("@todate", TEntity.todate == "" ? null : TEntity.todate));
            arrParams.Add(new SqlParameter("@custId", string.IsNullOrWhiteSpace(TEntity.custId) ? null : TEntity.custId));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        #endregion
        #region Update Consumer tax Payment
        public Int64 InsUpConTaxPayment(PartyMaster_PM TEntity, string SP_Name)
        {
             List<SqlParameter> arrParams = new List<SqlParameter>();

               arrParams.Add(new SqlParameter("@OppType", "UPDATE"));

            arrParams.Add(new SqlParameter("@PM_PartyCode", TEntity.PM_PartyCode));
            arrParams.Add(new SqlParameter("@PM_SFlag", TEntity.PM_SFlag));
            arrParams.Add(new SqlParameter("@PM_TaxDate", TEntity.PM_TaxDate));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region Water Tax Collection
        public T GetReceptNoForTax<T>(PartyTax_PT TEntity, string SP_Name) where T : class, new()
        
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECTReceptCode"));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        public T GetLastTaxDate<T>(PartyTax_PT TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECTLastTaxDate"));
            arrParams.Add(new SqlParameter("@PT_PtyCode", TEntity.PT_PtyCode));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        public Int64 InsUpWaterTaxCollection(PartyTax_PT TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.PT_ID != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@PT_ID", TEntity.PT_ID));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            arrParams.Add(new SqlParameter("@PT_RcptNo", TEntity.PT_RcptNo));
            arrParams.Add(new SqlParameter("@PT_PtyCode", TEntity.PT_PtyCode));
            arrParams.Add(new SqlParameter("@PT_PmtDate", TEntity.PT_PmtDate));
            arrParams.Add(new SqlParameter("@PM_PartyCode", TEntity.PM_PartyCode));
            arrParams.Add(new SqlParameter("@PT_DtFrom", TEntity.PT_DtFrom));
            arrParams.Add(new SqlParameter("@PT_DtTo", TEntity.PT_DtTo));
            arrParams.Add(new SqlParameter("@PT_Mth", TEntity.PT_Mth));
            arrParams.Add(new SqlParameter("@PT_Rate", TEntity.PT_Rate));
            arrParams.Add(new SqlParameter("@PT_Amount", TEntity.PT_Amount));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@UserId", TEntity.UserId));
            arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        public List<T> GetWaterTaxCollection<T>(PartyTax_PT TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECT"));
            arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));

            arrParams.Add(new SqlParameter("@fromdate", TEntity.fromdate == "" ? null : TEntity.fromdate));
            arrParams.Add(new SqlParameter("@todate", TEntity.todate == "" ? null : TEntity.todate));
            arrParams.Add(new SqlParameter("@PT_PtyCode", string.IsNullOrWhiteSpace(TEntity.PT_PtyCode) ? null : TEntity.PT_PtyCode));



            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        public T GetTaxByIds<T>(PartyTax_PT TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SelectTaxEdit"));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@PT_ID", TEntity.PT_ID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }

        public List<T> GetRepReConnectionList<T>(RepReConnection TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECT"));
            //arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            //arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        #endregion
        #region Re Connection
        public T GetBillNoForRecon<T>(RepReConnection TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECTBillNo"));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        public Int64 InsUpReConnection(RepReConnection TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;

            Int64 r=0 ;
            arrParams.Add(new SqlParameter("@OppType", "GET-RCPTNO"));
            arrParams.Add(OutPutId);
            var PL_RcptNo = common.GetAnySelectOne<PartyLedger_PL>(arrParams, SP_Name); ;

            //var PL_RcptNo= common.GetGlobalSelect<PartyLedger_PL>("PartyLedger_PL", "PL_Id", null).Where(x => x.PL_Type == "PA").OrderByDescending(x=> x.PL_RcptNo).FirstOrDefault();


            foreach (var item in TEntity.RepInsItemList)
            {
                 r = 0;
                arrParams = new List<SqlParameter>();
                arrParams = new List<SqlParameter>();
                if (TEntity.GS_SIID != 0 && item.GS_BillNo!="")
                {
                    arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                }
                else
                {
                    arrParams.Add(new SqlParameter("@OppType", "INSERT"));
                    arrParams.Add(new SqlParameter("@GS_BillNo", TEntity.GS_BillNo));
                    arrParams.Add(new SqlParameter("@PL_RcptNo", PL_RcptNo.PL_RcptNo));
                }
                arrParams.Add(new SqlParameter("@GS_BillDate", TEntity.GS_BillDate));
                arrParams.Add(new SqlParameter("@GS_PartyCode", TEntity.GS_PartyCode));
                arrParams.Add(new SqlParameter("@GS_GrossAmt", TEntity.GS_GrossAmt));
                arrParams.Add(new SqlParameter("@GS_AdjAmt", TEntity.GS_AdjAmt));
                arrParams.Add(new SqlParameter("@GS_NetAmt", TEntity.GS_NetAmt));
                arrParams.Add(new SqlParameter("@GS_Paid", TEntity.GS_Paid));
                arrParams.Add(new SqlParameter("@GS_Due", TEntity.GS_Due));
                arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
                arrParams.Add(new SqlParameter("@UserId", TEntity.UserId));
                arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));

                arrParams.Add(new SqlParameter("@GS_SIID", item.GS_SIID));
                arrParams.Add(new SqlParameter("@_BillNo", item.GS_BillNo)); 
                arrParams.Add(new SqlParameter("@GS_ItemCode", item.GS_ItemCode));
                arrParams.Add(new SqlParameter("@GS_Qty", item.GS_Qty)); 
                arrParams.Add(new SqlParameter("@GS_Rate", item.GS_Rate));
                arrParams.Add(new SqlParameter("@GS_Vat", item.GS_Vat));
                arrParams.Add(new SqlParameter("@GS_VatAmt", item.GS_VatAmt));
                arrParams.Add(new SqlParameter("@GS_Amount", item.GS_Amount));
                arrParams.Add(OutPutId);
                r = common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
            }
            return r;
        }
        public List<T> GetReconnection<T>(RepReConnection TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECT"));
            //arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));

            arrParams.Add(new SqlParameter("@fromdate", TEntity.fromdate == "" ? null : TEntity.fromdate));
            arrParams.Add(new SqlParameter("@todate", TEntity.todate == "" ? null : TEntity.todate));
            arrParams.Add(new SqlParameter("@GS_PartyCode", string.IsNullOrWhiteSpace(TEntity.GS_PartyCode) ? null : TEntity.GS_PartyCode));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        public List<T> SELECTRepReEdit<T>(RepReConnection TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECTRepReEdit"));
            arrParams.Add(new SqlParameter("@GS_BillNo", TEntity.GS_BillNo));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        public T GetRepReConDetailsEdit<T>(RepReConnection TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "GetRepReConDetailsEdit"));
            arrParams.Add(new SqlParameter("@GS_BillNo", TEntity.GS_BillNo));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        #endregion
        #region Consumer Invoice
        public T GetBillNoForConInv<T>(RepReConnection TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECTBillNo"));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        public Int64 InsUpConInv(RepReConnection TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output; 

            Int64 r = 0;
            arrParams.Add(new SqlParameter("@OppType", "GET-RCPTNO"));
            arrParams.Add(OutPutId);
            var PL_RcptNo = common.GetAnySelectOne<PartyLedger_PL>(arrParams, SP_Name); ;

            //var PL_RcptNo = common.GetGlobalSelect<PartyLedger_PL>("PartyLedger_PL", "PL_Id", null).Where(x => x.PL_Type == "PA").OrderByDescending(x => x.PL_RcptNo).FirstOrDefault();

            foreach (var item in TEntity.RepInsItemList)
            {
                r = 0;
                arrParams = new List<SqlParameter>();
                arrParams = new List<SqlParameter>();
                if (TEntity.GS_SIID != 0 && item.GS_BillNo != "")
                {
                    arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                }
                else
                {
                    arrParams.Add(new SqlParameter("@OppType", "INSERT"));
                    arrParams.Add(new SqlParameter("@GS_BillNo", TEntity.GS_BillNo));
                    arrParams.Add(new SqlParameter("@PL_RcptNo", PL_RcptNo.PL_RcptNo));

                }
                arrParams.Add(new SqlParameter("@GS_BillDate", TEntity.GS_BillDate));
                arrParams.Add(new SqlParameter("@GS_PartyCode", TEntity.GS_PartyCode));
                arrParams.Add(new SqlParameter("@GS_GrossAmt", TEntity.GS_GrossAmt));
                arrParams.Add(new SqlParameter("@GS_AdjAmt", TEntity.GS_AdjAmt));
                arrParams.Add(new SqlParameter("@GS_NetAmt", TEntity.GS_NetAmt));
                arrParams.Add(new SqlParameter("@GS_Paid", TEntity.GS_Paid));
                arrParams.Add(new SqlParameter("@GS_Due", TEntity.GS_Due));
                arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
                arrParams.Add(new SqlParameter("@UserId", TEntity.UserId));
                arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));

                arrParams.Add(new SqlParameter("@GS_SIID", item.GS_SIID));
                arrParams.Add(new SqlParameter("@_BillNo", item.GS_BillNo));
                arrParams.Add(new SqlParameter("@GS_ItemCode", item.GS_ItemCode));
                arrParams.Add(new SqlParameter("@GS_Qty", item.GS_Qty));
                arrParams.Add(new SqlParameter("@GS_Rate", item.GS_Rate));
                arrParams.Add(new SqlParameter("@GS_Vat", item.GS_Vat));
                arrParams.Add(new SqlParameter("@GS_VatAmt", item.GS_VatAmt));
                arrParams.Add(new SqlParameter("@GS_Amount", item.GS_Amount));
                arrParams.Add(OutPutId);
                r = common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
            }
            return r;
        }
        public List<T> GetConInv<T>(RepReConnection TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECT"));
            //arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));

            arrParams.Add(new SqlParameter("@fromdate", TEntity.fromdate == "" ? null : TEntity.fromdate));
            arrParams.Add(new SqlParameter("@todate", TEntity.todate == "" ? null : TEntity.todate));
            arrParams.Add(new SqlParameter("@GS_PartyCode", string.IsNullOrWhiteSpace(TEntity.GS_PartyCode) ? null : TEntity.GS_PartyCode));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        public List<T> SELECTConInvEdit<T>(RepReConnection TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECTRepReEdit"));
            arrParams.Add(new SqlParameter("@GS_BillNo", TEntity.GS_BillNo));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        public T GetConInvDetailsEdit<T>(RepReConnection TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "GetRepReConDetailsEdit"));
            arrParams.Add(new SqlParameter("@GS_BillNo", TEntity.GS_BillNo));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        //public T GetBillNoForConInv<T>(RepReConnection TEntity, string SP_Name) where T : class, new()
        //{
        //    List<SqlParameter> arrParams = new List<SqlParameter>();
        //    arrParams.Add(new SqlParameter("@OppType", "SELECTBillNo"));
        //    SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
        //    OutPutId.Direction = ParameterDirection.Output;
        //    arrParams.Add(OutPutId);
        //    return common.GetAnySelectOne<T>(arrParams, SP_Name);
        //}
        //public Int64 InsUpConInv(RepReConnection TEntity, string SP_Name)
        //{
        //    Int64 r = 0;

        //    foreach (var item in TEntity.RepInsItemList)
        //    {
        //        r = 0;
        //        List<SqlParameter> arrParams = new List<SqlParameter>();
        //        if (TEntity.GS_SIID != 0 && item.GS_BillNo != "")
        //        {
        //            arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
        //        }
        //        else
        //        {
        //            arrParams.Add(new SqlParameter("@OppType", "INSERT"));
        //            arrParams.Add(new SqlParameter("@GS_BillNo", TEntity.GS_BillNo));
        //        }
        //        arrParams.Add(new SqlParameter("@GS_BillDate", TEntity.GS_BillDate));
        //        arrParams.Add(new SqlParameter("@GS_PartyCode", TEntity.GS_PartyCode));
        //        arrParams.Add(new SqlParameter("@GS_GrossAmt", TEntity.GS_GrossAmt));
        //        arrParams.Add(new SqlParameter("@GS_AdjAmt", TEntity.GS_AdjAmt));
        //        arrParams.Add(new SqlParameter("@GS_NetAmt", TEntity.GS_NetAmt));
        //        arrParams.Add(new SqlParameter("@GS_Paid", TEntity.GS_Paid));
        //        arrParams.Add(new SqlParameter("@GS_Due", TEntity.GS_Due));
        //        arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
        //        arrParams.Add(new SqlParameter("@UserId", TEntity.UserId));
        //        arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));

        //        arrParams.Add(new SqlParameter("@GS_SIID", item.GS_SIID));
        //        arrParams.Add(new SqlParameter("@_BillNo", item.GS_BillNo));
        //        arrParams.Add(new SqlParameter("@GS_ItemCode", item.GS_ItemCode));
        //        arrParams.Add(new SqlParameter("@GS_Qty", item.GS_Qty));
        //        arrParams.Add(new SqlParameter("@GS_Rate", item.GS_Rate));
        //        arrParams.Add(new SqlParameter("@GS_Vat", item.GS_Vat));
        //        arrParams.Add(new SqlParameter("@GS_VatAmt", item.GS_VatAmt));
        //        arrParams.Add(new SqlParameter("@GS_Amount", item.GS_Amount));
        //        SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
        //        OutPutId.Direction = ParameterDirection.Output;
        //        arrParams.Add(OutPutId);
        //        r = common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        //    }
        //    return r;
        //}
        //public List<T> GetConInv<T>(RepReConnection TEntity, string SP_Name) where T : class, new()
        //{
        //    List<SqlParameter> arrParams = new List<SqlParameter>();
        //    arrParams.Add(new SqlParameter("@OppType", "SELECT"));
        //    //arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
        //    arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
        //    SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
        //    OutPutId.Direction = ParameterDirection.Output;
        //    arrParams.Add(OutPutId);
        //    return common.GetAnyList<T>(arrParams, SP_Name);
        //}
        //public T GeConInvConDetailsEdit<T>(RepReConnection TEntity, string SP_Name) where T : class, new()
        //{
        //    List<SqlParameter> arrParams = new List<SqlParameter>();
        //    arrParams.Add(new SqlParameter("@OppType", "GetRepReConDetailsEdit"));
        //    arrParams.Add(new SqlParameter("@GS_BillNo", TEntity.GS_BillNo));
        //    SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
        //    OutPutId.Direction = ParameterDirection.Output;
        //    arrParams.Add(OutPutId);
        //    return common.GetAnySelectOne<T>(arrParams, SP_Name);
        //}
        #endregion
        #region Tank Booking

        public T GetConsumerDetailss<T>(PartyLedger_PL TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            // Must match SP exactly
            arrParams.Add(new SqlParameter("@OppType", "SelectPartys"));

            arrParams.Add(new SqlParameter("@PM_PartyCode", TEntity.PM_PartyCode));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }





        public Int64 InsUpTankbooking(TankBooking_TB TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.AP_ID != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@AP_ID", TEntity.AP_ID));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@AP_Code", TEntity.AP_Code));
            arrParams.Add(new SqlParameter("@AP_Name", TEntity.AP_Name));
            arrParams.Add(new SqlParameter("@AP_FathersName", TEntity.AP_FathersName));
            arrParams.Add(new SqlParameter("@AP_Mobile", TEntity.AP_Mobile));
            arrParams.Add(new SqlParameter("@AM_AreaID", TEntity.AM_AreaID));
            arrParams.Add(new SqlParameter("@PM_ParaId", TEntity.PM_ParaId));
            arrParams.Add(new SqlParameter("@AP_Address", TEntity.AP_Address));
            arrParams.Add(new SqlParameter("@DeliveryLandmark", TEntity.DeliveryLandmark));
            arrParams.Add(new SqlParameter("@Bk_Purpose", TEntity.Bk_Purpose));
            arrParams.Add(new SqlParameter("@TM_ID", TEntity.TM_ID));
            arrParams.Add(new SqlParameter("@FromDate", TEntity.FromDate));
            arrParams.Add(new SqlParameter("@FromTime", TEntity.FromTime));
            arrParams.Add(new SqlParameter("@ToDate", TEntity.ToDate));
            arrParams.Add(new SqlParameter("@Qty", TEntity.Qty));
            arrParams.Add(new SqlParameter("@FormNo", TEntity.FormNo));
            arrParams.Add(new SqlParameter("@FormAmount", TEntity.FormAmount));
            arrParams.Add(new SqlParameter("@ToTime", TEntity.ToTime));
            arrParams.Add(new SqlParameter("@Amount", TEntity.Amount));
            arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            arrParams.Add(new SqlParameter("@CreatedDate", TEntity.CreatedDate));
            arrParams.Add(new SqlParameter("@RegDate", TEntity.RegDate));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        public List<T> GetTankBooking<T>(TankBooking_TB TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECT"));
            arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));

            arrParams.Add(new SqlParameter("@fromdates", TEntity.fromdates == "" ? null : TEntity.fromdates));
            arrParams.Add(new SqlParameter("@todates", TEntity.todates == "" ? null : TEntity.todates));
            arrParams.Add(new SqlParameter("@AP_Code", string.IsNullOrWhiteSpace(TEntity.AP_Code) ? null : TEntity.AP_Code));



            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        #endregion
        #region Van Booking

        public T GetConsumerDetailsss<T>(PartyLedger_PL TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            // Must match SP exactly
            arrParams.Add(new SqlParameter("@OppType", "SelectPartys"));

            arrParams.Add(new SqlParameter("@PM_PartyCode", TEntity.PM_PartyCode));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }

        public Int64 InsUpVanbooking(VanBooking_VB TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (TEntity.AP_ID != 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@AP_ID", TEntity.AP_ID));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@AP_Code", TEntity.AP_Code));
            arrParams.Add(new SqlParameter("@AP_Name", TEntity.AP_Name));
            arrParams.Add(new SqlParameter("@AP_FathersName", TEntity.AP_FathersName));
            arrParams.Add(new SqlParameter("@AP_Mobile", TEntity.AP_Mobile));
            arrParams.Add(new SqlParameter("@AM_AreaID", TEntity.AM_AreaID));
            arrParams.Add(new SqlParameter("@PM_ParaId", TEntity.PM_ParaId));
            arrParams.Add(new SqlParameter("@AP_Address", TEntity.AP_Address));
            arrParams.Add(new SqlParameter("@DeliveryLandmark", TEntity.DeliveryLandmark));
            arrParams.Add(new SqlParameter("@Bk_Purpose", TEntity.Bk_Purpose));
            arrParams.Add(new SqlParameter("@VM_ID", TEntity.VM_ID));
            //arrParams.Add(new SqlParameter("@VB_RcptNo", TEntity.VB_RcptNo));
            arrParams.Add(new SqlParameter("@FromDate", TEntity.FromDate));
            arrParams.Add(new SqlParameter("@FromTime", TEntity.FromTime));
            arrParams.Add(new SqlParameter("@ToDate", TEntity.ToDate));
            arrParams.Add(new SqlParameter("@Qty", TEntity.Qty));
            arrParams.Add(new SqlParameter("@FormNo", TEntity.FormNo));
            arrParams.Add(new SqlParameter("@FormAmount", TEntity.FormAmount));
            arrParams.Add(new SqlParameter("@ToTime", TEntity.ToTime));
            arrParams.Add(new SqlParameter("@Amount", TEntity.Amount));
            arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            arrParams.Add(new SqlParameter("@CreatedDate", TEntity.CreatedDate));
            arrParams.Add(new SqlParameter("@RegDate", TEntity.RegDate));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        public List<T> GetVanBooking<T>(VanBooking_VB TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SELECT"));
            arrParams.Add(new SqlParameter("@FyId", TEntity.FyId));
            arrParams.Add(new SqlParameter("@CM_ID", TEntity.CM_ID));
            arrParams.Add(new SqlParameter("@fromdates", TEntity.fromdates == "" ? null : TEntity.fromdates));
            arrParams.Add(new SqlParameter("@todates", TEntity.todates == "" ? null : TEntity.todates));
            arrParams.Add(new SqlParameter("@AP_Code", string.IsNullOrWhiteSpace(TEntity.AP_Code) ? null : TEntity.AP_Code));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        #endregion
        #endregion
        #region Report
        public T GetBeneficaryReport<T>(PartyMaster_PM TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SelectBeneficaryReport"));
            arrParams.Add(new SqlParameter("@PM_PartyId", TEntity.PM_PartyId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        public T GetFormSaleReport<T>(FORMSALE TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SelectFormSaleReport"));
            arrParams.Add(new SqlParameter("@ID", TEntity.ID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        public T GetFerrulChargesReport<T>(ferrulMaster TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SelectFormFerrulChargesReport"));
            arrParams.Add(new SqlParameter("@FerulId", TEntity.FerulId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        public T WaterTaxCollectionReport<T>(PartyTax_PT TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SelectWaterTaxCollectionReport"));
            arrParams.Add(new SqlParameter("@PT_ID", TEntity.PT_ID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }         
        public List<T> GetReConnectionReport<T>(RepReConnection TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SelectReconnectionReport"));
            arrParams.Add(new SqlParameter("@GS_BillNo", TEntity.GS_BillNo));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }

        public List<T> GetConsumerinvoiceReport<T>(RepReConnection TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SelectconsumerinvoiceReport"));
            arrParams.Add(new SqlParameter("@GS_BillNo", TEntity.GS_BillNo));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        public List<T> GetConsumerPaymentReport<T>(PartyLedger_PL TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "Consumerpaymentreport"));
            arrParams.Add(new SqlParameter("@PL_BillNo", TEntity.PL_BillNo));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        public T GetTankBookingReport<T>(TankBooking_TB TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SelectTankBookingReport"));
            arrParams.Add(new SqlParameter("@AP_ID", TEntity.AP_ID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        public T GetVanBookingReport<T>(VanBooking_VB TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "SelectVanBookingReport"));
            arrParams.Add(new SqlParameter("@AP_ID", TEntity.AP_ID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnySelectOne<T>(arrParams, SP_Name);
        }
        public object DeleteAnyMasters(RepReConnection entity, string v)
        {
            throw new NotImplementedException();
        }
        public object DeleteAnyMasters(object entity, string v)
        {
            throw new NotImplementedException();
        }
        #endregion
       
    }
}


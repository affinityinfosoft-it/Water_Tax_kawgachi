using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP
{
    public partial class VoucherPrint : System.Web.UI.Page
    {
        DataSet DMSObjSet = null;
        DataTable DataTable = new DataTable();
        ReportDocument objReportDoc_Collection = new ReportDocument();
        public class QuiryParameter
        {
            public int? LD_LedgerID { get; set; }
            public string LD_VoucherNo { get; set; }

        }
        QuiryParameter QParameter = new QuiryParameter();
        protected void Page_Load(object sender, EventArgs e)
        {
            try { QParameter.LD_LedgerID = Convert.ToInt32(Request.QueryString["LD_LedgerID"]); }
            catch (Exception Ex) { QParameter.LD_LedgerID = null; }
            try { QParameter.LD_VoucherNo = Convert.ToString(Request.QueryString["LD_VoucherNo"]); }
            catch (Exception Ex) { QParameter.LD_VoucherNo = null; }

           
            if (IsPostBack)
            {

                DMSObjSet = (DataSet)ViewState["ReportDataset"];
                if (DMSObjSet != null)
                {
                  
                        objReportDoc_Collection.Load(Server.MapPath("~/Views/Report/RptFiles/VoucherPrint1.rpt"));
                        CrystalReportViewer1.ReportSource = objReportDoc_Collection;
                        objReportDoc_Collection.SetDataSource(DMSObjSet);
                        
              
                }
                else
                {
                    printreport();
                }
            }
            else
            {
                printreport();
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            this.CrystalReportViewer1.ReportSource = null;
            objReportDoc_Collection.Close();
            objReportDoc_Collection.Dispose();
            GC.Collect();
        }
        public void printreport()
        {
            DataSet DMSObjSet = new DataSet();
            using (SqlDataAdapter da = new SqlDataAdapter("SP_AccountVoucherPrint", ConfigurationManager.ConnectionStrings["ERP_DB_Conn"].ToString()))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@LD_LedgerID", QParameter.LD_LedgerID);
                da.SelectCommand.Parameters.AddWithValue("@LD_VoucherNo", QParameter.LD_VoucherNo);

                da.SelectCommand.CommandTimeout = 600;
                da.Fill(DMSObjSet, "SP_AccountVoucherPrint");
            }
            DataTable = DMSObjSet.Tables["SP_AccountVoucherPrint"];
            objReportDoc_Collection.Load(Server.MapPath("~/Views/Report/RptFiles/VoucherPrint1.rpt"));
            CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
            objReportDoc_Collection.SetDataSource(DataTable);
          

            CrystalReportViewer1.ReportSource = objReportDoc_Collection;
            CrystalReportViewer1.DataBind();

            ViewState["ReportDataset"] = DMSObjSet;
            // ExportPDFWordExecel("PDF");
        }


        protected void imgBtnPdf_Click(object sender, ImageClickEventArgs e)
        {
            ExportPDFWordExecel("PDF");
        }
        public void ExportPDFWordExecel(string type)
        {

            if (Request.QueryString["id"] == "1")
            {
                printreport();
            }
            ExportFormatType formatType = ExportFormatType.NoFormat;
            switch (type)
            {
                case "Word":
                    formatType = ExportFormatType.WordForWindows;
                    break;
                case "PDF":
                    formatType = ExportFormatType.PortableDocFormat;
                    break;
                case "Excel":
                    formatType = ExportFormatType.Excel;
                    break;
                    //case "CSV":
                    //    formatType = ExportFormatType.CharacterSeparatedValues;
                    //    break;
            }

            //if (Request.QueryString["id"] == "1")
            //{
            objReportDoc_Collection.ExportToHttpResponse(formatType, Response, true, "Voucher Print");
            // }


            Response.End();
        }
        ReportDocument objReportDoc = new ReportDocument();
        protected void crystalreportviewer_Unload(object sender, EventArgs e)
        {
            try
            {
                if (objReportDoc != null)
                {
                    objReportDoc.Close();
                    objReportDoc.Dispose();
                }
            }
            catch (Exception Ex)
            {

            }
            finally
            {
                GC.Collect();
            }
        }


    }
}
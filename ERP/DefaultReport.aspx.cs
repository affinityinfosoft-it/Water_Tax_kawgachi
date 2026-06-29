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
    public partial class DefaultReport : System.Web.UI.Page
    {
        DataSet DMSObjSet = null;
        ReportDocument objReportDoc_Collection = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
             if (Request.QueryString["id"] == "1")
             {
            printreport();
             }
            if (IsPostBack)
            {

                DMSObjSet = (DataSet)ViewState["ReportDataset"];
                if (DMSObjSet != null)
                {
                    if (Request.QueryString["id"] == "1")
                    {
                        objReportDoc_Collection.Load(Server.MapPath("~/Views/Report/RptFiles/DefaulterReport.rpt"));
                        CrystalReportViewer1.ReportSource = objReportDoc_Collection;
                        objReportDoc_Collection.SetDataSource(DMSObjSet);
                        printreport();
                    }
                }
            }
        }
        public void printreport()
        {
            DataSet DMSObjSet = new DataSet();
            using (SqlDataAdapter da = new SqlDataAdapter("usp_DefaulterReport", ConfigurationManager.ConnectionStrings["ERP_DB_Conn"].ToString()))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@AM_AreaID", Request.QueryString["AM_AreaID"]);
                da.SelectCommand.Parameters.AddWithValue("@PM_ParaId", Request.QueryString["PM_ParaId"]);

                da.SelectCommand.CommandTimeout = 600;
                da.Fill(DMSObjSet, "usp_DefaulterReport");
            }
            if (Request.QueryString["id"] == "1")
            {
                objReportDoc_Collection.Load(Server.MapPath("~/Views/Report/RptFiles/DefaulterReport.rpt"));
            }


            CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
            objReportDoc_Collection.SetDataSource(DMSObjSet.Tables["usp_DefaulterReport"]);
            //objReportDoc_Collection.SetParameterValue("AM_AreaID", Request.QueryString["AM_AreaID"].ToString() == null || Request.QueryString["AM_AreaID"].ToString() == "" ? "All" : Request.QueryString["AM_AreaID"].ToString());
            //objReportDoc_Collection.SetParameterValue("PM_ParaId", Request.QueryString["PM_ParaId"].ToString() == null || Request.QueryString["PM_ParaId"].ToString() == "" ? "All" : Request.QueryString["PM_ParaId"].ToString());
            // objReportDoc_Collection.SetParameterValue("Co_Name", Session["CompanyName"].ToString());

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
            objReportDoc_Collection.ExportToHttpResponse(formatType, Response, true, "Defaulter Report");
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
    
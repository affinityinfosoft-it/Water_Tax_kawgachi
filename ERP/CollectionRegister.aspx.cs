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
    public partial class CollectionRegister : System.Web.UI.Page
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
                        objReportDoc_Collection.Load(Server.MapPath("~/Views/Report/RptFiles/CollectionRegister.rpt"));
                        CrystalReportViewer1.ReportSource = objReportDoc_Collection;
                        objReportDoc_Collection.SetDataSource(DMSObjSet);
                        printreport();
                    }
                }
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
            using (SqlDataAdapter da = new SqlDataAdapter("usp_CollectionRegister", ConfigurationManager.ConnectionStrings["ERP_DB_Conn"].ToString()))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fromdate", Request.QueryString["fromdate"]);
                da.SelectCommand.Parameters.AddWithValue("@todate", Request.QueryString["todate"]);
                da.SelectCommand.Parameters.AddWithValue("@AM_AreaID", Request.QueryString["AM_AreaID"]);
                da.SelectCommand.Parameters.AddWithValue("@PM_ParaId", Request.QueryString["PM_ParaId"]);


                da.SelectCommand.CommandTimeout = 600;
                da.Fill(DMSObjSet, "usp_CollectionRegister");
            }
            if (Request.QueryString["id"] == "1")
            {
                objReportDoc_Collection.Load(Server.MapPath("~/Views/Report/RptFiles/CollectionRegister.rpt"));
            }

            // objReportDoc_Collection.Load(Server.MapPath("~/Views/Report/RptFiles/FerrulReport.rpt""));
            CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
            objReportDoc_Collection.SetDataSource(DMSObjSet.Tables["usp_CollectionRegister"]);
            objReportDoc_Collection.Subreports["Sub_CollectionTypeWiseSummary"].SetDataSource(DMSObjSet.Tables["usp_CollectionRegister"]);
            objReportDoc_Collection.SetParameterValue("fromdate", Request.QueryString["fromdate"].ToString() == null || Request.QueryString["fromdate"].ToString() == "" ? "All" : Request.QueryString["fromdate"].ToString());
            objReportDoc_Collection.SetParameterValue("todate", Request.QueryString["todate"].ToString() == null || Request.QueryString["todate"].ToString() == "" ? "All" : Request.QueryString["todate"].ToString());
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
            objReportDoc_Collection.ExportToHttpResponse(formatType, Response, true, "Collection Register");
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
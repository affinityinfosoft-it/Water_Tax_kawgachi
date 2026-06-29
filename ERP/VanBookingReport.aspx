<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VanBookingReport.aspx.cs" Inherits="ERP.VanBooking_Report" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Van Booking Report</title>
    <script src="crystalreportviewers13/js/crviewer/crv.js"></script>
    <style type="text/css">
        .auto-style1 {
            width: 618px;
            height: 41px;
        }
        .auto-style4 {
            width: 28px;
            text-align: left;
        }
        .auto-style5 {
            margin-left: 0px;
        }
        </style>
         <script type="text/javascript">
             function Print() {
                 var dvReport = document.getElementById("dvReport");
                 var frame1 = dvReport.getElementsByTagName("iframe")[0];
                 if (navigator.appName.indexOf("Internet Explorer") != -1 || navigator.appVersion.indexOf("Trident") != -1) {
                     frame1.name = frame1.id;
                     window.frames[frame1.id].focus();
                     window.frames[frame1.id].print();
                 }
                 else {
                     var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
                     frameDoc.print();
                 }
                 return false;
             }
         </script>
</head>
    <body style="height: 63px">
    <form id="form2" runat="server">
          <div style="margin-left:16%; margin-right:10%" class="auto-style1">
        <table>
                <tr >
                    <td style="border-bottom-color:black" class="auto-style4">
                      
                        <asp:ImageButton runat="server" ID="imgBtnPdf" ImageUrl="/Logo/pdf.png" ToolTip="Export PDF" OnClick="imgBtnPdf_Click" width="37px" height="35px" CssClass="auto-style5"/>
                    </td>   
                         <td style="border-bottom-color:black" class="auto-style4">
                        <asp:ImageButton runat="server" ImageUrl="/Logo/print_icon.png" OnClientClick="Print()" Style="width: 30px; margin-right: 5px;" ToolTip="Print this report." />
                    </td>  
                    <td style="border-bottom-color:black" class="auto-style4">
              
                        &nbsp;</td>
                </tr>
            </table>

             <div id="dvReport">
  <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" />
              </div>
              
     </div>
  </form>
</body>
</html>

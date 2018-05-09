<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthWiseDataComparison.aspx.cs" Inherits="EPGM.Report.MonthWiseDataComparison" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
    <script type="text/javascript">
    function Redirection() {
        debugger;
        window.top.location.href = "/Login/MainIndex";
    }
</script>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="errordiv">
                <asp:Label ID="lblerror" Visible="true" runat="server"></asp:Label>
            </div>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" ShowPrintButton="true">
            </rsweb:ReportViewer>
            
        </div>
    </form>
    <div style="text-align:center;display:none" id="msg" runat="server"><b>Note: For further Reports refer to Analytics Section</b></div>
</body>
</html>

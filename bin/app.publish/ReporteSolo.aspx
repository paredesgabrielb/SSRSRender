<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteSolo.aspx.cs" Inherits="ReportsRender.ReporteSolo" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>DIGEPRES</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote" Width="100%" Height="700">
                <ServerReport ReportPath="/Carpeta1/repo2" ReportServerUrl="http://wk19dgpdatos/reportserver" />
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
<script type="text/JavaScript">
    //window.onload =
        function resize() {
        var viewer = document.getElementById("<%=ReportViewer1.ClientID %>");
		var viewer_ct = document.getElementById("<%=ReportViewer1.ClientID %>" + "_ctl09");

        var htmlheight = document.documentElement.clientHeight;
        viewer.style.height = (htmlheight - 120) + "px";
        viewer_ct.style.height = (htmlheight + 120) + "px";
    }
</script>
</html>




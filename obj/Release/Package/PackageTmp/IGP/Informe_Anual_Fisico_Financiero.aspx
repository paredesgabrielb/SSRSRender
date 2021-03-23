<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/ReportWithFolder.aspx.cs" Inherits="ReportsRender.ReportWithFolder" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        // TODO: PASAR AQUI EL REPORTFORLDER Y EL REPORT 
        ReportName = "noname";
        ReportFolder = "/Carpeta1";
        base.OnInit(e);
    }
</script>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>DIGEPRES</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table id="ParamTable" style="padding: 0px; margin: 0px; width: 100%; background-color: #ddd" runat="server">
                <tr>
                    <td style="padding: 5px; width: 150px; text-align: right; font-weight: bold">Seleccione la Carpeta:</td>
                    <td style="padding: 5px;">
                        <asp:DropDownList ID="ddlFolders" runat="server"  AutoPostBack="True"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px; width: 150px; text-align: right; font-weight: bold">Seleccione el reporte:</td>
                    <td style="padding: 5px;">
                        <asp:DropDownList ID="ddlReports" runat="server"  AutoPostBack="True"></asp:DropDownList>
                    </td>
                </tr>
            </table>

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote" Width="100%" Height="700">
                <ServerReport ReportPath="" ReportServerUrl="http://wk19dgpdatos/reportserver" />
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




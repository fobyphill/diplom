<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rept.aspx.cs" Inherits="temp_web1.rept" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" 
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link href="/Content/mycss.css" rel="stylesheet"/>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel Width="480" ID="p_header" runat="server" HorizontalAlign="Center">
            <h1><span class="logo1">Pla</span><span class="logo2">Za</span>
        <asp:Label ID="l_header" runat="server" Text=" отчет"></asp:Label></h1>
        <h2><asp:Label ID="l_descript" runat="server" Text=""></asp:Label></h2>
        </asp:Panel>
            
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="rv" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt" Height="665px" Width="477px">
            <LocalReport ReportPath="rep_fast.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="ds_fast" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetData" 
            TypeName="temp_web1.ds_fullTableAdapters.report_fastTableAdapter"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" 
            TypeName="temp_web1.ds_fullTableAdapters.consumptionsTableAdapter"></asp:ObjectDataSource>
    </form>
</body>
</html>

﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rept_plan.aspx.cs" Inherits="temp_web1.rept_plan" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/Content/mycss.css" rel="stylesheet"/>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <title>Отчет о планировании затрат</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel Width="480" ID="p_header" runat="server" HorizontalAlign="Center">
            <h1><span class="logo1">Pla</span><span class="logo2">Za</span>
        <asp:Label ID="l_header" runat="server" Text=" отчет"></asp:Label></h1>
        <h2><asp:Label ID="l_descript" runat="server" Text=""></asp:Label></h2>
        </asp:Panel>
        <rsweb:ReportViewer ID="rv" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt" Width="480px" Height="1000px">
            <LocalReport ReportPath="rep_plan.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dset_plan" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="temp_web1.dset_plans_outputTableAdapters.plans_outputTableAdapter"></asp:ObjectDataSource>
        <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
    </form>
</body>
</html>

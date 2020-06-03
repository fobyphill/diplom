<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="temp_web1.test" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/Content/mycss.css" rel="stylesheet"/>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
        <ajaxToolkit:ModalPopupExtender PopupControlID="p_modal" OkControlID="b_choise" ID="mpe" runat="server" TargetControlID="b_3_vu"></ajaxToolkit:ModalPopupExtender>
    <div>
        <asp:Button ID="b_3_vu" runat="server" Text="Button" />
        <br />
        <asp:Label ID="l_out" runat="server" Text=""></asp:Label>
    </div>
        <asp:Panel CssClass="modalwin" ID="p_modal" runat="server" Width="400px" ScrollBars="Vertical">
            <asp:TreeView ID="tv" runat="server" OnSelectedNodeChanged="tv_SelectedNodeChanged"></asp:TreeView>
            <br />
            <asp:Button ID="b_choise" runat="server" Text="Выбрать" OnClick="b_choise_Click" />
            <asp:Button ID="b_cancel" runat="server" Text="Отмена" />
        </asp:Panel>
        <asp:TextBox ID="tb_linq" runat="server"></asp:TextBox>
        <asp:Button ID="b_linq" runat="server" Text="to" OnClick="b_linq_Click" />
        <asp:Button ID="b_linq0" runat="server" Text="from" OnClick="b_linq0_Click" />
    </form>
</body>
</html>

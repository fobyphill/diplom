<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reports.aspx.cs" 
    Inherits="temp_web1.reports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="maindiv">
    <div class="onerow mymenu">
       <div class="mar10ver"><a href="Default.aspx">Главная</a></div>
       <div class="mar10ver"><a href="consumptions.aspx">Управление затратами</a></div>
        <div class="mar10ver"><a href="plans.aspx">Планирование затрат</a></div>
       <div class="mar10ver"> <a href="cats.aspx">Категории затрат</a></div>
       <div class="mar10ver"> <a href="bils.aspx">Счета</a></div>
       <div class="mar10ver"> <a href="users.aspx">Пользователи</a></div>
       <div class="mar10ver"> <a href="reports.aspx">Отчеты</a></div>
   </div>
    <div class="onerow maincontent">
        <asp:Panel CssClass="diverror" ID="p_error" runat="server" Visible="false">
            <asp:Label  ID="l_error" runat="server" Text="Отчет не выбран"></asp:Label>
        </asp:Panel>
        <asp:RadioButtonList ID="rbl_choice_report" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbl_choice_report_SelectedIndexChanged">
            <asp:ListItem Value="0">Быстрый отчет</asp:ListItem>
            <asp:ListItem Value="1">Настраиваемый отчет</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Panel Visible="false" CssClass="mar10" ID="p_fast_report" runat="server">
            <asp:TextBox ID="tb_month" runat="server" TextMode="Month" Width="135px"></asp:TextBox>
        </asp:Panel>
        <asp:Button CssClass="bluebutton mar10"  ID="b_print" runat="server" Text="Вывести" 
            OnClick="b_add_Click" OnClientClick="SetTarget();" />
        <asp:Button ID="b_clear" runat="server" Text="Очистить" CssClass="redbutton mar10" />

            <ajaxtoolkit:modalpopupextender TargetControlID="b_inv" PopupControlID="p_modal_confirm" 
                ID="mpe" runat="server" DropShadow="True"></ajaxtoolkit:modalpopupextender>
       
        <div class="invis"><asp:Button ID="b_inv" runat="server" Text="Невидимка" /></div>
        

</div>
    <asp:Panel CssClass="modalwin" ID="p_modal_confirm" runat="server">
                Вы уверены, что желаете удалить запись о планировании?<br /><br />
                <asp:Button ID="b_yes" runat="server" Text="Да" />
                <asp:Button ID="b_no" runat="server" Text="Нет" />
            </asp:Panel>
   
    <script type = "text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
</script>
        </div>
</asp:Content>



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
        <asp:Panel CssClass="diverror mar10ver" ID="p_error" runat="server" Visible="false">
            <asp:Label  ID="l_error" runat="server" Text="Отчет не выбран"></asp:Label>
        </asp:Panel>
        <asp:RadioButtonList CssClass="mar10ver" ID="rbl_choice_report" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbl_choice_report_SelectedIndexChanged">
            <asp:ListItem Value="0">Быстрый отчет</asp:ListItem>
            <asp:ListItem Value="1">Настраиваемый отчет</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Panel Visible="false" CssClass="mar10ver" ID="p_fast_report" runat="server">
            <asp:TextBox ID="tb_month" runat="server" TextMode="Month" Width="135px"></asp:TextBox>
        </asp:Panel>
        <asp:Panel ID="p_custom_report" Visible="false" runat="server">
            <asp:Panel CssClass="onerow" BackColor="white" Width="300" ScrollBars="Vertical" ID="p_cats" runat="server">
            <asp:TreeView ID="tv" Width="276px" Height="276px" runat="server" ShowCheckBoxes="All">
                        <NodeStyle BorderColor="Black" ForeColor="Black" />
                        <SelectedNodeStyle BackColor="#16dbdb" />
                    </asp:TreeView>
        </asp:Panel>
            <div class="onerow">
                <asp:Label ID="l_cats" runat="server" Text=""></asp:Label>
            </div>
            <div class ="mar10ver">
           <asp:ImageButton CssClass="checkbox_uncheck" ID="ib_show_hide" ImageUrl="img/double_checkbox_blue.png" Width="20px" 
               runat="server" OnClick="ib_show_hide_Click" />
                <asp:Label ID="l_collapse" runat="server" Text="Развернуть все"></asp:Label>
       </div>
        </asp:Panel>
        
        <asp:Button CssClass="bluebutton mar10ver" Height="30"  ID="b_print" runat="server" Text="Вывести" 
            OnClick="b_print_Click" OnClientClick="SetTarget();" />
        <asp:Button ID="b_clear" Height="30" runat="server" Text="Очистить" CssClass="redbutton mar10" OnClick="b_clear_Click" />
        

</div>
    <!--<script type = "text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
</script>-->
        </div>
</asp:Content>



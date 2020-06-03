<%@ Page Title="Отчеты" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reports.aspx.cs" 
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
        <asp:CheckBoxList ID="cbl_con_plan" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="Small" OnSelectedIndexChanged="cbl_con_plan_SelectedIndexChanged">
            <asp:ListItem Selected>Затраты</asp:ListItem>
            <asp:ListItem>Планирование</asp:ListItem>
        </asp:CheckBoxList>
        <asp:Label ID="l_con_plan" runat="server" Text=""></asp:Label>
        <asp:RadioButtonList CssClass="mar10ver" ID="rbl_choice_report" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbl_choice_report_SelectedIndexChanged" Font-Names="Arial" Font-Size="Small">
            <asp:ListItem Selected Value="0">Быстрый отчет</asp:ListItem>
            <asp:ListItem Value="1">Настраиваемый отчет</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Panel Visible="false" ID="p_period_choise" runat="server">
            <asp:RadioButtonList ID="rbl_period_choise" runat="server" CssClass="mar10ver" Font-Names="Arial" Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="rbl_period_choise_SelectedIndexChanged">
                <asp:ListItem Selected Value="0">Месяц</asp:ListItem>
                <asp:ListItem Value="1">Произвольный период</asp:ListItem>
            </asp:RadioButtonList>
        </asp:Panel>
        <asp:Panel CssClass="mar10ver" ID="p_fast_report" runat="server" Visible="true">
            <asp:TextBox ID="tb_month" runat="server" TextMode="Month" Width="135px"></asp:TextBox>
        </asp:Panel>
        <asp:Panel Visible="false" ID="p_free_period" runat="server">
            <span >от</span>
            <asp:TextBox ID="tb_date_from" runat="server" TextMode="Date" Width="135px"></asp:TextBox>
            <span >до</span>
            <asp:TextBox ID="tb_date_to" runat="server" TextMode="Date" Width="135px"></asp:TextBox>
        </asp:Panel>
        <asp:Panel ID="p_custom_report" Visible="false" runat="server">
            <div class="mar10ver">
                <asp:RadioButtonList ID="rbl_option" runat="server" Font-Names="Arial" Font-Size="Small">
                    <asp:ListItem Selected>Только выбранные категории</asp:ListItem>
                    <asp:ListItem>Выбранные и дочерние категории</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class ="mar10ver">
           <asp:ImageButton CssClass="checkbox_uncheck" ID="ib_check" ImageUrl="img/double_checkbox_blue.png" Width="20px" 
               runat="server" OnClick="ib_check_Click" />
                <asp:Label ID="l_check" runat="server" Text="Отметить все"></asp:Label>
       </div>
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
        

</div>
        </div>
</asp:Content>



﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="edit_consumpt.aspx.cs" Inherits="temp_web1.edit_consumpt2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="p_menu" CssClass="mymenu" runat="server">
       <div class="forlink"><a href="Default.aspx">Главная</a></div>
       <div class="forlink"><a href="consumptions.aspx">Управление затратами</a></div>
        <div class="forlink"><a href="plans.aspx">Планирование затрат</a></div>
       <div class="forlink"> <a href="cats.aspx">Категории затрат</a></div>
       <div class="forlink"> <a href="bils.aspx">Счета</a></div>
       <div class="forlink"> <a href="users.aspx">Пользователи</a></div>
       <div class="forlink"> <a href="reports.aspx">Отчеты</a></div>
     </asp:Panel>
    <asp:Panel ID="p_main" CssClass="maincontent" runat="server">
        <div class="mar10">
            <div class="onerow border1">
                <asp:Panel ID="p_tv" runat="server" ScrollBars="Vertical">
                    <asp:TreeView ID="tv" Width="276px" Height="276px" runat="server">
                        <NodeStyle BorderColor="Black" ForeColor="Black" />
                        <SelectedNodeStyle BackColor="#16dbdb" />
                    </asp:TreeView>
                    </asp:Panel>
            </div>
            <div class="onerow">
                    <asp:Label ID="l_cat"  runat="server" Text="Категория"></asp:Label>
            </div>
        </div>
       <div class ="mar10">
           <asp:ImageButton ID="ib_show_hide" ImageUrl="img/checkbox.png" Width="20px" runat="server" OnClick="ib_show_hide_Click" />
                <asp:Label ID="l_collapse" runat="server" Text="Свернуть все"></asp:Label>
       </div>
       <div class ="mar10">
           <asp:TextBox ID="tb_data" runat="server" TextMode="Date"></asp:TextBox>
           <asp:Label ID="l_data" runat="server" Text="Дата расхода"></asp:Label>
       </div>
        <div class="mar10">
            <asp:TextBox ID="tb_value" CssClass="border1" runat="server" Width="292px"></asp:TextBox>
            <asp:Label ID="l_value" runat="server" Text="Cумма"></asp:Label>
        </div>
       <div class="mar10">
           <asp:DropDownList ID="ddl_bils" CssClass="border1" Width="296px" runat="server"></asp:DropDownList>
           <asp:Label ID="l_bil" runat="server" Text="Счет"></asp:Label>
       </div>
        <div class="mar10">
            <asp:TextBox ID="tb_descript" CssClass="border1" TextMode="MultiLine" runat="server" Height="55px" 
                Width="292px"></asp:TextBox> 
            <asp:Label ID="l_descript" runat="server" Text="Комментарий"></asp:Label>
        </div>
       <div class="mar10">
         <asp:Button ID="b_save" runat="server" Text="Сохранить" OnClick="b_save_Click" />
            <asp:Button ID="b_cancel" runat="server" Text="Отмена" OnClick="b_cancel_Click" />
        </div>
    </asp:Panel>
</asp:Content>

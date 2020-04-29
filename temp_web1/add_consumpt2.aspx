﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="add_consumpt2.aspx.cs" Inherits="temp_web1.add_consumpt2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="mymenu">
       <div class="forlink"><a href="Default.aspx">Главная</a></div>
       <div class="forlink"><a href="consumptions.aspx">Управление затратами</a></div>
       <div class="forlink"> <a href="Default.aspx">Категории затрат</a></div>
       <div class="forlink"> <a href="Default.aspx">Счета</a></div>
       <div class="forlink"> <a href="Default.aspx">Пользователи</a></div>
       <div class="forlink"> <a href="Default.aspx">Отчеты</a></div>
   </div>
   <div class="maincontent">
       <div class="mar10">
        <div class="onerow "><div class="border1">
            <asp:Panel ID="p_tv" runat="server" ScrollBars="Vertical">
            <asp:TreeView Width="275px" Height =" 275px" ID="tv" runat="server">
                <NodeStyle BorderColor="Black" ForeColor="Black" />
                <SelectedNodeStyle BackColor="#16dbdb" />
            </asp:TreeView>
                </asp:Panel>
                </div>
        </div>
        <div class="onerow">
            <asp:Label ID="l_cat" runat="server" Text="Категория"></asp:Label>
        </div>
    </div>
       
        <div class="mar10">
            <asp:TextBox ID="tb_value" CssClass="border1" runat="server" Width="292px"></asp:TextBox>
            <asp:Label ID="l_value" runat="server" Text="Cумма"></asp:Label>
        </div>
       <div class="mar10">
           <asp:DropDownList class="border1" ID="ddl_bils" runat="server" Width="296px">
           </asp:DropDownList>
           <asp:Label ID="l_bil" runat="server" Text="Cчет"></asp:Label>
       </div>
        <div class="mar10">
            <asp:TextBox ID="tb_descript" CssClass="border1" TextMode="MultiLine" runat="server" Height="55px" Width="292px"></asp:TextBox> 
            <asp:Label ID="l_descript" runat="server" Text="Комментарий"></asp:Label>
        </div>
       <div class="mar10">
         <asp:Button ID="b_save" runat="server" Text="Сохранить" OnClick="b_save_Click" />
            <asp:Button ID="b_cancel" runat="server" Text="Отмена" 
            OnClick="b_cancel_Click" />
        </div>
    </div>
</asp:Content>
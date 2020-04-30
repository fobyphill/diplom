﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="bils.aspx.cs" Inherits="temp_web1.bils" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mymenu">
       <div class="forlink"><a href="Default.aspx">Главная</a></div>
       <div class="forlink"><a href="consumptions.aspx">Управление затратами</a></div>
        <div class="forlink"><a href="plans.aspx">Планирование затрат</a></div>
       <div class="forlink"> <a href="cats.aspx">Категории затрат</a></div>
       <div class="forlink"> <a href="bils.aspx">Счета</a></div>
       <div class="forlink"> <a href="users.aspx">Пользователи</a></div>
       <div class="forlink"> <a href="reports.aspx">Отчеты</a></div>
   </div>
    <div class="maincontent">
        
        <div class="divhint">
            <asp:Label cssclass ="hint stress" ID="l_hint_no_1" runat="server" 
            Text="Ни одна запись не была выбрана" Visible="False"></asp:Label>
            <asp:Label CssClass="hint" ID="l_click_left" runat="server" 
            Text="Выберите счет из списка"></asp:Label>
        </div>
            <div class="mar10 onerow">
            <asp:ListBox ID="lb_bils" runat="server" Height="180px" style="margin-top: 0px" Width="192px" AutoPostBack="True" OnSelectedIndexChanged="lb_bils_SelectedIndexChanged"></asp:ListBox>
            <asp:Label ID="l_bils" runat="server" Text="счета"></asp:Label>
            </div>
            <div class="onerow">
                <div class="mar10">
            <asp:TextBox ID="tb_name" runat="server"></asp:TextBox>
            <asp:Label ID="l_name" runat="server" Text="Название счета"></asp:Label>
                </div>
                <div class="mar10">
            <asp:TextBox ID="tb_num" runat="server"></asp:TextBox>
            <asp:Label ID="l_num" runat="server" Text="Номер счета"></asp:Label>
                </div>
                <div class="mar10">
            <asp:TextBox ID="tb_descript" runat="server" Height="119px" TextMode="MultiLine"></asp:TextBox>
            <asp:Label ID="l_desript" runat="server" Text="Описание счета"></asp:Label>
               </div>
            </div>
        <div>
        <div class="onerow mar10">
        <asp:Button  ID="b_add_con" runat="server" Text="Добавить" 
            OnClick="b_add_con_Click" />
            </div>
       <div class="onerow mar10">
            <asp:Button ID="b_change" runat="server" Text="Изменить" OnClick="b_change_Click" />
           </div>
        <div class="onerow mar10">
                    <asp:Button ID="b_delete" runat="server" Text="Удалить" OnClick="b_delete_Click"/>
            
            </div>
            </div>
            <ajaxtoolkit:modalpopupextender TargetControlID="b_inv" PopupControlID="p_modal_confirm" 
                ID="mpe" runat="server" DropShadow="True"></ajaxtoolkit:modalpopupextender>
       
        <div class="invis"><asp:Button ID="b_inv" runat="server" Text="Невидимка" /></div>
        

</div>
    <asp:Panel CssClass="modalwin" ID="p_modal_confirm" runat="server">
                Вы уверены, что желаете удалить запись о планировании?<br /><br />
                <asp:Button ID="b_yes" runat="server" Text="Да" OnClick="b_yes_Click" />
                <asp:Button ID="b_no" runat="server" Text="Нет" OnClick="b_no_Click" />
            </asp:Panel>
</asp:Content>

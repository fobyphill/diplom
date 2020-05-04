﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="users.aspx.cs" Inherits="temp_web1.users" %>
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
            Text="Пользователь не выбран. " Visible="False"></asp:Label>
            <asp:Label CssClass="hint" ID="l_click_left" runat="server" 
            Text="Выберите пользователя из списка"></asp:Label>
        </div>
            <div class="mar10 onerow">
            <asp:ListBox ID="lb_users" runat="server" Height="225px" style="margin-top: 0px" Width="192px" AutoPostBack="True" 
                OnSelectedIndexChanged="lb_users_SelectedIndexChanged"></asp:ListBox>
            <asp:Label ID="l_users" runat="server" Text="Пользователи"></asp:Label>
                <br />
                <asp:ImageButton ID="ib_show_hide_pass" ImageUrl="img/checkbox.png" Width="20px" runat="server" OnClick="ib_show_hide_pass_Click" />
                <asp:Label ID="l_collapse" runat="server" Text="Показать пароли"></asp:Label>
            </div>
            <div class="onerow">
                <div class="mar10">
            <asp:TextBox ID="tb_name" runat="server"></asp:TextBox>
            <asp:Label ID="l_name" runat="server" Text="Имя пользователя"></asp:Label>
                </div>
                <div class="mar10">
            <asp:TextBox ID="tb_fam" runat="server"></asp:TextBox>
            <asp:Label ID="l_fam" runat="server" Text="Фамилия пользователя"></asp:Label>
                </div>
                <div class="mar10">
            <asp:TextBox ID="tb_login" runat="server" Height="16px"></asp:TextBox>
            <asp:Label ID="l_login" runat="server" Text="Логин пользователя"></asp:Label>
               </div>
                <div class="mar10">
            <asp:TextBox ID="tb_password" runat="server" Height="16px" TextMode="Password"></asp:TextBox>
            <asp:Label ID="l_password" runat="server" Text="Пароль пользователя"></asp:Label>
               </div>
                <div class="mar10">
            <asp:TextBox ID="tb_pass2" runat="server" Height="16px" TextMode="Password"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Text="Повторите пароль"></asp:Label>
               </div>
                <div class="mar10">
                    <div class="onerow"><asp:RadioButtonList ID="rbl_status" runat="server" Width="172px">
                        <asp:ListItem Value="a">Администратор</asp:ListItem>
                        <asp:ListItem Value="u">Сотрудник</asp:ListItem>

                    </asp:RadioButtonList>
                        </div>
                   <div class="onerow"> <asp:Label ID="l_status" runat="server" Text="Права пользователя"></asp:Label>
                       </div>
                </div>
            </div>
        <div>
        <div class="onerow mar10">
        <asp:Button  ID="b_add" runat="server" Text="Добавить" 
            OnClick="b_add_Click" />
            </div>
       <div class="onerow mar10">
            <asp:Button ID="b_change" runat="server" Text="Изменить" OnClick="b_change_Click" />
           </div>
        <div class="onerow mar10">
                    <asp:Button ID="b_delete" runat="server" Text="Удалить" OnClick="b_delete_Click"/>
            
            </div>
            <div class="onerow mar10">
                    <asp:Button ID="b_clear" runat="server" Text="Очистить" OnClick="b_clear_Click" />
            
            </div>
            </div>
            <ajaxtoolkit:modalpopupextender TargetControlID="b_inv" PopupControlID="p_modal_confirm" 
                ID="mpe" runat="server" DropShadow="True"></ajaxtoolkit:modalpopupextender>
       
        <div class="invis"><asp:Button ID="b_inv" runat="server" Text="Невидимка" /></div>
        

</div>
    <asp:Panel CssClass="modalwin" ID="p_modal_confirm" runat="server">
                Вы уверены, что желаете удалить пользователя?<br />
        Все записи о затратах и планированиях, привязанные к этому пользователю,<br />
                будут перенаправлены пользователю administrator<br /><br />
                <asp:Button ID="b_yes" runat="server" Text="Да" OnClick="b_yes_Click" />
                <asp:Button ID="b_no" runat="server" Text="Нет" OnClick="b_no_Click" />
            </asp:Panel>
</asp:Content>
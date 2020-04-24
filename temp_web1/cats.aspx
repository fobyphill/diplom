<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cats.aspx.cs" Inherits="temp_web1.cats" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mymenu">
       <div class="forlink"><a href="Default.aspx">Главная</a></div>
       <div class="forlink"><a href="consumptions.aspx">Управление затратами</a></div>
       <div class="forlink"> <a href="cats.aspx">Категории затрат</a></div>
       <div class="forlink"> <a href="Default.aspx">Счета</a></div>
       <div class="forlink"> <a href="Default.aspx">Пользователи</a></div>
       <div class="forlink"> <a href="Default.aspx">Отчеты</a></div>
   </div>
    <div class="maincontent">
        <div class="mar10">
            <div class="onerow border1">
                <asp:Panel ID="p_tv" runat="server" ScrollBars="Vertical">
                    <asp:TreeView ID="tv" Width="276px" Height="276px" runat="server" OnSelectedNodeChanged="tv_SelectedNodeChanged">
                        <NodeStyle BorderColor="Black" ForeColor="Black" />
                        <SelectedNodeStyle BackColor="#16dbdb" />
                    </asp:TreeView>
                    </asp:Panel>
            </div>
            <div class="onerow">
                    <asp:Label ID="l_cats"  runat="server" Text="Категория"></asp:Label>
            </div>
        </div>
        <asp:Panel ID="p_add_edit" runat="server">
            <div class="onerow"> 
                <asp:ImageButton ID="ib_show_hide" ImageUrl="img/checkbox.png" Width="20px" runat="server" OnClick="ib_show_hide_Click" />
                <asp:Label ID="l_collapse" runat="server" Text="Развернуть все"></asp:Label><br />
                <asp:TextBox ID="tb_parent_cat" runat="server"></asp:TextBox>
                <asp:Label ID="l_parent_cat" runat="server" Text="Родительская категория">
                </asp:Label>
                <br />
                 <asp:TextBox ID="tb_cat" runat="server"></asp:TextBox>
                <asp:Label ID="l_cat" runat="server" Text="Категория затрат"></asp:Label>
                <br />
                <asp:TextBox ID="tb_descript" runat="server" Height="72px" Width="119px" TextMode="MultiLine"></asp:TextBox>
                <asp:Label ID="l_descript" runat="server" Text="Описание"></asp:Label>
            </div>
        </asp:Panel>
       
        <div class="mar10 onerow">
        <asp:Button  ID="b_add_cat" runat="server" Text="Добавить" 
            OnClick="b_add_cat_Click" />
            </div>
       <div class="mar10 onerow">
            <asp:Button ID="b_change" runat="server" Text="Изменить" OnClick="b_change_Click" />
           </div>
        <div class="mar10 onerow">
                    <asp:Button ID="b_delete" runat="server" Text="Удалить" OnClick="b_delete_Click"/>
            
            
            <ajaxToolkit:ModalPopupExtender TargetControlID="b_inv" PopupControlID="p_modal_confirm" 
                ID="mpe" runat="server" DropShadow="True"></ajaxToolkit:ModalPopupExtender>
       </div>
        <div class="invis"><asp:Button ID="b_inv" runat="server" Text="Невидимка" /></div>
        

</div>
    <asp:Panel CssClass="modalwin" ID="p_modal_confirm" runat="server">
                Вы уверены, что желаете удалить запись о расходе?<br /><br />
                <asp:Button ID="b_yes" runat="server" Text="Да" OnClick="b_yes_Click" />
                <asp:Button ID="b_no" runat="server" Text="Нет" />
            </asp:Panel>
</asp:Content>

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

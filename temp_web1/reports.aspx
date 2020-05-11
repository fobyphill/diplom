<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reports.aspx.cs" 
    Inherits="temp_web1.reports" %>
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
            Text="Отчет не выбран. " Visible="False"></asp:Label>
            <asp:Label CssClass="hint" ID="l_click_left" runat="server" 
            Text="Выберите параметры отчета"></asp:Label>
        </div>
            <div class="mar10 onerow">
                <asp:DropDownList ID="ddl_month" runat="server">
                    <asp:ListItem>Январь</asp:ListItem>
                    <asp:ListItem>Февраль</asp:ListItem>
                    <asp:ListItem>Март</asp:ListItem>
                    <asp:ListItem>Апрель</asp:ListItem>
                    <asp:ListItem>Май</asp:ListItem>
                    <asp:ListItem>Июнь</asp:ListItem>
                    <asp:ListItem>Июль</asp:ListItem>
                    <asp:ListItem>Август</asp:ListItem>
                    <asp:ListItem>Сентябрь</asp:ListItem>
                    <asp:ListItem>Октябрь</asp:ListItem>
                    <asp:ListItem>Ноябрь</asp:ListItem>
                    <asp:ListItem>Декабрь</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="onerow">
            </div>
        <div>
        <div class="onerow mar10">
        <asp:Button  ID="b_print" runat="server" Text="Вывести" 
            OnClick="b_add_Click" OnClientClick="SetTarget();" />
            </div>
       <div class="onerow mar10">
            <asp:Button ID="b_change" runat="server" Text="Изменить" OnClick="b_change_Click" />
           </div>
        <div class="onerow mar10">
                    <asp:Button ID="b_delete" runat="server" Text="Удалить"/>
            
            </div>
            <div class="onerow mar10">
                    <asp:Button ID="b_clear" runat="server" Text="Очистить" />
            
            </div>
            </div>
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
</asp:Content>



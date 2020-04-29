<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="edit_plan.aspx.cs" Inherits="temp_web1.edit_plan" %>
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
       <div class="mar10">
        <div class="onerow "><div class="border1">
            <asp:Panel ID="p_tv" runat="server" ScrollBars="Vertical">
            <asp:TreeView Width="275px" Height =" 275px" ID="tv" runat="server" style="margin-right: 20px">
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
        <asp:ImageButton ID="ib_show_hide" ImageUrl="img/checkbox.png" Width="20px" runat="server" OnClick="ib_show_hide_Click" />
                <asp:Label ID="l_collapse" runat="server" Text="Свернуть все"></asp:Label>
    </div>
        <div class="mar10">
            <asp:TextBox ID="tb_value" CssClass="border1" runat="server" Width="292px"></asp:TextBox>
            <asp:Label ID="l_value" runat="server" Text="Cумма"></asp:Label>
        </div>
       <div class ="mar10">
           <asp:DropDownList ID="ddl_month" runat="server">
               <asp:ListItem Value="1">Январь</asp:ListItem>
               <asp:ListItem Value="2">Февраль</asp:ListItem>
               <asp:ListItem Value="3">Март</asp:ListItem>
               <asp:ListItem Value="4">Апрель</asp:ListItem>
               <asp:ListItem Value="5">Май</asp:ListItem>
               <asp:ListItem Value="6">Июнь</asp:ListItem>
               <asp:ListItem Value="7">Июль</asp:ListItem>
               <asp:ListItem Value="8">Август</asp:ListItem>
               <asp:ListItem Value="9">Сентябрь</asp:ListItem>
               <asp:ListItem Value="10">Октябрь</asp:ListItem>
               <asp:ListItem Value="11">Ноябрь</asp:ListItem>
               <asp:ListItem Value="12">Декабрь</asp:ListItem>
           </asp:DropDownList>
           <asp:DropDownList ID="ddl_year" runat="server">
           </asp:DropDownList>
           <asp:Label ID="l_period" runat="server" Text="Период планируемых затрат"></asp:Label>
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

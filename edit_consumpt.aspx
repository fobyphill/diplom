<%@ Page Language="C#" AutoEventWireup="true" 
    CodeBehind="edit_consumpt.aspx.cs" Inherits="temp_web1.edit_consumpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Редактировать расход</title>
     <link href="/Content/mycss.css" rel="stylesheet"/>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>
<body>
    <div class="myhead">
            <div class="textcenter">
                <h1 ><span class="logo1">Пла</span><span class="logo2">За</span></h1>
            </div>
        </div>
    <form id="form1" runat="server">
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
    </div>

    
    </form>
     <footer class="myhead">
                <div class="textcenter">
                <p><span class="logo1">&copy;</span> 
                    <span class="logo2"> <%: DateTime.Now.Year %></span> 
                    <span class="logo1">Pla</span><span class="logo2">Za</span></p>
                    </div>
            </footer>
</body>
</html>

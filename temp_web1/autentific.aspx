<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="autentific.aspx.cs" Inherits="temp_web1.autentific" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/Content/mycss.css" rel="stylesheet"/>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <title>Авторизация</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="myhead">
            <div class="textcenter">
                <h1 ><span class="logo1">Пла</span><span class="logo2">За</span></h1>
            </div>
        </div>
        <div class="myhead">
            <div class="textcenter">
            <h1 ><span class="logo1 mar10">Логин</span><asp:TextBox CssClass="mar10" ID="tb_login" runat="server"></asp:TextBox>
                <span class="logo2 mar10">Пароль</span><asp:TextBox CssClass="mar10" ID="tb_password" runat="server" 
                    TextMode="Password"></asp:TextBox><asp:Button CssClass="mar10 mybutton" ID="b_enter" runat="server" Text="Войти" Height="38px" Width="117px" Font-Names="Times New Roman" Font-Size="X-Large" /></h1>
            </div>
        </div>
        <footer class="myhead">
                <div class="textcenter">
                <p><span class="logo1">&copy;</span> 
                    <span class="logo2"> <%: DateTime.Now.Year %></span> 
                    <span class="logo1">Pla</span><span class="logo2">Za</span></p>
                    </div>
            </footer>
    </div>
    </form>
</body>
</html>

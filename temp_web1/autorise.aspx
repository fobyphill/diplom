<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="autorise.aspx.cs" 
    Inherits="temp_web1.autorise" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="auten">
            <div class="textcenter">
            <h1 ><span class="logo1 mar10">Логин</span><asp:TextBox CssClass="mar10" ID="tb_login" runat="server"></asp:TextBox>
                <span class="logo2 mar10">Пароль</span><asp:TextBox CssClass="mar10" ID="tb_password" runat="server" 
                    TextMode="Password"></asp:TextBox><asp:Button CssClass="mar10 mybutton" ID="b_enter" runat="server" 
                    Text="Войти" Height="38px" Width="117px" Font-Names="Times New Roman" Font-Size="X-Large" OnClick="b_enter_Click" />
                <br />
                <asp:Label ID="l_incorrect" runat="server" CssClass="stress" Text="Логин или пароль введены неверно" Visible="False"></asp:Label>
                </h1>
            </div>
        </div>
</asp:Content>

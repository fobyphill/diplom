<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="autorise2.aspx.cs" 
    Inherits="temp_web1.autorise" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="auten">
            <div class="textcenter">

               <h1 ><span class="logo1">Pla</span><span class="logo2">Za</span></h1>
            </div>
                <h4>Вход в личный кабинет</h4>
            <h2 ><asp:TextBox CssClass="mar10" ID="tb_login" runat="server"></asp:TextBox></h2><br />
                <span class="">Логин</span>
                <span class="logo2 mar10">Пароль</span><asp:TextBox CssClass="mar10" ID="tb_password" runat="server" 
                    TextMode="Password"></asp:TextBox><asp:Button CssClass="mar10 bluebutton" ID="b_enter" runat="server" 
                    Text="Войти"  Height="30px" Width="60px" Font-Names="Times New Roman" Font-Size="Medium" OnClick="b_enter_Click" />
                <br />
                <asp:Label ID="l_incorrect" runat="server" CssClass="stress" Text="Логин или пароль введены неверно" Visible="False"></asp:Label>
                </h2>
            </div>
        </div>
</asp:Content>

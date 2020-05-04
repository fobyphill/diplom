<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cons_user.aspx.cs" 
    Inherits="temp_web1.cons_user" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auten">
        
        <div class="divhint">
            <asp:Label cssclass ="hint stress" ID="l_hint_no_1" runat="server" 
            Text="Ни одна запись не была выбрана" Visible="False"></asp:Label>
            <asp:Label CssClass="hint" ID="l_click_left" runat="server" 
            Text="Для отметки записи кликните мышью по левому столбцу таблицы"></asp:Label></div>
        <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="true" ButtonType="Image" 
                    SelectImageUrl="checkbox.png" HeaderStyle-Width="25px" ControlStyle-Width="20px" Visible="True">
<ControlStyle Width="20px"></ControlStyle>

<HeaderStyle Width="25px" HorizontalAlign="Left"></HeaderStyle>    
                </asp:CommandField>
                <asp:BoundField DataField="id_con" HeaderText="№" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="15px" />
                </asp:BoundField>
                 <asp:BoundField DataField="data_create" DataFormatString = "{0:dd/MM/yyyy}" 
                     HeaderText="Дата внесения" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="60px"/>
                </asp:BoundField>
                <asp:BoundField DataField="data_change" DataFormatString = "{0:dd/MM/yyyy}" 
                    HeaderText="Дата изменения" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="60px"/>
                </asp:BoundField>
                <asp:BoundField DataField="value_con" HeaderText="Значение" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="60px"/>
                </asp:BoundField>
                <asp:BoundField DataField="name_cat" HeaderText="Категория" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="60px"/>
                </asp:BoundField>
                <asp:BoundField DataField="bil_con" HeaderText="Счет" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="155px"/>
                </asp:BoundField>
                <asp:BoundField DataField="descript_con" HeaderText="Комментарий" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="250px"/>
                </asp:BoundField>
                <asp:BoundField DataField="u_f" HeaderText="Создал сотрудник">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                </asp:BoundField>
                    <asp:BoundField DataField="u_f2" HeaderText="Изменил сотрудник" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"/>
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#26a9e0" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#26a9e0" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#16dbdb" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#e4e9eb" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
        <div class="mar10 onerow">
        <asp:Button  ID="b_add_con" runat="server" Text="Добавить" OnClick="b_add_con_Click" />
            </div>
       <div class="mar10 onerow">
            <asp:Button ID="b_change" runat="server" Text="Изменить" OnClick="b_change_Click" />
           </div>
        <div class="mar10 onerow">
                    <asp:Button ID="b_delete" runat="server" Text="Удалить"/>
            
            </div>
            <ajaxtoolkit:modalpopupextender TargetControlID="b_inv" PopupControlID="p_modal_confirm" 
                ID="mpe" runat="server" DropShadow="True"></ajaxtoolkit:modalpopupextender>
       
        <div class="invis"><asp:Button ID="b_inv" runat="server" Text="Невидимка" /></div>
        

</div>
    <asp:Panel CssClass="modalwin" ID="p_modal_confirm" runat="server">
                Вы уверены, что желаете удалить запись о расходе?<br /><br />
                <asp:Button ID="b_yes" runat="server" Text="Да" />
                <asp:Button ID="b_no" runat="server" Text="Нет" />
            </asp:Panel>
</asp:Content>

<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="temp_web1.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CellPadding="4" GridLines="None" ForeColor="#333333">
            <RowStyle BackColor="#EFF3FB" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="чек">
                    <ItemTemplate>
                        <asp:CheckBox ID="cb" runat="server" AutoPostBack="True" OnCheckedChanged="cb_CheckedChanged" Width="20px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="name_bil" HeaderText="данные" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>

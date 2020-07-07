<%@ Page Language="VB" AutoEventWireup="false" CodeFile="detalleevaluacion.aspx.vb" Inherits="personal_aulavirtual_lebir_eva_detalleevaluacion" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalle Evaluación</title>
</head>
<body>
    <form id="form1" runat="server">
    <h3>
        Evaluación del participante<br />
    </h3>
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" 
        Font-Size="10pt" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="3" ForeColor="#333333" Width="80%" AllowSorting="True" 
        Font-Names="Tahoma" Font-Size="9pt">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField HeaderText="Fecha de registro" DataField="fechareg_evp">
                <ItemStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Actividad" DataField="descripcion_aev">
                <ItemStyle Width="50%" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Calificativo" DataField="descripcion_evp">
                <ItemStyle Width="10%" />
            </asp:BoundField>
        </Columns>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <br />
    </form>
</body>
</html>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMuestraPedidoOdonto.aspx.vb" Inherits="academico_frmMuestraPedidoOdonto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="Small" 
        ForeColor="Red"></asp:Label>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblFecha" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td>
                <asp:Label ID="lblHistoria" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td>
                <asp:Label ID="lblPrecio" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" Width="100%">
        <Columns>
            <asp:BoundField DataField="descripcionResumidaArt" HeaderText="Material" />
            <asp:BoundField DataField="cantidad_dpo" HeaderText="Cantidad">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="precio_dpo" HeaderText="Precio Unit." 
                Visible="False">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="cantidadEntrega_dpo" HeaderText="Entregado">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="cantidadFaltaEntregar" HeaderText="Pendiente">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="estado_pod" HeaderText="Estado" />
        </Columns>
        <FooterStyle BackColor="#e8eef7" Font-Bold="True" ForeColor="#3366CC" 
                HorizontalAlign="Center" />
            <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                BorderWidth="1px" ForeColor="#3366CC" />
    </asp:GridView>
    </form>
</body>
</html>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCosultarPedidosRealizados.aspx.vb" Inherits="logistica_frmCosultarPedidosRealizados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <table cellpadding="2" cellspacing="0" style="width:100%">
        <tr>
            <td style="width: 10%">
                &nbsp;<div style="width: 125px">
                Proceso:</div>
            </td>
            <td style="width: 90%">
                <asp:DropDownList ID="dpCodigo_pct" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    
    </div>
    <div>
    <table cellpadding="2" cellspacing="0" style="width:100%">
        <tr>
            <td style="width: 10%">
                Centro Costo:</td>
            <td style="width: 90%">
                <asp:DropDownList ID="dpCodigo_cco" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
            </div>
    <div style="margin-left: 120px">
&nbsp;&nbsp;
                <asp:Button ID="cmdConsultar" runat="server" Text="   Consultar" 
                    CssClass="buscar2" />
            &nbsp; <asp:Button ID="cmdExportar" runat="server" Text="   Exportar" 
                    CssClass="excel2" />
            </div>
    <div>
        <asp:Label ID="Label1" runat="server" 
            Text="______________________________________________________________________________________________________________________________________________________________"></asp:Label>
        <br />
        <br />
    </div>
    <asp:Panel ID="Panel1" runat="server" BorderStyle="Groove" Height="778px">
        <asp:GridView ID="dgvPedidos" runat="server" 
    AutoGenerateColumns="False" CellPadding="4" Font-Bold="False" Font-Size="Small" 
    ForeColor="#333333" GridLines="None" Height="236px" Width="1272px">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="codigo_ped" 
            HeaderText="Pedido" />
                <asp:BoundField DataField="fecha_ped" 
            HeaderText="Fecha Pedido" />
                <asp:BoundField DataField="descripcionArt" 
            HeaderText="Artículo" />
                <asp:BoundField DataField="cantidad_Dpe" 
            HeaderText="Cantidad" />
                <asp:BoundField DataField="precioReferencial_Dpe" 
            HeaderText="Precio" />
                <asp:BoundField DataField="observacion_Dpe" 
            HeaderText="Observación" />
                <asp:BoundField DataField="descripcionEstado_Eped" 
            HeaderText="Estado" />
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" 
        ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" 
        HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" 
        ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" 
        ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" 
        ForeColor="#284775" />
        </asp:GridView>
    </asp:Panel>
    </form>
</body>
</html>

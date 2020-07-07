<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCosultarPedidosRealizados_v1.aspx.vb" Inherits="logistica_frmCosultarPedidosRealizados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
       body
       {
           font-family:Calibri;
           
           }
    </style>
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
                <asp:DropDownList ID="dpCodigo_pct" runat="server" AutoPostBack="true">
                </asp:DropDownList>
            </td>
        </tr>
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
    ForeColor="#333333" GridLines="None" Width="1272px" EmptyDataText="No se Encontraron Items Registrados..." EmptyDataRowStyle-ForeColor="Red"  >
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundField DataField="codigo_ped" 
            HeaderText="Pedido" />
                <asp:BoundField DataField="fecha_ped" 
            HeaderText="Fecha Pedido" />
                <asp:BoundField DataField="descripcionArt" 
            HeaderText="Articulo" />
                <asp:BoundField DataField="cantidad_Dpe" 
            HeaderText="Cantidad" />
                <asp:BoundField DataField="precioReferencial_Dpe" 
            HeaderText="Precio" />
                <asp:BoundField DataField="observacion_Dpe" 
            HeaderText="Observación" />
                <asp:BoundField DataField="descripcionEstado_Eped" 
            HeaderText="Estado" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" 
        ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" 
        HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" 
        ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" 
        ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </asp:Panel>
    </form>
</body>
</html>

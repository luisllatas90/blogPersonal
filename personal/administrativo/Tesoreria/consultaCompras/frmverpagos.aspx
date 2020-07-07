<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmverpagos.aspx.vb" Inherits="frmverpagos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script src="funciones.js"></script>
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="lstinformacioncargos" runat="server" AutoGenerateColumns="False"
            Font-Names="Arial" Font-Size="8pt" Height="1px" Width="98%">
            <Columns>
                <asp:BoundField DataField="codigo_egr" HeaderText="Id">
                    <ItemStyle Font-Size="X-Small" Width="10%" />
                    <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                </asp:BoundField>
                <asp:BoundField DataField="descripcion_tdo" HeaderText="Documento">
                    <ItemStyle Font-Size="X-Small" Width="20%" />
                    <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                </asp:BoundField>
                <asp:BoundField DataField="descripcion_rub" HeaderText="Rubro">
                    <ItemStyle Font-Size="X-Small" Width="20%" />
                    <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                </asp:BoundField>
                <asp:BoundField HeaderText="importeunitario">
                    <ItemStyle Font-Size="X-Small" Width="5%" />
                    <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                </asp:BoundField>
                <asp:BoundField DataField="cantidad_deg" HeaderText="Cantidad">
                    <ItemStyle Font-Size="X-Small" Width="5%" />
                    <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                </asp:BoundField>
                <asp:BoundField DataField="importe_deg" HeaderText="Importe Cancelado">
                    <ItemStyle Font-Size="X-Small" Width="10%" />
                    <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                </asp:BoundField>
                <asp:BoundField DataField="descripcion_cco" HeaderText="Centro de Costos">                
                    <ItemStyle Font-Size="X-Small" Width="20%" />
                    <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                </asp:BoundField>
                
                <asp:BoundField DataField="importetransferido_deg" HeaderText="Importe transferido">
                    <ItemStyle Font-Size="X-Small" Width="20%" />
                    <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                </asp:BoundField>
                <asp:CommandField />
                <asp:ImageField DataImageUrlField="codigo_deg" DataImageUrlFormatString="~/iconos/buscar.gif" AlternateText ="Ver documento de Pago">
                </asp:ImageField>
            </Columns>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>

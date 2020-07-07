<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmVerAbonos.aspx.vb" Inherits="administrativo_pec_frmVerAbonos" Theme="Acero" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    Detalle de Abonos:</td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:GridView ID="gvAbonos" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" GridLines="None" SkinID="skinGridView">
                        <FooterStyle Font-Bold="True" />
                        <Columns>
                            <asp:BoundField DataField="Fecha" HeaderText="FECHA ABONO" 
                                SortExpression="Fecha" />
                            <asp:BoundField DataField="descripcion_Tdo" HeaderText="TIPO DOCUMENTO" 
                                SortExpression="descripcion_Tdo" />
                            <asp:BoundField DataField="nroDocumento_Cin" HeaderText="NRO.DOCUMENTO" 
                                SortExpression="nroDocumento_Cin" />
                            <asp:BoundField DataField="descripcion_Sco" HeaderText="SERVICIO" 
                                SortExpression="descripcion_Sco" />
                            <asp:BoundField DataField="subtotal_Dci" HeaderText="IMPORTE" 
                                SortExpression="subtotal_Dci" />
                            <asp:BoundField DataField="montoTransf_dci" HeaderText="TRANSFERIDO" 
                                SortExpression="montoTransf_dci" />
                            <asp:BoundField DataField="observacion_Cin" HeaderText="OBSERVACION" 
                                SortExpression="observacion_Cin" />
                        </Columns>
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle Font-Bold="True" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmconsultacompras.aspx.vb" Inherits="frmconsultacompras" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script src="funciones.js">
    </script>
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            font-family: "Courier New", Courier, "espacio sencillo";
            color: #FFFFFF;
            font-size: small;
        }
    </style>
    </head>
<body bgcolor="White">
    <form id="form1" runat="server">
    
        <table width ="100%" 
            style="border: 1px solid #000000; background-color: #FFFFFF;" border="0" 
            cellpadding="-1" cellspacing="-1">
            <tr>
                <td style="border: 0px solid #000000; background-color: #993300;" 
                    class="style1" >
                    Consultar documentos por número</td>
            </tr>
            <tr>
                <td style="border: 0px solid #000000" >
                    <hr style="color: #FF9900" />
                </td>
            </tr>
            <tr>
                <td style="border: 0px solid #000000" >
                    <asp:Panel ID="pnlconsultarpornumero" runat="server" Height="26px" 
                        Width="613px" BorderStyle="None" BorderWidth="0px">
                        <asp:Label ID="Label1" runat="server" Text="Nùmero de Documento :" 
                            Width="208px" Font-Names="Arial" Font-Size="8pt" Height="30px" 
                            
                            style="color: #333300; font-family: 'Courier New', Courier, 'espacio sencillo';"></asp:Label>
                        <asp:TextBox ID="txtnumerodocumento" runat="server" Width="304px" Height="21px" 
                            style="font-family: Arial; font-size: x-small"></asp:TextBox>
                        <asp:Button ID="cmdbusacrpornumero" runat="server" Text="Buscar" Width="54px" 
                            BackColor="#3399FF" BorderColor="White" BorderStyle="Ridge" 
                            Font-Names="Courier New" ForeColor="White" /></asp:Panel>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="height: 141px; width : 99%">
                <div style="border-width: 1px; border-color: #000000; overflow : auto; height : 696px; width: 99%; background-color: #FFFFFF; border-top-style: solid;"><img src ="iconos/atencion.gif" />
                    <asp:Label ID="lblobservacion" runat="server" BackColor="#FFFFD7" 
                        BorderColor="#FFFFCC" Font-Names="Arial" Font-Size="Small" ForeColor="#993300" 
                        Text="No existen elementos a mostrar" Width="1136px" 
                        style="color: #FFFFFF; font-family: 'Courier New'; background-color: #993300"></asp:Label>
                    <asp:GridView ID="lstinformacion" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="codigo_rco" Width ="98%" BackColor="White" Height="102px">
                        <Columns>
                            <asp:BoundField DataField="codigo_rco" HeaderText="Id" InsertVisible="False"
                                ReadOnly="True" SortExpression="codigo_rco">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_tdo" HeaderText="Tipo Documento" SortExpression="descripcion_tdo">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechadoc_rco" HeaderText="Fecha">
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numerodoc_rco" HeaderText="Número" SortExpression="numerodoc_rco">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="Proveedor" ReadOnly="True" SortExpression="nombres">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_tip" HeaderText="Moneda" SortExpression="descripcion_tip" >
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="igvcompra_rco" HeaderText="IGV" SortExpression="igvcompra_rco">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="totalcompra_rco" HeaderText="Total Compra" SortExpression="totalcompra_rco">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="totalfisico_rco" HeaderText="Total Físico" SortExpression="totalfisico_rco">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valorcompra_rco" HeaderText="Valor compra" SortExpression="valorcompra_rco">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_tcom" HeaderText="Tipo Compra" SortExpression="descripcion_tcom">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:ImageField DataImageUrlField="codigo_rco" 
                                DataImageUrlFormatString="~/iconos/Atencion.gif" 
                                AlternateText="Visualizar Cargos">
                            </asp:ImageField>
                            <asp:ImageField DataImageUrlField="codigo_rco" DataImageUrlFormatString="~/iconos/buscar.gif">
                            </asp:ImageField>
                            <asp:ImageField DataImageUrlField="codigo_rco" 
                                DataImageUrlFormatString="~/iconos/buscar.gif">
                            </asp:ImageField>
                        </Columns>
                    </asp:GridView>
                   
                    </td>
            </tr>
            </table>
    
    
            <p>
                <img src ="iconos/atencion.gif" /><asp:Label ID="lblobservacion0" 
                    runat="server" BackColor="#FFFFD7" 
                        BorderColor="#FFFFCC" Font-Names="Arial" Font-Size="Small" ForeColor="#993300" 
                        Text="No existen elementos a mostrar" Width="1192px"></asp:Label>
    
    
    </form>
</body>
</html>

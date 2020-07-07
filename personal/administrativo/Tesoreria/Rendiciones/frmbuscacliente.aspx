<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmbuscacliente.aspx.vb" Inherits="frmbuscacliente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link  rel ="stylesheet" href="estilo.css"/>
    <title>Página sin título</title>
</head>
<body bgcolor="#EEEEEE">
    <form id="form1" runat="server">
    <div>
        <br />
        <table width="100%" >
            <tr>
                <td class="usatCeldaTitulo" colspan="3" style="height: 26px">
                    Buscar Personal</td>
                <td class="usatCeldaTitulo" colspan="1" style="height: 26px">
                </td>
            </tr>
            <tr>
                <td style="width: 10%; height: 25px;" class="usatCeldaTitulo" >
                    &nbsp;Nombres :</td>
                <td style="width: 40%; height: 25px;">
                    <asp:TextBox ID="txtcliente" runat="server" Width="520px"></asp:TextBox></td>
                <td style="height: 25px;" colspan="2">
                    <asp:Button ID="cmdbuscar" runat="server" Text="Buscar" BackColor="LemonChiffon" Font-Names="Verdana" Font-Size="8pt" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblmensaje" runat="server" ForeColor="Maroon" Width="632px"></asp:Label></td>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="lstinformacion" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                        Font-Size="8pt" Height="1px" Style="background-image: url(eliminar.gif); background-repeat: no-repeat"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="codigo_tcl" HeaderText="ID">
                                <HeaderStyle CssClass="usatCeldaTitulo" Width="10%" />
                                <ItemStyle BackColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="Cliente">
                                <HeaderStyle CssClass="usatCeldaTitulo" Width="90%" />
                                <ItemStyle BackColor="White" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle BorderStyle="Solid" />
                        <EmptyDataRowStyle CssClass="usatCeldaTitulo" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 15px">
                </td>
                <td style="width: 2px; height: 15px">
                    <asp:Button ID="cmdaceptar" runat="server" BackColor="LemonChiffon" Font-Names="Verdana"
                        Font-Size="8pt" Text="Aceptar" Width="104px" />
                </td>
                <td style="width: 100px; height: 15px">
                    <asp:Button ID="cmdcerrar" runat="server" BackColor="LemonChiffon" Font-Names="Verdana"
                        Font-Size="8pt" Text="Cerrar" Width="104px" /></td>
            </tr>
        </table>
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>

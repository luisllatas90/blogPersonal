<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReporteDeCompras.aspx.vb" Inherits="Biblioteca_Reporte_de_compras" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" type="text/css" rel="Stylesheet" />    
</head>
<body style="margin:0">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table bgcolor="#F0F0F0" width="100%">
                        <tr>
                            <td align="center" class="usatTitulousat" colspan="2">
                    <asp:Label ID="LblTitulo" runat="server" Text="REPORTE DE COMPRAS"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Ver:
                                <asp:DropDownList ID="CboVer" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="0">Todos</asp:ListItem>
                                    <asp:ListItem Value="1">Año</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="CboAnio" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:Button ID="CmdConsultar" runat="server" CssClass="buscar" 
                                    Text="   Consultar" Width="85px" Height="20px" EnableViewState="False" />
                            &nbsp;
                                <asp:Button ID="CmdExportar" runat="server" Text="  Exportar" 
                    CssClass="Exportar" Width="85px" Height="20px" EnableViewState="False" />
                                      </td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="LblTotal" runat="server" ForeColor="#0000CC"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GvwDatos" runat="server" CellPadding="4" ForeColor="#333333" 
                        GridLines="None" Width="100%" EnableViewState="False">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LibrosnoInventariados.aspx.vb" Inherits="Biblioteca_LibrosnoInventariados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <script src="../../../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    <link href="../private/estilo.css" type="text/css" rel="Stylesheet" />
</head>
<body style="margin:0">
<%  response.write(clsfunciones.cargacalendario) %>
    <form id="form1" runat="server">
    
        <table style="width:100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="usatTitulousat" align="center" bgcolor="#F0F0F0">
                    <asp:Label ID="LblTitulo" runat="server" Text="REPORTE DE LIBROS"></asp:Label>
                    &nbsp;</td>
            </tr>
            <tr>
                <td bgcolor="#F0F0F0">

    
    <table align="center" width="70%" bgcolor="#F0F0F0">
        <tr>
            <td width="70px">
                Ver</td>
            <td >
                :</td>
            <td align="justify" >
                <asp:RadioButtonList ID="RblVer" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="2">Inventariados</asp:ListItem>
                    <asp:ListItem Value="3">No inventariados</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td align="right" >
                &nbsp;</td>
        </tr>
        <tr>
            <td width="70px" >
                Estado</td>
            <td>
                :</td>
            <td >
                <asp:DropDownList ID="CboEstado" runat="server">
                </asp:DropDownList>
                                </td>
            <td align="right">
                <asp:Button ID="CmdBuscar" runat="server"  Text="Buscar" CssClass="buscar" 
                    Width="80px" Height="20px" />
            </td>
        </tr>
        <tr>
            <td width="70px" >
                Inventario</td>
            <td>
                :</td>
            <td >
                <asp:DropDownList ID="cboInventarios" runat="server">
                </asp:DropDownList>
                <asp:TextBox ID="TxtFechafin" runat="server" Width="87px" Visible="False"></asp:TextBox>
                <input id="Button2" type="button"  class="cunia" 
                    onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechafin,'dd/mm/yyyy')" 
                    style="height: 22px; visibility: hidden;" /></td>
            <td align="right">
                <asp:Button ID="CmdExportar" runat="server" Text="Exportar" 
                    CssClass="Exportar" Width="80px" Height="20px" />
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
                <td align="center">
                    <asp:GridView ID="GvInventario" runat="server" CellPadding="4" 
                        ForeColor="#333333" GridLines="Horizontal" Width="98%" BorderColor="Black" 
                        BorderStyle="Solid">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            <asp:Label ID="Label1" runat="server" ForeColor="#FF3300" 
                                style="text-align: center" Text="No se encontraron registros" Width="100%"></asp:Label>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </form>
</body>
</html>

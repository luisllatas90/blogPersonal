<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SolicitudesPorUsuario.aspx.vb" Inherits="SisSolicitudes_SolicitudesPorUsuario" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" /> 
     <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../private/calendario.js" language="javascript" type="text/javascript"></script>
    <script src="../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
</head>
<body  bgcolor="#F0F0F0">
    <form id="form1" runat="server">
<%  response.write(clsfunciones.cargacalendario) %>
    <table width="100%" border="0" cellpadding="0" 
        cellspacing="0">
        <tr>
            <td bgcolor="#F0F0F0" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td bgcolor="#F0F0F0" colspan="2">
                &nbsp;&nbsp;
                <asp:Label ID="LblTitulo" runat="server" Text="Solicitudes registradas por:"></asp:Label>
&nbsp;<asp:DropDownList ID="CboUsuario" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td bgcolor="#F0F0F0">
                &nbsp;&nbsp;
                Fecha Inicio:
                <asp:TextBox ID="TxtFechaIni" runat="server" Width="100px"></asp:TextBox>
                <input id="Button1" type="button"  class="cunia" onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechaIni,'dd/mm/yyyy')" style="height: 22px" />&nbsp;&nbsp;Fecha Final:
                <asp:TextBox ID="TxtFechaFin" runat="server" Width="100px"></asp:TextBox>
                <input id="Button2" type="button"  class="cunia" 
                    onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechaFin,'dd/mm/yyyy')" 
                    style="height: 22px" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="CmdBuscar" runat="server" Text="Buscar" CssClass="Buscar" 
                    Width="70px" />
            </td>
            <td align="right" bgcolor="#F0F0F0">
                <asp:Button ID="CmdExportar" runat="server" Text="Exportar" 
                    CssClass="ExportarAWord" Width="70px" />
            </td>
        </tr>
        <tr>
            <td colspan="2" bgcolor="#F0F0F0" height="15px">
                </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Table ID="TblSolicitudes" runat="server" BorderColor="Black" 
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="0" CellSpacing="0" 
                    GridLines="Horizontal" Width="100%" BackColor="White">
                </asp:Table>
            </td>
        </tr>
        <tr>
            <td colspan="2" >
                &nbsp;</td>
        </tr>
    </table>

    </form>
</body>
</html>

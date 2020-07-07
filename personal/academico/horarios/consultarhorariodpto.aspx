<%@ Page Language="VB" AutoEventWireup="false" CodeFile="consultarhorariodpto.aspx.vb" Inherits="consultarhorariodpto" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Horarios por Departamento Académico</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <style type="text/css">
    .Marcado {
    background-color: #FFCC00;
    text-align:center;
    font-size: 7pt;
    }
    .etiquetaTabla {
    background-color: #EAEAEA;
    color: #0000FF;
    text-align:center;
    }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Reporte de Horarios por Departamento Académico</p>
    <table style="width:100%" cellpadding="3">
        <tr>
            <td width="20%">
                Ciclo:</td>
            <td width="80%">
                <asp:DropDownList ID="dpCodigo_cac" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="20%">
                Dpto. Académico</td>
            <td width="80%">
                <asp:DropDownList ID="dpCodigo_dac" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td width="80%">
                <asp:Button ID="cmdBuscar" runat="server" CssClass="usatbuscar" 
                    Text="Consultar" />
            </td>
        </tr>
        </table>
&nbsp;<asp:Table ID="tblHorario" runat="server" BorderColor="#999999" 
                            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="0" 
                            GridLines="Both" Width="100%">
                            <asp:TableRow runat="server" CssClass="etiquetaTabla">
                                <asp:TableCell runat="server">HORA</asp:TableCell>
                                <asp:TableCell runat="server">LUNES</asp:TableCell>
                                <asp:TableCell runat="server">MARTES</asp:TableCell>
                                <asp:TableCell runat="server">MIERCOLES</asp:TableCell>
                                <asp:TableCell runat="server">JUEVES</asp:TableCell>
                                <asp:TableCell runat="server">VIERNES</asp:TableCell>
                                <asp:TableCell runat="server">SABADO</asp:TableCell>
                                <asp:TableCell runat="server">DOMINGO</asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
        
    </form>

</body>
</html>

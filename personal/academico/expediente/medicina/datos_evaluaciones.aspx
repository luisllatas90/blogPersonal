<%@ Page Language="VB" AutoEventWireup="false" CodeFile="datos_evaluaciones.aspx.vb" Inherits="medicina_datos_evaluaciones" %>

<html>
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilonotas.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td style="width: 130px; height: 24px; font-size: 11px; color: black; font-family: verdana;">
                    Ciclos Matriculados</td>
                <td>
                    <asp:DropDownList ID="DDLCiclos" runat="server" AutoPostBack="True" style="font-size: 11px; color: #000066; font-family: verdana">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 130px; font-size: 11px; color: black; font-family: verdana; height: 24px;">
                    Cursos Matriculados</td>
                <td>
                    <asp:DropDownList ID="DDLCursos" runat="server" AutoPostBack="True" style="font-size: 11px; color: #000066; font-family: verdana">
                    </asp:DropDownList></td>
            </tr>
        </table>
    
    </div><table width="100%">
        <tr>
            <td align="center" colspan="2" rowspan="3">
                    <asp:Table ID="TblNotas" runat="server" CellPadding="0" CellSpacing="0" BorderStyle="Solid" BorderWidth="1px">
                        <asp:TableRow runat="server" Height="20px">
                            <asp:TableCell runat="server" CssClass="columnaizquierda" HorizontalAlign="Center"
                                Width="500px">Examen</asp:TableCell>
                            <asp:TableCell runat="server" CssClass="columnaderecha" HorizontalAlign="Center"
                                Width="50px">Nota</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    &nbsp;</td>
        </tr>
        <tr>
            </tr>
            <tr>
            </tr>
        </table>
    </form>
</body>
</html>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="datos_asistencias.aspx.vb" Inherits="medicina_datos_asistencias" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilonotas.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td style="font-size: 11px; width: 130px; color: black; font-family: verdana; height: 24px">
                    Ciclos Matriculados</td>
                <td>
                    <asp:DropDownList ID="DDLCiclos" runat="server" AutoPostBack="True" Style="font-size: 11px;
                        color: #000066; font-family: verdana">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="font-size: 11px; width: 130px; color: black; font-family: verdana; height: 24px">
                    Cursos Matriculados</td>
                <td>
                    <asp:DropDownList ID="DDLCursos" runat="server" AutoPostBack="True" Style="font-size: 11px;
                        color: #000066; font-family: verdana">
                    </asp:DropDownList></td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td align="center" colspan="2" rowspan="1" style="height: 21px">
                    <asp:Panel ID="Panel1" runat="server" Height="10px" HorizontalAlign="Right" Style="font-size: 11px;
                        color: black; font-family: verdana" Width="950px">
                        &nbsp;<asp:Label ID="Label1" runat="server" Text="Asistencias"></asp:Label>
                    :
                    <asp:Label ID="LblAsistencia" runat="server" Font-Bold="True"></asp:Label>
                    &nbsp; &nbsp; &nbsp;
                    <asp:Label ID="Label2" runat="server" Text="Tardanzas"></asp:Label>
                    :
                    <asp:Label ID="LblTardanzas" runat="server" Font-Bold="True"></asp:Label>
                    &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="Label3" runat="server" Text="Inasistencias"></asp:Label>
                    :
                    <asp:Label ID="LblFaltas" runat="server" Font-Bold="True"></asp:Label></asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" rowspan="1" style="height: 21px">
                    <asp:Table ID="TblNotas" runat="server" CellPadding="0" CellSpacing="0" BorderStyle="Solid" BorderWidth="1px" Width="950px">
                        <asp:TableRow runat="server" Height="18px">
                            <asp:TableCell runat="server" CssClass="columnatitulo">Actividad</asp:TableCell>
                            <asp:TableCell runat="server" CssClass="columnatitulo">Hora de Inicio</asp:TableCell>
                            <asp:TableCell runat="server" CssClass="columnatitulo">Hora Ingreso Alum</asp:TableCell>
                            <asp:TableCell runat="server" CssClass="columnatitulo">Condicion</asp:TableCell>
                            <asp:TableCell runat="server" CssClass="columnatitulo">Observaciones</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

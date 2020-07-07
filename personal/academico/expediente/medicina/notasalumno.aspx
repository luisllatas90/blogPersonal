<%@ Page Language="VB" AutoEventWireup="false" CodeFile="notasalumno.aspx.vb" Inherits="medicina_notasalumno" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Detalle de Notas de Alumno</title>
      <link href="../private/estilonotas.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" type="text/css" href="../../../../private/estiloimpresion.css" media="print"/>
      <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin:0,0,0,0">
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td colspan="3" style="border-top: black 1px solid; font-weight: bold; font-size: 11pt;
                    color: white; border-bottom: black 1px solid; font-family: verdana; height: 21px;
                    background-color: firebrick; text-align: center">
                    Detalle de Notas de Alumno</td>
            </tr>
            <tr>
                <td class="NoImprimir">
                    &nbsp;
                    <asp:HyperLink ID="LinkRegresar" runat="server" Style="font-size: 8pt; font-family: verdana">«« Regresar</asp:HyperLink></td>
                <td align="right" colspan="2" style="font-size: 8pt; width: 1416px; color: #000000;
                    font-family: verdana">
                </td>
            </tr>
            <tr>
                <td colspan="3" style="border-top: #660000 1px solid; font-weight: bold; font-size: 8pt;
                    text-transform: capitalize; color: #003300; font-family: verdana; font-variant: normal">
                    &nbsp; &nbsp;Profesor :
                    <asp:Label ID="LblProfesor" runat="server" Style="text-transform: uppercase; color: dimgray;
                        font-family: verdana"></asp:Label></td>
            </tr>
            <tr style="font-size: 8pt">
                <td colspan="3" style="font-weight: bold; font-size: 8pt; text-transform: capitalize;
                    color: #003300; border-bottom: #660000 1px solid; font-family: verdana; font-variant: normal">
                    &nbsp; &nbsp;Asignatura :
                    <asp:Label ID="LblAsignatura" runat="server" Style="text-transform: uppercase; color: dimgray;
                        font-family: verdana"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" colspan="2" style="width: 90%">
                    &nbsp;<input id="CmdImprimir" onclick="javascript:print();" class="Imprimir2 NoImprimir" type="button" value="Imprimir" /></td>
                <td align="left">
                    </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <table width="550">
                        <tr>
                            <td align="center" rowspan="3" style="width: 150px">
                                <asp:Image ID="ImgFoto" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                    Width="80px" /></td>
                            <td align="center" colspan="2" rowspan="4" valign="top">
                                <asp:Table ID="TblResumen" runat="server" BorderStyle="Solid" BorderWidth="1px" CellPadding="0"
                        CellSpacing="0">
                                    <asp:TableRow runat="server" HorizontalAlign="Center">
                                        <asp:TableCell runat="server" ColumnSpan="2" CssClass="fila3" HorizontalAlign="Center">Resumen de Notas</asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server" Height="20px">
                                        <asp:TableCell runat="server" CssClass="columnaizquierda" HorizontalAlign="Center"
                                            Width="200px">Examen</asp:TableCell>
                                        <asp:TableCell runat="server" CssClass="columnaderecha" HorizontalAlign="Center"
                                            Width="50px">Nota</asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                            <td align="center" rowspan="1" style="width: 150px">
                                <asp:Label ID="LblCodigo" runat="server" Style="font-weight: bold; font-size: 10pt;
                                    color: navy; font-family: verdana"></asp:Label>
                                <br />
                                <asp:Label ID="LblNombre" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="9pt"
                                    ForeColor="Black" Width="150px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3" rowspan="1" class="usatCeldaHeader">
                                Reporte Generado :
                                <asp:Label ID="LblFecha" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Table ID="TblNotas" runat="server" BorderStyle="Solid" BorderWidth="1px" CellPadding="0"
                        CellSpacing="0">
                        <asp:TableRow runat="server" Height="20px">
                            <asp:TableCell runat="server" CssClass="columnaizquierda" HorizontalAlign="Center"
                                Width="500px">Examen</asp:TableCell>
                            <asp:TableCell runat="server" CssClass="columnaderecha" HorizontalAlign="Center"
                                Width="50px">Nota</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

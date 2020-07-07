<%@ Page Language="VB" AutoEventWireup="false" CodeFile="comentarios_investigacion.aspx.vb" Inherits="Investigador_comentarios_investigacion" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
    <script language="JavaScript" src="../../../../private/funciones.js"></script>
    <STYLE type="text/css">
BODY {
scrollbar-face-color:#AED9F4;
scrollbar-highlight-color:#FFFFFF;
scrollbar-3dlight-color:#FFFFFF;
scrollbar-darkshadow-color:#FFFFFF;
scrollbar-shadow-color:#FFFFFF;
scrollbar-arrow-color:#000000;

scrollbar-track-color:#FFFFFF;
}
a:link {text-decoration: none; color: #00080; }
a:visited {text-decoration: none; color: #000080; }
a:hover {text-decoration: none; black; }
a:hover{color: black; text-decoration: none; }
        .style1
        {
            width: 532px;
        }
    </STYLE>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr>
                <td align="right" colspan="3" style="width: 100%; height: 44px">
                    <asp:Button ID="CmdComentar" runat="server" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" CssClass="nuevocomentario" Text="            Comentar" Width="99px" ToolTip="Agregue un comentario a esta investigación" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:DataList ID="DataList1" runat="server" DataSourceID="Comentarios" Width="100%">
                        <ItemTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td style="font-weight: bold; font-size: 9pt; color: black; font-family: Verdana; width: 90px;">
                                        Autor</td>
                                    <td style="font-size: 8pt; color: black; font-family: Verdana" class="style1">
                                        <asp:Label ID="Datos_PerLabel" runat="server" Text='<%# Eval("Datos_Per") %>'></asp:Label></td>
                                    <td style="font-weight: bold; font-size: 9pt; color: black; font-family: Verdana; text-align: right;">
                                        Fecha:<asp:Label ID="fecha_ComLabel" runat="server" Font-Bold="False" 
                                            Text='<%# Eval("fecha_Com") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; font-size: 9pt; color: black; font-family: Verdana; width: 90px;">
                                        Asunto</td>
                                    <td colspan="2" style="font-size: 8pt; color: black; font-family: Verdana">
                                        <asp:Label ID="asunto_ComLabel" runat="server" Text='<%# Eval("asunto_Com") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; font-size: 9pt; color: black; font-family: Verdana; width: 90px;">
                                        Observacion</td>
                                    <td colspan="2" style="font-size: 8pt; color: black; font-family: Verdana">
                                        <asp:Label ID="observacion_ComLabel" runat="server" Text='<%# Eval("observacion_Com") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="3" 
                                        style="font-weight: bold; font-size: 9pt; color: black; font-family: Verdana">
                                        <hr />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td colspan="3" rowspan="3" style="background-color: #e1f1fb">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:DataList></td>
            </tr>
            </table>
    
    </div>
        <asp:SqlDataSource ID="Comentarios" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
            SelectCommand="ConsultarObservaciones" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter DefaultValue="2" Name="tipo" Type="String" />
                <asp:QueryStringParameter DefaultValue="" Name="param1" QueryStringField="codigo_inv"
                    Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>

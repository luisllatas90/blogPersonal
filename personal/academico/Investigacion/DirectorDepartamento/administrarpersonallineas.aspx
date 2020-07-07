<%@ Page Language="VB" AutoEventWireup="false" CodeFile="administrarpersonallineas.aspx.vb" Inherits="DirectorDepartamento_administrarpersonallineas" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../../../private/funciones.js"></script>
    <script language="JavaScript" src="../../../../private/tooltip.js"></script>
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
</STYLE>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
        <table>
            <tr>
                <td align="center" colspan="3" style="height: 34px; font-weight: bold; font-size: 10.5pt; color: midnightblue;">
                    &nbsp;ASIGNACIÓN DE LÍNEAS DE INVESTIGACIÓN DE PERSONAL</td>
            </tr>
            <tr>
                <td colspan="3">
                    <table class="contornotabla" style="width: 100%">
                        <tr>
                            <td style="height: 36px; font-weight: bold; color: black;">
                                &nbsp; &nbsp; Unidad de Investigacion</td>
                            <td style="height: 36px">
                                <asp:DropDownList ID="DDLUnidad" runat="server" AutoPostBack="True" Width="422px">
                                </asp:DropDownList></td>
                            <td style="height: 36px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 36px; font-weight: bold; color: black;">
                                &nbsp; &nbsp; Area de Investigación</td>
                            <td style="height: 36px">
                                <asp:DropDownList ID="DDLArea" runat="server" AutoPostBack="True" Width="422px">
                                </asp:DropDownList></td>
                            <td style="height: 36px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 36px; font-weight: bold; color: black;">
                                &nbsp; &nbsp; Línea de Investigación</td>
                            <td style="height: 36px">
                                <asp:DropDownList ID="DDLLinea" runat="server" AutoPostBack="True" Width="422px">
                                </asp:DropDownList></td>
                            <td style="height: 36px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-weight: bold; color: black; height: 36px">
                                <asp:Label ID="LblTematica" runat="server" Text="Temática de Investigación" Visible="False"></asp:Label></td>
                            <td style="height: 36px">
                                <asp:DropDownList ID="DDLTematica" runat="server" AutoPostBack="True" Width="422px" Visible="False">
                                </asp:DropDownList></td>
                            <td style="height: 36px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="border-top: black 1px solid" align="center">
                                <table style="width: 100%">
                                    <tr>
                                        <td align="center" colspan="2">
                                            <div style="width: 590px; height: 100%; text-align: justify; border-right: black 1px solid; padding-right: 5px; border-top: black 1px solid; padding-left: 5px; padding-bottom: 5px; border-left: black 1px solid; color: black; padding-top: 5px; border-bottom: black 1px solid; background-color: lemonchiffon;">
                                                &nbsp;<img height="17" src="../../../../images/help.gif" style="cursor: help"
                                                    width="22" />
                                                Seleccione un personal de la lista (puede seleccionar varios en distinta posición
                                                teniendo presionada
                                                la tecla CTRL y luego haciendo CLIC en cada uno de ellos) y luego presione AGREGAR o RETIRAR según la lista en que se encuentre y la acción a
                                                realizar.</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-weight: bold; color: maroon">
                                            Investigaciones NO asignadas<asp:RangeValidator ID="RangeValidator1" runat="server"
                                                ControlToValidate="LstFaltantes" ErrorMessage="Debe seleccionar un personal de la lista para agregar."
                                                MaximumValue="8000" MinimumValue="1" SetFocusOnError="True" Type="Integer" ValidationGroup="agregar">*</asp:RangeValidator></td>
                                        <td align="center" style="font-weight: bold; color: maroon">
                                            Investigaciones ASIGNADAS al personal seleccionado.<asp:RangeValidator ID="RangeValidator2" runat="server"
                                                ControlToValidate="LstActivos" ErrorMessage="Debe seleccionar un personal de la lista para retirar."
                                                MaximumValue="8000" MinimumValue="1" SetFocusOnError="True" Type="Integer" ValidationGroup="retirar">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:ListBox ID="LstFaltantes" runat="server" Height="340px" SelectionMode="Multiple"
                                                Width="290px" ValidationGroup="agregar"></asp:ListBox></td>
                                        <td align="center">
                                            <asp:ListBox ID="LstActivos" runat="server" Height="340px" SelectionMode="Multiple"
                                                Width="290px" ValidationGroup="retirar"></asp:ListBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="CmdAgregar" runat="server" Text="    > Agregar" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="attach_prp" Height="25px" ValidationGroup="agregar" Width="87px" Enabled="False" />
                                            &nbsp;
                                        </td>
                                        <td align="left">
                                            &nbsp; &nbsp;<asp:Button ID="CmdRetirar" runat="server" Text="    < Retirar" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="remove_prp" Height="25px" ValidationGroup="retirar" Width="87px" Enabled="False" /></td>
                                    </tr>
                                </table>
                                <asp:Label ID="Label1" runat="server"></asp:Label></td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
    </center>
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="agregar" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="retirar" />
        <asp:HiddenField ID="HddCodigoCco" runat="server" />
    </form>
</body>
</html>

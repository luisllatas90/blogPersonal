<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Copia de frmactividades.aspx.vb" Inherits="copia_de_medicina_frmactividades" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <script type="text/javascript" src="../../../../private/calendario.js"></script>
    <link   href="../../../../private/estilo.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" language="javascript">
    function numeros()
            {
                var key=window.event.keyCode;//codigo de tecla.
                if (key < 46 || key > 57){//si no es numero 
                window.event.keyCode=0; }//anula la entrada de texto. 
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: black 2px solid;
            padding-right: 5px; border-top: black 2px solid; padding-left: 5px; padding-bottom: 5px;
            margin: 3px; border-left: black 2px solid; padding-top: 5px; border-bottom: black 2px solid;
            background-color: beige" width="100%">
            <tr>
                <td colspan="3" rowspan="3">
                    <table width="100%">
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Label ID="LblTitulos" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#804040"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; width: 164px">
                                Nombre</td>
                            <td>
                                <asp:TextBox ID="TxtNombre" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" TextMode="MultiLine" Width="241px" Height="38px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; width: 164px">
                                Tipo de Actividad</td>
                            <td>
                                <asp:DropDownList ID="DDLTipoActividad" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; width: 164px">
                                Semana de Desarrollo</td>
                            <td>
                                <asp:DropDownList ID="DDLSemana" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; width: 164px">
                                Fecha y Hora Inicio</td>
                            <td>
                                <input id="Button1" class="cunia" onclick="MostrarCalendario('TxtFechaIni')" type="button" />
                                <asp:TextBox ID="TxtFechaIni" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" Width="70px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtFechaIni"
                                    ErrorMessage="Fecha de Inicio requerido" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                <asp:TextBox ID="TxtHoraIni" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" MaxLength="2" Width="25px"></asp:TextBox><strong>
                                    </strong>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TxtHoraIni"
                                    Display="Dynamic" ErrorMessage="Hora debe estar entre 0 - 23" Operator="LessThanEqual"
                                    SetFocusOnError="True" Type="Integer" ValueToCompare="23">*</asp:CompareValidator><b>:</b>
                                <asp:TextBox ID="TxtMinutoIni" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" MaxLength="2" Width="25px"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="TxtMinutoIni"
                                    Display="Dynamic" ErrorMessage="Minuto debe estar entre 0 - 59" Operator="LessThanEqual"
                                    SetFocusOnError="True" Type="Integer" ValueToCompare="59">*</asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToCompare="TxtFechaFin"
                                    ControlToValidate="TxtFechaIni" Display="Dynamic" ErrorMessage="Fecha de Inicio mayor a la fecha de Fin"
                                    Operator="LessThanEqual" SetFocusOnError="True" Type="Date">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; width: 164px">
                                Fecha y Hora Final</td>
                            <td>
                                <input id="Button2" class="cunia" onclick="MostrarCalendario('TxtFechaFin')" type="button" />
                                <asp:TextBox ID="TxtFechaFin" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" Width="70px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtFechaFin"
                                    ErrorMessage="Fecha Final requerida" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                <asp:TextBox ID="TxtHoraFin" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" MaxLength="2" Width="25px"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="TxtHoraFin"
                                    Display="Dynamic" ErrorMessage="Hora debe estar entre 0 - 23" Operator="LessThanEqual"
                                    SetFocusOnError="True" Type="Integer" ValueToCompare="23">*</asp:CompareValidator><b>:</b>
                                <asp:TextBox ID="TxtMinutoFin" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8pt" MaxLength="2" Width="25px"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="TxtMinutoFin"
                                    Display="Dynamic" ErrorMessage="Minuto debe estar entre 0 - 59" Operator="LessThanEqual"
                                    SetFocusOnError="True" Type="Integer" ValueToCompare="59">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; width: 164px">
                                Estado</td>
                            <td>
                                <asp:CheckBox ID="ChkEstado" runat="server" Text="Habilitado" /></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="font-weight: bold; height: 34px;">
                                <asp:Label ID="LblMensaje" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; width: 164px; height: 66px;">
                            </td>
                            <td style="height: 66px">
                                <asp:Button ID="CmdGuardar" runat="server" Text="     Guardar" CssClass="guardar2" Width="70px" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
        </table>
    
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>

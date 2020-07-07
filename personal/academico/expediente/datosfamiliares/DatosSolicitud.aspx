<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DatosSolicitud.aspx.vb" Inherits="_Default" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <script src="../private/calendario.js" language="javascript" type="text/javascript"></script>
 <script src="../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" />
    <title>Datos de solicitud</title>
</head>
<body style="text-align: center">
    <form id="frmsolicitud" runat="server">
    <%  response.write(clsfunciones.cargacalendario) %>
        <table style="width: 100%; height: 396px; text-align: center;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" class="Titulocel" style="width: 90%; height: 20px; text-align: center;">
                                Nueva Solicitud</td>
            </tr>
            <tr>
                <td style="width: 90%; height: 93px" align="center">
                    <table style="text-align: center;" border="0" cellspacing="2" width="85%">
                        <tr>
                            <td colspan="3" style="height: 27px">
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 113px; height: 35px" align="left" valign="top">
                                Tipo de solicitud</td>
                            <td style="width: 2px; height: 21px" valign="top">
                                :</td>
                            <td style="width: 326px; height: 21px; text-align: left;" valign="top">
                                <asp:DropDownList ID="CboTipo" runat="server" Width="385px" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="CboTipo"
                                    ErrorMessage="Seleccione Tipo de solicitud" Operator="GreaterThanEqual" Type="Integer"
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 113px; height: 45px; text-align: left;" align="left" valign="top">
                                Descripción</td>
                            <td style="width: 2px; height: 45px" valign="top">
                                :</td>
                            <td colspan="1" style="height: 45px; width: 326px; text-align: left;" valign="top">
                                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Width="546px" Height="62px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescripcion"
                                    ErrorMessage="Ingrese la descripcion de la solicitud" Font-Bold="False" Font-Names="Verdana" ValidationGroup="Guardar">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr style="color: #2f4f4f">
                            <td style="width: 113px; height: 35px" align="left" valign="top">
                                Prioridad</td>
                            <td style="width: 2px; height: 34px" valign="top">
                                :</td>
                            <td style="width: 326px; height: 34px; text-align: left;" valign="top">
                                <asp:DropDownList ID="CboPrioridad" runat="server" Width="385px">
                                    <asp:ListItem Value="-1">-- Seleccione prioridad --</asp:ListItem>
                                    <asp:ListItem Value="1">Muy Baja</asp:ListItem>
                                    <asp:ListItem Value="2">Baja</asp:ListItem>
                                    <asp:ListItem Value="3">Media</asp:ListItem>
                                    <asp:ListItem Value="4">Alta</asp:ListItem>
                                    <asp:ListItem Value="5">Muy Alta</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="CboPrioridad"
                                    ErrorMessage="Seleccione prioridad" Operator="GreaterThanEqual" Type="Integer"
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 113px; height: 35px" align="left" valign="top">
                                Aplicación</td>
                            <td style="width: 2px; height: 21px" valign="top">
                                :</td>
                            <td style="width: 326px; height: 21px; text-align: left;" valign="top">
                                <asp:DropDownList ID="CboAplicacion" runat="server" Width="385px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="CboAplicacion"
                                    ErrorMessage="Seleccione aplicación" Operator="GreaterThanEqual" Type="Integer"
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 113px; height: 35px" align="left" valign="top">
                                Area</td>
                            <td style="width: 2px; height: 21px" valign="top">
                                :</td>
                            <td style="width: 326px; height: 21px; text-align: left;" valign="top">
                                <asp:DropDownList ID="CboArea" runat="server" AutoPostBack="True" Width="385px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="CboArea"
                                    ErrorMessage="Seleccione área" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 113px; height: 35px" align="left" valign="top">
                                Persona</td>
                            <td style="width: 2px; height: 21px" valign="top">
                                :</td>
                            <td style="width: 326px; height: 21px; text-align: left;" valign="top">
                                <asp:DropDownList ID="CboPersona" runat="server" Width="385px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="CboPersona"
                                    ErrorMessage="Seleccione persona" Operator="GreaterThanEqual" Type="Integer"
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 113px; height: 35px" align="left" valign="top">
                                Estado</td>
                            <td style="width: 2px; height: 21px" valign="top">
                                :</td>
                            <td style="width: 326px; height: 21px; text-align: left;" valign="top">
                                <asp:DropDownList ID="CboEstado" runat="server" Width="120px">
                                </asp:DropDownList>&nbsp;
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 113px; height: 35px" align="left" valign="top">
                                Observación</td>
                            <td style="width: 2px; height: 21px" valign="top">
                                :</td>
                            <td style="width: 326px; text-align: left;" valign="top">
                                <asp:TextBox ID="txtobservacion" runat="server" TextMode="MultiLine" Width="546px" Height="49px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 113px; height: 35px" valign="top">
                                Fecha solicitada</td>
                            <td style="width: 2px; height: 21px" valign="top">
                                :</td>
                            <td style="width: 326px; text-align: left" valign="top">
                                <asp:TextBox ID="txtfecha" runat="server" Height="22px" Width="80px"></asp:TextBox><input id="Button1" type="button"  class="cunia" onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.frmsolicitud.txtfecha,'dd/mm/yyyy')" style="height: 22px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtfecha"
                                    ErrorMessage="Eliga la fecha de solicitud" Font-Names="Verdana" ValidationGroup="Guardar">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 21px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 21px">
                                &nbsp;
                                &nbsp;<asp:Button ID="cmdGuardar" runat="server" Text="Guardar" CssClass="guardar" ValidationGroup="Guardar" Width="98px" />
                                &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
                                &nbsp;&nbsp;<asp:Button ID="CmdLimpiar" runat="server" CssClass="limpiar" Text="Limpiar" Width="93px" /></td>
                        </tr>
                        </table>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="Guardar" />
    </form>
</body>
</html>

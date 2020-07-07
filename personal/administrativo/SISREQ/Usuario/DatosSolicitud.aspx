<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DatosSolicitud.aspx.vb" Inherits="_Default" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <script src="../private/calendario.js" language="javascript" type="text/javascript"></script>
 <script src="../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" />
    <title>Datos de solicitud</title>
    <style type="text/css">
        .style1
        {
            height: 71px;
        }
        .style2
        {
            height: 62px;
        }
    </style>
</head>
<body style="text-align: center">
    <form id="frmsolicitud" runat="server">
    <%  response.write(clsfunciones.cargacalendario) %>
        <table style="width: 100%;  text-align: center;" border="0" 
        cellpadding="0" cellspacing="0">
            <tr><td> &nbsp; </td></tr>
            <tr>
                <td  align="center">
                    <table class="ContornoTabla"  cellpadding="0" cellspacing="0" >
                    <tr>
                            <td align="center" valign="middle" height="30" bgcolor="#365E89" 
                                style="color: #FFFFFF">
                                NUEVO REQUERIMIENTO</td>
                        </tr>
                        <tr>
                            <td  >
                                
                            <table style="background-color: #FFFFFF; width: 570px; height: 449px; font-family: Verdana; font-weight: bold;" border="0" 
                        cellspacing="2" align="right">
                        
                        <tr>
                            <td align="left" valign="top" width="5" rowspan="10">
                                &nbsp;</td>
                            <td align="left" valign="top" width="130">
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td align="left" valign="top" width="130">
                                Tipo de solicitud</td>
                            <td valign="top">
                                <asp:DropDownList ID="CboTipo" runat="server" Width="400px" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="CboTipo"
                                    ErrorMessage="Seleccione Tipo de solicitud" Operator="GreaterThanEqual" Type="Integer"
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                Descripción</td>
                            <td colspan="1" valign="top" class="style1">
                                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" 
                                    Width="400px" Height="62px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescripcion"
                                    ErrorMessage="Ingrese la descripcion de la solicitud" Font-Bold="False" Font-Names="Verdana" ValidationGroup="Guardar">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr style="color: #2f4f4f">
                            <td align="left" valign="top">
                                Prioridad</td>
                            <td valign="top">
                                <asp:DropDownList ID="CboPrioridad" runat="server" Width="400px">
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
                            <td align="left" valign="top">
                                Aplicación</td>
                            <td valign="top">
                                <asp:DropDownList ID="CboAplicacion" runat="server" Width="400px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="CboAplicacion"
                                    ErrorMessage="Seleccione aplicación" Operator="GreaterThanEqual" Type="Integer"
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                Area</td>
                            <td valign="top">
                                <asp:DropDownList ID="CboArea" runat="server" AutoPostBack="True" Width="400px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="CboArea"
                                    ErrorMessage="Seleccione área" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                Persona</td>
                            <td valign="top">
                                <asp:DropDownList ID="CboPersona" runat="server" Width="400px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="CboPersona"
                                    ErrorMessage="Seleccione persona" Operator="GreaterThanEqual" Type="Integer"
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                Estado</td>
                            <td valign="top" align="left">
                                <asp:DropDownList ID="CboEstado" runat="server" Width="120px">
                                </asp:DropDownList>&nbsp;
                                </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                Observación</td>
                            <td valign="top" class="style2">
                                <asp:TextBox ID="txtobservacion" runat="server" TextMode="MultiLine" 
                                    Width="400px" Height="100px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                Fecha solicitada</td>
                            <td valign="top" align="left">
                                <asp:TextBox ID="txtfecha" runat="server" Height="22px" Width="80px"></asp:TextBox><input id="Button1" type="button"  class="cunia" onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.frmsolicitud.txtfecha,'dd/mm/yyyy')" style="height: 22px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtfecha"
                                    ErrorMessage="Eliga la fecha de solicitud" Font-Names="Verdana" ValidationGroup="Guardar">*</asp:RequiredFieldValidator></td>
                        </tr>
                        </table>
                        
                            </td>
                        </tr>
                        <tr>
                            <td align="right" height="1" style="background-color=#004182"  > </td>
                        </tr>
                        <tr>
                            <td align="right" height="40"  >
                                
                                <asp:Button ID="cmdGuardar" runat="server" Text="    Guardar" 
                                    CssClass="guardar" ValidationGroup="Guardar" Width="81px" />
                                &nbsp; <asp:Button ID="CmdLimpiar" runat="server" CssClass="limpiar" Text="Limpiar" Width="93px" />&nbsp;&nbsp;
                        
                            </td>
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

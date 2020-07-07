<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditarSolicitud.aspx.vb" Inherits="Equipo_EditarSolicitud" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Editar Solicitud de Requerimiento</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" />
    <script src="../private/calendario.js" language="javascript" type="text/javascript"></script>
    <script src="../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
</head>
<body>    
    <form id="form1" runat="server">
        <%  response.write(clsfunciones.cargacalendario) %>
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" 
            align="center">
            <tr><td> &nbsp; </td></tr>
            <tr>
                <td >
                    <table class="ContornoTabla"  cellpadding="0" cellspacing="0" align="center" >
                    <tr>
                            <td valign="middle" height="30" bgcolor="#365E89" 
                                style="color: #FFFFFF" align="center">
                                EDITAR REQUERIMIENTO</td>
                        </tr>
                        <tr>
                            <td  >
                                
                            <table style="background-color: #FFFFFF; width: 570px; height: 449px; font-family: Verdana; text-transform: capitalize; font-weight: normal;" border="0" 
                        cellspacing="2" >
                        
                        <tr>
                            <td align="left" valign="top" width="5" rowspan="9">
                                &nbsp;</td>
                            <td align="left" valign="top" width="130">
                                <b></b></td>
                            <td valign="top" >
                                &nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td align="left" valign="top" width="130">
                                <b>Tipo de solicitud</b></td>
                            <td valign="top" align="justify" >
                                &nbsp;<asp:Label ID="LblTipo" runat="server" Font-Bold="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" >
                                <b>Descripción</b></td>
                            <td colspan="1" valign="top" >
                                <b>
                                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" 
                                    Width="400px" Height="62px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescripcion"
                                    ErrorMessage="Ingrese la descripcion de la solicitud" Font-Bold="False" Font-Names="Verdana" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                </b></td>
                        </tr>
                        <tr style="color: #2f4f4f">
                            <td align="left" valign="top">
                                <b>Prioridad</b></td>
                            <td valign="top">
                                <b>
                                <asp:DropDownList ID="CboPrioridad" runat="server" Width="400px">
                                    <asp:ListItem Value="1">Muy Baja</asp:ListItem>
                                    <asp:ListItem Value="2">Baja</asp:ListItem>
                                    <asp:ListItem Value="3">Media</asp:ListItem>
                                    <asp:ListItem Value="4">Alta</asp:ListItem>
                                    <asp:ListItem Value="5">Muy Alta</asp:ListItem>
                                </asp:DropDownList>
                                </b>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="CboPrioridad"
                                    ErrorMessage="Seleccione prioridad" Operator="GreaterThanEqual" Type="Integer"
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <b>Aplicación</b></td>
                            <td valign="top" >
                                <asp:Label ID="LblAplicacion" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <b>área</b></td>
                            <td valign="top">
                                <asp:Label ID="LblArea" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <b>Persona</b></td>
                            <td valign="top">
                                <asp:Label ID="LblPersona" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <b>Observación</b></td>
                            <td valign="top" >
                                <b>
                                <asp:TextBox ID="txtobservacion" runat="server" TextMode="MultiLine" 
                                    Width="400px" Height="49px"></asp:TextBox></b></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <b>Fecha solicitada</b></td>
                            <td valign="top" >
                                <b>
                                <asp:TextBox ID="txtfecha" runat="server" Width="80px" BorderColor="White" 
                                    BorderStyle="None" ReadOnly="True"></asp:TextBox>
                                </b>
                                <input id="Button1" type="button"  
                                    onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtfecha,'dd/mm/yyyy')" 
                                    style="height: 22px; visibility: hidden;" class="cunia" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtfecha"
                                    ErrorMessage="Eliga la fecha de solicitud" Font-Names="Verdana" ValidationGroup="Guardar">*</asp:RequiredFieldValidator></td>
                        </tr>
                        </table>
                        
                            </td>
                        </tr>
                        <tr>
                            <td align="right" height="1" style="background-color:#004182"  > </td>
                        </tr>
                        <tr>
                            <td align="right" height="40"  >
                                
                                <asp:Button ID="cmdGuardar" runat="server" Text="    Guardar" 
                                    CssClass="guardar" ValidationGroup="Guardar" Width="81px" />
                                &nbsp; <asp:Button ID="CmdCancelar" runat="server" CssClass="cancelar" 
                                    Text="Cancelar" Width="93px" />&nbsp;&nbsp;
                        
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

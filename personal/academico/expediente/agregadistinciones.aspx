<%@ Page Language="VB" AutoEventWireup="false" CodeFile="agregadistinciones.aspx.vb" Inherits="agregadistinciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="STYLESHEET" href="private/estilo.css"/>
    <script src="private/calendario.js"></script>
    <title>Hoja de Vida :: Agregar Distinciones.</title>
        
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table style="width: 610px; border-right: darkgray 1px solid; border-top: darkgray 1px solid; border-left: darkgray 1px solid; border-bottom: darkgray 1px solid;" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3" rowspan="3" style="width: 481px">
        <table style="width: 602px">
            <tr>
                <td align="center" colspan="3" style="font-weight: bold; font-size: 11pt; color: white;
                    font-family: Verdana; height: 20px; background-color: #c2a877">
                    Registro de Distinciones y Honores</td>
            </tr>
            <tr>
                <td colspan="3">
                    <table id="tabla" style="width: 592px">
                        <tr>
                            <td style="font-size: 10pt; width: 141px; color: olive; font-family: Verdana">
                                Nombre de Distinción</td>
                            <td>
                                :<asp:TextBox ID="TxtDistincion" runat="server" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy" Width="420px" ToolTip="Ingrese nombre de la distincion que recibió."></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtDistincion"
                                    ErrorMessage="Nombre de Distincion Requerida" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; width: 141px; color: olive; font-family: Verdana">
                                Otorgado por</td>
                            <td>
                                :<asp:TextBox ID="TxtOtorgado" runat="server" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy" Width="420px" ToolTip="Ingrese un nombre de la institucion o persona que hizo entrega de la distincion."></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtOtorgado"
                                    ErrorMessage="Ingrese Institución o persona que hizo entrega la distinción."
                                    SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; width: 141px; color: olive; font-family: Verdana">
                                Ciudad</td>
                            <td style="width: 415px">
                                :<asp:TextBox ID="TxtCiudad" runat="server" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy" Width="166px" ToolTip="Ingrese nombre de la ciudad donde se le hizo entrega de la distincion"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtCiudad"
                                    ErrorMessage="Ingrese ciudad de entrega de distincion" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; width: 141px; color: olive; font-family: Verdana">
                                Tipo de Distinción</td>
                            <td style="width: 415px">
                                :<asp:DropDownList ID="DDLDistinciones" runat="server" Font-Names="Arial" Font-Size="X-Small"
                                    ForeColor="Navy" Width="171px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; width: 141px; color: olive; font-family: Verdana">
                                Fecha de Entrega</td>
                            <td>
                                :<asp:TextBox ID="TxtFecha" runat="server" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Arial" Font-Size="X-Small" Width="68px" ForeColor="Navy"></asp:TextBox><input id="Button2" class="cunia" type="button" onclick="MostrarCalendario('TxtFecha')" />&nbsp;<asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtFecha" ErrorMessage="Fecha de Entrega Requerida">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; width: 141px; color: olive; font-family: Verdana">
                                Breve Descripción de motivo de entrega de la distincion.</td>
                            <td id="mensaje" style="font-size: 10pt; color: olive; font-family: Verdana">
                                :<asp:TextBox ID="TxtDescripcion" runat="server" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Arial" Font-Size="Small" ForeColor="Navy" Height="53px"
                                    MaxLength="400" TextMode="MultiLine" Width="420px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtDescripcion"
                                    ErrorMessage="Ingrese breve descripcion de motivo de entrega de distincion" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                    </table>
                    &nbsp;
                    <asp:Label ID="LblError" runat="server" ForeColor="#C00000"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" colspan="3" style="height: 21px">
                    &nbsp;<asp:Button ID="CmdGuardar" runat="server" CssClass="guardar" Text="Guardar"
                        Width="85px" />
                    <asp:Button ID="CmdCancelar" runat="server" CssClass="salir" OnClientClick="javascript:window.close();return false;"
                        Text="Cancelar" Width="86px" /></td>
            </tr>
        </table>
        </td></tr></table> 
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
    
    </div>
    </form>
</body>
</html>

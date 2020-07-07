<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AgregarPresupuesto.aspx.vb" Inherits="presupuesto_areas_AgregarPresupuesto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <%  response.write(clsfunciones.cargacalendario) %>
    <div>
    
        <br />
        <table align="center" class="ContornoTabla1" width="75%">
            <tr>
                <td bgcolor="#999999" height="1px">
                </td>
            </tr>
            <tr>
                <td class="TituloTabla" height="20" style="height: 25px">
&nbsp;CENTRO DE COSTO:
                    <asp:Label ID="lblCentroCostos" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td bgcolor="#999999" height="1px">
                </td>
            </tr>
            <tr>
                <td>
    
        <table align="center" cellpadding="3" class="contornotabla" width="100%">
            <tr>
                <td width="10px">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    Proceso:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="cboProceso" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="cboProceso" ErrorMessage="Elegir proceso de presupuesto" 
                        ValidationGroup="Aceptar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    Fecha Inicio:
                                <asp:TextBox ID="TxtFechaIni" runat="server" Width="80px"></asp:TextBox>
                              <input id="Button2" type="button"  class="cunia" 
                                    onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechaIni,'dd/mm/yyyy')" 
                                    style="height: 22px" /><asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtFechaIni" 
                        ErrorMessage="Ingresar fecha de inicio" ValidationGroup="Aceptar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    Fecha Fin:&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="TxtFechaFin" runat="server" Width="80px"></asp:TextBox>
                              <input id="Button3" type="button"  class="cunia" 
                                    onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechaFin,'dd/mm/yyyy')" 
                                    style="height: 22px" /><asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtFechaFin" 
                        ErrorMessage="Ingresar fecha final" ValidationGroup="Aceptar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    Observación:</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtObservacion" runat="server" Rows="5" TextMode="MultiLine" 
                        Width="85%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;</td>
                <td align="right">
                    <asp:Button ID="cmdAceptar" runat="server" CssClass="guardar" Text="    Grabar" 
                        ValidationGroup="Aceptar" Height="22px" Width="70px" />
&nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="cancelar" Text="   Cancelar" 
                        Width="75px" Height="22px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;</td>
                <td align="right">
                    &nbsp;</td>
            </tr>
        </table>
    
                </td>
            </tr>
        </table>
        <br />
    
    </div>
    <asp:HiddenField ID="hddCecos" runat="server" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Aceptar" />
    </form>
</body>
</html>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="agregaidiomas.aspx.vb" Inherits="agregaidiomas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link rel="STYLESHEET" href="private/estilo.css"/>
    <title>Hoja de Vida : Agregar Idiomas</title>
    <script type="text/jscript">
    function valida(source, arguments)  {
    var Valor = document.form1.TxtOtros.value;
    var Valor2 = parseInt(document.form1.DDLCentro.value);
    if ( (Valor2==1 && Valor=="") || (Valor2>=190 && Valor2<=204 && Valor==""))
        {
        arguments.IsValid = false;
        return; }
    arguments.IsValid=true;}
    
      function mostrarcaja2(){
    if (eval("document.form1.DDLCentro.value==1") || ( eval("document.form1.DDLCentro.value>=190") && eval("document.form1.DDLCentro.value<=204") ))
        eval("document.form1.TxtOtros.disabled=false")
     else
        eval("document.form1.TxtOtros.disabled=true")
    }</script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="border-right: darkgray 1px solid; border-top: darkgray 1px solid;
            border-left: darkgray 1px solid; width: 610px; border-bottom: darkgray 1px solid">
            <tr>
                <td colspan="3" rowspan="3" style="width: 481px">
                    <table style="width: 602px">
                        <tr>
                            <td align="center" colspan="3" style="font-weight: bold; font-size: 11pt; color: white;
                                font-family: Verdana; height: 20px; background-color: #c2a877">
                                Registro de Idiomas</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table id="tabla" style="width: 592px">
                                    <tr>
                                        <td style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana">
                                            Idioma</td>
                                        <td style="font-size: 10pt; color: olive; font-family: Verdana">
                                            :
                                            <asp:DropDownList ID="DDLIdioma" runat="server" Font-Size="X-Small"
                                                ForeColor="Navy" Width="138px">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="DDLIdioma"
                                                ErrorMessage="Seleccione idioma" MaximumValue="8000" MinimumValue="1" SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>Año de graduación:
                                            <asp:DropDownList ID="DDlAno" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="62px">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana">
                                            Tipo Institución</td>
                                        <td>
                                            :
                                            <asp:DropDownList ID="DDLInstitucion" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                                ForeColor="Navy" Width="199px">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="DDLInstitucion"
                                                ErrorMessage="Seleccione tipo de institucion" MaximumValue="8000" MinimumValue="1"
                                                SetFocusOnError="True">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr style="color: #000000">
                                        <td style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana">
                                            Procedencia</td>
                                        <td>
                                            :
                                            <asp:DropDownList ID="DDLProcedencia" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                                ForeColor="Navy" Width="111px">
                                                <asp:ListItem Value="1">Nacional</asp:ListItem>
                                                <asp:ListItem Value="2">Extranjera</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana; height: 17px;">
                                            Centro de Estudios</td>
                                        <td style="height: 17px">
                                            :
                                            <asp:DropDownList ID="DDLCentro" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="422px">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="DDLCentro"
                                                ErrorMessage="Seleccione centro de estudios" MaximumValue="8000" MinimumValue="1"
                                                SetFocusOnError="True" Type="Integer">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana">
                                        </td>
                                        <td id="mensaje" style="font-size: 10pt; color: olive; font-family: Verdana">
                                            Otros Especifique :
                                            <asp:TextBox ID="TxtOtros" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Size="X-Small" ForeColor="Navy" Width="294px"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="valida"
                                                ErrorMessage="Ingrese centro de estudios en otros">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana; height: 21px;">
                                            Situación</td>
                                        <td style="height: 21px">
                                            :
                                            <asp:DropDownList ID="DDLSituacion" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="116px">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="DDLSituacion"
                                                ErrorMessage="Seleccione Situacion de Estudios de Idioma" MaximumValue="8000" MinimumValue="1"
                                                Type="Integer">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana; height: 15px">
                                            Niveles</td>
                                        <td style="height: 15px">
                                            Lee :
                                            <asp:DropDownList ID="DDLLee" runat="server" Font-Size="X-Small" ForeColor="Navy">
                                                <asp:ListItem Value="0">Bajo</asp:ListItem>
                                                <asp:ListItem Value="1">Medio</asp:ListItem>
                                                <asp:ListItem Value="2">Alto</asp:ListItem>
                                            </asp:DropDownList>
                                            Escribe
                                            <asp:DropDownList ID="DDLEscribe" runat="server" Font-Size="X-Small" ForeColor="Navy">
                                                <asp:ListItem Value="0">Bajo</asp:ListItem>
                                                <asp:ListItem Value="1">Medio</asp:ListItem>
                                                <asp:ListItem Value="2">Alto</asp:ListItem>
                                            </asp:DropDownList>
                                            Habla :
                                            <asp:DropDownList ID="DDLHabla" runat="server" Font-Size="X-Small" ForeColor="Navy">
                                                <asp:ListItem Value="0">Bajo</asp:ListItem>
                                                <asp:ListItem Value="1">Medio</asp:ListItem>
                                                <asp:ListItem Value="2">Alto</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana; height: 42px">
                                            Observaciones</td>
                                        <td style="height: 42px">
                                            :
                                            <asp:TextBox ID="TXtObservaciones" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Size="X-Small" ForeColor="Navy" Height="32px" TextMode="MultiLine"
                                                Width="421px"></asp:TextBox></td>
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
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
    
    </div>
    </form>
</body>
</html>

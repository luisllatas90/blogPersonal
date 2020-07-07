<%@ Page Language="VB" AutoEventWireup="false" CodeFile="agregatitulo.aspx.vb" Inherits="agregatitulo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css" />
<link rel="STYLESHEET" href="private/estilo.css"/>

    <title>Hoja de Vida : Agregar Titulos</title>
    <script type="text/javascript">
    function titulo(source, arguments)  {
    var Valor = document.form1.TxtOtrosTitulo.value;
    var Valor2 = parseInt(document.form1.DDLTitulo.value);
    if (Valor2==65 && Valor=="")    {
        arguments.IsValid = false;
        return; }
    arguments.IsValid=true;}
    
    function estudios(source, arguments){
    var valor2 = parseInt(document.form1.DDLCentro.value);
    var valor = document.form1.TxtOtros.value;
    if ( (valor2==1 && valor=="") || (valor2>=190 && valor2<=204 && valor=="") )
        {
        arguments.IsValid = false;
        return; }
    arguments.IsValid=true;}
    
    function mostrarcaja(){
    if (eval("document.form1.DDLTitulo.value==65"))
        eval("document.form1.TxtOtrosTitulo.disabled=false")
     else
        eval("document.form1.TxtOtrosTitulo.disabled=true")
    }
    
      function mostrarcaja2(){
    if (eval("document.form1.DDLCentro.value==1") || ( eval("document.form1.DDLCentro.value>=190") && eval("document.form1.DDLCentro.value<=204") ))
        eval("document.form1.TxtOtros.disabled=false")
     else
        eval("document.form1.TxtOtros.disabled=true")
    }
    </script>

</head>
<body>
<div id="divmensaje"></div>
<script type="text/javascript" src="div.js"></script>
    <form id="form1" runat="server">
    <div>
        <table style="width: 610px; border-right: darkgray 1px solid; border-top: darkgray 1px solid; border-left: darkgray 1px solid; border-bottom: darkgray 1px solid;" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3" rowspan="3" style="width: 481px">
                    <table style="width: 602px">
                        <tr>
                            <td align="center" colspan="3" style="font-weight: bold; font-size: 11pt; color: white;
                                font-family: Verdana; background-color: #c2a877; height: 20px;">
                    Registro de Títulos Profesionales</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table style="width: 592px" id="tabla">
                                    <tr>
                                        <td style="font-size: 10pt; width: 127px; color: olive; font-family: Verdana">
                                            Título Profesional</td>
                                        <td>
                                            :
                                            <asp:DropDownList ID="DDLTitulo" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="411px">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="DDLTitulo"
                                                ErrorMessage="Seleccion un Titulo Profesional" MaximumValue="80000" MinimumValue="1"
                                                SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>
                                            <asp:Image ID="Img" runat="server" ImageUrl="~/images/menus/prioridad_.gif" ToolTip="Si el titulo no se encuentra en la lista anterior, seleccione OTRO y escriba el nombre en la caja de texto" style="cursor: hand" /></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 127px; color: olive; font-family: Verdana">
                                        </td>
                                        <td style="font-size: 10pt; color: olive; font-family: Verdana">
                                            Otros Especifique :
                                            <asp:TextBox ID="TxtOtrosTitulo" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Height="13px" Width="304px" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Navy"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="titulo"
                                                ErrorMessage="Ingrese un Titulo en Otros">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 127px; color: olive; font-family: Verdana">
                                            Año</td>
                                        <td style="font-size: 10pt; color: olive; font-family: Verdana">
                                            : Ingreso
                                            <asp:DropDownList ID="DDlAIngreso" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="58px">
                                            </asp:DropDownList>
                                            Egreso &nbsp;<asp:DropDownList ID="DDLAEgreso" runat="server" Font-Size="X-Small"
                                                ForeColor="Navy" Width="63px">
                                            </asp:DropDownList>
                                            Titulación &nbsp;<asp:DropDownList ID="DDLATitulo" runat="server" Font-Size="X-Small"
                                                ForeColor="Navy" Width="64px">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="DDLAEgreso"
                                                ControlToValidate="DDlAIngreso" ErrorMessage="Año de ingreso debe ser menor que de el egreso"
                                                Operator="LessThanEqual" SetFocusOnError="True" Type="Integer">*</asp:CompareValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 127px; color: olive; font-family: Verdana">
                                            Tipo Institución</td>
                                        <td>
                                            :
                                            <asp:DropDownList ID="DDLInstitucion" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="199px" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="DDLInstitucion"
                                                ErrorMessage="Seleccione tipo de institucion" MaximumValue="8000" MinimumValue="1"
                                                SetFocusOnError="True">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 127px; color: olive; font-family: Verdana">
                                            Procedencia</td>
                                        <td>
                                            :
                                            <asp:DropDownList ID="DDLProcedencia" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="111px" AutoPostBack="True">
                                                <asp:ListItem Value="1">Nacional</asp:ListItem>
                                                <asp:ListItem Value="2">Extranjera</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 127px; color: olive; font-family: Verdana">
                                            Centro de Estudios</td>
                                        <td>
                                            :
                                            <asp:DropDownList ID="DDLCentro" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="432px">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="DDLCentro"
                                                ErrorMessage="Seleccione centro de estudios" MaximumValue="8000" MinimumValue="1"
                                                SetFocusOnError="True" Type="Integer">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 127px; color: olive; font-family: Verdana">
                                        </td>
                                        <td id="mensaje" style="font-size: 10pt; color: olive; font-family: Verdana">
                                            Otros Especifique :
                                            <asp:TextBox ID="TxtOtros" runat="server" Font-Size="X-Small" ForeColor="Navy" Width="304px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="estudios"
                                                ErrorMessage="Ingrese Centro de Estudios en otros">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 127px; color: olive; font-family: Verdana">
                                            Situación</td>
                                        <td>
                                            :
                                            <asp:DropDownList ID="DDLSituacion" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="116px">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="DDLSituacion"
                                                ErrorMessage="Seleccione Situacion de Titulo" MaximumValue="8000" MinimumValue="1"
                                                Type="Integer">*</asp:RangeValidator></td>
                                    </tr>
                                </table>
                                &nbsp;
                                <asp:Label ID="LblError" runat="server" ForeColor="#C00000"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 21px" align="right">
                                &nbsp;<asp:Button ID="CmdGuardar" runat="server" Text="Guardar" CssClass="guardar" Width="85px" />
                                <asp:Button ID="CmdCancelar" runat="server" Text="Cancelar" CssClass="salir" Width="86px" OnClientClick="javascript:window.close();return false;" /></td>
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

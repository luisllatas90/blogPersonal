<%@ Page Language="VB" AutoEventWireup="false" CodeFile="agregagrado.aspx.vb" Inherits="agregagrado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Hoja de Vida : Agregar Grados Académicos</title>
        <link rel="STYLESHEET" href="private/estilo.css"/>
<script type="text/javascript">
function valida(source, arguments)  {
    var Valor = document.form1.TxtOtrosGrados.value;
    var Valor2 = parseInt(document.form1.DDLGrado.value);
    if ((Valor2==26 || Valor2 == 27 || Valor2 == 28) && Valor=="")    {
        arguments.IsValid = false;
        return; }
    arguments.IsValid=true;}
    
   function mostrarcaja(){
    if (eval("document.form1.DDLGrado.value==26") || eval("document.form1.DDLGrado.value==27") || eval("document.form1.DDLGrado.value==28"))
        eval("document.form1.TxtOtrosGrados.disabled=false")
     else
        eval("document.form1.TxtOtrosGrados.disabled=true")
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
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="border-right: darkgray 1px solid; border-top: darkgray 1px solid;
            border-left: darkgray 1px solid; width: 610px; border-bottom: darkgray 1px solid; height: 312px;">
            <tr>
                <td colspan="3" rowspan="3" style="width: 481px">
                    <table style="width: 602px">
                        <tr>
                            <td align="center" colspan="3" style="font-weight: bold; font-size: 11pt; color: white;
                                font-family: Verdana; height: 20px; background-color: #c2a877">
                                Registro de Grados Académicos</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table id="tabla" style="width: 592px">
                                    <tr>
                                        <td style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana">
                                            Tipo de Estudios</td>
                                        <td>
                                            :
                                            <asp:DropDownList ID="DDLTipoGrado" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="97px" AutoPostBack="True">
                                                <asp:ListItem Value="3">Bachillerato</asp:ListItem>
                                                <asp:ListItem Value="4">Maestria</asp:ListItem>
                                                <asp:ListItem Value="5">Doctorado</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana">
                                            Grado Académico</td>
                                        <td>
                                            :
                                            <asp:DropDownList ID="DDLGrado" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="425px">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="DDLGrado"
                                                ErrorMessage="Seleccione Grado Academico" MaximumValue="8000" MinimumValue="1"
                                                SetFocusOnError="True" Type="Integer">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana">
                                        </td>
                                        <td style="font-size: 10pt; color: olive; font-family: Verdana">
                                            Otros Especifique:
                                            <asp:TextBox ID="TxtOtrosGrados" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy" Width="302px"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="valida"
                                                ErrorMessage="Ingrese Grado Academico en otros">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana">
                                            Mención</td>
                                        <td>
                                            :
                                            <asp:TextBox ID="TxtMención" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                Width="420px" BorderColor="Black" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana">
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
                                        <td style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana">
                                            Centro de Estudios</td>
                                        <td>
                                            :
                                            <asp:DropDownList ID="DDLCentro" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="428px">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="DDLCentro"
                                                ErrorMessage="Seleccione centro de estudios" MaximumValue="8000" MinimumValue="1"
                                                SetFocusOnError="True" Type="Integer">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana; height: 7px;">
                                        </td>
                                        <td id="mensaje" style="font-size: 10pt; color: olive; font-family: Verdana; height: 7px;">
                                            Otros Especifique :
                                            <asp:TextBox ID="TxtOtros" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Size="X-Small" ForeColor="Navy" Width="300px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana">
                                            Situación</td>
                                        <td>
                                            :
                                            <asp:DropDownList ID="DDLSituacion" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="116px">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="DDLSituacion"
                                                ErrorMessage="Seleccione Situacion de grado academico" MaximumValue="8000" MinimumValue="1"
                                                Type="Integer">*</asp:RangeValidator></td>
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
    
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="agregaotros.aspx.vb" Inherits="agregaotros" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link rel="STYLESHEET" href="private/estilo.css"/>
    <title>Hoja de Vida : Agregar Otros Cursos</title>
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
                                Registro de Otros Estudios</td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 239px">
                                <table id="tabla" style="width: 592px">
                                    <tr>
                                        <td style="font-size: 10pt; width: 128px; color: olive; font-family: Verdana">
                                            Area de Estudio</td>
                                        <td style="font-size: 10pt; color: olive; font-family: Verdana">
                                            :
                                            <asp:DropDownList ID="DDLArea" runat="server" Font-Size="X-Small"
                                                ForeColor="Navy" Width="228px">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="DDLArea"
                                                ErrorMessage="Seleccione area de estudio" MaximumValue="8000" MinimumValue="1" SetFocusOnError="True">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 128px; color: olive; font-family: Verdana">
                                            Nombre de Estudio</td>
                                        <td>
                                            :
                                            <asp:TextBox ID="TxtEstudio" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Size="X-Small" ForeColor="Navy" Width="425px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtEstudio"
                                                ErrorMessage="Ingrese Nombre de Estudio" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 128px; color: olive; font-family: Verdana">
                                            Tipo Institucion</td>
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
                                        <td style="font-size: 10pt; width: 128px; color: olive; font-family: Verdana; height: 2px;">
                                            Procedencia</td>
                                        <td style="height: 2px">
                                            :
                                            <asp:DropDownList ID="DDLProcedencia" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                                ForeColor="Navy" Width="111px">
                                                <asp:ListItem Value="1">Nacional</asp:ListItem>
                                                <asp:ListItem Value="2">Extranjera</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 128px; color: olive; font-family: Verdana; height: 2px;">
                                            Centro de Estudios</td>
                                        <td style="height: 2px">
                                            :
                                            <asp:DropDownList ID="DDLCentro" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="431px">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="DDLCentro"
                                                ErrorMessage="Seleccione centro de estudios" MaximumValue="8000" MinimumValue="1"
                                                SetFocusOnError="True" Type="Integer">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 128px; color: olive; font-family: Verdana">
                                        </td>
                                        <td id="mensaje" style="font-size: 10pt; color: olive; font-family: Verdana">
                                            Otros Especifique :
                                            <asp:TextBox ID="TxtOtros" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Size="X-Small" ForeColor="Navy" Width="303px"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="valida"
                                                ErrorMessage="Ingrese centro de estudios en otros">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 128px; color: olive; font-family: Verdana; height: 1px">
                                            Fecha de Estudios</td>
                                        <td style="height: 1px">
                                            Inicio Mes
                                            <asp:DropDownList ID="DDLMesIni" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="68px">
                                                <asp:ListItem Value="Enero">Enero</asp:ListItem>
                                                <asp:ListItem Value="Febrero">Febrero</asp:ListItem>
                                                <asp:ListItem Value="Marzo">Marzo</asp:ListItem>
                                                <asp:ListItem Value="Abril">Abril</asp:ListItem>
                                                <asp:ListItem Value="Mayo">Mayo</asp:ListItem>
                                                <asp:ListItem Value="Junio">Junio</asp:ListItem>
                                                <asp:ListItem Value="Julio">Julio</asp:ListItem>
                                                <asp:ListItem Value="Agosto">Agosto</asp:ListItem>
                                                <asp:ListItem Value="Setiembre">Setiembre</asp:ListItem>
                                                <asp:ListItem Value="Octubre">Octubre</asp:ListItem>
                                                <asp:ListItem Value="Noviembre">Noviembre</asp:ListItem>
                                                <asp:ListItem Value="Diciembre">Diciembre</asp:ListItem>
                                            </asp:DropDownList>
                                            Año&nbsp;<asp:DropDownList ID="DDLAnioIni" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="65px">
                                            </asp:DropDownList>
                                            Fin Mes&nbsp;<asp:DropDownList ID="DDLMesFin" runat="server" Font-Size="X-Small"
                                                ForeColor="Navy" Width="68px">
                                                <asp:ListItem Value="En curso">En curso</asp:ListItem>
                                                <asp:ListItem Value="Enero">Enero</asp:ListItem>
                                                <asp:ListItem Value="Febrero">Febrero</asp:ListItem>
                                                <asp:ListItem Value="Marzo">Marzo</asp:ListItem>
                                                <asp:ListItem Value="Abril">Abril</asp:ListItem>
                                                <asp:ListItem Value="Mayo">Mayo</asp:ListItem>
                                                <asp:ListItem Value="Junio">Junio</asp:ListItem>
                                                <asp:ListItem Value="Julio">Julio</asp:ListItem>
                                                <asp:ListItem Value="Agosto">Agosto</asp:ListItem>
                                                <asp:ListItem Value="Setiembre">Setiembre</asp:ListItem>
                                                <asp:ListItem Value="Octubre">Octubre</asp:ListItem>
                                                <asp:ListItem Value="Noviembre">Noviembre</asp:ListItem>
                                                <asp:ListItem Value="Diciembre">Diciembre</asp:ListItem>
                                            </asp:DropDownList>
                                            Año<asp:DropDownList ID="DDLAnioFin" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="70px">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 128px; color: olive; font-family: Verdana; height: 6px">
                                            Tipo estudio</td>
                                        <td style="height: 6px">
                                            :
                                            <asp:DropDownList ID="DDLModalidad" runat="server" Font-Size="X-Small" ForeColor="Navy">
                                                <asp:ListItem Value="1">Mensual</asp:ListItem>
                                                <asp:ListItem Value="2">Semestral</asp:ListItem>
                                                <asp:ListItem Value="3">Anual</asp:ListItem>
                                            </asp:DropDownList>
                                            Mes/Semestre/Año que cursa actualmente :
                                            <asp:DropDownList ID="DDLCursa" runat="server" Font-Size="X-Small" ForeColor="Navy" Width="55px">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem Value="999">Culminado</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 128px; color: olive; font-family: Verdana;">
                                            Situacion</td>
                                        <td>
                                            :
                                            <asp:DropDownList ID="DDLSituacion" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="116px">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="DDLSituacion"
                                                ErrorMessage="Seleccione Situacion de estudio realizado" MaximumValue="8000" MinimumValue="1"
                                                Type="Integer">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 128px; color: olive; font-family: Verdana; height: 8px">
                                            Observaciones</td>
                                        <td style="height: 8px">
                                            :
                                            <asp:TextBox ID="TXtObservaciones" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Size="X-Small" ForeColor="Navy" Height="25px" TextMode="MultiLine"
                                                Width="428px"></asp:TextBox></td>
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

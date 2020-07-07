<%@ Page Language="VB" AutoEventWireup="false" CodeFile="agregaexperiencia.aspx.vb" Inherits="agregaexperiencia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    
<script type="text/javascript">
    function activar()
    {
    var Valor = parseInt(document.form1.DDLMesFin.value);
    var Valor2 = parseInt(document.form1.DDLAnioFin.value);
    if ((Valor2!=0 && Valor==0) || (Valor2==0 && Valor!=0) ||(Valor2==0 && Valor==0))
         {
         document.form1.DDLCese.options[0].selected = true;   
         document.form1.DDLCese.disabled = true;
         }
    else
         document.form1.DDLCese.disabled = false;
         
    
    }
    
</script>

<script type ="text/javascript">
  function valida(source, arguments)  {
    var Valor = parseInt(document.form1.DDLMesFin.value);
    var Valor2 = parseInt(document.form1.DDLAnioFin.value);
    if ((Valor2!=0 && Valor==0) || (Valor2==0 && Valor!=0))
        {
        arguments.IsValid = false; return false; }
     else  { arguments.IsValid=true; }
    }
    
 function valida2(source,arguments)
    {
    if (document.form1.DDLCese.disabled==false && document.form1.DDLCese.value=='Laborando')
        {
        arguments.IsValid = false;
        return false;
        }
     else
        arguments.IsValid = true;
       
    }
</script>

    <link rel="STYLESHEET" href="private/estilo.css"/>
    <title>Hoja de Vida :: Experiencia Laboral</title>
      
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
                                Registro de Experiencia Laboral</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table id="tabla" style="width: 592px">
                                    <tr>
                                        <td style="font-size: 10pt; width: 137px; color: olive; font-family: Verdana;">
                                            Institución/Empresa</td>
                                        <td>
                                            :<asp:TextBox ID="TxtEmpresa" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy" Width="415px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtEmpresa"
                                                ErrorMessage="Ingrese institucion o empresa que laboró" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 137px; color: olive; font-family: Verdana">
                                            Ciudad</td>
                                        <td style="width: 415px">
                                            :<asp:TextBox ID="TxtCiudad" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy" Width="166px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtCiudad"
                                                ErrorMessage="Ingrese ciudad que laboró" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 137px; color: olive; font-family: Verdana">
                                            Tipo de Contrato</td>
                                        <td>
                                            :<asp:DropDownList ID="DDLContrato" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="174px">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="DDLContrato"
                                                ErrorMessage="Seleccione tipo de contrato" Operator="NotEqual" SetFocusOnError="True"
                                                ValueToCompare="0" Width="7px">*</asp:CompareValidator>
                                            Cargo: &nbsp;<asp:DropDownList ID="DDLCargo" runat="server" Font-Size="X-Small"
                                                ForeColor="Navy" Width="184px">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="DDLCargo"
                                                ErrorMessage="Seleccione cargo" Operator="NotEqual" SetFocusOnError="True" ValueToCompare="0">*</asp:CompareValidator></td>
                                    </tr>
                                    <tr style="color: #808000">
                                        <td style="font-size: 10pt; width: 137px; color: olive; font-family: Verdana">
                                            Función Desempeño</td>
                                        <td style="font-size: 10pt; color: olive; font-family: Verdana">
                                            :<asp:TextBox ID="TxtFuncion" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy" Width="414px"></asp:TextBox></td>
                                    </tr>
                                    <tr style="color: #808000">
                                        <td style="font-size: 10pt; width: 137px; color: olive; font-family: Verdana">
                                            Fechas</td>
                                        <td style="font-size: 10pt; color: olive; font-family: Verdana; width: 415px;">
                                            :Inicio
                                            <asp:DropDownList ID="DDLMesIni" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="68px">
                                                <asp:ListItem Value="1">Enero</asp:ListItem>
                                                <asp:ListItem Value="2">Febrero</asp:ListItem>
                                                <asp:ListItem Value="3">Marzo</asp:ListItem>
                                                <asp:ListItem Value="4">Abril</asp:ListItem>
                                                <asp:ListItem Value="5">Mayo</asp:ListItem>
                                                <asp:ListItem Value="6">Junio</asp:ListItem>
                                                <asp:ListItem Value="7">Julio</asp:ListItem>
                                                <asp:ListItem Value="8">Agosto</asp:ListItem>
                                                <asp:ListItem Value="9">Setiembre</asp:ListItem>
                                                <asp:ListItem Value="10">Octubre</asp:ListItem>
                                                <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                                <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                            </asp:DropDownList><asp:DropDownList ID="DDLAnioIni" runat="server" Font-Size="X-Small"
                                                ForeColor="Navy" Width="65px">
                                            </asp:DropDownList>
                                            Fin
                                            <asp:DropDownList ID="DDLMesFin" runat="server" Font-Size="X-Small" ForeColor="Navy"
                                                Width="68px">
                                                <asp:ListItem Value="0">En curso</asp:ListItem>
                                                <asp:ListItem Value="1">Enero</asp:ListItem>
                                                <asp:ListItem Value="2">Febrero</asp:ListItem>
                                                <asp:ListItem Value="3">Marzo</asp:ListItem>
                                                <asp:ListItem Value="4">Abril</asp:ListItem>
                                                <asp:ListItem Value="5">Mayo</asp:ListItem>
                                                <asp:ListItem Value="6">Junio</asp:ListItem>
                                                <asp:ListItem Value="7">Julio</asp:ListItem>
                                                <asp:ListItem Value="8">Agosto</asp:ListItem>
                                                <asp:ListItem Value="9">Setiembre</asp:ListItem>
                                                <asp:ListItem Value="10">Octubre</asp:ListItem>
                                                <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                                <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                            </asp:DropDownList><asp:DropDownList ID="DDLAnioFin" runat="server" Font-Size="X-Small"
                                                ForeColor="Navy" Width="68px">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="valida"
                                                ErrorMessage="Ambas fecha de fin deben estar en curso o seleccione una fecha correcta">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 137px; color: olive; font-family: Verdana">
                                            Motivo de Cese</td>
                                        <td style="width: 415px">
                                            :
                                            <asp:DropDownList ID="DDLCese" runat="server" Font-Size="X-Small"
                                                ForeColor="Navy" Width="170px" Enabled="False">
                                                <asp:ListItem Value="Laborando">-- Seleccione Motivo Cese --</asp:ListItem>
                                                <asp:ListItem>Termino Contrato</asp:ListItem>
                                                <asp:ListItem>Termino de Proyecto</asp:ListItem>
                                                <asp:ListItem>Renuncia</asp:ListItem>
                                                <asp:ListItem>Despido</asp:ListItem>
                                                <asp:ListItem>Cambio de Puesto</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="valida2"
                                                ErrorMessage="Seleccione motivo de cese">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10pt; width: 137px; color: olive; font-family: Verdana">
                                            Breve Descripción del cargo desempeñado</td>
                                        <td id="mensaje" style="font-size: 10pt; color: olive; font-family: Verdana;">
                                            :<asp:TextBox ID="TxtDescripcion" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Size="Small" ForeColor="Navy" Width="418px" Height="53px" TextMode="MultiLine" Font-Names="Arial" MaxLength="400"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtDescripcion"
                                                ErrorMessage="Ingrese breve descripcion de labor desempeñada" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
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

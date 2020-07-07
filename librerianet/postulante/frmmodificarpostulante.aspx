<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmmodificarpostulante.aspx.vb" Inherits="frmmodificarpostulante" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Personas interesadas en Estudiar en la USAT</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../private/funciones.js"></script>
    <script language="javascript" type="text/javascript">
    function ValidarEmail(source, arguments){
      var campo=document.getElementById("txtemail_Pot").value
      var ubicacion
      var enter = "\n"
      var caracteres = "abcdefghijklmnopqrstuvwxyzn1234567890@_-." + String.fromCharCode(13) + enter
      var contador = 0
 
        for (var i=0; i < campo.length; i++) {
          ubicacion = campo.substring(i, i + 1)
            if (caracteres.indexOf(ubicacion) != -1) {
            contador++
            } else {
            //alert("ERROR: No se acepta el caracter '" + ubicacion + "'.")
            arguments.IsValid=false;
            return;
            }
        }
        //OK
        arguments.IsValid=true;
    }
    function MostrarPopUp(codigo_pot) 
    { 
    var modal = $find('mpeFicha'); 
    modal.show(); 
    //WebService.CargarDatos(codigo_pot); 
    }
</script>
<style type="text/css">
    /* .... */
    .FondoAplicacion
    {
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
</style>

</head>
<body>
    <form id="form1" runat="server">
	<table style="width:90%; background-color:White;" class="contornotabla_azul" cellpadding="3" cellspacing="0">
                <tr>
                    <td style="width: 20%">
                        &nbsp;</td>
                    <td style="width: 80%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="usatTitulo" colspan="2" 
                    
                    style="border-width: 1px; border-color: #C0C0C0; border-bottom-style: solid;">
                        &nbsp; Registro de datos</td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Apellido Paterno</td>
                    <td style="width: 80%">
                        <asp:TextBox ID="txtapellidoPatRazSoc_Pot" runat="server" CssClass="Cajas" 
                            Width="50%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RqPaterno" runat="server" 
                    ControlToValidate="txtapellidoPatRazSoc_Pot" 
                    ErrorMessage="Debe ingresar el Apellido Paterno">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Apellido Materno</td>
                    <td style="width: 80%">
                        <asp:TextBox ID="txtapellidoMat_Pot" runat="server" CssClass="Cajas" 
                            Width="50%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RqMaterno" runat="server" 
                    ControlToValidate="txtapellidoMat_Pot" 
                    ErrorMessage="Debe ingresar el Apellido Materno">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Nombres</td>
                    <td style="width: 80%">
                        <asp:TextBox ID="txtnombres_Pot" runat="server" CssClass="Cajas" Width="50%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RqMaterno0" runat="server" 
                        ControlToValidate="txtnombres_Pot" ErrorMessage="Debe ingresar los nombres">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Fecha Nac.</td>
                    <td style="width: 80%">
                        <asp:DropDownList ID="DDlDia" runat="server" ToolTip="Dia de Nacimiento">
                            <asp:ListItem Value="-1">Dia</asp:ListItem>
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
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>17</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                            <asp:ListItem>19</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>21</asp:ListItem>
                            <asp:ListItem>22</asp:ListItem>
                            <asp:ListItem>23</asp:ListItem>
                            <asp:ListItem>24</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>26</asp:ListItem>
                            <asp:ListItem>27</asp:ListItem>
                            <asp:ListItem>28</asp:ListItem>
                            <asp:ListItem>29</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>31</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RangeValidator ID="RngDia" runat="server" ControlToValidate="DDlDia" 
                        ErrorMessage="Seleccione dia de nacimiento" MaximumValue="31" MinimumValue="1" 
                        SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>
                        <asp:DropDownList ID="DDLMes" runat="server" ToolTip="Mes de Nacimiento">
                            <asp:ListItem Value="-1">Mes</asp:ListItem>
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
                        </asp:DropDownList>
                        &nbsp;<asp:RangeValidator ID="RngMes" runat="server" ControlToValidate="DDLMes" 
                        ErrorMessage="Seleccione mes de nacimiento" MaximumValue="12" MinimumValue="1" 
                        SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>
                        <asp:DropDownList ID="DDlAño" runat="server" ToolTip="Año de Nacimiento">
                        </asp:DropDownList>
                        <asp:RangeValidator ID="RngAnio" runat="server" ControlToValidate="DDlAño" 
                        ErrorMessage="Seleccione Año de nacimiento" MaximumValue="2020" 
                        MinimumValue="1915" SetFocusOnError="True" Type="Integer">*</asp:RangeValidator>
                    &nbsp;Sexo<asp:RadioButtonList ID="rdSexo" runat="server" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                            <asp:ListItem Value="F">Femenino</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RqSexo" runat="server" 
                            ControlToValidate="rdSexo" ErrorMessage="Seleccione el sexo">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        E-mail<asp:RequiredFieldValidator ID="RqEmail" runat="server" 
                            ControlToValidate="txtemail_Pot" ErrorMessage="Debe ingresar su E-mail">*</asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" 
                            ClientValidationFunction="ValidarEmail" ControlToValidate="txtemail_Pot" 
                            ErrorMessage="Ingrese correctamente el email: \n - Mayor a 3 caracteres\n No se aceptan caracteres especiales.">*</asp:CustomValidator>
                    </td>
                    <td style="width: 80%">
                        <asp:TextBox ID="txtemail_Pot" runat="server" CssClass="Cajas" Width="50%"></asp:TextBox>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Dirección<asp:RequiredFieldValidator ID="RqDireccion" runat="server" 
                            ControlToValidate="txtdireccion_Pot" 
                            ErrorMessage="Debe ingresar la dirección (calle/avenida y número)">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 80%">
                        <asp:TextBox ID="txtdireccion_Pot" runat="server" Width="50%" 
                    CssClass="Cajas"></asp:TextBox>
                        &nbsp;Ingresar Dirección y Número</td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Urbanización<asp:RequiredFieldValidator ID="RqUbicacion" runat="server" 
                            ControlToValidate="txturbanizacion_pot" 
                            ErrorMessage="Debe ingresar el lugar de ubicación de la dirección.">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 80%">
                        <asp:TextBox ID="txturbanizacion_pot" runat="server" CssClass="Cajas" 
                        MaxLength="80" Width="50%"></asp:TextBox>
                    &nbsp;Ingresar PJ, Urb u otra Zona</td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        &nbsp;</td>
                    <td style="width: 80%">
                        Departamento:
                            <asp:DropDownList ID="dpDpto" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                        &nbsp;Provincia:
                            <asp:DropDownList ID="dpProvincia" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                        &nbsp;Distrito:
                            <asp:DropDownList ID="dpDistrito" runat="server">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToValidate="dpDistrito" ErrorMessage="Seleccione el distrito" 
                        Operator="NotEqual" ValueToCompare="-2">*</asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" 
                        ControlToValidate="dpProvincia" ErrorMessage="Seleccione la provincia" 
                        Operator="NotEqual" ValueToCompare="-2">*</asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" 
                        ControlToValidate="dpDpto" ErrorMessage="Seleccione el Departamento" 
                        Operator="NotEqual" ValueToCompare="-2">*</asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Teléfono</td>
                    <td style="width: 80%">
                        <asp:TextBox ID="txttelefono_Pot" runat="server" CssClass="Cajas"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RqTelefono" runat="server" 
                    ControlToValidate="txttelefono_Pot" 
                    ErrorMessage="Debe ingresar el número telefónico">*</asp:RequiredFieldValidator>
                        &nbsp;Celular
                            <asp:TextBox ID="txttelefonoMovil_Pot" runat="server" CssClass="Cajas"></asp:TextBox>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Apoderado<asp:RequiredFieldValidator ID="RqAporderado" runat="server" 
                        ControlToValidate="txtapoderado_dpo" ErrorMessage="Debe ingresar los datos del Apoderado">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="dpcodigo_vfam"
                                ErrorMessage="Seleccione el tipo de vínculo familiar" Operator="NotEqual" 
                    ValueToCompare="-2">*</asp:CompareValidator>
                    </td>
                    <td style="width: 80%">
                        <asp:TextBox ID="txtapoderado_dpo" runat="server" CssClass="Cajas" Width="40%"></asp:TextBox>
                            <asp:DropDownList ID="dpcodigo_vfam" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Datos del Colegio<asp:CompareValidator ID="CompareValidator9" runat="server" 
                            ControlToValidate="dpDistritoColegio" ErrorMessage="Seleccione el distrito" 
                            Operator="NotEqual" ValueToCompare="-2">*</asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator10" runat="server" 
                            ControlToValidate="dpProvinciaColegio" ErrorMessage="Seleccione la provincia" 
                            Operator="NotEqual" ValueToCompare="-2">*</asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator11" runat="server" 
                            ControlToValidate="dpDptoColegio" ErrorMessage="Seleccione el Departamento" 
                            Operator="NotEqual" ValueToCompare="-2">*</asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator6" runat="server" 
                            ControlToValidate="dpColegio" ErrorMessage="Seleccione el Colegio" 
                            Operator="NotEqual" ValueToCompare="-2">*</asp:CompareValidator>
                    </td>
                    <td style="width: 80%">
                        Departamento:
                            <asp:DropDownList ID="dpDptoColegio" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                        &nbsp;Provincia:
                            <asp:DropDownList ID="dpProvinciaColegio" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                        &nbsp;Distrito:
                            <asp:DropDownList ID="dpDistritoColegio" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:DropDownList ID="dpColegio" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Escuela Profesional</td>
                    <td style="width: 80%">
                        <asp:DropDownList ID="dpEscuela" runat="server">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="dpEscuela"
                                ErrorMessage="Seleccione la Escuela Profesional de Preferencia" Operator="NotEqual" 
                    ValueToCompare="-2">*</asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Grado que cursa</td>
                    <td style="width: 80%">
                        <asp:RadioButtonList ID="rdGrado" runat="server" RepeatDirection="Horizontal" 
                            RepeatLayout="Flow">
                            <asp:ListItem Value="4">4to Grado</asp:ListItem>
                            <asp:ListItem Value="5">5to Grado</asp:ListItem>
                        </asp:RadioButtonList>
                        &nbsp;<asp:RequiredFieldValidator ID="RqGrado" runat="server" 
                            ControlToValidate="rdGrado" ErrorMessage="Seleccione el grado que cursa">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" valign="top">
                        ¿Qué radio escucha?</td>
                    <td style="width: 80%">
                        <asp:DropDownList ID="chkRadio" runat="server">
                        </asp:DropDownList>
                        
                        <asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="chkRadio"
                                
                    ErrorMessage="Seleccione la Radio de preferencia" Operator="NotEqual" 
                    ValueToCompare="-2">*</asp:CompareValidator>
                        
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="width: 100%">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                            ShowMessageBox="True" ShowSummary="False" />
                        <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" />
                        &nbsp; <asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" 
                        ValidationGroup="cancelar" />
                    </td>
                </tr>
            </table>
    </form>
</body>
</html>

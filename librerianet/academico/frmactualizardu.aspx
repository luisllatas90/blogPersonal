<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmactualizardu.aspx.vb" Inherits="librerianet_academico_frmactualizardu" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ficha de actualización de datos personales</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../private/funciones.js"></script>
    <script language="javascript" type="text/javascript">
    function ValidarEmail(source, arguments){
      var campo=document.getElementById("email1").value
      var ubicacion
      var enter = "\n"
      var caracteres = "abcdefghijklmnñopqrstuvwxyzn1234567890_-." + String.fromCharCode(13) + enter
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
    function ValidarEmail2(source, arguments){
      var campo=document.getElementById("email2").value
      var ubicacion
      var enter = "\n"
      var caracteres = "abcdefghijklmnñopqrstuvwxyzn1234567890_-." + String.fromCharCode(13) + enter
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
</script>

</head>
<body bgcolor="#f0f0f0">
    <form id="form1" runat="server">
    
    <p class="usatTitulo">
        Actualización de datos personales del estudiante</p>
    <p style="padding: 3px; margin: 3px; border: 1px solid #808080; font-weight: normal; font-size: 12px; font-family: Verdana; background-color: #FFFF99; color: #000080;">
        (*) Campos obligatorios.<br />
    </p>
    
    <table style="width:100%" bgcolor="White" cellpadding="3" cellspacing="0">
        <tr>
            <td colspan="3" style="width: 100%" class="usatCeldaTitulo">
                Estudiante</td>
        </tr>
        <tr>
            <td colspan="3" style="width: 100%">
                <asp:DataList ID="dlEstudiante" runat="server" RepeatColumns="1" 
                    RepeatDirection="Horizontal" Width="100%" 
                    DataKeyField="codigo_Alu">
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td align="center" rowspan="5" width="10%">
                                    <asp:Image ID="FotoAlumno" runat="server" Height="104px" Width="90px" />
                                </td>
                                <td width="90%">
                                    <asp:Label ID="lblcodigo" runat="server" Text='<%# eval("codigouniver_alu") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="90%">
                                    <asp:Label ID="lblalumno" runat="server" Text='<%# eval("alumno") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="90%">
                                    <asp:Label ID="lblescuela" runat="server" Text='<%# eval("nombre_cpf") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="90%">
                                    <asp:Label ID="lblcicloingreso" runat="server" 
                                        Text='<%# "Ciclo de Ingreso: " + eval("cicloing_alu") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="90%">
                                    <asp:Label ID="lblplan" runat="server" Text='<%# eval("descripcion_pes") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="width: 100%">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3" style="width: 100%" class="usatCeldaTitulo">
                Datos personales</td>
        </tr>
        <tr>
            <td style="width: 5%">
                1.</td>
            <td style="width: 25%">
                Email Principal (*)</td>
            <td style="width: 75%">
                <asp:TextBox ID="email1" runat="server" CssClass="cajas" 
                    MaxLength="30"></asp:TextBox>
                <asp:DropDownList ID="dpProveedor1" runat="server">
                    <asp:ListItem Value="-2">Seleccione el proveedor de Email</asp:ListItem>
                    <asp:ListItem>@hotmail.com</asp:ListItem>
                    <asp:ListItem>@gmail.com</asp:ListItem>
                    <asp:ListItem>@yahoo.es</asp:ListItem>
                    <asp:ListItem>@yahoo.com</asp:ListItem>
                    <asp:ListItem>@aol.com</asp:ListItem>
                </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="dpProveedor1"
                                ErrorMessage="Seleccione el proveedor de email" Operator="NotEqual" 
                    ValueToCompare="-2">*</asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RqEmail" runat="server" 
                    ControlToValidate="email1" 
                    ErrorMessage="Debe ingresar su correo electrónico principal (email)">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                    ControlToValidate="email1" 
                    
                    ErrorMessage="Ingrese correctamente el correo electrónico principal (mayor a 3 caracteres). No se aceptan caracteres especiales." 
                    ClientValidationFunction="ValidarEmail">*</asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 5%">
                2</td>
            <td style="width: 25%">
                Email Secundario</td>
            <td style="width: 75%">
                <asp:TextBox ID="email2" runat="server" CssClass="cajas" 
                    MaxLength="30"></asp:TextBox>
                <asp:DropDownList ID="dpProveedor2" runat="server">
                    <asp:ListItem Value="-2">Seleccione el proveedor de Email</asp:ListItem>
                    <asp:ListItem>@hotmail.com</asp:ListItem>
                    <asp:ListItem>@gmail.com</asp:ListItem>
                    <asp:ListItem>@yahoo.es</asp:ListItem>
                    <asp:ListItem>@yahoo.com</asp:ListItem>
                    <asp:ListItem>@aol.com</asp:ListItem>
                </asp:DropDownList>
                <asp:CustomValidator ID="CustomValidator2" runat="server" 
                    ControlToValidate="email2" 
                    
                    ErrorMessage="Ingrese correctamente el correo electrónico secundario (mayor a 3 caracteres). No se aceptan caracteres especiales." 
                    ClientValidationFunction="ValidarEmail2">*</asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 5%">
                2.             </td>
            <td style="width: 25%">
                Dirección (*)</td>
            <td style="width: 75%">
                <asp:TextBox ID="direccion_dal" runat="server" CssClass="cajas2" Width="95%" 
                    MaxLength="80"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RqDireccion" runat="server" 
                    ControlToValidate="direccion_dal" 
                    ErrorMessage="Debe ingresar la dirección, indicando la calle/avenida y número">*</asp:RequiredFieldValidator>
                <br />
                Indicar la calle/avenida y número</td>
        </tr>
        <tr>
            <td style="width: 5%">
                3.             </td>
            <td style="width: 25%">Urbanización o Lugar (*)</td>
            <td style="width: 75%">
                <asp:TextBox ID="urbanizacion_dal" runat="server" Width="95%" MaxLength="80" 
                    CssClass="cajas"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RqUbicacion" runat="server" 
                    ControlToValidate="urbanizacion_dal" 
                    ErrorMessage="Debe ingresar el lugar de ubicación de la dirección.">*</asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td>
                Provincia:
                <asp:DropDownList ID="dpProvincia" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            &nbsp;Distrito:
                <asp:DropDownList ID="dpDistrito" runat="server">
                </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="dpDistrito"
                                ErrorMessage="Seleccione el distrito" Operator="NotEqual" 
                    ValueToCompare="-2">*</asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="dpProvincia"
                                ErrorMessage="Seleccione la provincia" Operator="NotEqual" 
                    ValueToCompare="-2">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
                4.</td>
            <td>
                Teléfono de casa</td>
            <td>
                <asp:TextBox ID="telefonoCasa_Dal" runat="server" CssClass="cajas" 
                    MaxLength="15"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
                5.</td>
            <td>
                Teléfono Celular</td>
            <td>
                <asp:TextBox ID="telefonoMovil_Dal" runat="server" CssClass="cajas" 
                    MaxLength="15"></asp:TextBox>
&nbsp;(si es de otro departamento, indicar el código)</td>
        </tr>
        <tr>
            <td>
                6.</td>
            <td>
                                Teléfono de apoderado (*)</td>
            <td>
                <asp:TextBox ID="telefonofam_dal" runat="server" MaxLength="15" 
                    CssClass="cajas"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RqTelefApoderado" runat="server" 
                    ControlToValidate="telefonofam_dal" 
                    ErrorMessage="Debe ingresar el número telefónico de su apoderado.">*</asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    
    <p align="center">
    <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar_prp" 
        Text="          Guardar" />
&nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="noconforme1" 
        Text="       Cancelar" ValidationGroup="cancelar" />
        <asp:HiddenField ID="hdID" runat="server" Value="0" />
    </p>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>

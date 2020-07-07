<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmactualizardni_v0.aspx.vb" Inherits="frmactualizardni" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ficha de actualización de datos personales</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <!-- <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script> -->
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
	<script type="text/javascript" language="javascript">

	    $(document).ready(function() {
	        jQuery(function($) {
	            $("#TxtFechaNac").mask("99/99/9999"); //.mask("(999)-999999");
	            //   $("#txttelefono").mask("(999)-9999999");
	            //   $("#txtcelular").mask("(999)-9999999");  
	        });

	    })
	    function ValidaDni(source, arguments) {
	        //alert(form1.dpTipoDoc.value)
	        if (form1.dpTipoDoc.value == "DNI") {
	            if (form1.txtdni.value.length == 8)
	                if (parseFloat(form1.txtdni.value) > 0)
	                arguments.IsValid = true;
	            else
	                arguments.IsValid = false;
	            else
	                if (form1.txtdni.value.length == 0)
	                arguments.IsValid = true;
	            else
	                arguments.IsValid = false;
	        }
	        else {
	            if (form1.txtdni.value.length <= 13)
	                if (parseFloat(form1.txtdni.value) > 0)
	                arguments.IsValid = true;
	            else
	                arguments.IsValid = false;
	            else
	                if (form1.txtdni.value.length == 0)
	                arguments.IsValid = true;
	            else
	                arguments.IsValid = false;
	        }
	        	        
	    }
	    
	    function VerificarCheck(source, arguments) {
	        
	        if (form1.chkAcepto.checked == true )
	            arguments.IsValid = true;
	        else
	            arguments.IsValid = false;
	    }
	    
	    function enter2tab(e) {
	        if (e.keyCode == 13) {
	            cb = parseInt($(this).attr('tabindex'));

	            if ($(':input[tabindex=\'' + (cb + 1) + '\']') != null) {
	                $(':input[tabindex=\'' + (cb + 1) + '\']').focus();
	                $(':input[tabindex=\'' + (cb + 1) + '\']').select();
	                e.preventDefault();

	                return false;
	            }
	        }
	    }

	    $(document).ready(function() {
	       
	        $("#txtdni").blur(function() {
	            if ($("#txtdni").val().length > 0) { //Validar solamente si hay datos ingresados en el DNI
	                $("#errornrodoc").empty();
	                if ($("#dpTipoDoc").val() == "DNI" && ($("#txtdni").val().length < 8 || $("#txtdni").val().length > 8)) {
	                    $("#errornrodoc").text("El número de DNI es incorrecto. Debe contener 8 caracteres.");
	                }
	                if ($("#dpTipoDoc").val() != "DNI" && $("#txtdni").val().length < 9) {
	                    $("#errornrodoc").text("El número de pasaporte es incorrecto. Mínimo 9 caracteres.");
	                }
	            }
	        });

	        /* Aquí podría filtrar que controles necesitará manejar,en el caso de incluir un dropbox $('input, select');*/
	        tb = $('input');
	        if ($.browser.mozilla)
	        { $(tb).keypress(enter2tab); }
	        else { $(tb).keydown(enter2tab); }
	    })
	</script>
    <style type="text/css">
        .style3
        {
            color: #FF0000;
        }
        .style4
        {
            font-size: xx-small;
            font-weight: bold;
        }
        .style5
        {
            font-size: xx-small;
        }
        .style6
        {
            width: 100%;
            height: 73px;
        }
        .style7
        {
            width: 5%;
        }
        .style8
        {
            width: 5%;
            height: 18px;
        }
        .style9
        {
            width: 25%;
            height: 18px;
        }
        .style10
        {
            height: 18px;
        }
        </style>
</head>
<body bgcolor="#f0f0f0">
    <form id="form1" runat="server">
    <table style="width:100%" bgcolor="White" cellpadding="3" cellspacing="0">
        <tr>
            <td colspan="4" style="width: 100%; color: #333333; font-weight: bold;" 
                bgcolor="#E0E0E0">
    <span style="font-weight: bold; font-size: medium;">Verifica y actualiza tus datos</span></td>
        </tr>
        <tr>
            <td colspan="4" style="width: 100%; color: #FFFFFF; font-weight: bold;" 
                bgcolor="#0066CC">
                Estudiante</td>
        </tr>
        <tr>
            <td colspan="4" style="width: 100%">
                <asp:DataList ID="dlEstudiante" runat="server" RepeatColumns="1" 
                    RepeatDirection="Horizontal" Width="100%" 
                    DataKeyField="codigo_Alu">
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td align="center" rowspan="6" width="10%">
                                    <asp:Image ID="FotoAlumno" runat="server" Height="104px" Width="90px" 
                                        BorderColor="#666666" BorderWidth="1px" />
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
                                        Text='<%# "CICLO DE INGRESO " + eval("cicloing_alu") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="90%">
                                    <asp:Label ID="lblplan" runat="server" Text='<%# eval("descripcion_pes") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="90%">
                                    <asp:Label ID="lbltipoNuevo" runat="server" Font-Bold="True" ForeColor="#000099" 
                                        Text='DNI'></asp:Label>
                                    &nbsp;<asp:Label ID="lblnrodocNuevo" runat="server" Font-Bold="True" ForeColor="#0000CC" 
                                        Text='<%# eval("dni") %>'></asp:Label>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="width: 100%; color: #FFFFFF; font-weight: bold;" 
                bgcolor="#0066CC"> Cambio de Clave</td>            
        </tr>
        <tr>
            <td></td>
            <td>Clave Actual:</td>
            <td style="width: 75%" colspan="2">
                <asp:TextBox ID="txtClaveActual" runat="server" TextMode="Password" 
                    MaxLength="15"></asp:TextBox>&nbsp;<asp:Label ID="Label1" runat="server" 
                    ForeColor="Red" 
                    Text="Verificar si la clave se encuentra en mayúsculas o minúsculas"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Nueva Clave:</td>
            <td style="width: 75%" colspan="2">
                <asp:TextBox ID="txtClaveNueva" runat="server" TextMode="Password" 
                    MaxLength="20"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4" style="width: 100%; color: #FFFFFF; font-weight: bold;" 
                bgcolor="#0066CC">
                Datos personales 
            </td>
        </tr>        
        <tr>            
            <td style="width: 5%">
                1.</td>
            <td style="width: 20%">
                Tipo de Documento
                <asp:CompareValidator ID="CompareValidator3" runat="server" 
                    ControlToValidate="dpTipoDoc" ErrorMessage="Seleccione el Tipo de Documento" 
                    Operator="NotEqual" ValueToCompare="-1" ValidationGroup="Guardar">*</asp:CompareValidator>
            </td>
            <td style="width: 75%" colspan="2">
                <asp:Label ID="lblDni" runat="server" Text="Label"></asp:Label>
                <asp:DropDownList ID="dpTipoDoc" runat="server" SkinID="ComboObligatorio" 
                    Enabled="False" AutoPostBack="True">
                    <asp:ListItem Value="-1">--Seleccione--</asp:ListItem>
                    <asp:ListItem Selected="True">DNI</asp:ListItem>
                    <asp:ListItem Value="CE">CARN&#201; DE EXTRANJER&#205;A</asp:ListItem>
                </asp:DropDownList>
            &nbsp;
                </td>
        </tr>
        <tr>
            <td style="width: 5%">
                &nbsp;</td>
            <td style="width: 20%">
                Número de Documento <b>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                    ControlToValidate="txtdni" 
                    ErrorMessage="Debe ingresar el número  de doc. de indentificación" 
                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                    ClientValidationFunction="ValidaDni" 
                    ErrorMessage="Número inválido, verifique nuevamente" ValidationGroup="Guardar">*</asp:CustomValidator>
                </b>
            </td>
            <td style="width: 75%" colspan="2">
                <asp:Label ID="lblNroDoc" runat="server" Text="Label"></asp:Label>
                <asp:TextBox ID="txtdni" runat="server" CssClass="cajas" MaxLength="8" 
                    SkinID="CajaTextoObligatorio" 
                    
                    ToolTip="Digite nuevamente su DNI para confirmar y/o actualizar su número correctamente" 
                    Width="99px" BackColor="#FFFF99"></asp:TextBox>
            &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 5%">
                                2. </td>
            <td>
                Sexo <b>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                    ControlToValidate="rblSexo" 
                    ErrorMessage="Debe especificar su sexo" 
                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </b>
            </td>
            <td style="width: 75%; " class="style3" colspan="2">
                <asp:Label ID="lblSexo" class="" runat="server" ForeColor="Black"></asp:Label>
                <asp:RadioButtonList ID="rblSexo" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                    <asp:ListItem Value="F">Femenino</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td style="width: 5%">
                3.</td>
            <td>
                Fecha de Nacimiento
                <asp:RequiredFieldValidator ID="rfvProveedor0" runat="server" 
                    ControlToValidate="TxtFechaNac" 
                    ErrorMessage="Debe especificar su fecha de nacimiento" 
                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
            </td>
            <td style="width: 75%; " colspan="2">
                <asp:Label ID="lblFechaNac" runat="server"></asp:Label>
                <asp:TextBox ID="TxtFechaNac" runat="server" Width="151px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 5%">
                4.</td>
            <td style="width: 25%">
                Correo alternativo
                <asp:RequiredFieldValidator ID="rfvProveedor" runat="server" 
                    ControlToValidate="txtCorreo" 
                    ErrorMessage="Debe especificar un correo alternativo" 
                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
            </td>
            <td colspan="2">
                <asp:Label ID="lblCorreo" runat="server"></asp:Label>
                <asp:TextBox ID="txtCorreo" runat="server" Width="147px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style8">
                </td>
            <td class="style9">
                </td>
            <td colspan="2" class="style10">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 100%; color: #FFFFFF; font-weight: bold;" bgcolor="#0066CC" 
                colspan="4">
                Datos de Colegio</td>
        </tr>
        <tr>
            <td class="style7">
                5.</td>
            <td  >
                Pais </td>
            <td colspan="2">
                <asp:Label ID="lblPais" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlPais" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 5%">
                6.</td>
            <td style="width: 25%">
                <asp:Label ID="lblDep" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlDepartamento" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                <asp:CompareValidator ID="cvDepartamento" runat="server" 
                    ControlToValidate="ddlDepartamento" 
                    ErrorMessage="Seleccione departamento del colegio" Operator="GreaterThan" 
                    ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
            </td>
            <td width="30%">
                <asp:Label ID="lblProvincia" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlProvincia" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                <asp:CompareValidator ID="cvProvincia" runat="server" 
                    ControlToValidate="ddlProvincia" 
                    ErrorMessage="Seleccione provincia del colegio" Operator="GreaterThan" 
                    ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
            </td>
            <td width="40%">
                <asp:Label ID="lblDistrito" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlDistrito" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                <asp:CompareValidator ID="cvDistrito" runat="server" 
                    ControlToValidate="ddlDistrito" ErrorMessage="Seleccione distrito del colegio" 
                    Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 5%">
                &nbsp;</td>
            <td style="width: 25%">
                [Departamento]</td>
            <td>
                [Provincia]</td>
            <td style="width: 75%; ">
                [Distrito]</td>
        </tr>
        <tr>
            <td style="width: 5%">
                7.</td>
            <td colspan="3">
                Colegio            
            <asp:RequiredFieldValidator ID="rfvProveedor2" runat="server" 
                    ControlToValidate="ddlColegio" Enabled="false"
                    ErrorMessage="Seleccione el colegio donde egresó" 
                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>                                       
            &nbsp;<asp:Label ID="lblColegio" runat="server"></asp:Label>
             
                <asp:DropDownList ID="ddlColegio" runat="server">           
                </asp:DropDownList>
            &nbsp;<asp:TextBox ID="txtOtroColegio" runat="server" Width="304px"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvOtroColegio" runat="server" 
                    ControlToValidate="txtOtroColegio" Enabled="false"
                    ErrorMessage="Debe especificar el nombre del colegio que egresó" 
                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
             
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblmensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
    <p style="text-align: justify; font-size: medium;">&nbsp;<asp:CheckBox ID="chkAcepto" runat="server" Text=" " />        
        <b>
        <asp:CustomValidator ID="CustomValidator2" runat="server" 
            ClientValidationFunction="VerificarCheck"             
            ErrorMessage="Usted debe aceptar los términos de la actualización para proceder a guardar su información" 
            ValidationGroup="Guardar" Visible="true">*</asp:CustomValidator>
        Yo acepto</b> que los datos ingresados son los correctos y verdaderos para acceder a los 
        servicios brindados por la Universidad y realizar mis pagos en el banco, <b>en 
        caso de ingresar la información incorrecta es mi responsabilidad 
        <span class="style3">
        no tener acceso</span> a algunos servicios que requieran de estos datos y acepto 
        las sanciones que se me impongan</b>.</p>
            </td>
        </tr>
        <tr>
            <td colspan="4" 
                style="border-top: 1px solid #000099; color: #0000CC; " class="style6">
                * Si eres <span class="style4">extranjero</span> o aún no cuentas con DNI deberás acercarte a 
                Caja y Pensiones.
                <br />
                * Si <b><span class="style5">te falta un dato</span> </b>dar clic en <b>cancelar</b> 
                para acceder al campus</td>
        </tr>
        </table>
    <div align="center">
    <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar_prp" 
        Text="          Guardar" Height="30px" ValidationGroup="Guardar" />
&nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="noconforme1" 
        Text="       Cancelar" ValidationGroup="cancelar" Height="30px" 
            Visible="True" />
        <asp:HiddenField ID="hddCodigo_alu" runat="server" />
        <asp:HiddenField ID="hddCodigo_reg" runat="server" />
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    </form>
</body>
</html>
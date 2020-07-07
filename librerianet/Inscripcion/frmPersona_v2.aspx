<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPersona_v2.aspx.vb" Inherits="Inscripcion_frmPersona" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Registrar Persona</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
    <script src="../../private/calendario.js"></script>
    <style type="text/css">
        .CabeceraLabel
        {
            top:20px;
            left:50px
    </style>
	<script type="text/javascript" language="javascript">
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
	        $("#txtFechaNac").mask("99/99/9999");
	        $("#txtMatrimonio").mask("99/99/9999");
	       
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

	        $(".solonumero").keydown(function(event) {
	            if (event.keyCode < 48 || event.keyCode > 57) {
	                return false;
	            }
	        });
	        
	        /* Aquí podría filtrar que controles necesitará manejar,en el caso de incluir un dropbox $('input, select');*/
	        tb = $('input');
	        if ($.browser.mozilla)
	            { $(tb).keypress(enter2tab); }
	        else { $(tb).keydown(enter2tab); }
	    })

	  $(function() {
	        $(".validar").keydown(function(event) {
	            if (event.keyCode < 48 || event.keyCode > 57) {
	                return false;
	            }
	        });
	  });
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <table cellpadding="0" cellspacing="0" width="100%" style="background:#BF1518">
        <tr style="height:113px; 
            background:url(image/Header2.png) no-repeat; background-position:left;">
            <td align="right" valign="middle">
                <br /><br />
                <asp:Label ID="lblHeader" runat="server" Text="Evento: " 
                    ForeColor="White" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
    </table>
    </center>    
    <br />
    <asp:Label ID="lblAviso" runat="server" Font-Bold="True" Font-Size="Medium" 
        ForeColor="#0066FF"></asp:Label>
    <br />    
    <span class="usatTitulo">Registro del Participante</span>    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>        
                <br />
                <table style="width: 100%; background:#E7F2F6" class="contornotabla">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label3" runat="server" Text="Datos Personales" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>                
                    </table>
                    <table style="width: 100%" class="contornotabla">
                    <tr>
                        <td style="width:20%">
                            Doc. de Identidad</td>
                        <td>
                            <asp:DropDownList ID="dpTipoDoc" runat="server" SkinID="ComboObligatorio" 
                                AutoPostBack="True">
                                <asp:ListItem>DNI</asp:ListItem>
                                <asp:ListItem>CARNÉ DE EXTRANJERÍA</asp:ListItem>
                                <asp:ListItem Value="PAS">PASAPORTE</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            <b>Nro. Doc. Identidad</b><asp:RequiredFieldValidator ID="RequiredFieldValidator11" 
                                runat="server" ControlToValidate="txtdni" 
                                ErrorMessage="Debe ingresar el número  de doc. de indentificación">*</asp:RequiredFieldValidator>
                            
                        </td>
                        <td>
                            <asp:TextBox ID="txtdni" runat="server" CssClass="cajas" MaxLength="15" 
                                SkinID="CajaTextoObligatorio"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;<span id="errornrodoc" style="color:red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Apellido Paterno                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtAPaterno" 
                                ErrorMessage="Debe ingresar el Apellido Paterno">*</asp:RequiredFieldValidator>
                            </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAPaterno" runat="server" MaxLength="100" 
                                Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Apellido Materno<b><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtAMaterno" 
                                ErrorMessage="Debe ingresar el Apellido Materno">*</asp:RequiredFieldValidator>
                            </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAMaterno" runat="server" MaxLength="100" 
                                Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Nombres <b>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtNombres" 
                    ErrorMessage="Debe ingresar los Nombres">*</asp:RequiredFieldValidator>
                            </b>
                        </td>
                        <td style="margin-left: 40px">
                            <asp:TextBox ID="txtNombres" runat="server" MaxLength="80" 
                    Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                            &nbsp;</td>
                    </tr>
                    <tr runat="server" id="trConcidencias">
                        <td style="width:20%">
                            RED Cientifica a participar</td>
                        <td>                            
                            <asp:TextBox ID="txtRedCientifica" runat="server" Width="250px"></asp:TextBox>                            
                        </td>
                    </tr>
                    </table>
                    <br />
                    <table style="width: 100%; background:#E7F2F6" class="contornotabla">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label4" runat="server" Text="Datos Generales" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    </table>
                    <table style="width: 100%" class="contornotabla">
                    <tr runat="server" id="fila1">
                        <td style="width:20%">
                            Fecha Nac. <b>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txtFechaNac" 
                    ErrorMessage="Debe ingresar la Fecha de Nac.">*</asp:RequiredFieldValidator>
                            </b>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                ControlToValidate="txtFechaNac" 
                                ErrorMessage="La fecha de Nacimiento es incorrecta" MaximumValue="31/12/2050" 
                                MinimumValue="01/01/1920" Type="Date">*</asp:RangeValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaNac" runat="server" MaxLength="11" 
                    Width="110px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;Sexo:&nbsp;<asp:DropDownList ID="dpSexo" runat="server" 
                                SkinID="ComboObligatorio">
                                <asp:ListItem Value="-1">&gt;&gt;Seleccione&lt;&lt;</asp:ListItem>
                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                <asp:ListItem Value="F">Femenino</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" 
                                ControlToValidate="dpSexo" ErrorMessage="Seleccione el sexo" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                            &nbsp;Estado Civil:
                            <asp:DropDownList ID="dpEstadoCivil" runat="server" SkinID="ComboObligatorio">
                                <asp:ListItem Value="-1">&gt;&gt;Seleccione&lt;&lt;</asp:ListItem>
                                <asp:ListItem Value="SOLTERO">SOLTERO</asp:ListItem>
                                <asp:ListItem Value="CASADO">CASADO</asp:ListItem>
                                <asp:ListItem Value="VIUDO">VIUDO</asp:ListItem>
                                <asp:ListItem Value="DIVORCIADO">DIVORCIADO</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                ControlToValidate="dpEstadoCivil" ErrorMessage="Seleccione el Estado Civil" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr runat="server" id="fila2">
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Fecha Matrimonio:"></asp:Label>                                                                                    
                        &nbsp;(Opcional)</td>
                        <td>
                            <asp:TextBox ID="txtMatrimonio" runat="server" Width="85px"></asp:TextBox>
                            <!-- <input onclick="MostrarCalendario('txtMatrimonio')" type="button" value="..." /> --> &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label2" runat="server" Text="Conyuge:"></asp:Label>
                            <asp:TextBox ID="txtConyugue" runat="server" Width="28.5%"></asp:TextBox>
                        &nbsp;(Opcional)</td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Email Principal<b><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                ControlToValidate="txtemail1" ErrorMessage="Ingrese un Email principal válido." 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtemail1" runat="server" Width="80%" 
                                MaxLength="80"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Email Alternativo<b><asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtemail2" 
                                ErrorMessage="Ingrese un Email secundario válido." 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </b></td>
                        <td>
                            <asp:TextBox ID="txtemail2" runat="server" Width="80%" 
                                MaxLength="80"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="fila3">
                        <td style="width:20%">
                            Dirección <b>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                    ControlToValidate="txtdireccion" 
                    ErrorMessage="Debe ingresar la dirección de la persona">*</asp:RequiredFieldValidator>
                            </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtdireccion" runat="server" Width="80%" 
                                MaxLength="150" SkinID="CajaTextoObligatorio"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            <asp:Label ID="lblDepartamento" runat="server" Text="Departamento"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="dppais" runat="server" Visible="False">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator8" runat="server" 
                                ControlToValidate="dppais" ErrorMessage="Seleccione el Pais" 
                                Operator="NotEqual" ValueToCompare="-1" Visible="False">*</asp:CompareValidator>
                            <asp:DropDownList ID="dpdepartamento" runat="server" 
                    AutoPostBack="True" SkinID="ComboObligatorio">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" 
                                ControlToValidate="dpdepartamento" ErrorMessage="Seleccione el Departamento" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                            &nbsp;<asp:Label ID="lblProvincia" runat="server" Text="Provincia:"></asp:Label>
&nbsp;<asp:DropDownList ID="dpprovincia" runat="server" 
                    AutoPostBack="True" SkinID="ComboObligatorio">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator5" runat="server" 
                                ControlToValidate="dpprovincia" ErrorMessage="Seleccione la provincia" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                            &nbsp;<asp:Label ID="lblDistrito" runat="server" Text="Distrito:"></asp:Label>
                            <asp:DropDownList ID="dpdistrito" runat="server" SkinID="ComboObligatorio">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator6" runat="server" 
                                ControlToValidate="dpdistrito" ErrorMessage="Seleccione el Distrito" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>                            
                        </td>
                    </tr>
                    <tr runat="server" id="fila4">
                        <td style="width:20%">
                            Teléfono</td>
                        <td>
                            Fijo:
                            <asp:TextBox ID="txttelefono" runat="server" MaxLength="20"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;Celular:&nbsp;<asp:TextBox ID="txtcelular" 
                                runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="fila99">
                        <td style="width:20%">
                            Requiere Factura</td>
                        <td>
                            <asp:CheckBox ID="chkreqfact" runat="server" />
                        </td>
                    </tr>
                    <tr runat="server" id="fila5">
                        <td style="width:20%">
                            RUC</td>
                        <td>
                            <asp:TextBox ID="txtruc" runat="server" MaxLength="12"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="fila7">
                        <td style="width:20%">
                            Modalidad Ingreso</td>
                        <td>
                            <asp:DropDownList ID="dpModalidad" runat="server" SkinID="ComboObligatorio">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator7" runat="server" 
                                ControlToValidate="dpModalidad" ErrorMessage="Seleccione la Modalidad" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                        </td>
                    </tr>
                     </table>
                     <br />
                     <table id="tableformacion" runat="server" style="width: 100%; background:#E7F2F6" class="contornotabla">                    
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="Label6" runat="server" Text="Datos de Formación Académica" Font-Bold="True"></asp:Label>
                            </td>                            
                        </tr>
                        </table>
                        <table id="tableformacion_1" runat="server" style="width: 100%" class="contornotabla">   
                        <tr>
                            <td colspan="4">SOLO PARA ESTUDIANTES DE PREGRADO<br />
                            Universidad: <asp:TextBox ID="txtPregadoUniv" Width="250px" runat="server"></asp:TextBox> 
                            &nbsp;&nbsp; 
                            Ciclo: <asp:TextBox ID="txtPreGradoCiclo" CssClass="solonumero" runat="server"></asp:TextBox>
                            </td>                                                    
                        </tr>
                        <tr>
                            <td align="center"><b>ESTUDIOS POST-GRADO</b></td>
                            <td align="center"><b>AREA O MENCIÓN</b></td>
                            <td align="center"><b>ESTADO</b></td>
                            <td align="center"><b>UNIVERSIDAD</b></td>
                        </tr>
                        <tr>
                            <td>Especialidad:</td>
                            <td>
                                <asp:TextBox ID="txtEspeArea" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboEspeEstado" runat="server">
                                    <asp:ListItem Text="Proceso" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="Culminado" Value="C"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEspeUniversidad" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Maestria:</td>
                            <td>
                                <asp:TextBox ID="txtMaestArea" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboMaestEstado" runat="server">
                                    <asp:ListItem Text="Proceso" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="Culminado" Value="C"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMaestUniv" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Doctorado:</td>
                            <td>
                                <asp:TextBox ID="txtDoctorArea" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboDoctorArea" runat="server">
                                    <asp:ListItem Text="Proceso" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="Culminado" Value="C"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDoctorUniversidad" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                     </table>
                     <br />
                    <table style="width: 100%; background:#E7F2F6" class="contornotabla">                    
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label5" runat="server" Text="Datos Laborales" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>  
                    </table>
                    <table style="width: 100%" class="contornotabla">                                      
                    <tr>
                        <td>Tipo</td>
                        <td colspan="3">
                            <asp:DropDownList ID="dpTipoInscrito" runat="server" Enabled="False">
                                <asp:ListItem Value="ESTUDIANTE" Text="ESTUDIANTE"></asp:ListItem>
                                <asp:ListItem Value="PROFESIONAL" Text="PROFESIONAL" Selected></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="fila6">
                        <td>
                            <asp:Label ID="lblLugarTipo" runat="server" Text="Instituto o Centro Labores"></asp:Label></td>
                        <td colspan="3">
                            <asp:TextBox ID="txLugarTipo" runat="server" Width="250px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="trCargo">
                        <td>Cargo Actual</td>
                        <td>
                            <asp:TextBox ID="txtCargoActual" runat="server"></asp:TextBox>
                        </td>
                        <td>E-mail Institución:</td>
                        <td>
                            <asp:TextBox ID="txtemailinstitucion" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="trArea">
                        <td>Área de trabajo</td>
                        <td>
                            <asp:TextBox ID="txtAreaTrabajo" runat="server"></asp:TextBox>
                        </td>
                        <td>Telef. Institución</td>
                        <td>
                            <asp:TextBox ID="txtTelefInstitucion" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="trCiudad">
                        <td>Ciudad</td>
                        <td>
                            <asp:TextBox ID="txtCiudad" runat="server"></asp:TextBox>
                        </td>
                        <td>Provincia/Departamento</td>
                        <td>
                            <asp:TextBox ID="txtProvincia" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Observacion</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" 
                                Width="409px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            &nbsp;</td>
                        <td colspan="3"> <!-- <img src="image/Captcha.gif" /> -->
                            <asp:Image ID="Image1" runat="server"  ImageUrl="image/Captcha.gif" 
                                Visible="False" />
                            <asp:Label ID="lblIngreseCodigo" runat="server" Text="Ingrese codigo:" 
                                Visible="False"></asp:Label>
                            <asp:TextBox ID="txtCaptcha" runat="server" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            &nbsp;</td>
                        <td colspan="3">        
                <span class="usatTitulo">
                <asp:Label ID="lblmensaje" runat="server" CssClass="rojo" Font-Bold="True" 
                                Font-Size="X-Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                ShowMessageBox="True" ShowSummary="False" />
                        </td>
                        <td colspan="3">
                            <asp:Button ID="cmdGuardar" runat="server" 
                                Text="Guardar" SkinID="BotonGuardar" CssClass="guardar" Width="100px" Height="22px" />
                            &nbsp;<asp:Button ID="cmdLimpiar" runat="server" SkinID="BotonLimpiar" CssClass="usatNuevo" 
                                Text="Limpiar" ValidationGroup="Limpiar" Width="100px" Height="22px" />
                            &nbsp;<asp:Button ID="cmdCancelar" runat="server" SkinID="BotonSalir" Width="100px" Height="22px" 
                                Text="Cerrar" ValidationGroup="Salir" CssClass="Salir"
                                EnableTheming="True" />
                            <asp:HiddenField ID="hdcodigo_cco" 
                                runat="server" Value="0" />
                            <asp:HiddenField ID="hdgestionanotas" runat="server" Value="0" />
                            <asp:HiddenField ID="hdAccionForm" runat="server" Value="" />
                            <asp:HiddenField ID="hdTest" runat="server" />
                            <asp:HiddenField ID="HdBusqueda" runat="server" />
                            <asp:HiddenField ID="hdCodCaptcha" runat="server" />
                            <asp:HiddenField ID="hdcodigo_pso" runat="server" Value="0" />
                            <asp:HiddenField ID="hdcodigo_cpf" runat="server" Value="0" />
                        </td>
                    </tr>
                    </table>
            

    </form>
</body>
</html>
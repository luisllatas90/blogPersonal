<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmpersonasincargo.aspx.vb" Inherits=" frmpersonasincargo" Theme="Acero" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrar Persona</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
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
</head>
<body>
    <form id="form1" runat="server">
    <input id="cli" name="cli" type="hidden"  runat="server">
    <span class="usatTitulo">Registro del Participante</span><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
                <table style="width: 100%" class="contornotabla">
                    <tr>
                        <td style="width:20%">
                            Doc. de Identidad</td>
                        <td>
                            <asp:DropDownList ID="dpTipoDoc" runat="server" SkinID="ComboObligatorio" AutoPostBack="True">
                                <asp:ListItem>DNI</asp:ListItem>
                                <asp:ListItem>CARNÉ DE EXTRANJERÍA</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<asp:HyperLink ID="lnkreniec" runat="server" Font-Bold="True" 
                                Font-Underline="True" ForeColor="Red" 
                                NavigateUrl="https://cel.reniec.gob.pe/valreg/valreg.do?accion=ini" 
                                Target="_blank">[Buscar DNI en RENIEC]</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            <b>Nro. Doc. Identidad<asp:RequiredFieldValidator ID="RequiredFieldValidator11" 
                                runat="server" ControlToValidate="txtdni" 
                                ErrorMessage="Debe ingresar el número  de doc. de indentificación">*</asp:RequiredFieldValidator>
                            </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtdni" runat="server" CssClass="cajas" MaxLength="15" 
                                SkinID="CajaTextoObligatorio"></asp:TextBox>
                            &nbsp;<asp:LinkButton ID="lnkComprobarDNI" runat="server" Font-Bold="True" 
                                Font-Underline="True" ForeColor="Blue" ValidationGroup="Comprobar">[Buscar]</asp:LinkButton>
                        &nbsp;&nbsp;<span id="errornrodoc" style="color:red"></span>
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
                            <asp:TextBox ID="txtAMaterno" runat="server" MaxLength="100" Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>
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
                        <td>
                            <asp:TextBox ID="txtNombres" runat="server" MaxLength="80" 
                    Width="250px" SkinID="CajaTextoObligatorio"></asp:TextBox>
                            &nbsp;<asp:LinkButton ID="lnkComprobarNombres" runat="server" Font-Bold="True" 
                                Font-Underline="True" ForeColor="Blue" ValidationGroup="Comprobar" 
                                Visible="False">Clic aquí para buscar coincidencias</asp:LinkButton>
                        </td>
                    </tr>
                    <tr runat="server" id="trConcidencias">
                        <td style="width:20%">
                            &nbsp;</td>
                        <td>
                            <div id="listadiv" style="width:100%;heigh:200px">
                            <asp:GridView ID="grwCoincidencias" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="codigo_pso" Width="100%" SkinID="skinGridViewLineas">
                                <Columns>
                                    <asp:BoundField DataField="apellidoPaterno_Pso" HeaderText="Ap. Paterno" >
                                        <ItemStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="apellidoMaterno_Pso" HeaderText="Ap. Materno" >
                                        <ItemStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombres_Pso" HeaderText="Nombres" >
                                        <ItemStyle Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="numeroDocIdent_Pso" HeaderText="Doc. Ident." >
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fechaNacimiento_pso" DataFormatString="{0:d}" 
                                        HeaderText="Fecha Nac." />
                                    <asp:BoundField DataField="direccion_pso" HeaderText="Dirección" />
                                    <asp:BoundField DataField="emailprincipal_pso" HeaderText="Email" />
                                    <asp:CommandField ShowSelectButton="True">
                                        <ControlStyle Font-Underline="True" ForeColor="Blue" />
                                        <ItemStyle Width="5%" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
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
                    <tr>
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
                            Departamento</td>
                        <td>
                            <asp:DropDownList ID="dpdepartamento" runat="server" 
                    AutoPostBack="True" SkinID="ComboObligatorio">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" 
                                ControlToValidate="dpdepartamento" ErrorMessage="Seleccione el Departamento" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                            &nbsp;Provincia:
                            <asp:DropDownList ID="dpprovincia" runat="server" 
                    AutoPostBack="True" SkinID="ComboObligatorio">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator5" runat="server" 
                                ControlToValidate="dpprovincia" ErrorMessage="Seleccione la provincia" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                            &nbsp;Distrito:
                            <asp:DropDownList ID="dpdistrito" runat="server" SkinID="ComboObligatorio">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator6" runat="server" 
                                ControlToValidate="dpdistrito" ErrorMessage="Seleccione el Distrito" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            Teléfono</td>
                        <td>
                            Fijo:
                            <asp:TextBox ID="txttelefono" runat="server" MaxLength="20"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;Celular:&nbsp;<asp:TextBox ID="txtcelular" 
                                runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            RUC</td>
                        <td>
                            <asp:TextBox ID="txtruc" runat="server" MaxLength="12"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trCargoEmpresa" runat="server">
                        <td style="width:20%">Cargo</td>
                        <td>
                            <asp:TextBox ID="txtCargo" runat="server" SkinID="CajaTextoObligatorio" MaxLength="200"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;Centro de Labores:&nbsp;
                            <asp:TextBox ID="txtCentroTrabajo" runat="server" SkinID="CajaTextoObligatorio" MaxLength="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
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
                    <tr>
                        <td style="width:20%">
                            &nbsp;</td>
                        <td>
        
                <asp:Label ID="lblmensaje" runat="server" CssClass="rojo" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:20%">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                ShowMessageBox="True" ShowSummary="False" />
                        </td>
                        <td>
                            <asp:Button ID="cmdGuardar" runat="server" Enabled="False" 
                                Text="Guardar" SkinID="BotonGuardar" />
                            &nbsp;<asp:Button ID="cmdLimpiar" runat="server" SkinID="BotonLimpiar" 
                                Text="Limpiar" ValidationGroup="Limpiar" />
                            &nbsp;<asp:Button ID="cmdCancelar" runat="server" SkinID="BotonSalir" 
                                Text="Cerrar" ValidationGroup="Salir" 
                                EnableTheming="True" />
                            <asp:HiddenField ID="hdcodigo_cco" 
                                runat="server" Value="0" />
                            <asp:HiddenField ID="hdgestionanotas" runat="server" Value="0" />
                            <asp:HiddenField ID="hdcodigo_pso" runat="server" Value="0" />
                            <asp:HiddenField ID="hdcodigo_cpf" runat="server" Value="0" />
                        </td>
                    </tr>
                    </table>
            

    </form>
</body>
</html>
